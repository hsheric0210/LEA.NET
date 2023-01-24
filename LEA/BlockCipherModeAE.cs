namespace LEA;

public abstract class BlockCipherModeAE
{
    protected OperatingMode mode;
    protected BlockCipher engine;
    protected byte[] buffer;
    protected byte[] nonce;
    protected int bufferOffset;
    protected int blockSize;
    protected int tagLength;

    public BlockCipherModeAE(BlockCipher cipher)
    {
        engine = cipher;
        blockSize = engine.GetBlockSize();
        buffer = new byte[blockSize];
    }

    public abstract void Init(OperatingMode mode, ReadOnlySpan<byte> key, ReadOnlySpan<byte> nonce, int tagLength);

    public abstract void UpdateAAD(ReadOnlySpan<byte> input);

    public abstract ReadOnlySpan<byte> Update(ReadOnlySpan<byte> message);

    public abstract ReadOnlySpan<byte> DoFinal();

    public abstract int GetOutputSize(int length);

    public virtual ReadOnlySpan<byte> DoFinal(ReadOnlySpan<byte> message)
    {
        if (mode == OperatingMode.Encrypt)
        {
            ReadOnlySpan<byte> part1 = Update(message);
            ReadOnlySpan<byte> part2 = DoFinal();
            var len1 = part1.Length;
            var len2 = part2.Length;
            Span<byte> output = new byte[len1 + len2];
            part1.CopyTo(output);
            part2.CopyTo(output[len1..]);
            return output;
        }
        else
        {
            Update(message);
            return DoFinal();
        }
    }
}