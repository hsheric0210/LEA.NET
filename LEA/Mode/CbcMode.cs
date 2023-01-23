using static LEA.Utils.Xor;

namespace LEA.Mode;

public class CbcMode : BlockCipherModeBlock
{
    private byte[] iv;
    private byte[] feedback;
    public CbcMode(BlockCipher cipher) : base(cipher)
    {
    }

    public override string GetAlgorithmName() => engine.GetAlgorithmName() + "/CBC";

    public override void Init(OperatingMode mode, ReadOnlySpan<byte> key, ReadOnlySpan<byte> iv)
    {
        this.mode = mode;
        engine.Init(mode, key);
        this.iv = iv.ToArray();
        feedback = new byte[blockSize];
        Reset();
    }

    public override void Reset()
    {
        base.Reset();
        Array.Copy(iv, 0, feedback, 0, blockSize);
    }

    protected override int ProcessBlock(ReadOnlySpan<byte> input, int inOffset, Span<byte> output, int outOffset, int length)
    {
        if (length != blockSize)
            throw new ArgumentException("outlen should be " + blockSize + " in " + GetAlgorithmName());

        if (mode == OperatingMode.Encrypt)
            return EncryptBlock(input, inOffset, output, outOffset);

        return DecryptBlock(input, inOffset, output, outOffset);
    }

    private int EncryptBlock(ReadOnlySpan<byte> input, int inOffset, Span<byte> output, int outOffset)
    {
        if (inOffset + blockSize > input.Length)
        {
            throw new InvalidOperationException("input data too short");
        }

        XOR(feedback, 0, input, inOffset, blockSize);
        engine.ProcessBlock(feedback, 0, output, outOffset);
        output.Slice(outOffset, blockSize).CopyTo(feedback);
        return blockSize;
    }

    private int DecryptBlock(ReadOnlySpan<byte> input, int inOffset, Span<byte> output, int outOffset)
    {
        if (inOffset + blockSize > input.Length)
        {
            throw new InvalidOperationException("input data too short");
        }

        engine.ProcessBlock(input, inOffset, output, outOffset);
        XOR(output, outOffset, feedback, 0, blockSize);
        input.Slice(inOffset, blockSize).CopyTo(feedback);
        return blockSize;
    }
}