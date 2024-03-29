using System;
using System.IO;
using System.Linq;
using static LEA.BlockCipher;
using static LEA.Utils.Ops;
using static LEA.Utils.Pack;

namespace LEA.OperatingMode
{
	public class GcmMode : BlockCipherModeAE
	{
		private const int MaxTagLength = 16;
		#region Reduction
		private static readonly byte[][] Reduction = new byte[][] { new[] { (byte)0, (byte)0 }, new[] { (byte)0x01, (byte)0xc2 }, new[] { (byte)0x03, (byte)0x84 }, new[] { (byte)0x02, (byte)0x46 }, new[] { (byte)0x07, (byte)0x08 }, new[] { (byte)0x06, (byte)0xca }, new[] { (byte)0x04, (byte)0x8c }, new[] { (byte)0x05, (byte)0x4e }, new[] { (byte)0x0e, (byte)0x10 }, new[] { (byte)0x0f, (byte)0xd2 }, new[] { (byte)0x0d, (byte)0x94 }, new[] { (byte)0x0c, (byte)0x56 }, new[] { (byte)0x09, (byte)0x18 }, new[] { (byte)0x08, (byte)0xda }, new[] { (byte)0x0a, (byte)0x9c }, new[] { (byte)0x0b, (byte)0x5e }, new[] { (byte)0x1c, (byte)0x20 }, new[] { (byte)0x1d, (byte)0xe2 }, new[] { (byte)0x1f, (byte)0xa4 }, new[] { (byte)0x1e, (byte)0x66 }, new[] { (byte)0x1b, (byte)0x28 }, new[] { (byte)0x1a, (byte)0xea }, new[] { (byte)0x18, (byte)0xac }, new[] { (byte)0x19, (byte)0x6e }, new[] { (byte)0x12, (byte)0x30 }, new[] { (byte)0x13, (byte)0xf2 }, new[] { (byte)0x11, (byte)0xb4 }, new[] { (byte)0x10, (byte)0x76 }, new[] { (byte)0x15, (byte)0x38 }, new[] { (byte)0x14, (byte)0xfa }, new[] { (byte)0x16, (byte)0xbc }, new[] { (byte)0x17, (byte)0x7e }, new[] { (byte)0x38, (byte)0x40 }, new[] { (byte)0x39, (byte)0x82 }, new[] { (byte)0x3b, (byte)0xc4 }, new[] { (byte)0x3a, (byte)0x06 }, new[] { (byte)0x3f, (byte)0x48 }, new[] { (byte)0x3e, (byte)0x8a }, new[] { (byte)0x3c, (byte)0xcc }, new[] { (byte)0x3d, (byte)0x0e }, new[] { (byte)0x36, (byte)0x50 }, new[] { (byte)0x37, (byte)0x92 }, new[] { (byte)0x35, (byte)0xd4 }, new[] { (byte)0x34, (byte)0x16 }, new[] { (byte)0x31, (byte)0x58 }, new[] { (byte)0x30, (byte)0x9a }, new[] { (byte)0x32, (byte)0xdc }, new[] { (byte)0x33, (byte)0x1e }, new[] { (byte)0x24, (byte)0x60 }, new[] { (byte)0x25, (byte)0xa2 }, new[] { (byte)0x27, (byte)0xe4 }, new[] { (byte)0x26, (byte)0x26 }, new[] { (byte)0x23, (byte)0x68 }, new[] { (byte)0x22, (byte)0xaa }, new[] { (byte)0x20, (byte)0xec }, new[] { (byte)0x21, (byte)0x2e }, new[] { (byte)0x2a, (byte)0x70 }, new[] { (byte)0x2b, (byte)0xb2 }, new[] { (byte)0x29, (byte)0xf4 }, new[] { (byte)0x28, (byte)0x36 }, new[] { (byte)0x2d, (byte)0x78 }, new[] { (byte)0x2c, (byte)0xba }, new[] { (byte)0x2e, (byte)0xfc }, new[] { (byte)0x2f, (byte)0x3e }, new[] { (byte)0x70, (byte)0x80 }, new[] { (byte)0x71, (byte)0x42 }, new[] { (byte)0x73, (byte)0x04 }, new[] { (byte)0x72, (byte)0xc6 }, new[] { (byte)0x77, (byte)0x88 }, new[] { (byte)0x76, (byte)0x4a }, new[] { (byte)0x74, (byte)0x0c }, new[] { (byte)0x75, (byte)0xce }, new[] { (byte)0x7e, (byte)0x90 }, new[] { (byte)0x7f, (byte)0x52 }, new[] { (byte)0x7d, (byte)0x14 }, new[] { (byte)0x7c, (byte)0xd6 }, new[] { (byte)0x79, (byte)0x98 }, new[] { (byte)0x78, (byte)0x5a }, new[] { (byte)0x7a, (byte)0x1c }, new[] { (byte)0x7b, (byte)0xde }, new[] { (byte)0x6c, (byte)0xa0 }, new[] { (byte)0x6d, (byte)0x62 }, new[] { (byte)0x6f, (byte)0x24 }, new[] { (byte)0x6e, (byte)0xe6 }, new[] { (byte)0x6b, (byte)0xa8 }, new[] { (byte)0x6a, (byte)0x6a }, new[] { (byte)0x68, (byte)0x2c }, new[] { (byte)0x69, (byte)0xee }, new[] { (byte)0x62, (byte)0xb0 }, new[] { (byte)0x63, (byte)0x72 }, new[] { (byte)0x61, (byte)0x34 }, new[] { (byte)0x60, (byte)0xf6 }, new[] { (byte)0x65, (byte)0xb8 }, new[] { (byte)0x64, (byte)0x7a }, new[] { (byte)0x66, (byte)0x3c }, new[] { (byte)0x67, (byte)0xfe }, new[] { (byte)0x48, (byte)0xc0 }, new[] { (byte)0x49, (byte)0x02 }, new[] { (byte)0x4b, (byte)0x44 }, new[] { (byte)0x4a, (byte)0x86 }, new[] { (byte)0x4f, (byte)0xc8 }, new[] { (byte)0x4e, (byte)0x0a }, new[] { (byte)0x4c, (byte)0x4c }, new[] { (byte)0x4d, (byte)0x8e }, new[] { (byte)0x46, (byte)0xd0 }, new[] { (byte)0x47, (byte)0x12 }, new[] { (byte)0x45, (byte)0x54 }, new[] { (byte)0x44, (byte)0x96 }, new[] { (byte)0x41, (byte)0xd8 }, new[] { (byte)0x40, (byte)0x1a }, new[] { (byte)0x42, (byte)0x5c }, new[] { (byte)0x43, (byte)0x9e }, new[] { (byte)0x54, (byte)0xe0 }, new[] { (byte)0x55, (byte)0x22 }, new[] { (byte)0x57, (byte)0x64 }, new[] { (byte)0x56, (byte)0xa6 }, new[] { (byte)0x53, (byte)0xe8 }, new[] { (byte)0x52, (byte)0x2a }, new[] { (byte)0x50, (byte)0x6c }, new[] { (byte)0x51, (byte)0xae }, new[] { (byte)0x5a, (byte)0xf0 }, new[] { (byte)0x5b, (byte)0x32 }, new[] { (byte)0x59, (byte)0x74 }, new[] { (byte)0x58, (byte)0xb6 }, new[] { (byte)0x5d, (byte)0xf8 }, new[] { (byte)0x5c, (byte)0x3a }, new[] { (byte)0x5e, (byte)0x7c }, new[] { (byte)0x5f, (byte)0xbe }, new[] { (byte)0xe1, (byte)0 }, new[] { (byte)0xe0, (byte)0xc2 }, new[] { (byte)0xe2, (byte)0x84 }, new[] { (byte)0xe3, (byte)0x46 }, new[] { (byte)0xe6, (byte)0x08 }, new[] { (byte)0xe7, (byte)0xca }, new[] { (byte)0xe5, (byte)0x8c }, new[] { (byte)0xe4, (byte)0x4e }, new[] { (byte)0xef, (byte)0x10 }, new[] { (byte)0xee, (byte)0xd2 }, new[] { (byte)0xec, (byte)0x94 }, new[] { (byte)0xed, (byte)0x56 }, new[] { (byte)0xe8, (byte)0x18 }, new[] { (byte)0xe9, (byte)0xda }, new[] { (byte)0xeb, (byte)0x9c }, new[] { (byte)0xea, (byte)0x5e }, new[] { (byte)0xfd, (byte)0x20 }, new[] { (byte)0xfc, (byte)0xe2 }, new[] { (byte)0xfe, (byte)0xa4 }, new[] { (byte)0xff, (byte)0x66 }, new[] { (byte)0xfa, (byte)0x28 }, new[] { (byte)0xfb, (byte)0xea }, new[] { (byte)0xf9, (byte)0xac }, new[] { (byte)0xf8, (byte)0x6e }, new[] { (byte)0xf3, (byte)0x30 }, new[] { (byte)0xf2, (byte)0xf2 }, new[] { (byte)0xf0, (byte)0xb4 }, new[] { (byte)0xf1, (byte)0x76 }, new[] { (byte)0xf4, (byte)0x38 }, new[] { (byte)0xf5, (byte)0xfa }, new[] { (byte)0xf7, (byte)0xbc }, new[] { (byte)0xf6, (byte)0x7e }, new[] { (byte)0xd9, (byte)0x40 }, new[] { (byte)0xd8, (byte)0x82 }, new[] { (byte)0xda, (byte)0xc4 }, new[] { (byte)0xdb, (byte)0x06 }, new[] { (byte)0xde, (byte)0x48 }, new[] { (byte)0xdf, (byte)0x8a }, new[] { (byte)0xdd, (byte)0xcc }, new[] { (byte)0xdc, (byte)0x0e }, new[] { (byte)0xd7, (byte)0x50 }, new[] { (byte)0xd6, (byte)0x92 }, new[] { (byte)0xd4, (byte)0xd4 }, new[] { (byte)0xd5, (byte)0x16 }, new[] { (byte)0xd0, (byte)0x58 }, new[] { (byte)0xd1, (byte)0x9a }, new[] { (byte)0xd3, (byte)0xdc }, new[] { (byte)0xd2, (byte)0x1e }, new[] { (byte)0xc5, (byte)0x60 }, new[] { (byte)0xc4, (byte)0xa2 }, new[] { (byte)0xc6, (byte)0xe4 }, new[] { (byte)0xc7, (byte)0x26 }, new[] { (byte)0xc2, (byte)0x68 }, new[] { (byte)0xc3, (byte)0xaa }, new[] { (byte)0xc1, (byte)0xec }, new[] { (byte)0xc0, (byte)0x2e }, new[] { (byte)0xcb, (byte)0x70 }, new[] { (byte)0xca, (byte)0xb2 }, new[] { (byte)0xc8, (byte)0xf4 }, new[] { (byte)0xc9, (byte)0x36 }, new[] { (byte)0xcc, (byte)0x78 }, new[] { (byte)0xcd, (byte)0xba }, new[] { (byte)0xcf, (byte)0xfc }, new[] { (byte)0xce, (byte)0x3e }, new[] { (byte)0x91, (byte)0x80 }, new[] { (byte)0x90, (byte)0x42 }, new[] { (byte)0x92, (byte)0x04 }, new[] { (byte)0x93, (byte)0xc6 }, new[] { (byte)0x96, (byte)0x88 }, new[] { (byte)0x97, (byte)0x4a }, new[] { (byte)0x95, (byte)0x0c }, new[] { (byte)0x94, (byte)0xce }, new[] { (byte)0x9f, (byte)0x90 }, new[] { (byte)0x9e, (byte)0x52 }, new[] { (byte)0x9c, (byte)0x14 }, new[] { (byte)0x9d, (byte)0xd6 }, new[] { (byte)0x98, (byte)0x98 }, new[] { (byte)0x99, (byte)0x5a }, new[] { (byte)0x9b, (byte)0x1c }, new[] { (byte)0x9a, (byte)0xde }, new[] { (byte)0x8d, (byte)0xa0 }, new[] { (byte)0x8c, (byte)0x62 }, new[] { (byte)0x8e, (byte)0x24 }, new[] { (byte)0x8f, (byte)0xe6 }, new[] { (byte)0x8a, (byte)0xa8 }, new[] { (byte)0x8b, (byte)0x6a }, new[] { (byte)0x89, (byte)0x2c }, new[] { (byte)0x88, (byte)0xee }, new[] { (byte)0x83, (byte)0xb0 }, new[] { (byte)0x82, (byte)0x72 }, new[] { (byte)0x80, (byte)0x34 }, new[] { (byte)0x81, (byte)0xf6 }, new[] { (byte)0x84, (byte)0xb8 }, new[] { (byte)0x85, (byte)0x7a }, new[] { (byte)0x87, (byte)0x3c }, new[] { (byte)0x86, (byte)0xfe }, new[] { (byte)0xa9, (byte)0xc0 }, new[] { (byte)0xa8, (byte)0x02 }, new[] { (byte)0xaa, (byte)0x44 }, new[] { (byte)0xab, (byte)0x86 }, new[] { (byte)0xae, (byte)0xc8 }, new[] { (byte)0xaf, (byte)0x0a }, new[] { (byte)0xad, (byte)0x4c }, new[] { (byte)0xac, (byte)0x8e }, new[] { (byte)0xa7, (byte)0xd0 }, new[] { (byte)0xa6, (byte)0x12 }, new[] { (byte)0xa4, (byte)0x54 }, new[] { (byte)0xa5, (byte)0x96 }, new[] { (byte)0xa0, (byte)0xd8 }, new[] { (byte)0xa1, (byte)0x1a }, new[] { (byte)0xa3, (byte)0x5c }, new[] { (byte)0xa2, (byte)0x9e }, new[] { (byte)0xb5, (byte)0xe0 }, new[] { (byte)0xb4, (byte)0x22 }, new[] { (byte)0xb6, (byte)0x64 }, new[] { (byte)0xb7, (byte)0xa6 }, new[] { (byte)0xb2, (byte)0xe8 }, new[] { (byte)0xb3, (byte)0x2a }, new[] { (byte)0xb1, (byte)0x6c }, new[] { (byte)0xb0, (byte)0xae }, new[] { (byte)0xbb, (byte)0xf0 }, new[] { (byte)0xba, (byte)0x32 }, new[] { (byte)0xb8, (byte)0x74 }, new[] { (byte)0xb9, (byte)0xb6 }, new[] { (byte)0xbc, (byte)0xf8 }, new[] { (byte)0xbd, (byte)0x3a }, new[] { (byte)0xbf, (byte)0x7c }, new[] { (byte)0xbe, (byte)0xbe } };
		#endregion

