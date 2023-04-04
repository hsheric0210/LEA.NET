using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using static LEA.BlockCipher;
using static LEA.util.Ops;
using static LEA.util.Pack;

namespace LEA.mode
{
	public class GCMMode : BlockCipherModeAE
	{
		private static readonly int MAX_TAGLEN = 16;
		#region Reduction
		private static readonly byte[][] REDUCTION = new byte[][] { new[] { (byte)0x00, (byte)0x00 }, new[] { (byte)0x01, (byte)0xc2 }, new[] { (byte)0x03, (byte)0x84 }, new[] { (byte)0x02, (byte)0x46 }, new[] { (byte)0x07, (byte)0x08 }, new[] { (byte)0x06, (byte)0xca }, new[] { (byte)0x04, (byte)0x8c }, new[] { (byte)0x05, (byte)0x4e }, new[] { (byte)0x0e, (byte)0x10 }, new[] { (byte)0x0f, (byte)0xd2 }, new[] { (byte)0x0d, (byte)0x94 }, new[] { (byte)0x0c, (byte)0x56 }, new[] { (byte)0x09, (byte)0x18 }, new[] { (byte)0x08, (byte)0xda }, new[] { (byte)0x0a, (byte)0x9c }, new[] { (byte)0x0b, (byte)0x5e }, new[] { (byte)0x1c, (byte)0x20 }, new[] { (byte)0x1d, (byte)0xe2 }, new[] { (byte)0x1f, (byte)0xa4 }, new[] { (byte)0x1e, (byte)0x66 }, new[] { (byte)0x1b, (byte)0x28 }, new[] { (byte)0x1a, (byte)0xea }, new[] { (byte)0x18, (byte)0xac }, new[] { (byte)0x19, (byte)0x6e }, new[] { (byte)0x12, (byte)0x30 }, new[] { (byte)0x13, (byte)0xf2 }, new[] { (byte)0x11, (byte)0xb4 }, new[] { (byte)0x10, (byte)0x76 }, new[] { (byte)0x15, (byte)0x38 }, new[] { (byte)0x14, (byte)0xfa }, new[] { (byte)0x16, (byte)0xbc }, new[] { (byte)0x17, (byte)0x7e }, new[] { (byte)0x38, (byte)0x40 }, new[] { (byte)0x39, (byte)0x82 }, new[] { (byte)0x3b, (byte)0xc4 }, new[] { (byte)0x3a, (byte)0x06 }, new[] { (byte)0x3f, (byte)0x48 }, new[] { (byte)0x3e, (byte)0x8a }, new[] { (byte)0x3c, (byte)0xcc }, new[] { (byte)0x3d, (byte)0x0e }, new[] { (byte)0x36, (byte)0x50 }, new[] { (byte)0x37, (byte)0x92 }, new[] { (byte)0x35, (byte)0xd4 }, new[] { (byte)0x34, (byte)0x16 }, new[] { (byte)0x31, (byte)0x58 }, new[] { (byte)0x30, (byte)0x9a }, new[] { (byte)0x32, (byte)0xdc }, new[] { (byte)0x33, (byte)0x1e }, new[] { (byte)0x24, (byte)0x60 }, new[] { (byte)0x25, (byte)0xa2 }, new[] { (byte)0x27, (byte)0xe4 }, new[] { (byte)0x26, (byte)0x26 }, new[] { (byte)0x23, (byte)0x68 }, new[] { (byte)0x22, (byte)0xaa }, new[] { (byte)0x20, (byte)0xec }, new[] { (byte)0x21, (byte)0x2e }, new[] { (byte)0x2a, (byte)0x70 }, new[] { (byte)0x2b, (byte)0xb2 }, new[] { (byte)0x29, (byte)0xf4 }, new[] { (byte)0x28, (byte)0x36 }, new[] { (byte)0x2d, (byte)0x78 }, new[] { (byte)0x2c, (byte)0xba }, new[] { (byte)0x2e, (byte)0xfc }, new[] { (byte)0x2f, (byte)0x3e }, new[] { (byte)0x70, (byte)0x80 }, new[] { (byte)0x71, (byte)0x42 }, new[] { (byte)0x73, (byte)0x04 }, new[] { (byte)0x72, (byte)0xc6 }, new[] { (byte)0x77, (byte)0x88 }, new[] { (byte)0x76, (byte)0x4a }, new[] { (byte)0x74, (byte)0x0c }, new[] { (byte)0x75, (byte)0xce }, new[] { (byte)0x7e, (byte)0x90 }, new[] { (byte)0x7f, (byte)0x52 }, new[] { (byte)0x7d, (byte)0x14 }, new[] { (byte)0x7c, (byte)0xd6 }, new[] { (byte)0x79, (byte)0x98 }, new[] { (byte)0x78, (byte)0x5a }, new[] { (byte)0x7a, (byte)0x1c }, new[] { (byte)0x7b, (byte)0xde }, new[] { (byte)0x6c, (byte)0xa0 }, new[] { (byte)0x6d, (byte)0x62 }, new[] { (byte)0x6f, (byte)0x24 }, new[] { (byte)0x6e, (byte)0xe6 }, new[] { (byte)0x6b, (byte)0xa8 }, new[] { (byte)0x6a, (byte)0x6a }, new[] { (byte)0x68, (byte)0x2c }, new[] { (byte)0x69, (byte)0xee }, new[] { (byte)0x62, (byte)0xb0 }, new[] { (byte)0x63, (byte)0x72 }, new[] { (byte)0x61, (byte)0x34 }, new[] { (byte)0x60, (byte)0xf6 }, new[] { (byte)0x65, (byte)0xb8 }, new[] { (byte)0x64, (byte)0x7a }, new[] { (byte)0x66, (byte)0x3c }, new[] { (byte)0x67, (byte)0xfe }, new[] { (byte)0x48, (byte)0xc0 }, new[] { (byte)0x49, (byte)0x02 }, new[] { (byte)0x4b, (byte)0x44 }, new[] { (byte)0x4a, (byte)0x86 }, new[] { (byte)0x4f, (byte)0xc8 }, new[] { (byte)0x4e, (byte)0x0a }, new[] { (byte)0x4c, (byte)0x4c }, new[] { (byte)0x4d, (byte)0x8e }, new[] { (byte)0x46, (byte)0xd0 }, new[] { (byte)0x47, (byte)0x12 }, new[] { (byte)0x45, (byte)0x54 }, new[] { (byte)0x44, (byte)0x96 }, new[] { (byte)0x41, (byte)0xd8 }, new[] { (byte)0x40, (byte)0x1a }, new[] { (byte)0x42, (byte)0x5c }, new[] { (byte)0x43, (byte)0x9e }, new[] { (byte)0x54, (byte)0xe0 }, new[] { (byte)0x55, (byte)0x22 }, new[] { (byte)0x57, (byte)0x64 }, new[] { (byte)0x56, (byte)0xa6 }, new[] { (byte)0x53, (byte)0xe8 }, new[] { (byte)0x52, (byte)0x2a }, new[] { (byte)0x50, (byte)0x6c }, new[] { (byte)0x51, (byte)0xae }, new[] { (byte)0x5a, (byte)0xf0 }, new[] { (byte)0x5b, (byte)0x32 }, new[] { (byte)0x59, (byte)0x74 }, new[] { (byte)0x58, (byte)0xb6 }, new[] { (byte)0x5d, (byte)0xf8 }, new[] { (byte)0x5c, (byte)0x3a }, new[] { (byte)0x5e, (byte)0x7c }, new[] { (byte)0x5f, (byte)0xbe }, new[] { (byte)0xe1, (byte)0x00 }, new[] { (byte)0xe0, (byte)0xc2 }, new[] { (byte)0xe2, (byte)0x84 }, new[] { (byte)0xe3, (byte)0x46 }, new[] { (byte)0xe6, (byte)0x08 }, new[] { (byte)0xe7, (byte)0xca }, new[] { (byte)0xe5, (byte)0x8c }, new[] { (byte)0xe4, (byte)0x4e }, new[] { (byte)0xef, (byte)0x10 }, new[] { (byte)0xee, (byte)0xd2 }, new[] { (byte)0xec, (byte)0x94 }, new[] { (byte)0xed, (byte)0x56 }, new[] { (byte)0xe8, (byte)0x18 }, new[] { (byte)0xe9, (byte)0xda }, new[] { (byte)0xeb, (byte)0x9c }, new[] { (byte)0xea, (byte)0x5e }, new[] { (byte)0xfd, (byte)0x20 }, new[] { (byte)0xfc, (byte)0xe2 }, new[] { (byte)0xfe, (byte)0xa4 }, new[] { (byte)0xff, (byte)0x66 }, new[] { (byte)0xfa, (byte)0x28 }, new[] { (byte)0xfb, (byte)0xea }, new[] { (byte)0xf9, (byte)0xac }, new[] { (byte)0xf8, (byte)0x6e }, new[] { (byte)0xf3, (byte)0x30 }, new[] { (byte)0xf2, (byte)0xf2 }, new[] { (byte)0xf0, (byte)0xb4 }, new[] { (byte)0xf1, (byte)0x76 }, new[] { (byte)0xf4, (byte)0x38 }, new[] { (byte)0xf5, (byte)0xfa }, new[] { (byte)0xf7, (byte)0xbc }, new[] { (byte)0xf6, (byte)0x7e }, new[] { (byte)0xd9, (byte)0x40 }, new[] { (byte)0xd8, (byte)0x82 }, new[] { (byte)0xda, (byte)0xc4 }, new[] { (byte)0xdb, (byte)0x06 }, new[] { (byte)0xde, (byte)0x48 }, new[] { (byte)0xdf, (byte)0x8a }, new[] { (byte)0xdd, (byte)0xcc }, new[] { (byte)0xdc, (byte)0x0e }, new[] { (byte)0xd7, (byte)0x50 }, new[] { (byte)0xd6, (byte)0x92 }, new[] { (byte)0xd4, (byte)0xd4 }, new[] { (byte)0xd5, (byte)0x16 }, new[] { (byte)0xd0, (byte)0x58 }, new[] { (byte)0xd1, (byte)0x9a }, new[] { (byte)0xd3, (byte)0xdc }, new[] { (byte)0xd2, (byte)0x1e }, new[] { (byte)0xc5, (byte)0x60 }, new[] { (byte)0xc4, (byte)0xa2 }, new[] { (byte)0xc6, (byte)0xe4 }, new[] { (byte)0xc7, (byte)0x26 }, new[] { (byte)0xc2, (byte)0x68 }, new[] { (byte)0xc3, (byte)0xaa }, new[] { (byte)0xc1, (byte)0xec }, new[] { (byte)0xc0, (byte)0x2e }, new[] { (byte)0xcb, (byte)0x70 }, new[] { (byte)0xca, (byte)0xb2 }, new[] { (byte)0xc8, (byte)0xf4 }, new[] { (byte)0xc9, (byte)0x36 }, new[] { (byte)0xcc, (byte)0x78 }, new[] { (byte)0xcd, (byte)0xba }, new[] { (byte)0xcf, (byte)0xfc }, new[] { (byte)0xce, (byte)0x3e }, new[] { (byte)0x91, (byte)0x80 }, new[] { (byte)0x90, (byte)0x42 }, new[] { (byte)0x92, (byte)0x04 }, new[] { (byte)0x93, (byte)0xc6 }, new[] { (byte)0x96, (byte)0x88 }, new[] { (byte)0x97, (byte)0x4a }, new[] { (byte)0x95, (byte)0x0c }, new[] { (byte)0x94, (byte)0xce }, new[] { (byte)0x9f, (byte)0x90 }, new[] { (byte)0x9e, (byte)0x52 }, new[] { (byte)0x9c, (byte)0x14 }, new[] { (byte)0x9d, (byte)0xd6 }, new[] { (byte)0x98, (byte)0x98 }, new[] { (byte)0x99, (byte)0x5a }, new[] { (byte)0x9b, (byte)0x1c }, new[] { (byte)0x9a, (byte)0xde }, new[] { (byte)0x8d, (byte)0xa0 }, new[] { (byte)0x8c, (byte)0x62 }, new[] { (byte)0x8e, (byte)0x24 }, new[] { (byte)0x8f, (byte)0xe6 }, new[] { (byte)0x8a, (byte)0xa8 }, new[] { (byte)0x8b, (byte)0x6a }, new[] { (byte)0x89, (byte)0x2c }, new[] { (byte)0x88, (byte)0xee }, new[] { (byte)0x83, (byte)0xb0 }, new[] { (byte)0x82, (byte)0x72 }, new[] { (byte)0x80, (byte)0x34 }, new[] { (byte)0x81, (byte)0xf6 }, new[] { (byte)0x84, (byte)0xb8 }, new[] { (byte)0x85, (byte)0x7a }, new[] { (byte)0x87, (byte)0x3c }, new[] { (byte)0x86, (byte)0xfe }, new[] { (byte)0xa9, (byte)0xc0 }, new[] { (byte)0xa8, (byte)0x02 }, new[] { (byte)0xaa, (byte)0x44 }, new[] { (byte)0xab, (byte)0x86 }, new[] { (byte)0xae, (byte)0xc8 }, new[] { (byte)0xaf, (byte)0x0a }, new[] { (byte)0xad, (byte)0x4c }, new[] { (byte)0xac, (byte)0x8e }, new[] { (byte)0xa7, (byte)0xd0 }, new[] { (byte)0xa6, (byte)0x12 }, new[] { (byte)0xa4, (byte)0x54 }, new[] { (byte)0xa5, (byte)0x96 }, new[] { (byte)0xa0, (byte)0xd8 }, new[] { (byte)0xa1, (byte)0x1a }, new[] { (byte)0xa3, (byte)0x5c }, new[] { (byte)0xa2, (byte)0x9e }, new[] { (byte)0xb5, (byte)0xe0 }, new[] { (byte)0xb4, (byte)0x22 }, new[] { (byte)0xb6, (byte)0x64 }, new[] { (byte)0xb7, (byte)0xa6 }, new[] { (byte)0xb2, (byte)0xe8 }, new[] { (byte)0xb3, (byte)0x2a }, new[] { (byte)0xb1, (byte)0x6c }, new[] { (byte)0xb0, (byte)0xae }, new[] { (byte)0xbb, (byte)0xf0 }, new[] { (byte)0xba, (byte)0x32 }, new[] { (byte)0xb8, (byte)0x74 }, new[] { (byte)0xb9, (byte)0xb6 }, new[] { (byte)0xbc, (byte)0xf8 }, new[] { (byte)0xbd, (byte)0x3a }, new[] { (byte)0xbf, (byte)0x7c }, new[] { (byte)0xbe, (byte)0xbe } };
		#endregion

