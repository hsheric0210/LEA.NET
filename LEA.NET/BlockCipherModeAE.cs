namespace LEA.NET;

public abstract class BlockCipherModeAE
{
	protected Mode mode;
	protected BlockCipher engine;
	protected byte[] buffer;
	protected byte[] nonce;
	protected int bufferOffset;
	protected int blockSize;
	protected int tagLength;

	public BlockCipherModeAE(BlockCipher cipher)
	{
		engine = cipher;
		blockSize = engine.GetBlockSize();
		buffer = new byte[blockSize];
	}

	public abstract void Init(Mode mode, byte[] mk, byte[] nonce, int taglen);

	public abstract void UpdateAAD(byte[] aad);

	public abstract byte[] Update(byte[] msg);

	public abstract byte[] DoFinal();

	public abstract int GetOutputSize(int len);

	public virtual byte[] DoFinal(byte[] msg)
	{
		byte[] outBytes;
		if (mode == Mode.ENCRYPT)
		{
			var part1 = Update(msg);
			var part2 = DoFinal();
			var len1 = part1 == null ? 0 : part1.Length;
			var len2 = part2 == null ? 0 : part2.Length;
			outBytes = new byte[len1 + len2];
			if (part1 != null)
				Array.Copy(part1, 0, outBytes, 0, len1);
			if (part2 != null)
				Array.Copy(part2, 0, outBytes, len1, len2);
		}
		else
		{
			Update(msg);
			outBytes = DoFinal();
		}

		return outBytes;
	}
}