using static LEA.Utils.Xor;

namespace LEA.OpMode;

public class CtrMode : BlockCipherModeStream
{
    private byte[] iv;
    private byte[] ctr;
    private byte[] block;

    public CtrMode(BlockCipher cipher) : base(cipher)
    {
    }

    public override string GetAlgorithmName() => Engine.GetAlgorithmName() + "/CTR";

    public override void Init(Mode mode, ReadOnlySpan<byte> key, ReadOnlySpan<byte> iv)
    {
        Mode = mode;
        Engine.Init(Mode.Encrypt, key);
        this.iv = iv.ToArray();
        ctr = new byte[BlockSize];
        block = new byte[BlockSize];
        Reset();
    }

    public override void Reset()
    {
        base.Reset();
        iv.CopyTo(ctr, 0);
    }

    protected override int ProcessBlock(ReadOnlySpan<byte> inBlock, int inOffset, Span<byte> outBlock, int outOffset, int outLength)
    {
        var processed = Engine.ProcessBlock(ctr, 0, block, 0);
        AddCounter();
        XOR(outBlock, outOffset, inBlock, inOffset, block, 0, outLength);
        return processed;
    }

    private void AddCounter()
    {
        for (var i = ctr.Length - 1; i >= 0; --i)
        {
            if (++ctr[i] != 0)
                break;
        }
    }
}