		private byte[] initialCtr;
		private byte[] nonce;
		private byte[] tag;
		private byte[][] hTable;
		private byte[] block;
		private byte[] macBlock;
		private byte[] aadBlock;
		private byte[] hashBlock;
		private byte[] mulBlock;
		private byte[] inBuffer;
		private int blockOff;
		private int aadOff;
		private int aadlen;
		private int msglen;
		private MemoryStream baos;
		public GCMMode(BlockCipher cipher) : base(cipher)
		{
			block = new byte[blocksize];
			nonce = new byte[blocksize];
			hashBlock = new byte[blocksize];
			macBlock = new byte[blocksize];
			aadBlock = new byte[blocksize];
			mulBlock = new byte[blocksize];
			taglen = blocksize;
			msglen = 0;
		}

		public override void Init(Mode mode, byte[] mk, byte[] nonce, int taglen)
		{
			this.mode = mode;
			engine.Init(Mode.ENCRYPT, mk);
			if (mode == Mode.ENCRYPT)
				inBuffer = new byte[blocksize];
			else
			{
				inBuffer = new byte[blocksize + taglen];
			}

			Reset();
			engine.ProcessBlock(block, 0, block, 0);
			Init_8bit_table();
			SetNonce(nonce);
			SetTaglen(taglen);
			baos = mode == Mode.ENCRYPT ? null : new MemoryStream();
		}

