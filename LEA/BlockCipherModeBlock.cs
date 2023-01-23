namespace LEA;

public abstract class BlockCipherModeBlock : BlockCipherModeImpl
{
    protected PaddingBase? padding;
    protected BlockCipherModeBlock(BlockCipher cipher) : base(cipher)
    {
    }

    public override int GetOutputSize(int length)
    {

        var size = (length + bufferOffset & blockMask) + blockSize;
        if (mode == OperatingMode.Encrypt)
        {
            return padding != null ? size : length;
        }

        return length;
    }

    public override int GetUpdateOutputSize(int length)
    {
        if (mode == OperatingMode.Decrypt && padding != null)
        {
            return length + bufferOffset - blockSize & blockMask;
        }

        return length + bufferOffset & blockMask;
    }

    public override void Init(OperatingMode mode, ReadOnlySpan<byte> key) => throw new InvalidOperationException("This init method is not applicable to " + GetAlgorithmName());

    public override void Init(OperatingMode mode, ReadOnlySpan<byte> key, ReadOnlySpan<byte> iv) => throw new InvalidOperationException("This init method is not applicable to " + GetAlgorithmName());

    public override void Reset()
    {
        bufferOffset = 0;
        Array.Fill(buffer, (byte)0);
    }

    public override void SetPadding(PaddingBase padding) => this.padding = padding;

    public override ReadOnlySpan<byte> Update(ReadOnlySpan<byte> bytes)
    {
        if (padding != null && mode == OperatingMode.Decrypt)
        {
            return DecryptWithPadding(bytes);
        }

        var length = bytes.Length;
        var gap = buffer.Length - bufferOffset;
        var inOffset = 0;
        var outOffset = 0;
        Span<byte> output = new byte[GetUpdateOutputSize(length)];
        if (length >= gap)
        {
            bytes.Slice(inOffset, gap).CopyTo(buffer.AsSpan()[bufferOffset..]);
            outOffset += ProcessBlock(buffer, 0, output, outOffset);
            bufferOffset = 0;
            length -= gap;
            inOffset += gap;
            while (length >= buffer.Length)
            {
                outOffset += ProcessBlock(bytes, inOffset, output, outOffset);
                length -= blockSize;
                inOffset += blockSize;
            }
        }

        if (length > 0)
        {
            bytes.Slice(inOffset, length).CopyTo(buffer.AsSpan()[bufferOffset..]);
            bufferOffset += length;
        }

        return output;
    }

    public override ReadOnlySpan<byte> DoFinal()
    {
        if (padding != null)
        {
            return DoFinalWithPadding(padding);
        }

        if (bufferOffset == 0)
        {
            return null;
        }
        else if (bufferOffset != blockSize)
        {
            throw new InvalidOperationException("Bad padding");
        }

        Span<byte> output = new byte[blockSize];
        ProcessBlock(buffer, 0, output, 0, blockSize);
        return output;
    }

    /// <summary>
    /// 패딩 사용시 복호화 처리, 마지막 블록을 위해 데이터를 남겨둠
    /// </summary>
    /// <param name="msg"></param>
    /// <returns></returns>
    private ReadOnlySpan<byte> DecryptWithPadding(ReadOnlySpan<byte> msg)
    {
        if (msg == null)
            return Array.Empty<byte>();

        var length = msg.Length;
        var gap = buffer.Length - bufferOffset;
        var inOffset = 0;
        var outOffset = 0;
        Span<byte> output = new byte[GetUpdateOutputSize(length)];
        if (length > gap)
        {
            msg.Slice(inOffset, gap).CopyTo(buffer.AsSpan()[bufferOffset..]);
            outOffset += ProcessBlock(buffer, 0, output, outOffset);
            bufferOffset = 0;
            length -= gap;
            inOffset += gap;
            while (length > buffer.Length)
            {
                outOffset += ProcessBlock(msg, inOffset, output, outOffset);
                length -= blockSize;
                inOffset += blockSize;
            }
        }

        if (length > 0)
        {
            msg.Slice(inOffset, length).CopyTo(buffer.AsSpan()[bufferOffset..]);
            bufferOffset += length;
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
        if (mode == OperatingMode.Encrypt)
        {
            padding.Pad(buffer, bufferOffset);
            Span<byte> output = new byte[GetOutputSize(0)];
            ProcessBlock(buffer, 0, output, 0);
            return output;
        }
        else
        {
            var block = new byte[blockSize];
            ProcessBlock(buffer, 0, block, 0);
            return padding.Unpad(block);
        }
    }
}