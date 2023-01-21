namespace LEA;

public abstract class BlockCipherModeImpl : BlockCipherMode
{
	protected OperatingMode mode;
	protected BlockCipher engine;
	protected byte[] buffer;
	protected int bufferOffset;
	protected int blockSize;
	protected int blockMask;
	public BlockCipherModeImpl(BlockCipher cipher)
	{
		engine = cipher;
		blockSize = engine.GetBlockSize();
		blockMask = GetBlockmask(blockSize);
		buffer = new byte[blockSize];
	}

	public override ReadOnlySpan<byte> DoFinal(ReadOnlySpan<byte> bytes)
	{
		var part1 = Update(bytes);
		var part2 = DoFinal();
		var len1 = part1 == null ? 0 : part1.Length;
		var len2 = part2 == null ? 0 : part2.Length;
		var output = new byte[len1 + len2];
		if (len1 > 0)
			part1.CopyTo(output);
		if (len2 > 0)
			part2.CopyTo(output[len1..]);
		return output;
	}

	protected abstract int ProcessBlock(ReadOnlySpan<byte> input, int inOffset, ReadOnlySpan<byte> output, int outOffset, int length);
	protected virtual int ProcessBlock(ReadOnlySpan<byte> input, int inOffset, ReadOnlySpan<byte> output, int outOffset) => ProcessBlock(input, inOffset, output, outOffset, blockSize);

	protected static int GetBlockmask(int blocksize)
	{
		return unchecked((int)(blocksize switch
		{
			8 => 0xfffffff7u,
			16 => 0xfffffff0u,
			32 => 0xffffffe0u,
			_ => 0u,
		}));
	}
}