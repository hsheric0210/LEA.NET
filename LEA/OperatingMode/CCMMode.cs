using System;
using System.IO;
using System.Linq;
using static LEA.BlockCipher;
using static LEA.Utils.Ops;
using static LEA.Utils.Hex;

namespace LEA.OperatingMode
{
	public class CcmMode : BlockCipherModeAE
	{
		private byte[] counter;
		private byte[] mac;
		private byte[] tag;
		private byte[] block;
		private MemoryStream aadBytes;
		private MemoryStream inputBytes;
		private int messageLength;
		private int nonceLength;
		public CcmMode(BlockCipher cipher) : base(cipher)
		{
			counter = new byte[blocksize];
			mac = new byte[blocksize];
			block = new byte[blocksize];
		}

		public override void Init(Mode mode, byte[] key, byte[] nonce, int tagLength)
		{
			this.mode = mode;
			engine.Init(Mode.ENCRYPT, key);
			aadBytes = new MemoryStream();
			inputBytes = new MemoryStream();
			SetTagLength(tagLength);
			SetNonce(nonce);
		}

		public override void UpdateAAD(byte[] aad)
		{
			if (aad == null || aad.Length == 0)
				return;

			aadBytes.Write(aad, 0, aad.Length);
		}

		public override byte[] Update(byte[] message)
		{
			inputBytes.Write(message, 0, message.Length);
			return null;
		}

		public override byte[] DoFinal()
		{
			if (aadBytes.Length > 0)
				block[0] |= 0x40;

			messageLength = (int)inputBytes.Length;
			if (mode == Mode.DECRYPT)
				messageLength -= taglen;

			ToBytes(messageLength, block, nonceLength + 1, 15 - nonceLength);
			engine.ProcessBlock(block, 0, mac, 0);
			byte[] outBytes;
			ProcessAAD();
			if (mode == Mode.ENCRYPT)
			{
				outBytes = new byte[messageLength + taglen];
				EncryptData(outBytes, 0);
			}
			else
			{
				outBytes = new byte[messageLength];
				DecryptData(outBytes, 0);
			}

			ResetCounter();
			engine.ProcessBlock(counter, 0, block, 0);
			if (mode == Mode.ENCRYPT)
			{
				XOR(mac, block);
				Buffer.BlockCopy(mac, 0, tag, 0, taglen);
				Buffer.BlockCopy(mac, 0, outBytes, outBytes.Length - taglen, taglen);
			}
			else
			{
				mac = mac.CopyOf(taglen);
				if (!tag.SequenceEqual(mac))
					outBytes.FillBy((byte)0);
			}

			return outBytes;
		}

		public override int GetOutputSize(int length)
		{
			var outSize = length + bufOff;
			if (mode == Mode.ENCRYPT)
				return outSize + taglen;

			return outSize < taglen ? 0 : outSize - taglen;
		}

		private void SetNonce(byte[] nonce)
		{
			if (nonce == null)
				throw new ArgumentNullException(nameof(nonce));

			nonceLength = nonce.Length;
			if (nonceLength < 7 || nonceLength > 13)
				throw new ArgumentException("length of nonce should be 7 ~ 13 bytes");

			// init counter
			counter[0] = (byte)(14 - nonceLength);
			Buffer.BlockCopy(nonce, 0, counter, 1, nonceLength);

			// init b0
			var tagfield = (taglen - 2) / 2;
			block[0] = (byte)(tagfield << 3 & 0xff);
			block[0] |= (byte)(14 - nonceLength & 0xff);
			Buffer.BlockCopy(nonce, 0, block, 1, nonceLength);
		}

		private void SetTagLength(int taglen)
		{
			if (taglen < 4 || taglen > 16 || (taglen & 0x01) != 0)
				throw new ArgumentException("length of tag should be 4, 6, 8, 10, 12, 14, 16 bytes");

			this.taglen = taglen;
			tag = new byte[taglen];
		}

		private void ResetCounter() => counter.FillBy(nonceLength + 1, counter.Length, (byte)0);

		private void IncreaseCounter()
		{
			var i = counter.Length - 1;
			while (++counter[i] == 0)
			{
				--i;
				if (i < nonceLength + 1)
					throw new InvalidOperationException("exceed maximum counter");
			}
		}

		private void ProcessAAD()
		{
			var aad = aadBytes.ToArray();
			block.FillBy((byte)0);
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
			var processed = remained > blocksize - alen ? blocksize - alen : aad.Length;
			i += processed;
			remained -= processed;
			Buffer.BlockCopy(aad, 0, block, alen, processed);
			XOR(mac, block);
			engine.ProcessBlock(mac, 0, mac, 0);
			while (remained > 0)
			{
				processed = remained >= blocksize ? blocksize : remained;
				XOR(mac, 0, mac, 0, aad, i, processed);
				engine.ProcessBlock(mac, 0, mac, 0);
				i += processed;
				remained -= processed;
			}
		}

		private void EncryptData(byte[] outBytes, int offset)
		{
			var inIdx = 0;
			var outIdx = offset;
			var inBytes = inputBytes.BufferData();
			var remained = messageLength;
			while (remained > 0)
			{
				var processed = remained >= blocksize ? blocksize : remained;
				XOR(mac, 0, mac, 0, inBytes, inIdx, processed);
				engine.ProcessBlock(mac, 0, mac, 0);
				IncreaseCounter();
				engine.ProcessBlock(counter, 0, block, 0);
				XOR(outBytes, outIdx, block, 0, inBytes, inIdx, processed);
				inIdx += processed;
				outIdx += processed;
				remained -= processed;
			}
		}

		private void DecryptData(byte[] outBytes, int offset)
		{
			var i = 0;
			var outIdx = offset;
			var inBytes = inputBytes.BufferData();
			Buffer.BlockCopy(inBytes, messageLength, tag, 0, taglen);
			engine.ProcessBlock(counter, 0, block, 0);
			XOR(tag, 0, block, 0, taglen);
			var remained = messageLength;
			while (remained > 0)
			{
				var processed = remained >= blocksize ? blocksize : remained;
				IncreaseCounter();
				engine.ProcessBlock(counter, 0, block, 0);
				XOR(outBytes, outIdx, block, 0, inBytes, i, processed);
				XOR(mac, 0, mac, 0, outBytes, outIdx, processed);
				engine.ProcessBlock(mac, 0, mac, 0);
				i += processed;
				outIdx += processed;
				remained -= processed;
			}
		}
	}
}