using System;
using static LEA.BlockCipher;
using static LEA.Utils.Ops;

namespace LEA.OperatingMode
{
	// DONE: block vs buffer
	public class CfbMode : BlockCipherModeStream
	{
		private byte[] iv;
		private byte[] block;
		private byte[] feedback;
		public CfbMode(BlockCipher cipher) : base(cipher)
		{
		}

		public override string GetAlgorithmName() => engine.GetAlgorithmName() + "/CFB";

		public override void Init(Mode mode, byte[] key, byte[] iv)
		{
			this.mode = mode;
			engine.Init(Mode.ENCRYPT, key);
			this.iv = iv.CopyOf();
			block = new byte[blocksize];
			feedback = new byte[blocksize];
			Reset();
		}

		public override void Reset()
		{
			base.Reset();
			Buffer.BlockCopy(iv, 0, feedback, 0, blocksize);
		}

		protected override int ProcessBlock(byte[] inBytes, int inOffset, byte[] outBytes, int outOffset, int outlen)
		{
			var length = engine.ProcessBlock(feedback, 0, block, 0);
			XOR(outBytes, outOffset, inBytes, inOffset, block, 0, outlen);
			if (mode == Mode.ENCRYPT)
				Buffer.BlockCopy(outBytes, outOffset, feedback, 0, blocksize);
			else
			{
				Buffer.BlockCopy(inBytes, inOffset, feedback, 0, blocksize);
			}

			return length;
		}
	}
}