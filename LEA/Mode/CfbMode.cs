using static LEA.Utils.Xor;

namespace LEA.Mode;

// DONE: block vs buffer
public class CfbMode : BlockCipherModeStream
{
    private byte[] iv;
    private byte[] block;
    private byte[] feedback;
    public CfbMode(BlockCipher cipher) : base(cipher)
    {
    }

    public override string GetAlgorithmName() => engine.GetAlgorithmName() + "/CFB";

    public override void Init(OperatingMode mode, ReadOnlySpan<byte> mk, ReadOnlySpan<byte> iv)
    {
        this.mode = mode;
        engine.Init(OperatingMode.Encrypt, mk);
        this.iv = iv.ToArray();
        block = new byte[blockSize];
        feedback = new byte[blockSize];
        Reset();
    }

    public override void Reset()
    {
        base.Reset();
        Buffer.BlockCopy(iv, 0, feedback, 0, blockSize);
    }

    protected override int ProcessBlock(ReadOnlySpan<byte> input, int inOffset, Span<byte> output, int outOffset, int outLength)
    {
        var length = engine.ProcessBlock(feedback, 0, block, 0);
        XOR(output, outOffset, input, inOffset, block, 0, outLength);
        if (mode == OperatingMode.Encrypt)
        {
            output.Slice(outOffset, blockSize).CopyTo(feedback);
        }
        else
        {
            input.Slice(inOffset, blockSize).CopyTo(feedback);
        }

        return length;
    }
}