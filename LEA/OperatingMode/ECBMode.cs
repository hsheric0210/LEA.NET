using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static LEA.BlockCipher;

namespace LEA.mode
{
	public class ECBMode : BlockCipherModeBlock
	{
		public ECBMode(BlockCipher cipher) : base(cipher)
		{
		}

		public override string GetAlgorithmName()
		{
			return engine.GetAlgorithmName() + "/ECB";
		}

		public override void Init(Mode mode, byte[] mk)
		{
			this.mode = mode;
			engine.Init(mode, mk);
		}

		protected override int ProcessBlock(byte[] @in, int inOff, byte[] @out, int outOff, int outlen)
		{
			if (outlen != blocksize)
				throw new ArgumentException("outlen should be " + blocksize + " in " + GetAlgorithmName());

			if (inOff + blocksize > @in.Length)
				throw new InvalidOperationException("input data too short");

			return engine.ProcessBlock(@in, inOff, @out, outOff);
		}
	}
}