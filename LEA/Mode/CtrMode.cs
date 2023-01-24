using static LEA.Utils.Xor;

namespace LEA.Mode;

public class CtrMode : BlockCipherModeStream
{
    private byte[] iv;
    private byte[] ctr;
    private byte[] block;

    public CtrMode(BlockCipher cipher) : base(cipher)
    {
    }

    public override string GetAlgorithmName() => engine.GetAlgorithmName() + "/CTR";

    public override void Init(OperatingMode mode, ReadOnlySpan<byte> key, ReadOnlySpan<byte> iv)
    {
        this.mode = mode;
        engine.Init(OperatingMode.Encrypt, key);
        this.iv = iv.ToArray();
        ctr = new byte[blockSize];
        block = new byte[blockSize];
        Reset();
    }

    public override void Reset()
    {
        base.Reset();
        iv.CopyTo(ctr, 0);
    }

    protected override int ProcessBlock(ReadOnlySpan<byte> input, int inOffset, Span<byte> output, int outOffset, int length)
    {
        var processed = engine.ProcessBlock(ctr, 0, block, 0);
        AddCounter();
        XOR(output, outOffset, input, inOffset, block, 0, length);
        return processed;
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