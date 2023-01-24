namespace LEA;

public abstract class BlockCipherModeAE
{
    protected Mode mode;
    protected BlockCipher engine;
    protected byte[] buffer;
    protected int bufferOffset;
    protected int blockSize;
    protected abstract int TagLength { get; set; }

    protected BlockCipherModeAE(BlockCipher cipher)
    {
        engine = cipher;
        blockSize = engine.BlockSizeBytes;
        buffer = new byte[blockSize];
    }

    public abstract void Init(Mode mode, ReadOnlySpan<byte> key, ReadOnlySpan<byte> nonce, int tagLength);

    public abstract void UpdateAssociatedData(ReadOnlySpan<byte> aad);

    public abstract ReadOnlySpan<byte> Update(ReadOnlySpan<byte> message);

    public abstract ReadOnlySpan<byte> DoFinal();

    public abstract int GetOutputSize(int length);

    public virtual ReadOnlySpan<byte> DoFinal(ReadOnlySpan<byte> message)
    {
        if (mode == Mode.Encrypt)
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