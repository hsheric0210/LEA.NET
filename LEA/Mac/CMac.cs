using static LEA.Utils.Xor;
using static LEA.Utils.Shifting;

namespace LEA.Mac;

public class CMac : MacBase
{
    private static readonly byte[] R256 = new[] { (byte)0x04, (byte)0x25 };
    private static readonly byte[] R128 = new[] { (byte)0x87 };
    private static readonly byte[] R64 = new[] { (byte)0x1b };

    private BlockCipher engine;
    private int blockSize;
    private int blockIndex;
    private byte[] block;
    private byte[] mac;
    private byte[] RB;
    private byte[] k1, k2;

    public CMac(BlockCipher cipher) => engine = cipher;

    public override void Init(ReadOnlySpan<byte> key)
    {
        engine.Init(OperatingMode.Encrypt, key);
        blockIndex = 0;
        blockSize = engine.GetBlockSize();
        block = new byte[blockSize];
        mac = new byte[blockSize];
        k1 = new byte[blockSize];
        k2 = new byte[blockSize];
        SelectRB();
        var zero = new byte[blockSize];
        engine.ProcessBlock(zero, 0, zero, 0);
        Cmac_subkey(k1, zero);
        Cmac_subkey(k2, k1);
    }

    public override void Reset()
    {
        engine.Reset();
        Array.Fill(block, (byte)0);
        Array.Fill(mac, (byte)0);
        blockIndex = 0;
    }

    public override void Update(ReadOnlySpan<byte> msg)
    {
        if (msg.Length == 0)
        {
            return;
        }

        var len = msg.Length;
        var msgOff = 0;
        var gap = blockSize - blockIndex;
        if (len > gap)
        {
            msg.Slice(msgOff, gap).CopyTo(block.AsSpan()[blockIndex..]);
            blockIndex = 0;
            len -= gap;
            msgOff += gap;
            while (len > blockSize)
            {
                XOR(block, mac);
                engine.ProcessBlock(block, 0, mac, 0);
                msg.Slice(msgOff, blockSize).CopyTo(block.AsSpan()[blockIndex..]);
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
            msg.Slice(msgOff, len).CopyTo(block.AsSpan()[blockIndex..]);
            blockIndex += len;
        }
    }

    public override ReadOnlySpan<byte> DoFinal(ReadOnlySpan<byte> msg)
    {
        Update(msg);
        return DoFinal();
    }

    public override ReadOnlySpan<byte> DoFinal()
    {
        if (blockIndex < blockSize)
        {
            block[blockIndex] = 0x80;
            Array.Fill(block, (byte)0x00, blockIndex + 1, blockSize - (blockIndex + 1));
        }

        XOR(block, blockIndex == blockSize ? k1 : k2);
        XOR(block, mac);
        engine.ProcessBlock(block, 0, mac, 0);

        var copy = new byte[mac.Length];
        Array.Copy(mac, copy, mac.Length);
        return copy;
    }

    private void SelectRB()
    {
        switch (blockSize)
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
        old_key[..blockSize].CopyTo(new_key);
        ShiftLeft(new_key, 1);
        if ((old_key[0] & 0x80) != 0)
        {
            for (var i = 0; i < RB.Length; ++i)
            {
                new_key[blockSize - RB.Length + i] ^= RB[i];
            }
        }
    }
}