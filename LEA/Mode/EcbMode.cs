namespace LEA.Mode;

public class ECBMode : BlockCipherModeBlock
{
    public ECBMode(BlockCipher cipher) : base(cipher)
    {
    }

    public override string GetAlgorithmName() => engine.GetAlgorithmName() + "/ECB";

    public override void Init(OperatingMode mode, ReadOnlySpan<byte> mk)
    {
        this.mode = mode;
        engine.Init(mode, mk);
    }

    protected override int ProcessBlock(ReadOnlySpan<byte> input, int inOffset, Span<byte> output, int outOffset, int length)
    {
        if (length != blockSize)
        {
            throw new ArgumentException("outlen should be " + blockSize + " in " + GetAlgorithmName());
        }

        if (inOffset + blockSize > input.Length)
        {
            throw new InvalidOperationException("input data too short");
        }

        return engine.ProcessBlock(input, inOffset, output, outOffset);
    }
}