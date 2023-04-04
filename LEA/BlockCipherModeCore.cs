using System;
using static LEA.BlockCipher;

namespace LEA
{
	public abstract class BlockCipherModeCore : BlockCipherMode
	{
		protected Mode mode;
		protected BlockCipher engine;
		protected byte[] buffer;
		protected int bufferOffset;
		protected int blocksize;
		protected int blockmask;
		protected BlockCipherModeCore(BlockCipher cipher)
		{
			engine = cipher;
			blocksize = engine.GetBlockSize();
			blockmask = GetBlockmask(blocksize);
			buffer = new byte[blocksize];
		}

		public override byte[] DoFinal(byte[] message)
		{
			var part1 = Update(message);
			var part2 = DoFinal();
			var len1 = (part1?.Length) ?? 0;
			var len2 = (part2?.Length) ?? 0;
			var outBytes = new byte[len1 + len2];
			if (len1 > 0)
				Buffer.BlockCopy(part1, 0, outBytes, 0, len1);

			if (len2 > 0)
				Buffer.BlockCopy(part2, 0, outBytes, len1, len2);

			return outBytes;
		}

		protected abstract int ProcessBlock(byte[] inBytes, int inOffset, byte[] outBytes, int outOffset, int outLength);
		protected virtual int ProcessBlock(byte[] inBytes, int inOffset, byte[] outBytes, int outOffset) => ProcessBlock(inBytes, inOffset, outBytes, outOffset, blocksize);

		protected static int GetBlockmask(int blocksize)
		{
			var mask = 0;
			switch (blocksize)
			{
				case 8:
					mask = unchecked((int)0xfffffff7);
					break;
				case 16:
					mask = unchecked((int)0xfffffff0);
					break;
				case 32:
					mask = unchecked((int)0xffffffe0);
					break;
			}

			return mask;
		}
	}
}
