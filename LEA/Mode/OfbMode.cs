using static LEA.Utils.Xor;

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

    public override void Init(OperatingMode mode, ReadOnlySpan<byte> key, ReadOnlySpan<byte> iv)
    {
        this.mode = mode;
        engine.Init(OperatingMode.Encrypt, key);
        this.iv = iv.ToArray();
        block = new byte[blockSize];
        Reset();
    }

    public override void Reset()
    {
        base.Reset();
        Array.Copy(iv, 0, block, 0, blockSize);
    }

    protected override int ProcessBlock(ReadOnlySpan<byte> input, int inOffset, Span<byte> output, int outOffset, int length)
    {
        var processed = engine.ProcessBlock(block, 0, block, 0);
        XOR(output, outOffset, input, inOffset, block, 0, length);
        return processed;
    }
}