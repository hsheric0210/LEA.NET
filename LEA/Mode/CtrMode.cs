namespace LEA.Mode;

public class CTRMode : BlockCipherModeStream
{
	private byte[] iv;
	private byte[] ctr;
	private byte[] block;
	public CTRMode(BlockCipher cipher) : base(cipher)
	{
	}

	public override string GetAlgorithmName() => engine.GetAlgorithmName() + "/CTR";

	public override void Init(OperatingMode mode, ReadOnlySpan<byte> mk, ReadOnlySpan<byte> iv)
	{
		this.mode = mode;
		engine.Init(OperatingMode.Encrypt, mk);
		this.iv = iv.ToArray();
		ctr = new byte[blockSize];
		block = new byte[blockSize];
		Reset();
	}

	public override void Reset()
	{
		base.Reset();
		Array.Copy(iv, 0, ctr, 0, ctr.Length);
	}

	protected override int ProcessBlock(ReadOnlySpan<byte> input, int inOffset, Span<byte> output, int outOffset, int length)
	{
		var len = engine.ProcessBlock(ctr, 0, block, 0);
		AddCounter();
		output.XOR(outOffset, input, inOffset, block, 0, length);
		return len;
	}

	private void AddCounter()
	{
		for (var i = ctr.Length - 1; i >= 0; --i)
		{
			if (++ctr[i] != 0)
			{
				break;
			}
		}
	}
}