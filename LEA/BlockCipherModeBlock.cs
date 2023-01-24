namespace LEA;

public abstract class BlockCipherModeBlock : BlockCipherModeCore
{
    public override PaddingBase? Padding { protected get; set; }

    protected BlockCipherModeBlock(BlockCipher cipher) : base(cipher)
    {
    }

    public override int GetOutputSize(int length) => Mode == Mode.Encrypt && Padding != null ? (length + BlockBufferOffset & BlockMask) + BlockSizeBytes : length;

    public override int GetUpdateOutputSize(int length) => length + BlockBufferOffset - (Mode == Mode.Decrypt && Padding != null ? BlockSizeBytes : 0) & BlockMask;

    public override void Init(Mode mode, ReadOnlySpan<byte> key) => throw new InvalidOperationException("This init method is not applicable to " + GetAlgorithmName());

    public override void Init(Mode mode, ReadOnlySpan<byte> key, ReadOnlySpan<byte> iv) => throw new InvalidOperationException("This init method is not applicable to " + GetAlgorithmName());

    public override void Reset()
    {
        BlockBufferOffset = 0;
        Array.Fill(BlockBuffer, (byte)0);
    }

    public override ReadOnlySpan<byte> Update(ReadOnlySpan<byte> message)
    {
        if (Padding != null && Mode == Mode.Decrypt)
        {
            return DecryptWithPadding(message);
        }

        var length = message.Length;
        var gap = BlockBuffer.Length - BlockBufferOffset;
        var inOffset = 0;
        var outOffset = 0;
        Span<byte> output = new byte[GetUpdateOutputSize(length)];
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
        if (Padding != null)
            return DoFinalWithPadding(Padding);

        if (BlockBufferOffset == 0)
            return null;
        else if (BlockBufferOffset != BlockSizeBytes)
            throw new InvalidOperationException("Bad padding");

        Span<byte> output = new byte[BlockSizeBytes];
        ProcessBlock(BlockBuffer, 0, output, 0, BlockSizeBytes);
        return output;
    }

    /// <summary>
    /// 패딩 사용시 복호화 처리, 마지막 블록을 위해 데이터를 남겨둠
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    private ReadOnlySpan<byte> DecryptWithPadding(ReadOnlySpan<byte> message)
    {
        var length = message.Length;
        var gap = BlockBuffer.Length - BlockBufferOffset;
        var inOffset = 0;
        var outOffset = 0;
        Span<byte> output = new byte[GetUpdateOutputSize(length)];
        if (length > gap)
        {
            message.Slice(inOffset, gap).CopyTo(BlockBuffer.AsSpan()[BlockBufferOffset..]);
            outOffset += ProcessBlock(BlockBuffer, 0, output, outOffset);
            BlockBufferOffset = 0;
            length -= gap;
            inOffset += gap;
            while (length > BlockBuffer.Length)
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
            //length = 0;
        }

        return output;
    }

    /// <summary>
    /// 패딩 사용시 마지막 블록 처리
    /// </summary>
    /// <returns></returns>
    private ReadOnlySpan<byte> DoFinalWithPadding(PaddingBase padding)
    {
        if (Mode == Mode.Encrypt)
        {
            padding.Pad(BlockBuffer, BlockBufferOffset);
            Span<byte> output = new byte[GetOutputSize(0)];
            ProcessBlock(BlockBuffer, 0, output, 0);
            return output;
        }
        else
        {
            var block = new byte[BlockSizeBytes];
            ProcessBlock(BlockBuffer, 0, block, 0);
            return padding.Unpad(block);
        }
    }
}