		private byte[] initialCounter;
		private byte[] tag;
		private byte[][] hTable;
		private readonly byte[] block;
		private byte[] macBlock;
		private readonly byte[] aadBlock;
		private readonly byte[] hashBlock;
		private readonly byte[] mulBlock;
		private byte[] inBuffer;
		private int blockOffset;
		private int aadOffset;
		private int aadLength;
		private int mesageLength;
		private MemoryStream stream;
		public GcmMode(BlockCipher cipher) : base(cipher)
		{
			block = new byte[blocksize];
			nonce = new byte[blocksize];
			hashBlock = new byte[blocksize];
			macBlock = new byte[blocksize];
			aadBlock = new byte[blocksize];
			mulBlock = new byte[blocksize];
			taglen = blocksize;
			mesageLength = 0;
		}

		public override void Init(Mode mode, byte[] key, byte[] nonce, int taglen)
		{
			this.mode = mode;
			engine.Init(Mode.ENCRYPT, key);
			if (mode == Mode.ENCRYPT)
				inBuffer = new byte[blocksize];
			else
			{
				inBuffer = new byte[blocksize + taglen];
			}

			Reset();
			engine.ProcessBlock(block, 0, block, 0);
			InitTable();
			SetNonce(nonce);
			SetTaglen(taglen);
			stream = mode == Mode.ENCRYPT ? null : new MemoryStream();
		}

