namespace LEA.Mode;

// DONE: block vs buffer
public class OfbMode : BlockCipherModeStream
{
	private byte[] iv;
	private byte[] block;
	public OfbMode(BlockCipher cipher) : base(cipher)
	{
	}

	public override string GetAlgorithmName() => engine.GetAlgorithmName() + "/OFB";

	public override void Init(OperatingMode mode, byte[] key, byte[] iv)
	{
		this.mode = mode;
		engine.Init(OperatingMode.Encrypt, key);
		this.iv = iv;
		block = new byte[blockSize];
		Reset();
	}

	public override void Reset()
	{
		base.Reset();
		Array.Copy(iv, 0, block, 0, blockSize);
	}

	protected override int ProcessBlock(byte[] input, int inOffset, byte[] output, int outOffset, int length)
	{
		var outLen = engine.ProcessBlock(block, 0, block, 0);
		XOR(output, outOffset, input, inOffset, block, 0, length);
		return outLen;
	}
}