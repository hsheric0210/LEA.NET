namespace LEA.Utils;
public static class Shifting
{
    public static void ShiftLeft(Span<byte> bytes, int shift)
    {
        if (shift is < 1 or > 7)
            throw new ArgumentException("the allowed shift amount is 1~7");

        int tmp = bytes[0];
        for (var i = 1; i < bytes.Length; ++i)
        {
            tmp = tmp << 8 | bytes[i] & 0xff;
            bytes[i - 1] = (byte)((tmp << shift & 0xff00) >> 8);
        }

        bytes[bytes.Length - 1] <<= shift;
    }

    public static void ShiftRight(Span<byte> bytes, int shift)
    {
        if (shift is < 1 or > 7)
            throw new ArgumentException("the allowed shift amount is 1~7");

        int tmp;
        for (var i = bytes.Length - 1; i > 0; --i)
        {
            tmp = bytes[i - 1] << 8 | bytes[i] & 0xff;
            bytes[i] = (byte)(tmp >> shift);
        }

        tmp = bytes[0] & 0xff;
        bytes[0] = (byte)(tmp >> shift);
    }
}