		public virtual void Reset()
		{
			blockOffset = 0;
			aadOffset = 0;
			mesageLength = 0;
			aadLength = 0;
			block.FillBy((byte)0);
			nonce.FillBy((byte)0);
			hashBlock.FillBy((byte)0);
			macBlock.FillBy((byte)0);
			aadBlock.FillBy((byte)0);
			mulBlock.FillBy((byte)0);
			inBuffer.FillBy((byte)0);
			stream?.SetLength(0);
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

			initialCounter = this.nonce.CopyOf();
		}

		private void SetTaglen(int taglen)
		{
			if (taglen < 0 || taglen > MaxTagLength)
				throw new ArgumentException("length of tag should be 0~16 bytes");

			this.taglen = taglen;
			tag = new byte[taglen];
		}

		public override int GetOutputSize(int len)
		{
			var outSize = len + blockOffset;
			if (mode == Mode.ENCRYPT)
				return outSize + taglen;

			return outSize < taglen ? 0 : outSize - taglen;
		}

		public virtual int GetUpdateOutputSize(int len)
		{
			var outSize = len + blockOffset;
			if (mode == Mode.DECRYPT)
			{
				if (outSize < taglen)
					return 0;

				outSize -= taglen;
			}

			return unchecked(outSize & (int)0xfffffff0);
		}

