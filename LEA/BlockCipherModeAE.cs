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
		public BlockCipherModeAE(BlockCipher cipher)
		{
			engine = cipher;
			blocksize = engine.GetBlockSize();
			buffer = new byte[blocksize];
		}

		public abstract void Init(Mode mode, byte[] mk, byte[] nonce, int taglen);
		public abstract void UpdateAAD(byte[] aad);
		public abstract byte[] Update(byte[] msg);
		public abstract byte[] DoFinal();
		public abstract int GetOutputSize(int len);
		public virtual byte[] DoFinal(byte[] msg)
		{
			byte[] @out = null;
			if (mode == Mode.ENCRYPT)
			{
				var part1 = Update(msg);
				var part2 = DoFinal();
				var len1 = part1 == null ? 0 : part1.Length;
				var len2 = part2 == null ? 0 : part2.Length;
				@out = new byte[len1 + len2];
				if (part1 != null)
					Buffer.BlockCopy(part1, 0, @out, 0, len1);
				if (part2 != null)
					Buffer.BlockCopy(part2, 0, @out, len1, len2);
			}
			else
			{
				Update(msg);
				@out = DoFinal();
			}

			return @out;
		}
	}
}