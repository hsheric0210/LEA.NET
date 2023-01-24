using static LEA.Utils.Xor;
using static LEA.Utils.Shifting;

namespace LEA.Mac;

public class CMac : MacBase
{
    private static readonly byte[] R256 = new[] { (byte)0x04, (byte)0x25 };
    private static readonly byte[] R128 = new[] { (byte)0x87 };
    private static readonly byte[] R64 = new[] { (byte)0x1b };

    private readonly BlockCipher engine;

    private int blockSizeBytes;
    private int blockIndex;
    private byte[] block;
    private byte[] mac;
    private byte[] RB;
    private byte[] k1, k2;

    public CMac(BlockCipher cipher) => engine = cipher;

    public override void Init(ReadOnlySpan<byte> key)
    {
        engine.Init(Mode.Encrypt, key);
        blockIndex = 0;
        blockSizeBytes = engine.BlockSizeBytes;
        block = new byte[blockSizeBytes];
        mac = new byte[blockSizeBytes];
        k1 = new byte[blockSizeBytes];
        k2 = new byte[blockSizeBytes];
        SelectRB();
        var zero = new byte[blockSizeBytes];
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

    public override void Update(ReadOnlySpan<byte> message)
    {
        if (message.Length == 0)
            return;

        var length = message.Length;
        var messageOffset = 0;
        var gap = blockSizeBytes - blockIndex;
        if (length > gap)
        {
            message.Slice(messageOffset, gap).CopyTo(block.AsSpan()[blockIndex..]);
            blockIndex = 0;
            length -= gap;
            messageOffset += gap;
            while (length > blockSizeBytes)
            {
                XOR(block, mac);
                engine.ProcessBlock(block, 0, mac, 0);
                message.Slice(messageOffset, blockSizeBytes).CopyTo(block);
                length -= blockSizeBytes;
                messageOffset += blockSizeBytes;
            }

            if (length > 0)
            {
                XOR(block, mac);
                engine.ProcessBlock(block, 0, mac, 0);
            }
        }

        if (length > 0)
        {
            message.Slice(messageOffset, length).CopyTo(block.AsSpan()[blockIndex..]);
            blockIndex += length;
        }
    }

    public override ReadOnlySpan<byte> DoFinal(ReadOnlySpan<byte> message)
    {
        Update(message);
        return DoFinal();
    }

    public override ReadOnlySpan<byte> DoFinal()
    {
        if (blockIndex < blockSizeBytes)
        {
            block[blockIndex] = 0x80;
            block.AsSpan()[(blockIndex + 1)..blockSizeBytes].Fill(0x00);
        }

        XOR(block, blockIndex == blockSizeBytes ? k1 : k2);
        XOR(block, mac);
        engine.ProcessBlock(block, 0, mac, 0);

        return mac.ToArray(); // copy
    }

    private void SelectRB()
    {
        switch (blockSizeBytes)
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
        old_key[..blockSizeBytes].CopyTo(new_key);
        ShiftLeft(new_key, 1);
        if ((old_key[0] & 0x80) != 0)
        {
            for (var i = 0; i < RB.Length; ++i)
            {
                new_key[blockSizeBytes - RB.Length + i] ^= RB[i];
            }
        }
    }
}