		public virtual void Reset()
		{
			blockOff = 0;
			aadOff = 0;
			msglen = 0;
			aadlen = 0;
			block.FillBy((byte)0);
			nonce.FillBy((byte)0);
			hashBlock.FillBy((byte)0);
			macBlock.FillBy((byte)0);
			aadBlock.FillBy((byte)0);
			mulBlock.FillBy((byte)0);
			inBuffer.FillBy((byte)0);
			if (baos != null)
				baos.SetLength(0);
		}

		private void SetNonce(byte[] nonce)
		{
			if (nonce == null)
				throw new ArgumentNullException("Nonce should not be null");

			if (nonce.Length < 1)
				throw new ArgumentException("the length of nonce should be larger than or equal to 1");

			if (nonce.Length == 12)
			{
				Buffer.BlockCopy(nonce, 0, this.nonce, 0, nonce.Length);
				this.nonce[blocksize - 1] = 1;
			}
			else
			{
				Ghash(this.nonce, nonce, nonce.Length);
				var X = new byte[blocksize];
				LongToBigEndian((long)nonce.Length * 8, X, 8);
				Ghash(this.nonce, X, blocksize);
			}

			initialCtr = this.nonce.CopyOf();
		}

		private void SetTaglen(int taglen)
		{
			if (taglen < 0 || taglen > MAX_TAGLEN)
				throw new ArgumentException("length of tag should be 0~16 bytes");

			this.taglen = taglen;
			tag = new byte[taglen];
		}

