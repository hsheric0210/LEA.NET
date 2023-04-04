using static LEA.Utils.HexCoder;
using static LEA.Utils.Xor;
using System.Buffers;

namespace LEA.OpMode
{
	public class CcmMode : BlockCipherModeAE
	{
		private readonly byte[] counter;
		private readonly byte[] block;
		private byte[] mac;
		private byte[] tag;

		private MemoryStream aadStream;
		private MemoryStream inputStream;

		private int mesageLength;
		private int nonceLength;

		private int tagLength;
		protected override int TagLength
		{
			get => tagLength;
			set
			{
				if (value < 4 || value > 16 || (value & 0x01) != 0)
					throw new ArgumentException("Length of tag should be 4, 6, 8, 10, 12, 14, 16 bytes");

				tagLength = value;
				tag = new byte[value];
			}
		}

		public CcmMode(BlockCipher cipher) : base(cipher)
		{
			counter = new byte[blockSize];
			mac = new byte[blockSize];
			block = new byte[blockSize];
		}

		public override void Init(Mode mode, ReadOnlySpan<byte> key, ReadOnlySpan<byte> nonce, int tagLength)
		{
			this.mode = mode;
			engine.Init(Mode.Encrypt, key);
			aadStream = new MemoryStream();
			inputStream = new MemoryStream();
			TagLength = tagLength;
			SetNonce(nonce);
		}

		public override void UpdateAssociatedData(ReadOnlySpan<byte> aad)
		{
			if (aad.Length == 0)
				return;

			aadStream.Write(aad);
		}

		public override ReadOnlySpan<byte> Update(ReadOnlySpan<byte> message)
		{
			inputStream.Write(message);
			return null;
		}

		public override ReadOnlySpan<byte> DoFinal()
		{
			if (aadStream.Length > 0)
				block[0] |= 0x40;

			mesageLength = inputStream.ToArray().Length;
			if (mode == Mode.Decrypt)
				mesageLength -= TagLength;

			ToBytes(mesageLength, block, nonceLength + 1, 15 - nonceLength);
			engine.ProcessBlock(block, 0, mac, 0);
			Span<byte> output;
			ProcessAAD();
			if (mode == Mode.Encrypt)
			{
				output = new byte[mesageLength + TagLength];
				EncryptData(output, 0);
			}
			else
			{
				output = new byte[mesageLength];
				DecryptData(output, 0);
			}

			ResetCounter();
			engine.ProcessBlock(counter, 0, block, 0);
			if (mode == Mode.Encrypt)
			{
				XOR(mac, block);
				Buffer.BlockCopy(mac, 0, tag, 0, TagLength);
				mac[..TagLength].CopyTo(output[(output.Length - TagLength)..]);
			}
			else
			{
				mac = mac[..TagLength].ToArray();
				if (!tag.SequenceEqual(mac))
					output.Fill(0);
			}

			Close(aadStream);
			Close(inputStream);

			return output;
		}

		public override int GetOutputSize(int length)
		{
			var outSize = length + bufferOffset;
			if (mode == Mode.Encrypt)
				return outSize + TagLength;

			return outSize < TagLength ? 0 : outSize - TagLength;
		}

		private void SetNonce(ReadOnlySpan<byte> nonce)
		{
			nonceLength = nonce.Length;
			if (nonceLength is < 7 or > 13)
				throw new ArgumentException("Length of nonce should be 7 ~ 13 bytes");


			// init counter
			counter[0] = (byte)(14 - nonceLength);
			nonce.CopyTo(counter.AsSpan()[1..]);

			// init b0
			var tagfield = (TagLength - 2) / 2;
			block[0] = (byte)(tagfield << 3 & 0xff);
			block[0] |= (byte)(14 - nonceLength & 0xff);
			nonce.CopyTo(block.AsSpan()[1..]);
		}

		private void ResetCounter() => counter.AsSpan()[(nonceLength + 1)..].Fill(0);

		private void IncreaseCounter()
		{
			var i = counter.Length - 1;
			while (++counter[i] == 0)
			{
				--i;
				if (i < nonceLength + 1)
					throw new InvalidOperationException("Exceed maximum counter");
			}
		}

		private void ProcessAAD()
		{
			var aad = aadStream.ToArray();
			Array.Fill(block, (byte)0);
			int alen;
			if (aad.Length < 0xff00)
			{
				alen = 2;
				ToBytes(aad.Length, block, 0, 2);
			}
			else
			{
				alen = 6;
				block[0] = 0xff;
				block[1] = 0xfe;
				ToBytes(aad.Length, block, 2, 4);
			}

			if (aad.Length == 0)
				return;

			var i = 0;
			var remained = aad.Length;
			var processed = remained > blockSize - alen ? blockSize - alen : aad.Length;
			i += processed;
			remained -= processed;
			Buffer.BlockCopy(aad, 0, block, alen, processed);
			XOR(mac, block);
			engine.ProcessBlock(mac, 0, mac, 0);
			while (remained > 0)
			{
				processed = remained >= blockSize ? blockSize : remained;
				XOR(mac, 0, mac, 0, aad, i, processed);
				engine.ProcessBlock(mac, 0, mac, 0);
				i += processed;
				remained -= processed;
			}
		}

		private void EncryptData(Span<byte> output, int offset)
		{
			var inIdx = 0;
			var outIdx = offset;
			var input = inputStream.ToArray();
			var remained = mesageLength;
			while (remained > 0)
			{
				var processed = remained >= blockSize ? blockSize : remained;
				XOR(mac, 0, mac, 0, input, inIdx, processed);
				engine.ProcessBlock(mac, 0, mac, 0);
				IncreaseCounter();
				engine.ProcessBlock(counter, 0, block, 0);
				XOR(output, outIdx, block, 0, input, inIdx, processed);
				inIdx += processed;
				outIdx += processed;
				remained -= processed;
			}
		}

		private void DecryptData(Span<byte> output, int offset)
		{
			var i = 0;
			var outIdx = offset;
			var input = inputStream.ToArray();
			Buffer.BlockCopy(input, mesageLength, tag, 0, TagLength);
			engine.ProcessBlock(counter, 0, block, 0);
			XOR(tag, 0, block, 0, TagLength);
			var remained = mesageLength;
			while (remained > 0)
			{
				var processed = remained >= blockSize ? blockSize : remained;
				IncreaseCounter();
				engine.ProcessBlock(counter, 0, block, 0);
				XOR(output, outIdx, block, 0, input, i, processed);
				XOR(mac, 0, mac, 0, output, outIdx, processed);
				engine.ProcessBlock(mac, 0, mac, 0);
				i += processed;
				outIdx += processed;
				remained -= processed;
			}
		}

		private static void Close(IDisposable obj)
		{
			if (obj == null)
				return;

			try
			{
				obj.Dispose();
			}
			catch
			{
			}
		}
	}
}