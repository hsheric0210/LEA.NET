using static LEA.Utils.PackUnpack;

namespace LEA.Engine;

public class LeaEngine : BlockCipher
{
    private const int BlockSize = 16;
    private static readonly uint[] Delta = new uint[]
    {
        0xc3efe9db,
        0x44626b02,
        0x79e27c8a,
        0x78df30ec,
        0x715ea49e,
        0xc785da0a,
        0xe04ef22a,
        0xe5c40957
    };

    private Mode mode;
    private int rounds;

    protected uint[,] roundKeys;
    private uint[] block;

    public LeaEngine() => block = new uint[BlockSize / 4];

    public override void Init(Mode mode, ReadOnlySpan<byte> key)
    {
        this.mode = mode;
        GenerateRoundKeys(key);
    }

    public override void Reset() => Array.Fill(block, 0u);

    public override string GetAlgorithmName() => "LEA";

    public override int GetBlockSize() => BlockSize;

    public override int ProcessBlock(ReadOnlySpan<byte> inBlock, int inOffset, Span<byte> outBlock, int outOffset)
    {
        if (inBlock.Length - inOffset < BlockSize)
            throw new InvalidOperationException("too short input data " + inBlock.Length + " " + inOffset);

        if (outBlock.Length - outOffset < BlockSize)
            throw new InvalidOperationException("too short output buffer " + outBlock.Length + " / " + outOffset);

        return mode == Mode.Encrypt
            ? EncryptBlock(inBlock, inOffset, outBlock, outOffset)
            : DecryptBlock(inBlock, inOffset, outBlock, outOffset);
    }

    private int EncryptBlock(ReadOnlySpan<byte> inBlock, int inOffset, Span<byte> outBlock, int outOffset)
    {
        Pack(inBlock, inOffset, block, 0, 16);
        for (var i = 0; i < rounds; ++i)
        {
            block[3] = ROR((block[2] ^ roundKeys[i, 4]) + (block[3] ^ roundKeys[i, 5]), 3);
            block[2] = ROR((block[1] ^ roundKeys[i, 2]) + (block[2] ^ roundKeys[i, 3]), 5);
            block[1] = ROL((block[0] ^ roundKeys[i, 0]) + (block[1] ^ roundKeys[i, 1]), 9);
            ++i;
            block[0] = ROR((block[3] ^ roundKeys[i, 4]) + (block[0] ^ roundKeys[i, 5]), 3);
            block[3] = ROR((block[2] ^ roundKeys[i, 2]) + (block[3] ^ roundKeys[i, 3]), 5);
            block[2] = ROL((block[1] ^ roundKeys[i, 0]) + (block[2] ^ roundKeys[i, 1]), 9);
            ++i;
            block[1] = ROR((block[0] ^ roundKeys[i, 4]) + (block[1] ^ roundKeys[i, 5]), 3);
            block[0] = ROR((block[3] ^ roundKeys[i, 2]) + (block[0] ^ roundKeys[i, 3]), 5);
            block[3] = ROL((block[2] ^ roundKeys[i, 0]) + (block[3] ^ roundKeys[i, 1]), 9);
            ++i;
            block[2] = ROR((block[1] ^ roundKeys[i, 4]) + (block[2] ^ roundKeys[i, 5]), 3);
            block[1] = ROR((block[0] ^ roundKeys[i, 2]) + (block[1] ^ roundKeys[i, 3]), 5);
            block[0] = ROL((block[3] ^ roundKeys[i, 0]) + (block[0] ^ roundKeys[i, 1]), 9);
        }

        Unpack(block, 0, outBlock, outOffset, 4);
        return BlockSize;
    }