		public override int GetOutputSize(int len)
		{
			var outSize = len + blockOff;
			if (mode == Mode.ENCRYPT)
				return outSize + taglen;

			return outSize < taglen ? 0 : outSize - taglen;
		}

		public virtual int GetUpdateOutputSize(int len)
		{
			var outSize = len + blockOff;
			if (mode == Mode.DECRYPT)
			{
				if (outSize < taglen)
					return 0;

				outSize -= taglen;
			}

			return unchecked(outSize & (int)0xfffffff0);
		}

		public override void UpdateAAD(byte[] @in)
		{
			if (@in == null || @in.Length == 0)
				return;

			int len = @in.Length;
			int gap = aadBlock.Length - aadOff;
			var inOff = 0;
			if (len > gap)
			{
				Buffer.BlockCopy(@in, inOff, aadBlock, aadOff, gap);
				Ghash(macBlock, aadBlock, blocksize);
				aadOff = 0;
				len -= gap;
				inOff += gap;
				aadlen += gap;
				while (len >= blocksize)
				{
					Buffer.BlockCopy(@in, inOff, aadBlock, 0, blocksize);
					Ghash(macBlock, aadBlock, blocksize);
					inOff += blocksize;
					len -= blocksize;
					aadlen += blocksize;
				}
			}

			if (len > 0)
			{
				Buffer.BlockCopy(@in, inOff, aadBlock, aadOff, len);
				aadOff += len;
				aadlen += len;
			}
		}