		public override void UpdateAAD(byte[] inBytes)
		{
			if (inBytes == null || inBytes.Length == 0)
				return;

			var len = inBytes.Length;
			var gap = aadBlock.Length - aadOffset;
			var inOffset = 0;
			if (len > gap)
			{
				Buffer.BlockCopy(inBytes, inOffset, aadBlock, aadOffset, gap);
				Ghash(macBlock, aadBlock, blocksize);
				aadOffset = 0;
				len -= gap;
				inOffset += gap;
				aadLength += gap;
				while (len >= blocksize)
				{
					Buffer.BlockCopy(inBytes, inOffset, aadBlock, 0, blocksize);
					Ghash(macBlock, aadBlock, blocksize);
					inOffset += blocksize;
					len -= blocksize;
					aadLength += blocksize;
				}
			}

			if (len > 0)
			{
				Buffer.BlockCopy(inBytes, inOffset, aadBlock, aadOffset, len);
				aadOffset += len;
				aadLength += len;
			}
		}

		public override byte[] Update(byte[] inBytes)
		{
			if (aadOffset != 0)
			{
				Ghash(macBlock, aadBlock, aadOffset);
				aadOffset = 0;
			}

			if (mode == Mode.ENCRYPT)
				return UpdateEncrypt(inBytes);
			else
			{
				UpdateDecrypt(inBytes);
			}

			return null;
		}

