using static LEA.Utils.Xor;

namespace LEA.OpMode;

// DONE: block vs buffer
public class CfbMode : BlockCipherModeStream
{
    private byte[] iv;
    private byte[] block;
    private byte[] feedback;
    public CfbMode(BlockCipher cipher) : base(cipher)
    {
    }

    public override string GetAlgorithmName() => Engine.GetAlgorithmName() + "/CFB";

    public override void Init(Mode mode, ReadOnlySpan<byte> key, ReadOnlySpan<byte> iv)
    {
        Mode = mode;
        Engine.Init(Mode.Encrypt, key);
        this.iv = iv.ToArray();
        block = new byte[BlockSize];
        feedback = new byte[BlockSize];
        Reset();
    }

    public override void Reset()
    {
        base.Reset();
        Buffer.BlockCopy(iv, 0, feedback, 0, BlockSize);
    }

    protected override int ProcessBlock(ReadOnlySpan<byte> inBlock, int inOffset, Span<byte> outBlock, int outOffset, int outLength)
    {
        var length = Engine.ProcessBlock(feedback, 0, block, 0);
        XOR(outBlock, outOffset, inBlock, inOffset, block, 0, length);
        if (Mode == Mode.Encrypt)
            outBlock.Slice(outOffset, BlockSize).CopyTo(feedback);
        else
        {
            inBlock.Slice(inOffset, BlockSize).CopyTo(feedback);
        }

        return length;
    }
}