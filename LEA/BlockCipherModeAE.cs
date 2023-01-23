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

    public abstract void Init(OperatingMode mode, ReadOnlySpan<byte> mk, ReadOnlySpan<byte> nonce, int taglen);

    public abstract void UpdateAAD(ReadOnlySpan<byte> aad);

    public abstract ReadOnlySpan<byte> Update(ReadOnlySpan<byte> msg);

    public abstract ReadOnlySpan<byte> DoFinal();

    public abstract int GetOutputSize(int len);

    public virtual ReadOnlySpan<byte> DoFinal(ReadOnlySpan<byte> msg)
    {
        if (mode == OperatingMode.Encrypt)
        {
            ReadOnlySpan<byte> part1 = Update(msg);
            ReadOnlySpan<byte> part2 = DoFinal();
            var len1 = part1.Length;
            var len2 = part2.Length;
            Span<byte> output = new byte[len1 + len2];
            part1[..len1].CopyTo(output);
            part2[..len2].CopyTo(output);
            return output;
        }
        else
        {
            Update(msg);
            return DoFinal();
        }
    }
}