		public override byte[] DoFinal()
		{
			var outBytes = new byte[GetOutputSize(0)];
			var outOffset = 0;
			var extra = blockOffset;
			if (extra != 0)
			{
				if (mode == Mode.ENCRYPT)
				{
					EncryptBlock(outBytes, outOffset, extra);
					outOffset += extra;
				}
				else
				{
					if (extra < taglen)
						throw new ArgumentException("data too short");

					extra -= taglen;
					if (extra > 0)
					{
						DecryptBlock(outBytes, outOffset, extra);
						stream.Write(outBytes, outOffset, extra);
					}

					Buffer.BlockCopy(inBuffer, extra, tag, 0, taglen);
				}
			}

			if (mode == Mode.DECRYPT)
				mesageLength -= taglen;

			block.FillBy((byte)0);
			IntToBigEndian(aadLength *= 8, block, 4);
			IntToBigEndian(mesageLength *= 8, block, 12);
			Ghash(macBlock, block, blocksize);
			engine.ProcessBlock(initialCounter, 0, block, 0);
			XOR(macBlock, block);
			if (mode == Mode.ENCRYPT)
				Buffer.BlockCopy(macBlock, 0, outBytes, outOffset, taglen);
			else
			{
				macBlock = macBlock.CopyOf(taglen);
				if (!macBlock.SequenceEqual(tag))
				{
					stream.SetLength(0);
					outBytes = null;
				}
				else
				{
					outBytes = stream.ToArray();
				}
			}

			return outBytes;
		}

