using System;
using static LEA.BlockCipher;
using static LEA.Utils.Ops;

namespace LEA.OperatingMode
{
	public class CbcMode : BlockCipherModeBlock
	{
		private byte[] iv;
		private byte[] feedback;
		public CbcMode(BlockCipher cipher) : base(cipher)
		{
		}

		public override string GetAlgorithmName() => engine.GetAlgorithmName() + "/CBC";

		public override void Init(Mode mode, byte[] key, byte[] iv)
		{
			this.mode = mode;
			engine.Init(mode, key);
			this.iv = iv.CopyOf();
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
			if (outlen != blocksize)
				throw new ArgumentException("outlen should be " + blocksize + " in " + GetAlgorithmName());

			if (mode == Mode.ENCRYPT)
				return EncryptBlock(inBytes, inOffset, outBytes, outOffset);

			return DecryptBlock(inBytes, inOffset, outBytes, outOffset);
		}

		private int EncryptBlock(byte[] inBytes, int inOffset, byte[] outBytes, int outOffset)
		{
			if (inOffset + blocksize > inBytes.Length)
				throw new InvalidOperationException("input data too short");

			XOR(feedback, 0, inBytes, inOffset, blocksize);
			engine.ProcessBlock(feedback, 0, outBytes, outOffset);
			Buffer.BlockCopy(outBytes, outOffset, feedback, 0, blocksize);
			return blocksize;
		}

		private int DecryptBlock(byte[] inBytes, int inOffset, byte[] outBytes, int outOffset)
		{
			if (inOffset + blocksize > inBytes.Length)
				throw new InvalidOperationException("input data too short");

			engine.ProcessBlock(inBytes, inOffset, outBytes, outOffset);
			XOR(outBytes, outOffset, feedback, 0, blocksize);
			Buffer.BlockCopy(inBytes, inOffset, feedback, 0, blocksize);
			return blocksize;
		}
	}
}