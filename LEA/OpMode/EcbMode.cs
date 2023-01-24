namespace LEA.OpMode;

public class EcbMode : BlockCipherModeBlock
{
    public EcbMode(BlockCipher cipher) : base(cipher)
    {
    }

    public override string GetAlgorithmName() => Engine.GetAlgorithmName() + "/ECB";

    public override void Init(Mode mode, ReadOnlySpan<byte> key)
    {
        Mode = mode;
        Engine.Init(mode, key);
    }

    protected override int ProcessBlock(ReadOnlySpan<byte> inBlock, int inOffset, Span<byte> outBlock, int outOffset, int outLength)
    {
        if (outLength != BlockSizeBytes)
            throw new ArgumentException("outlen should be " + BlockSizeBytes + " in " + GetAlgorithmName());

        if (inOffset + BlockSizeBytes > inBlock.Length)
            throw new InvalidOperationException("input data too short");

        return Engine.ProcessBlock(inBlock, inOffset, outBlock, outOffset);
    }
}