    private int DecryptBlock(ReadOnlySpan<byte> inBlock, int inOffset, Span<byte> outBlock, int outOffset)
    {
        Pack(inBlock, inOffset, block, 0, 16);
        for (var i = rounds - 1; i >= 0; --i)
        {
            block[0] = ROR(block[0], 9) - (block[3] ^ roundKeys[i, 0]) ^ roundKeys[i, 1];
            block[1] = ROL(block[1], 5) - (block[0] ^ roundKeys[i, 2]) ^ roundKeys[i, 3];
            block[2] = ROL(block[2], 3) - (block[1] ^ roundKeys[i, 4]) ^ roundKeys[i, 5];
            --i;
            block[3] = ROR(block[3], 9) - (block[2] ^ roundKeys[i, 0]) ^ roundKeys[i, 1];
            block[0] = ROL(block[0], 5) - (block[3] ^ roundKeys[i, 2]) ^ roundKeys[i, 3];
            block[1] = ROL(block[1], 3) - (block[0] ^ roundKeys[i, 4]) ^ roundKeys[i, 5];
            --i;
            block[2] = ROR(block[2], 9) - (block[1] ^ roundKeys[i, 0]) ^ roundKeys[i, 1];
            block[3] = ROL(block[3], 5) - (block[2] ^ roundKeys[i, 2]) ^ roundKeys[i, 3];
            block[0] = ROL(block[0], 3) - (block[3] ^ roundKeys[i, 4]) ^ roundKeys[i, 5];
            --i;
            block[1] = ROR(block[1], 9) - (block[0] ^ roundKeys[i, 0]) ^ roundKeys[i, 1];
            block[2] = ROL(block[2], 5) - (block[1] ^ roundKeys[i, 2]) ^ roundKeys[i, 3];
            block[3] = ROL(block[3], 3) - (block[2] ^ roundKeys[i, 4]) ^ roundKeys[i, 5];
        }

        Unpack(block, 0, outBlock, outOffset, 4);
        return BlockSize;
    }

    private void GenerateRoundKeys(ReadOnlySpan<byte> key)
    {
        if (key.Length is not 16 and not 24 and not 32)
            throw new ArgumentException("Illegal key length: " + key.Length);

        var T = new uint[8];
        rounds = (key.Length >> 1) + 16;
        roundKeys = new uint[rounds, 6];
        Pack(key, 0, T, 0, 16);
        if (key.Length > 16)
            Pack(key, 16, T, 4, 8);

        if (key.Length > 24)
            Pack(key, 24, T, 6, 8);

        if (key.Length == 16)
        {
            for (var i = 0; i < 24; ++i)
            {
                var temp = ROL(Delta[i & 3], i);
                roundKeys[i, 0] = T[0] = ROL(T[0] + ROL(temp, 0), 1);
                roundKeys[i, 1] = roundKeys[i, 3] = roundKeys[i, 5] = T[1] = ROL(T[1] + ROL(temp, 1), 3);
                roundKeys[i, 2] = T[2] = ROL(T[2] + ROL(temp, 2), 6);
                roundKeys[i, 4] = T[3] = ROL(T[3] + ROL(temp, 3), 11);
            }
        }
        else if (key.Length == 24)
        {
            for (var i = 0; i < 28; ++i)
            {
                var temp = ROL(Delta[i % 6], i);
                roundKeys[i, 0] = T[0] = ROL(T[0] + ROL(temp, 0), 1);
                roundKeys[i, 1] = T[1] = ROL(T[1] + ROL(temp, 1), 3);
                roundKeys[i, 2] = T[2] = ROL(T[2] + ROL(temp, 2), 6);
                roundKeys[i, 3] = T[3] = ROL(T[3] + ROL(temp, 3), 11);
                roundKeys[i, 4] = T[4] = ROL(T[4] + ROL(temp, 4), 13);
                roundKeys[i, 5] = T[5] = ROL(T[5] + ROL(temp, 5), 17);
            }
        }
        else
        {
            for (var i = 0; i < 32; ++i)
            {
                var temp = ROL(Delta[i & 7], i & 0x1f);
                roundKeys[i, 0] = T[6 * i + 0 & 7] = ROL(T[6 * i + 0 & 7] + temp, 1);
                roundKeys[i, 1] = T[6 * i + 1 & 7] = ROL(T[6 * i + 1 & 7] + ROL(temp, 1), 3);
                roundKeys[i, 2] = T[6 * i + 2 & 7] = ROL(T[6 * i + 2 & 7] + ROL(temp, 2), 6);
                roundKeys[i, 3] = T[6 * i + 3 & 7] = ROL(T[6 * i + 3 & 7] + ROL(temp, 3), 11);
                roundKeys[i, 4] = T[6 * i + 4 & 7] = ROL(T[6 * i + 4 & 7] + ROL(temp, 4), 13);
                roundKeys[i, 5] = T[6 * i + 5 & 7] = ROL(T[6 * i + 5 & 7] + ROL(temp, 5), 17);
            }
        }
    }

    private static uint ROL(uint state, int num) => state << num | state >> 32 - num;

    private static uint ROR(uint state, int num) => state >> num | state << 32 - num;
}