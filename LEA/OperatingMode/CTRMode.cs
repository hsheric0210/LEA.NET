using System;
using static LEA.BlockCipher;
using static LEA.Utils.Ops;

namespace LEA.OperatingMode
{
	public class CtrMode : BlockCipherModeStream
	{
		private byte[] iv;
		private byte[] ctr;
		private byte[] block;
		public CtrMode(BlockCipher cipher) : base(cipher)
		{
		}

		public override string GetAlgorithmName() => engine.GetAlgorithmName() + "/CTR";

		public override void Init(Mode mode, byte[] mk, byte[] iv)
		{
			this.mode = mode;
			engine.Init(Mode.ENCRYPT, mk);
			this.iv = iv.CopyOf();
			ctr = new byte[blocksize];
			block = new byte[blocksize];
			Reset();
		}

		public override void Reset()
		{
			base.Reset();
			Buffer.BlockCopy(iv, 0, ctr, 0, ctr.Length);
		}

		protected override int ProcessBlock(byte[] @in, int inOff, byte[] @out, int outOff, int outlen)
		{
			var length = engine.ProcessBlock(ctr, 0, block, 0);
			AddCounter();
			XOR(@out, outOff, @in, inOff, block, 0, outlen);
			return length;
		}

		private void AddCounter()
		{
			for (var i = ctr.Length - 1; i >= 0; --i)
			{
				if (++ctr[i] != 0)
					break;
			}
		}
	}
}