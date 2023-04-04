using System;
using static LEA.BlockCipher;

namespace LEA.OperatingMode
{
	public class EcbMode : BlockCipherModeBlock
	{
		public EcbMode(BlockCipher cipher) : base(cipher)
		{
		}

		public override string GetAlgorithmName() => engine.GetAlgorithmName() + "/ECB";

		public override void Init(Mode mode, byte[] key)
		{
			this.mode = mode;
			engine.Init(mode, key);
		}

		protected override int ProcessBlock(byte[] inBytes, int inOffset, byte[] outBytes, int outOffset, int outLength)
		{
			if (outLength != blocksize)
				throw new ArgumentException("outlen should be " + blocksize + " in " + GetAlgorithmName());

			if (inOffset + blocksize > inBytes.Length)
				throw new InvalidOperationException("input data too short");

			return engine.ProcessBlock(inBytes, inOffset, outBytes, outOffset);
		}
	}
}
