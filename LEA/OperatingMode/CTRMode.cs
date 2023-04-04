using System;
using static LEA.BlockCipher;
using static LEA.Utils.Ops;

namespace LEA.OperatingMode
{
	public class CtrMode : BlockCipherModeStream
	{
		private byte[] iv;
		private byte[] counter;
		private byte[] block;
		public CtrMode(BlockCipher cipher) : base(cipher)
		{
		}

		public override string GetAlgorithmName() => engine.GetAlgorithmName() + "/CTR";

		public override void Init(Mode mode, byte[] key, byte[] iv)
		{
			this.mode = mode;
			engine.Init(Mode.ENCRYPT, key);
			this.iv = iv.CopyOf();
			counter = new byte[blocksize];
			block = new byte[blocksize];
			Reset();
		}

		public override void Reset()
		{
			base.Reset();
			Buffer.BlockCopy(iv, 0, counter, 0, counter.Length);
		}

		protected override int ProcessBlock(byte[] inBytes, int inOffset, byte[] outBytes, int outOffset, int outLength)
		{
			var length = engine.ProcessBlock(counter, 0, block, 0);
			AddCounter();
			XOR(outBytes, outOffset, inBytes, inOffset, block, 0, outLength);
			return length;
		}

		private void AddCounter()
		{
			for (var i = counter.Length - 1; i >= 0; --i)
			{
				if (++counter[i] != 0)
					break;
			}
		}
	}
}