namespace LEA;

public abstract class BlockCipherModeImpl : BlockCipherMode
{
    protected OperatingMode mode;
    protected BlockCipher engine;
    protected byte[] buffer;
    protected int bufferOffset;
    protected int blockSize;
    protected int blockMask;

    protected BlockCipherModeImpl(BlockCipher cipher)
    {
        engine = cipher;
        blockSize = engine.GetBlockSize();
        blockMask = GetBlockmask(blockSize);
        buffer = new byte[blockSize];
    }

    public override ReadOnlySpan<byte> DoFinal(ReadOnlySpan<byte> bytes)
    {
        ReadOnlySpan<byte> part1 = Update(bytes);
        ReadOnlySpan<byte> part2 = DoFinal();
        var output = new byte[part1.Length + part2.Length];
        if (part1.Length > 0)
            part1.CopyTo(output);
        if (part2.Length > 0)
            part2.CopyTo(output.AsSpan()[part1.Length..]);
        return output;
    }

    protected abstract int ProcessBlock(ReadOnlySpan<byte> input, int inOffset, Span<byte> output, int outOffset, int length);
    protected virtual int ProcessBlock(ReadOnlySpan<byte> input, int inOffset, Span<byte> output, int outOffset) => ProcessBlock(input, inOffset, output, outOffset, blockSize);

    protected static int GetBlockmask(int blocksize)
    {
        return unchecked((int)(blocksize switch
        {
            8 => 0xfffffff7u,
            16 => 0xfffffff0u,
            32 => 0xffffffe0u,
            _ => 0u,
        }));
    }
}