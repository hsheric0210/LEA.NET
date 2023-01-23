namespace LEA.Padding;

public class Pkcs5Padding : PaddingBase
{
    public Pkcs5Padding(int blocksize) : base(blocksize)
    {
    }

    public override ReadOnlySpan<byte> Pad(ReadOnlySpan<byte> input)
    {
        if (input == null)
            throw new ArgumentNullException(nameof(input));

        if (input.Length > blockSize)
            throw new InvalidOperationException("input should be shorter than blocksize");

        Span<byte> output = new byte[blockSize];
        input.CopyTo(output);
        Pad(output, input.Length);
        return output;
    }

    public override void Pad(Span<byte> bytes, int offset)
    {
        if (bytes.Length < offset)
            throw new ArgumentException("Index out of bounds: " + nameof(bytes));

        var code = (byte)(bytes.Length - offset);
        bytes[offset..].Fill(code);
    }

    public override ReadOnlySpan<byte> Unpad(ReadOnlySpan<byte> input)
    {
        if (input.Length < 1)
            throw new ArgumentException("Empty array: " + nameof(input));
        if (input.Length % blockSize != 0)
            throw new ArgumentException("Bad padding"); // FIXME: Padding oracle attack

        var cnt = input.Length - GetPadCount(input);
        if (cnt == 0)
            return Array.Empty<byte>();

        Span<byte> output = new byte[cnt];
        input[..cnt].CopyTo(output);
        return output;
    }

    public override int GetPadCount(ReadOnlySpan<byte> input)
    {
        if (input == null)
            throw new ArgumentNullException(nameof(input));

        if (input.Length < 1)
            throw new ArgumentException("Empty array: " + nameof(input));

        if (input.Length % blockSize != 0)
            throw new ArgumentException("Bad padding");

        var count = input[input.Length - 1] & 0xff;
        var isBadPadding = false;
        var lower_bound = input.Length - count;
        for (var i = input.Length - 1; i > lower_bound; --i)
        {
            if (input[i] != count)
                isBadPadding = true;
        }

        if (isBadPadding)
            throw new InvalidOperationException("Bad Padding"); // FIXME: Padding oracle attack

        return count;
    }
}