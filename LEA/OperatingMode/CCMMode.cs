using System;
using System.IO;
using System.Linq;
using static LEA.BlockCipher;
using static LEA.Utils.Ops;
using static LEA.Utils.Hex;

namespace LEA.OperatingMode
{
	public class CCMMode : BlockCipherModeAE
	{
		private byte[] ctr;
		private byte[] mac;
		private byte[] tag;
		private byte[] block;
		private MemoryStream aadBytes;
		private MemoryStream inputBytes;
		private int msglen;
		private int taglen;
		private int noncelen;
		public CCMMode(BlockCipher cipher) : base(cipher)
		{
			ctr = new byte[blocksize];
			mac = new byte[blocksize];
			block = new byte[blocksize];
		}

		public override void Init(Mode mode, byte[] mk, byte[] nonce, int taglen)
		{
			this.mode = mode;
			engine.Init(Mode.ENCRYPT, mk);
			aadBytes = new MemoryStream();
			inputBytes = new MemoryStream();
			SetTaglen(taglen);
			SetNonce(nonce);
		}

		public override void UpdateAAD(byte[] aad)
		{
			if (aad == null || aad.Length == 0)
				return;

			aadBytes.Write(aad, 0, aad.Length);
		}

		public override byte[] Update(byte[] msg)
		{
			inputBytes.Write(msg, 0, msg.Length);
			return null;
		}

		public override byte[] DoFinal()
		{
			if (aadBytes.Length > 0)
				block[0] |= 0x40;

			msglen = (int)inputBytes.Length;
			if (mode == Mode.DECRYPT)
				msglen -= taglen;

			ToBytes(msglen, block, noncelen + 1, 15 - noncelen);
			engine.ProcessBlock(block, 0, mac, 0);
			byte[] @out;
			ProcessAAD();
			if (mode == Mode.ENCRYPT)
			{
				@out = new byte[msglen + taglen];
				EncryptData(@out, 0);
			}
			else
			{
				@out = new byte[msglen];
				DecryptData(@out, 0);
			}

			ResetCounter();
			engine.ProcessBlock(ctr, 0, block, 0);
			if (mode == Mode.ENCRYPT)
			{
				XOR(mac, block);
				Buffer.BlockCopy(mac, 0, tag, 0, taglen);
				Buffer.BlockCopy(mac, 0, @out, @out.Length - taglen, taglen);
			}
			else
			{
				mac = mac.CopyOf(taglen); // FIXME
				if (!tag.SequenceEqual(mac))
					@out.FillBy((byte)0);
			}

			return @out;
		}

		public override int GetOutputSize(int len)
		{
			var outSize = len + bufOff;
			if (mode == Mode.ENCRYPT)
				return outSize + taglen;

			return outSize < taglen ? 0 : outSize - taglen;
		}

		private void SetNonce(byte[] nonce)
		{
			if (nonce == null)
				throw new ArgumentNullException("nonce is null");

			noncelen = nonce.Length;
			if (noncelen < 7 || noncelen > 13)
				throw new ArgumentException("length of nonce should be 7 ~ 13 bytes");


			// init counter
			ctr[0] = (byte)(14 - noncelen);
			Buffer.BlockCopy(nonce, 0, ctr, 1, noncelen);

			// init b0
			var tagfield = (taglen - 2) / 2;
			block[0] = (byte)(tagfield << 3 & 0xff);
			block[0] |= (byte)(14 - noncelen & 0xff);
			Buffer.BlockCopy(nonce, 0, block, 1, noncelen);
		}

		private void SetTaglen(int taglen)
		{
			if (taglen < 4 || taglen > 16 || (taglen & 0x01) != 0)
				throw new ArgumentException("length of tag should be 4, 6, 8, 10, 12, 14, 16 bytes");

			this.taglen = taglen;
			tag = new byte[taglen];
		}

		private void ResetCounter() => ctr.FillBy(noncelen + 1, ctr.Length, (byte)0);

		private void IncreaseCounter()
		{
			var i = ctr.Length - 1;
			while (++ctr[i] == 0)
			{
				--i;
				if (i < noncelen + 1)
					throw new InvalidOperationException("exceed maximum counter");
			}
		}

		private void ProcessAAD()
		{
			var aad = aadBytes.ToArray();
			block.FillBy((byte)0);
			var alen = 0;
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

		private void EncryptData(byte[] @out, int offset)
		{
			var inIdx = 0;
			var remained = 0;
			var processed = 0;
			var outIdx = offset;
			var @in = inputBytes.GetBuffer();
			remained = msglen;
			while (remained > 0)
			{
				processed = remained >= blocksize ? blocksize : remained;
				XOR(mac, 0, mac, 0, @in, inIdx, processed);
				engine.ProcessBlock(mac, 0, mac, 0);
				IncreaseCounter();
				engine.ProcessBlock(ctr, 0, block, 0);
				XOR(@out, outIdx, block, 0, @in, inIdx, processed);
				inIdx += processed;
				outIdx += processed;
				remained -= processed;
			}
		}

		private void DecryptData(byte[] @out, int offset)
		{
			var i = 0;
			var remained = 0;
			var processed = 0;
			var outIdx = offset;
			var @in = inputBytes.GetBuffer();
			Buffer.BlockCopy(@in, msglen, tag, 0, taglen);
			engine.ProcessBlock(ctr, 0, block, 0);
			XOR(tag, 0, block, 0, taglen);
			remained = msglen;
			while (remained > 0)
			{
				processed = remained >= blocksize ? blocksize : remained;
				IncreaseCounter();
				engine.ProcessBlock(ctr, 0, block, 0);
				XOR(@out, outIdx, block, 0, @in, i, processed);
				XOR(mac, 0, mac, 0, @out, outIdx, processed);
				engine.ProcessBlock(mac, 0, mac, 0);
				i += processed;
				outIdx += processed;
				remained -= processed;
			}
		}
	}
}