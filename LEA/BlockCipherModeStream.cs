namespace LEA;

public abstract class BlockCipherModeStream : BlockCipherModeCore
{
    public override PaddingBase? Padding
    {
        protected get => null;
        set => _ = value; // Stream cipher mode doesn't require padding
    }

    protected BlockCipherModeStream(BlockCipher cipher) : base(cipher)
    {
    }

    public override int GetOutputSize(int length) => length + BlockBufferOffset;

    public override int GetUpdateOutputSize(int length) => length + BlockBufferOffset & BlockMask;

    public override void Init(Mode mode, ReadOnlySpan<byte> key) => throw new InvalidOperationException("This init method is not applicable to " + GetAlgorithmName());

    public override void Init(Mode mode, ReadOnlySpan<byte> key, ReadOnlySpan<byte> iv) => throw new InvalidOperationException("This init method is not applicable to " + GetAlgorithmName());

    public override void Reset()
    {
        BlockBufferOffset = 0;
        Array.Fill(BlockBuffer, (byte)0);
    }

    public override ReadOnlySpan<byte> Update(ReadOnlySpan<byte> message)
    {
        var length = message.Length;
        var gap = BlockBuffer.Length - BlockBufferOffset;
        var inOffset = 0;
        var outOffset = 0;
        var output = new byte[GetUpdateOutputSize(length)];
        if (length >= gap)
        {
            message.Slice(inOffset, gap).CopyTo(BlockBuffer.AsSpan()[BlockBufferOffset..]);
            outOffset += ProcessBlock(BlockBuffer, 0, output, outOffset);
            BlockBufferOffset = 0;
            length -= gap;
            inOffset += gap;
            while (length >= BlockBuffer.Length)
            {
                outOffset += ProcessBlock(message, inOffset, output, outOffset);
                length -= BlockSizeBytes;
                inOffset += BlockSizeBytes;
            }
        }

        if (length > 0)
        {
            message.Slice(inOffset, length).CopyTo(BlockBuffer.AsSpan()[BlockBufferOffset..]);
            BlockBufferOffset += length;
        }

        return output;
    }

    public override ReadOnlySpan<byte> DoFinal()
    {
        if (BlockBufferOffset == 0)
            return Array.Empty<byte>();

        var output = new byte[BlockBufferOffset];
        ProcessBlock(BlockBuffer, 0, output, 0, BlockBufferOffset);
        return output;
    }
}