using LEA.Extension;

namespace LEA.Mac;

public class CMac : MacBase
{
	private static readonly byte[] R256 = new[] { (byte)0x04, (byte)0x25 };
	private static readonly byte[] R128 = new[] { (byte)0x87 };
	private static readonly byte[] R64 = new[] { (byte)0x1b };

	private BlockCipher engine;
	private int blocksize;
	private int blkIdx;
	private byte[] block;
	private byte[] mac;
	private byte[] RB;
	private byte[] k1, k2;

	public CMac(BlockCipher cipher) => engine = cipher;

	public override void Init(ReadOnlySpan<byte> key)
	{
		engine.Init(OperatingMode.Encrypt, key);
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
		Array.Fill(block, (byte)0);
		Array.Fill(mac, (byte)0);
		blkIdx = 0;
	}

	public override void Update(ReadOnlySpan<byte> msg)
	{
		if (msg == null || msg.Length == 0)
		{
			return;
		}

		var len = msg.Length;
		var msgOff = 0;
		var gap = blocksize - blkIdx;
		if (len > gap)
		{
			Array.Copy(msg, msgOff, block, blkIdx, gap);
			blkIdx = 0;
			len -= gap;
			msgOff += gap;
			while (len > blocksize)
			{
				block.XOR(mac);
				engine.ProcessBlock(block, 0, mac, 0);
				Array.Copy(msg, msgOff, block, 0, blocksize);
				len -= blocksize;
				msgOff += blocksize;
			}

			if (len > 0)
			{
				block.XOR(mac);
				engine.ProcessBlock(block, 0, mac, 0);
			}
		}

		if (len > 0)
		{
			Array.Copy(msg, msgOff, block, blkIdx, len);
			blkIdx += len;
		}
	}

	public override ReadOnlySpan<byte> DoFinal(ReadOnlySpan<byte> msg)
	{
		Update(msg);
		return DoFinal();
	}

	public override ReadOnlySpan<byte> DoFinal()
	{
		if (blkIdx < blocksize)
		{
			block[blkIdx] = 0x80;
			Array.Fill(block, (byte)0x00, blkIdx + 1, blocksize);
		}

		block.XOR(blkIdx == blocksize ? k1 : k2);
		block.XOR(mac);
		engine.ProcessBlock(block, 0, mac, 0);

		var copy = new byte[mac.Length];
		Array.Copy(mac, copy, mac.Length);
		return copy;
	}

	private void SelectRB()
	{
		switch (blocksize)
		{
			case 8:
				RB = R64;
				break;
			case 16:
				RB = R128;
				break;
			case 32:
				RB = R256;
				break;
		}
	}

	private void Cmac_subkey(Span<byte> new_key, ReadOnlySpan<byte> old_key)
	{
		old_key[..blocksize].CopyTo(new_key);
		new_key.ShiftLeft(1);
		if ((old_key[0] & 0x80) != 0)
		{
			for (var i = 0; i < RB.Length; ++i)
			{
				new_key[blocksize - RB.Length + i] ^= RB[i];
			}
		}
	}
}