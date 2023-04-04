using System;
using static LEA.BlockCipher;
using static LEA.Utils.Ops;

namespace LEA.OperatingMode
{
	// DONE: block vs buffer
	public class OfbMode : BlockCipherModeStream
	{
		private byte[] iv;
		private byte[] block;
		public OfbMode(BlockCipher cipher) : base(cipher)
		{
		}

		public override string GetAlgorithmName() => engine.GetAlgorithmName() + "/OFB";

		public override void Init(Mode mode, byte[] key, byte[] iv)
		{
			this.mode = mode;
			engine.Init(Mode.ENCRYPT, key);
			this.iv = iv.CopyOf();
			block = new byte[blocksize];
			Reset();
		}

		public override void Reset()
		{
			base.Reset();
			Buffer.BlockCopy(iv, 0, block, 0, blocksize);
		}

		protected override int ProcessBlock(byte[] inBytes, int inOffset, byte[] outBytes, int outOffset, int outLength)
		{
			var length = engine.ProcessBlock(block, 0, block, 0);
			XOR(outBytes, outOffset, inBytes, inOffset, block, 0, outLength);
			return length;
		}
	}
}
