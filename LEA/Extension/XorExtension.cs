namespace LEA.Extension;

public static class XorExtension
{
	public static void XOR(this byte[] lhs, ReadOnlySpan<byte> rhs) => ((Span<byte>)lhs).XOR(rhs);
	public static void XOR(this Span<byte> lhs, ReadOnlySpan<byte> rhs)
	{
		if (lhs.Length != rhs.Length)
			throw new ArgumentException("the length of two arrays should be same");

		for (var i = 0; i < lhs.Length; ++i)
			lhs[i] ^= rhs[i];
	}

	public static void XOR(this byte[] lhs, int lhsOffset, ReadOnlySpan<byte> rhs, int rhsOffset, int length) => ((Span<byte>)lhs).XOR(lhsOffset, rhs, rhsOffset, length);
	public static void XOR(this Span<byte> lhs, int lhsOffset, ReadOnlySpan<byte> rhs, int rhsOffset, int length)
	{
		if (lhs.Length < lhsOffset + length)
			throw new ArgumentException("Index out of bounds: " + nameof(lhs));
		if (rhs.Length < rhsOffset + length)
			throw new ArgumentException("Index out of bounds: " + nameof(rhs));

		for (var i = 0; i < length; ++i)
			lhs[lhsOffset + i] ^= rhs[rhsOffset + i];
	}

	public static void XOR(this byte[] lhs, ReadOnlySpan<byte> rhs1, ReadOnlySpan<byte> rhs2) => ((Span<byte>)lhs).XOR(rhs1, rhs2);
	/// <summary>
	/// lhs = rhs1 ^ rhs2
	/// </summary>
	public static void XOR(this Span<byte> lhs, ReadOnlySpan<byte> rhs1, ReadOnlySpan<byte> rhs2)
	{
		if (lhs.Length != rhs1.Length || lhs.Length != rhs2.Length)
			throw new ArgumentException("the length of arrays should be same");

		for (var i = 0; i < lhs.Length; ++i)
			lhs[i] = (byte)(rhs1[i] ^ rhs2[i]);
	}

	public static void XOR(this byte[] output, int outOffset, ReadOnlySpan<byte> lhs, int lhsOffset, ReadOnlySpan<byte> rhs, int rhsOffset, int length) => ((Span<byte>)output).XOR(outOffset, lhs, lhsOffset, rhs, rhsOffset, length);
	public static void XOR(this Span<byte> output, int outOffset, ReadOnlySpan<byte> lhs, int lhsOffset, ReadOnlySpan<byte> rhs, int rhsOffset, int length)
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

	public static void XOR(this int[] lhs, ReadOnlySpan<int> rhs) => ((Span<int>)lhs).XOR(rhs);
	/// <summary>
	/// lhs ^= rhs
	/// </summary>
	public static void XOR(this Span<int> lhs, ReadOnlySpan<int> rhs)
	{
		if (lhs.Length != rhs.Length)
			throw new ArgumentException("the length of two arrays should be same");

		for (var i = 0; i < lhs.Length; ++i)
			lhs[i] ^= rhs[i];
	}

	public static void XOR(this int[] lhs, int lhsOffset, ReadOnlySpan<int> rhs, int rhsOffset, int length) => ((Span<int>)lhs).XOR(lhsOffset, rhs, rhsOffset, length);
	public static void XOR(this Span<int> lhs, int lhsOffset, ReadOnlySpan<int> rhs, int rhsOffset, int length)
	{
		if (lhs.Length < lhsOffset + length)
			throw new ArgumentException("Index out of bounds: " + nameof(lhs));
		if (rhs.Length < rhsOffset + length)
			throw new ArgumentException("Index out of bounds: " + nameof(rhs));

		for (var i = 0; i < length; ++i)
			lhs[lhsOffset + i] ^= rhs[rhsOffset + i];
	}

	public static void XOR(this int[] lhs, ReadOnlySpan<int> rhs1, ReadOnlySpan<int> rhs2) => ((Span<int>)lhs).XOR(rhs1, rhs2);
	/// <summary>
	/// lhs = rhs1 ^ rhs2
	/// </summary>
	public static void XOR(this Span<int> lhs, ReadOnlySpan<int> rhs1, ReadOnlySpan<int> rhs2)
	{
		if (lhs.Length != rhs1.Length || lhs.Length != rhs2.Length)
			throw new ArgumentException("the length of arrays should be same");

		for (var i = 0; i < lhs.Length; ++i)
			lhs[i] = rhs1[i] ^ rhs2[i];
	}

	public static void XOR(this int[] lhs, int lhsOffset, ReadOnlySpan<int> rhs1, int rhs1Offset, ReadOnlySpan<int> rhs2, int rhs2Offset, int length) => ((Span<int>)lhs).XOR(lhsOffset, rhs1, rhs1Offset, rhs2, rhs2Offset, length);
	public static void XOR(this Span<int> lhs, int lhsOffset, ReadOnlySpan<int> rhs1, int rhs1Offset, ReadOnlySpan<int> rhs2, int rhs2Offset, int length)
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

	public static void XOR(this long[] lhs, int lhsOffset, ReadOnlySpan<long> rhs, int rhsOffset, int length) => ((Span<long>)lhs).XOR(lhsOffset, rhs, rhsOffset, length);
	public static void XOR(this Span<long> lhs, int lhsOffset, ReadOnlySpan<long> rhs, int rhsOffset, int length)
	{
		if (lhs.Length < lhsOffset + length)
			throw new ArgumentException("Index out of bounds: " + nameof(lhs));
		if (rhs.Length < rhsOffset + length)
			throw new ArgumentException("Index out of bounds: " + nameof(rhs));

		for (var i = 0; i < length; ++i)
			lhs[lhsOffset + i] ^= rhs[rhsOffset + i];
	}
}