		private byte[] UpdateEncrypt(byte[] inBytes)
		{
			var length = inBytes.Length;
			var gap = blocksize - blockOffset;
			var inOffset = 0;
			var outOffset = 0;
			var outBytes = new byte[GetUpdateOutputSize(length)];
			if (length >= gap)
			{
				Buffer.BlockCopy(inBytes, inOffset, inBuffer, blockOffset, gap);
				EncryptBlock(outBytes, outOffset, blocksize);
				length -= gap;
				inOffset += gap;
				outOffset += gap;
				mesageLength += gap;
				blockOffset = 0;
				while (length >= blocksize)
				{
					Buffer.BlockCopy(inBytes, inOffset, inBuffer, 0, blocksize);
					EncryptBlock(outBytes, outOffset, blocksize);
					length -= blocksize;
					inOffset += blocksize;
					outOffset += blocksize;
					mesageLength += blocksize;
				}
			}

			if (length > 0)
			{
				Buffer.BlockCopy(inBytes, inOffset, inBuffer, 0, length);
				mesageLength += length;
				blockOffset += length;
			}

			return outBytes;
		}

		private void UpdateDecrypt(byte[] inBytes)
		{
			var length = inBytes.Length;
			var gap = inBuffer.Length - blockOffset;
			var inOffset = 0;
			var outOffset = 0;
			var outBytes = new byte[GetUpdateOutputSize(length)];
			if (length >= gap)
			{
				Buffer.BlockCopy(inBytes, inOffset, inBuffer, blockOffset, gap);
				DecryptBlock(outBytes, outOffset, blocksize);
				Buffer.BlockCopy(inBuffer, blocksize, inBuffer, 0, taglen);
				length -= gap;
				inOffset += gap;
				outOffset += blocksize;
				mesageLength += gap;
				blockOffset = taglen;
				while (length >= blocksize)
				{
					Buffer.BlockCopy(inBytes, inOffset, inBuffer, blockOffset, blocksize);
					DecryptBlock(outBytes, outOffset, blocksize);
					Buffer.BlockCopy(inBuffer, blocksize, inBuffer, 0, taglen);
					length -= blocksize;
					inOffset += blocksize;
					outOffset += blocksize;
					mesageLength += blocksize;
				}
			}

			if (length > 0)
			{
				Buffer.BlockCopy(inBytes, inOffset, inBuffer, blockOffset, length);
				mesageLength += length;
				blockOffset += length;
			}

			stream.Write(outBytes, 0, outBytes.Length);
		}

		private int EncryptBlock(byte[] inBytes, int inOffset, int length)
		{
			IncrementCounter(nonce);
			engine.ProcessBlock(nonce, 0, block, 0);
			XOR(block, inBuffer);
			Buffer.BlockCopy(block, 0, inBytes, inOffset, length);
			Ghash(macBlock, block, length);
			return length;
		}

		private int DecryptBlock(byte[] outBytes, int outOffset, int length)
		{
			Buffer.BlockCopy(inBuffer, 0, block, 0, length);
			Ghash(macBlock, block, length);
			IncrementCounter(nonce);
			engine.ProcessBlock(nonce, 0, block, 0);
			XOR(outBytes, outOffset, block, 0, inBuffer, 0, length);
			return length;
		}

		private void InitTable()
		{
			hTable = new byte[256][];
			for (var i = 0; i < 256; i++)
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

		private static void IncrementCounter(byte[] counter)
		{
			for (var i = 15; i >= 12; --i)
			{
				if (++counter[i] != 0)
					return;
			}
		}

		private void Ghash(byte[] r, byte[] data, int dataLength)
		{
			Buffer.BlockCopy(r, 0, hashBlock, 0, blocksize);
			var pos = 0;
			var length = dataLength;
			for (; length >= blocksize; pos += blocksize, length -= blocksize)
			{
				XOR(hashBlock, 0, data, pos, blocksize);
				Gfmul(hashBlock, hashBlock);
			}

			if (length > 0)
			{
				XOR(hashBlock, 0, data, pos, length);
				Gfmul(hashBlock, hashBlock);
			}

			Buffer.BlockCopy(hashBlock, 0, r, 0, blocksize);
		}

		private void Gfmul(byte[] r, byte[] x)
		{
			mulBlock.FillBy((byte)0);
			int rowIdx;
			for (rowIdx = 15; rowIdx > 0; --rowIdx)
			{
				int colIdx;
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
				mulBlock[0] ^= Reduction[mask][0];
				mulBlock[1] ^= Reduction[mask][1];
			}

			rowIdx = x[0] & 0xff;
			XOR(r, mulBlock, hTable[rowIdx]);
		}
	}
}
