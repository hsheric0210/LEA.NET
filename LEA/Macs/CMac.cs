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
		private int blockSize;
		private int blockIndex;
		private byte[] block;
		private byte[] mac;
		private byte[] rb;
		private byte[] subkey1, subkey2;
		public CMac(BlockCipher cipher) => engine = cipher;

		public override void Init(byte[] key)
		{
			engine.Init(Mode.ENCRYPT, key);
			blockIndex = 0;
			blockSize = engine.GetBlockSize();
			block = new byte[blockSize];
			mac = new byte[blockSize];
			subkey1 = new byte[blockSize];
			subkey2 = new byte[blockSize];
			SelectRB();
			var zero = new byte[blockSize];
			engine.ProcessBlock(zero, 0, zero, 0);
			Cmac_subkey(subkey1, zero);
			Cmac_subkey(subkey2, subkey1);
		}

		public override void Reset()
		{
			engine.Reset();
			block.FillBy((byte)0);
			mac.FillBy((byte)0);
			blockIndex = 0;
		}

		public override void Update(byte[] message)
		{
			if (message == null || message.Length == 0)
				return;

			var len = message.Length;
			var msgOff = 0;
			var gap = blockSize - blockIndex;
			if (len > gap)
			{
				Buffer.BlockCopy(message, msgOff, block, blockIndex, gap);
				blockIndex = 0;
				len -= gap;
				msgOff += gap;
				while (len > blockSize)
				{
					XOR(block, mac);
					engine.ProcessBlock(block, 0, mac, 0);
					Buffer.BlockCopy(message, msgOff, block, 0, blockSize);
					len -= blockSize;
					msgOff += blockSize;
				}

				if (len > 0)
				{
					XOR(block, mac);
					engine.ProcessBlock(block, 0, mac, 0);
				}
			}

			if (len > 0)
			{
				Buffer.BlockCopy(message, msgOff, block, blockIndex, len);
				blockIndex += len;
			}
		}

		public override byte[] DoFinal(byte[] message)
		{
			Update(message);
			return DoFinal();
		}

		public override byte[] DoFinal()
		{
			if (blockIndex < blockSize)
			{
				block[blockIndex] = 0x80;
				block.FillBy(blockIndex + 1, blockSize, (byte)0);
			}

			XOR(block, blockIndex == blockSize ? subkey1 : subkey2);
			XOR(block, mac);
			engine.ProcessBlock(block, 0, mac, 0);
			return mac.CopyOf();
		}

		private void SelectRB()
		{
			switch (blockSize)
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

		private void Cmac_subkey(byte[] newKey, byte[] oldKey)
		{
			Buffer.BlockCopy(oldKey, 0, newKey, 0, blockSize);
			ShiftLeft(newKey, 1);
			if ((oldKey[0] & 0x80) != 0)
			{
				for (var i = 0; i < rb.Length; ++i)
				{
					newKey[blockSize - rb.Length + i] ^= rb[i];
				}
			}
		}
	}
}