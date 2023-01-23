namespace LEA.Utils;

public static class Xor
{
    public static void XOR(Span<byte> lhs, ReadOnlySpan<byte> rhs)
    {
        if (lhs.Length != rhs.Length)
            throw new ArgumentException("the length of two arrays should be same");

        for (var i = 0; i < lhs.Length; ++i)
            lhs[i] ^= rhs[i];
    }

    public static void XOR(Span<byte> lhs, int lhsOffset, ReadOnlySpan<byte> rhs, int rhsOffset, int length)
    {
        if (lhs.Length < lhsOffset + length)
            throw new ArgumentException("Index out of bounds: " + nameof(lhs));
        if (rhs.Length < rhsOffset + length)
            throw new ArgumentException("Index out of bounds: " + nameof(rhs));

        for (var i = 0; i < length; ++i)
            lhs[lhsOffset + i] ^= rhs[rhsOffset + i];
    }

    /// <summary>
    /// lhs = rhs1 ^ rhs2
    /// </summary>
    public static void XOR(Span<byte> lhs, ReadOnlySpan<byte> rhs1, ReadOnlySpan<byte> rhs2)
    {
        if (lhs.Length != rhs1.Length || lhs.Length != rhs2.Length)
            throw new ArgumentException("the length of arrays should be same");

        for (var i = 0; i < lhs.Length; ++i)
            lhs[i] = (byte)(rhs1[i] ^ rhs2[i]);
    }

    public static void XOR(Span<byte> output, int outOffset, ReadOnlySpan<byte> lhs, int lhsOffset, ReadOnlySpan<byte> rhs, int rhsOffset, int length)
    {
        if (output.Length < outOffset + length)
            throw new ArgumentException("Index out of bounds: " + nameof(output));
        if (lhs.Length < lhsOffset + length)
            throw new ArgumentException("Index out of bounds: " + nameof(lhs));
        if (rhs.Length < rhsOffset + length)
            throw new ArgumentException("Index out of bounds: " + nameof(rhs));

        for (var i = 0; i < length; ++i)
        {
            output[outOffset + i] = (byte)(lhs[lhsOffset + i] ^ rhs[rhsOffset + i]);
        }
    }

    /// <summary>
    /// lhs ^= rhs
    /// </summary>
    public static void XOR(Span<int> lhs, ReadOnlySpan<int> rhs)
    {
        if (lhs.Length != rhs.Length)
            throw new ArgumentException("the length of two arrays should be same");

        for (var i = 0; i < lhs.Length; ++i)
            lhs[i] ^= rhs[i];
    }

    public static void XOR(Span<int> lhs, int lhsOffset, ReadOnlySpan<int> rhs, int rhsOffset, int length)
    {
        if (lhs.Length < lhsOffset + length)
            throw new ArgumentException("Index out of bounds: " + nameof(lhs));
        if (rhs.Length < rhsOffset + length)
            throw new ArgumentException("Index out of bounds: " + nameof(rhs));

        for (var i = 0; i < length; ++i)
            lhs[lhsOffset + i] ^= rhs[rhsOffset + i];
    }

    /// <summary>
    /// lhs = rhs1 ^ rhs2
    /// </summary>
    public static void XOR(Span<int> lhs, ReadOnlySpan<int> rhs1, ReadOnlySpan<int> rhs2)
    {
        if (lhs.Length != rhs1.Length || lhs.Length != rhs2.Length)
            throw new ArgumentException("the length of arrays should be same");

        for (var i = 0; i < lhs.Length; ++i)
            lhs[i] = rhs1[i] ^ rhs2[i];
    }

    public static void XOR(Span<int> lhs, int lhsOffset, ReadOnlySpan<int> rhs1, int rhs1Offset, ReadOnlySpan<int> rhs2, int rhs2Offset, int length)
    {
        if (lhs.Length < lhsOffset + length)
            throw new ArgumentException("Index out of bounds: " + nameof(lhs));
        if (rhs1.Length < rhs1Offset + length)
            throw new ArgumentException("Index out of bounds: " + nameof(rhs1));
        if (rhs2.Length < rhs2Offset + length)
            throw new ArgumentException("Index out of bounds: " + nameof(rhs2));

        for (var i = 0; i < length; ++i)
            lhs[lhsOffset + i] = rhs1[rhs1Offset + i] ^ rhs2[rhs2Offset + i];
    }

    public static void XOR(Span<long> lhs, int lhsOffset, ReadOnlySpan<long> rhs, int rhsOffset, int length)
    {
        if (lhs.Length < lhsOffset + length)
            throw new ArgumentException("Index out of bounds: " + nameof(lhs));
        if (rhs.Length < rhsOffset + length)
            throw new ArgumentException("Index out of bounds: " + nameof(rhs));

        for (var i = 0; i < length; ++i)
            lhs[lhsOffset + i] ^= rhs[rhsOffset + i];
    }
}