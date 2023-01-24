namespace LEA.Padding;

public class Pkcs5Padding : PaddingBase
{
    public Pkcs5Padding(int blockSize) : base(blockSize)
    {
    }

    public override ReadOnlySpan<byte> Pad(ReadOnlySpan<byte> unpadded)
    {
        if (unpadded.Length > BlockSize)
            throw new InvalidOperationException("input should be shorter than blocksize");

        Span<byte> output = new byte[BlockSize];
        unpadded.CopyTo(output);
        Pad(output, unpadded.Length);
        return output;
    }

    public override void Pad(Span<byte> unpadded, int offset)
    {
        if (unpadded.Length < offset)
            throw new ArgumentException("Index out of bounds: " + nameof(unpadded));

        var code = (byte)(unpadded.Length - offset);
        unpadded[offset..].Fill(code);
    }

    public override ReadOnlySpan<byte> Unpad(ReadOnlySpan<byte> padded)
    {
        if (padded.Length < 1)
            throw new ArgumentException("Empty array: " + nameof(padded));
        if (padded.Length % BlockSize != 0)
            throw new ArgumentException("Bad padding"); // FIXME: Padding oracle attack

        var paddingCount = padded.Length - GetPadCount(padded);
        return padded[..paddingCount].ToArray();
    }

    public override int GetPadCount(ReadOnlySpan<byte> padded)
    {
        if (padded.Length < 1)
            throw new ArgumentException("Empty array: " + nameof(padded));

        if (padded.Length % BlockSize != 0)
            throw new ArgumentException("Bad padding");

        var count = padded[padded.Length - 1] & 0xff;
        var isBadPadding = false;
        var lower_bound = padded.Length - count;
        for (var i = padded.Length - 1; i > lower_bound; --i)
        {
            if (padded[i] != count)
                isBadPadding = true;
        }

        if (isBadPadding)
            throw new InvalidOperationException("Bad Padding"); // FIXME: Padding oracle attack

        return count;
    }
}