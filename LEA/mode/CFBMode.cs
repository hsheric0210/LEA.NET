using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LEA.mode
{
	// DONE: block vs buffer
	public class CFBMode : BlockCipherModeStream
	{
		private byte[] iv;
		private byte[] block;
		private byte[] feedback;
		public CFBMode(BlockCipher cipher) : base(cipher)
		{
		}

		public override string GetAlgorithmName()
		{
			return engine.GetAlgorithmName() + "/CFB";
		}

		public override void Init(Mode mode, byte[] mk, byte[] iv)
		{
			this.mode = mode;
			engine.Init(Mode.ENCRYPT, mk);
			this.iv = iv.Clone();
			block = new byte[blocksize];
			feedback = new byte[blocksize];
			Reset();
		}

		public override void Reset()
		{
			base.Reset();
			System.Arraycopy(iv, 0, feedback, 0, blocksize);
		}

		protected override int ProcessBlock(byte[] @in, int inOff, byte[] @out, int outOff, int outlen)
		{
			var length = engine.ProcessBlock(feedback, 0, block, 0);
			XOR(@out, outOff, @in, inOff, block, 0, outlen);
			if (mode == Mode.ENCRYPT)
				System.Arraycopy(@out, outOff, feedback, 0, blocksize);
			else
			{
				System.Arraycopy(@in, inOff, feedback, 0, blocksize);
			}

			return length;
		}
	}
}