		public override byte[] Update(byte[] @in)
		{
			if (aadOff != 0)
			{
				Ghash(macBlock, aadBlock, aadOff);
				aadOff = 0;
			}

			if (mode == Mode.ENCRYPT)
				return UpdateEncrypt(@in);
			else
			{
				UpdateDecrypt(@in);
			}

			return null;
		}

		public override byte[] DoFinal()
		{
			byte[] @out = new byte[GetOutputSize(0)];
			var outOff = 0;
			var extra = blockOff;
			if (extra != 0)
			{
				if (mode == Mode.ENCRYPT)
				{
					EncryptBlock(@out, outOff, extra);
					outOff += extra;
				}
				else
				{
					if (extra < taglen)
						throw new ArgumentException("data too short");

					extra -= taglen;
					if (extra > 0)
					{
						DecryptBlock(@out, outOff, extra);
						try
						{
							baos.Write(@out, outOff, extra);
						}
						catch (Exception e)
						{
							Console.WriteLine(e); // FIXME
						}
					}

					Buffer.BlockCopy(inBuffer, extra, tag, 0, taglen);
				}
			}

			if (mode == Mode.DECRYPT)
				msglen -= taglen;

			block.FillBy((byte)0);
			IntToBigEndian(aadlen *= 8, block, 4);
			IntToBigEndian(msglen *= 8, block, 12);
			Ghash(macBlock, block, blocksize);
			engine.ProcessBlock(initialCtr, 0, block, 0);
			XOR(macBlock, block);
			if (mode == Mode.ENCRYPT)
				Buffer.BlockCopy(macBlock, 0, @out, outOff, taglen);
			else
			{
				macBlock = macBlock.CopyOf(taglen);
				if (!macBlock.SequenceEqual(tag) && @out != null) // fixme: side-channel timing attack
				{
					baos.SetLength(0);
					@out = null;
				}
				else
				{
					@out = baos.ToArray();
				}
			}

			return @out;
		}

		private byte[] UpdateEncrypt(byte[] @in)
		{
			int len = @in.Length;
			var gap = blocksize - blockOff;
			var inOff = 0;
			var outOff = 0;
			byte[] @out = new byte[GetUpdateOutputSize(len)];
			if (len >= gap)
			{
				Buffer.BlockCopy(@in, inOff, inBuffer, blockOff, gap);
				EncryptBlock(@out, outOff, blocksize);
				len -= gap;
				inOff += gap;
				outOff += gap;
				msglen += gap;
				blockOff = 0;
				while (len >= blocksize)
				{
					Buffer.BlockCopy(@in, inOff, inBuffer, 0, blocksize);
					EncryptBlock(@out, outOff, blocksize);
					len -= blocksize;
					inOff += blocksize;
					outOff += blocksize;
					msglen += blocksize;
				}
			}

			if (len > 0)
			{
				Buffer.BlockCopy(@in, inOff, inBuffer, 0, len);
				msglen += len;
				blockOff += len;
			}

			return @out;
		}

