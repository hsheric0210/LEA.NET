using static LEA.Utils.Xor;

namespace LEA.OpMode;

// DONE: block vs buffer
public class OfbMode : BlockCipherModeStream
{
    private byte[] iv;
    private byte[] block;
    public OfbMode(BlockCipher cipher) : base(cipher)
    {
    }

    public override string GetAlgorithmName() => Engine.GetAlgorithmName() + "/OFB";

    public override void Init(Mode mode, ReadOnlySpan<byte> key, ReadOnlySpan<byte> iv)
    {
        Mode = mode;
        Engine.Init(Mode.Encrypt, key);
        this.iv = iv.ToArray();
        block = new byte[BlockSizeBytes];
        Reset();
    }

    public override void Reset()
    {
        base.Reset();
        Buffer.BlockCopy(iv, 0, block, 0, BlockSizeBytes);
    }

    protected override int ProcessBlock(ReadOnlySpan<byte> inBlock, int inOffset, Span<byte> outBlock, int outOffset, int outLength)
    {
        var processed = Engine.ProcessBlock(block, 0, block, 0);
        XOR(outBlock, outOffset, inBlock, inOffset, block, 0, outLength);
        return processed;
    }
}