using System.Security.Cryptography;

namespace LEA.Padding;

public class Iso10126Padding : PaddingBase
{
    public Iso10126Padding(int blockSizeBytes) : base(blockSizeBytes)
    {
    }

    public override ReadOnlySpan<byte> Pad(ReadOnlySpan<byte> unpadded)
    {
        if (unpadded.Length > BlockSizeBytes)
            throw new ArgumentException("input should be shorter than blocksize");

        Span<byte> output = new byte[BlockSizeBytes];
        unpadded.CopyTo(output);
        Pad(output, unpadded.Length);
        return output;
    }

    public override void Pad(Span<byte> unpadded, int offset)
    {
        if (unpadded.Length < offset)
            throw new ArgumentException("Index out of bounds: " + nameof(unpadded));

        var code = (byte)(unpadded.Length - offset);
        if (unpadded.Length > offset)
        {
            RandomNumberGenerator.Fill(unpadded[offset..^1]);
            unpadded[^1] = code;
        }
    }

    public override ReadOnlySpan<byte> Unpad(ReadOnlySpan<byte> padded)
    {
        if (padded.Length < 1)
            throw new ArgumentException("Empty array: " + nameof(padded));
        if (padded.Length % BlockSizeBytes != 0)
            throw new ArgumentException("Bad padding"); // FIXME: Padding oracle attack

        var paddingCount = padded.Length - GetPadCount(padded);
        if (paddingCount < 0)
            throw new ArgumentException("Bad padding");
        return padded[..paddingCount].ToArray();
    }

    public override int GetPadCount(ReadOnlySpan<byte> padded)
    {
        if (padded.Length < 1)
            throw new ArgumentException("Empty array: " + nameof(padded));

        if (padded.Length % BlockSizeBytes != 0)
            throw new ArgumentException("Bad padding");
        return padded[padded.Length - 1] & 0xff;
    }
}