		private void UpdateDecrypt(byte[] @in)
		{
			int len = @in.Length;
			int gap = inBuffer.Length - blockOff;
			var inOff = 0;
			var outOff = 0;
			byte[] @out = new byte[GetUpdateOutputSize(len)];
			if (len >= gap)
			{
				Buffer.BlockCopy(@in, inOff, inBuffer, blockOff, gap);
				DecryptBlock(@out, outOff, blocksize);
				Buffer.BlockCopy(inBuffer, blocksize, inBuffer, 0, taglen);
				len -= gap;
				inOff += gap;
				outOff += blocksize;
				msglen += gap;
				blockOff = taglen;
				while (len >= blocksize)
				{
					Buffer.BlockCopy(@in, inOff, inBuffer, blockOff, blocksize);
					DecryptBlock(@out, outOff, blocksize);
					Buffer.BlockCopy(inBuffer, blocksize, inBuffer, 0, taglen);
					len -= blocksize;
					inOff += blocksize;
					outOff += blocksize;
					msglen += blocksize;
				}
			}

			if (len > 0)
			{
				Buffer.BlockCopy(@in, inOff, inBuffer, blockOff, len);
				msglen += len;
				blockOff += len;
			}

			try
			{
				baos.Write(@out, 0, @out.Length);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);//fixme
			}
		}

		private int EncryptBlock(byte[] @out, int offset, int len)
		{
			Increase_counter(nonce);
			engine.ProcessBlock(nonce, 0, block, 0);
			XOR(block, inBuffer);
			Buffer.BlockCopy(block, 0, @out, offset, len);
			Ghash(macBlock, block, len);
			return len;
		}

		private int DecryptBlock(byte[] @out, int outOff, int len)
		{
			Buffer.BlockCopy(inBuffer, 0, block, 0, len);
			Ghash(macBlock, block, len);
			Increase_counter(nonce);
			engine.ProcessBlock(nonce, 0, block, 0);
			XOR(@out, outOff, block, 0, inBuffer, 0, len);
			return len;
		}

		private void Init_8bit_table()
		{
			hTable = new byte[256][]; // fixme: convert this to multi-dim array
			for (int i = 0; i < 16; i++)
				hTable[i] = new byte[16];
			var temp = new byte[blocksize];
			Buffer.BlockCopy(block, 0, hTable[0x80], 0, block.Length);
			Buffer.BlockCopy(block, 0, temp, 0, block.Length);
			for (var j = 0x40; j >= 1; j >>= 1)
			{
				ShiftRight(temp, 1);
				if ((hTable[j << 1][15] & 1) != 0)
					temp[0] ^= 0xe1;

				Buffer.BlockCopy(temp, 0, hTable[j], 0, 16);
			}

			for (var j = 2; j < 256; j <<= 1)
			{
				for (var k = 1; k < j; k++)
				{
					XOR(hTable[j + k], hTable[j], hTable[k]);
				}
			}
		}

		private void Increase_counter(byte[] ctr)
		{
			for (var i = 15; i >= 12; --i)
			{
				if (++ctr[i] != 0)
					return;
			}
		}

		private void Ghash(byte[] r, byte[] data, int data_len)
		{
			Buffer.BlockCopy(r, 0, hashBlock, 0, blocksize);
			var pos = 0;
			var len = data_len;
			for (; len >= blocksize; pos += blocksize, len -= blocksize)
			{
				XOR(hashBlock, 0, data, pos, blocksize);
				Gfmul(hashBlock, hashBlock);
			}

			if (len > 0)
			{
				XOR(hashBlock, 0, data, pos, len);
				Gfmul(hashBlock, hashBlock);
			}

			Buffer.BlockCopy(hashBlock, 0, r, 0, blocksize);
		}

		private void Gfmul(byte[] r, byte[] x)
		{
			mulBlock.FillBy((byte)0);
			var rowIdx = 0;
			for (rowIdx = 15; rowIdx > 0; --rowIdx)
			{
				var colIdx = 0;
				for (colIdx = 0; colIdx < 16; ++colIdx)
				{
					mulBlock[colIdx] ^= hTable[x[rowIdx] & 0xff][colIdx];
				}

				var mask = mulBlock[15] & 0xff;
				for (colIdx = 15; colIdx > 0; --colIdx)
				{
					mulBlock[colIdx] = mulBlock[colIdx - 1];
				}

				mulBlock[0] = 0;
				mulBlock[0] ^= REDUCTION[mask][0];
				mulBlock[1] ^= REDUCTION[mask][1];
			}

			rowIdx = x[0] & 0xff;
			XOR(r, mulBlock, hTable[rowIdx]);
		}
	}
}