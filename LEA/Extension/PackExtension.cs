namespace LEA.Extension;
public static class PackExtension
{
	public static void Pack(this byte[] input, Span<uint> output) => ((ReadOnlySpan<byte>)input).Pack(output);
	public static void Pack(this ReadOnlySpan<byte> input, Span<uint> output)
	{
		if (input.Length != output.Length * 4)
			throw new ArgumentException("output length must be input length * 4");

		var outIdx = 0;
		for (var inIdx = 0; inIdx < input.Length; ++inIdx, ++outIdx)
		{
			output[outIdx] = input[inIdx] & 0xffu;
			output[outIdx] |= (input[++inIdx] & 0xffu) << 8;
			output[outIdx] |= (input[++inIdx] & 0xffu) << 16;
			output[outIdx] |= (input[++inIdx] & 0xffu) << 24;
		}
	}

	public static void Pack(this byte[] input, int inOffset, Span<uint> output, int outOffset, int length) => ((ReadOnlySpan<byte>)input).Pack(inOffset, output, outOffset, length);
	public static void Pack(this ReadOnlySpan<byte> input, int inOffset, Span<uint> output, int outOffset, int length)
	{
		if ((length & 3) != 0)
			throw new ArgumentException("length should be multiple of 4");
		if (input.Length < inOffset + length)
			throw new ArgumentException("Index out of bounds: " + nameof(input));
		if (output.Length < outOffset + length / 4)
			throw new ArgumentException("Index out of bounds: " + nameof(output));

		var outIdx = outOffset;
		var endInIdx = inOffset + length;
		for (var inIdx = inOffset; inIdx < endInIdx; ++inIdx, ++outIdx)
		{
			output[outIdx] = input[inIdx] & 0xffu;
			output[outIdx] |= (input[++inIdx] & 0xffu) << 8;
			output[outIdx] |= (input[++inIdx] & 0xffu) << 16;
			output[outIdx] |= (input[++inIdx] & 0xffu) << 24;
		}
	}

	public static void Unpack(this uint[] input, Span<byte> output) => ((ReadOnlySpan<uint>)input).Unpack(output);
	public static void Unpack(this ReadOnlySpan<uint> input, Span<byte> output)
	{
		if (input.Length * 4 != output.Length)
			throw new ArgumentException("output length must be input length / 4");

		var outIdx = 0;
		for (var inIdx = 0; inIdx < input.Length; ++inIdx, ++outIdx)
		{
			output[outIdx] = (byte)input[inIdx];
			output[++outIdx] = (byte)(input[inIdx] >> 8);
			output[++outIdx] = (byte)(input[inIdx] >> 16);
			output[++outIdx] = (byte)(input[inIdx] >> 24);
		}
	}

	public static void Unpack(this uint[] input, int inOffset, Span<byte> output, int outOffset, int length) => ((ReadOnlySpan<uint>)input).Unpack(inOffset, output, outOffset, length);
	public static void Unpack(this ReadOnlySpan<uint> input, int inOffset, Span<byte> output, int outOffset, int length)
	{
		if (input.Length < inOffset + length)
			throw new ArgumentException("Index out of bounds: " + nameof(input));
		if (output.Length < outOffset + length * 4)
			throw new ArgumentException("Index out of bounds: " + nameof(output));

		var outIdx = outOffset;
		var endInIdx = inOffset + length;
		for (var inIdx = inOffset; inIdx < endInIdx; ++inIdx, ++outIdx)
		{
			output[outIdx] = (byte)input[inIdx];
			output[++outIdx] = (byte)(input[inIdx] >> 8);
			output[++outIdx] = (byte)(input[inIdx] >> 16);
			output[++outIdx] = (byte)(input[inIdx] >> 24);
		}
	}
}
