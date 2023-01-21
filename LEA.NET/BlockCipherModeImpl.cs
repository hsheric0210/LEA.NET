namespace LEA.NET;

public abstract class BlockCipherModeImpl : BlockCipherMode
{
	protected Mode mode;
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

	public override byte[] DoFinal(byte[] msg)
	{
		var part1 = Update(msg);
		var part2 = DoFinal();
		var len1 = part1 == null ? 0 : part1.Length;
		var len2 = part2 == null ? 0 : part2.Length;
		var outBytes = new byte[len1 + len2];
		if (len1 > 0)
			Array.Copy(part1, 0, outBytes, 0, len1);

		if (len2 > 0)
			Array.Copy(part2, 0, outBytes, len1, len2);

		return outBytes;
	}

	protected abstract int ProcessBlock(byte[] inBytes, int inOffset, byte[] outBytes, int outOffset, int length);
	protected virtual int ProcessBlock(byte[] inBytes, int inOffset, byte[] outBytes, int outOffset) => ProcessBlock(inBytes, inOffset, outBytes, outOffset, blockSize);

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

	protected static byte[] Clone(byte[] array)
	{
		if (array == null)
			return Array.Empty<byte>();

		var clone = new byte[array.Length];
		Array.Copy(array, 0, clone, 0, clone.Length);
		return clone;
	}
}