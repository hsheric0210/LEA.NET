namespace LEA;

public abstract class BlockCipherModeStream : BlockCipherModeImpl
{
    public BlockCipherModeStream(BlockCipher cipher) : base(cipher)
    {
    }

    public override int GetOutputSize(int length) => length + bufferOffset;

    public override int GetUpdateOutputSize(int length) => length + bufferOffset & blockMask;

    public override void Init(OperatingMode mode, ReadOnlySpan<byte> key) => throw new InvalidOperationException("This init method is not applicable to " + GetAlgorithmName());

    public override void Init(OperatingMode mode, ReadOnlySpan<byte> key, ReadOnlySpan<byte> iv) => throw new InvalidOperationException("This init method is not applicable to " + GetAlgorithmName());

    public override void Reset()
    {
        bufferOffset = 0;
        Array.Fill(buffer, (byte)0);
    }

    public override void SetPadding(PaddingBase padding)
    {
    }

    public override ReadOnlySpan<byte> Update(ReadOnlySpan<byte> bytes)
    {
        if (bytes == null)
            return Array.Empty<byte>();

        var length = bytes.Length;
        var gap = buffer.Length - bufferOffset;
        var inOffset = 0;
        var outOffset = 0;
        var output = new byte[GetUpdateOutputSize(length)];
        if (length >= gap)
        {
            bytes.Slice(inOffset, gap).CopyTo(buffer[bufferOffset..]);
            //Array.Copy(bytes, inOffset, buffer, bufferOffset, gap);
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
            bytes.Slice(inOffset, length).CopyTo(buffer[bufferOffset..]);
            //Array.Copy(bytes, inOffset, buffer, bufferOffset, length);
            bufferOffset += length;
            //len = 0;
        }

        return output;
    }

    public override ReadOnlySpan<byte> DoFinal()
    {
        if (bufferOffset == 0)
            return Array.Empty<byte>();

        var output = new byte[bufferOffset];
        ProcessBlock(buffer, 0, output, 0, bufferOffset);
        return output;
    }
}