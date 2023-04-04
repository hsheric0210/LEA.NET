using System;
using static LEA.BlockCipher;
using static LEA.Utils.Ops;

namespace LEA.Macs
{
	public class CMac : Mac
	{
		private static readonly byte[] r256 = new[] { (byte)0x04, (byte)0x25 };
		private static readonly byte[] r128 = new[] { (byte)0x87 };
		private static readonly byte[] r64 = new[] { (byte)0x1b };
		private readonly BlockCipher engine;
		private int blocksize;
		private int blkIdx;
		private byte[] block;
		private byte[] mac;
		private byte[] rb;
		private byte[] k1, k2;
		public CMac(BlockCipher cipher) => engine = cipher;

		public override void Init(byte[] key)
		{
			engine.Init(Mode.ENCRYPT, key);
			blkIdx = 0;
			blocksize = engine.GetBlockSize();
			block = new byte[blocksize];
			mac = new byte[blocksize];
			k1 = new byte[blocksize];
			k2 = new byte[blocksize];
			SelectRB();
			var zero = new byte[blocksize];
			engine.ProcessBlock(zero, 0, zero, 0);
			Cmac_subkey(k1, zero);
			Cmac_subkey(k2, k1);
		}

		public override void Reset()
		{
			engine.Reset();
			block.FillBy((byte)0);
			mac.FillBy((byte)0);
			blkIdx = 0;
		}

		public override void Update(byte[] msg)
		{
			if (msg == null || msg.Length == 0)
				return;

			var len = msg.Length;
			var msgOff = 0;
			var gap = blocksize - blkIdx;
			if (len > gap)
			{
				Buffer.BlockCopy(msg, msgOff, block, blkIdx, gap);
				blkIdx = 0;
				len -= gap;
				msgOff += gap;
				while (len > blocksize)
				{
					XOR(block, mac);
					engine.ProcessBlock(block, 0, mac, 0);
					Buffer.BlockCopy(msg, msgOff, block, 0, blocksize);
					len -= blocksize;
					msgOff += blocksize;
				}

				if (len > 0)
				{
					XOR(block, mac);
					engine.ProcessBlock(block, 0, mac, 0);
				}
			}

			if (len > 0)
			{
				Buffer.BlockCopy(msg, msgOff, block, blkIdx, len);
				blkIdx += len;
			}
		}

		public override byte[] DoFinal(byte[] msg)
		{
			Update(msg);
			return DoFinal();
		}

		public override byte[] DoFinal()
		{
			if (blkIdx < blocksize)
			{
				block[blkIdx] = 0x80;
				block.FillBy(blkIdx + 1, blocksize, (byte)0);
			}

			XOR(block, blkIdx == blocksize ? k1 : k2);
			XOR(block, mac);
			engine.ProcessBlock(block, 0, mac, 0);
			return mac.CopyOf();
		}

		private void SelectRB()
		{
			switch (blocksize)
			{
				case 8:
					rb = r64;
					break;
				case 16:
					rb = r128;
					break;
				case 32:
					rb = r256;
					break;
			}
		}

		private void Cmac_subkey(byte[] new_key, byte[] old_key)
		{
			Buffer.BlockCopy(old_key, 0, new_key, 0, blocksize);
			ShiftLeft(new_key, 1);
			if ((old_key[0] & 0x80) != 0)
			{
				for (var i = 0; i < rb.Length; ++i)
				{
					new_key[blocksize - rb.Length + i] ^= rb[i];
				}
			}
		}
	}
}