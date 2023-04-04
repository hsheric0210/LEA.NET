using System;
using static LEA.BlockCipher;

namespace LEA
{
	public abstract class BlockCipherModeAE
	{
		protected Mode mode;
		protected BlockCipher engine;
		protected byte[] buffer;
		protected byte[] nonce;
		protected int bufOff;
		protected int blocksize;
		protected int taglen;
		protected BlockCipherModeAE(BlockCipher cipher)
		{
			engine = cipher;
			blocksize = engine.GetBlockSize();
			buffer = new byte[blocksize];
		}

		public abstract void Init(Mode mode, byte[] key, byte[] nonce, int taglen);
		public abstract void UpdateAAD(byte[] aad);
		public abstract byte[] Update(byte[] message);
		public abstract byte[] DoFinal();
		public abstract int GetOutputSize(int length);
		public virtual byte[] DoFinal(byte[] message)
		{
			byte[] outBytes;
			if (mode == Mode.ENCRYPT)
			{
				var part1 = Update(message);
				var part2 = DoFinal();
				var len1 = (part1?.Length) ?? 0;
				var len2 = (part2?.Length) ?? 0;
				outBytes = new byte[len1 + len2];
				if (part1 != null)
					Buffer.BlockCopy(part1, 0, outBytes, 0, len1);
				if (part2 != null)
					Buffer.BlockCopy(part2, 0, outBytes, len1, len2);
			}
			else
			{
				Update(message);
				outBytes = DoFinal();
			}

			return outBytes;
		}
	}
}