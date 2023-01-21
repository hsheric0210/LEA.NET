namespace LEA.NET.Mode;

public class ECBMode : BlockCipherModeBlock
    {
        public ECBMode(BlockCipher cipher): base(cipher)
        {
        }

	public override string GetAlgorithmName() => engine.GetAlgorithmName() + "/ECB";

	public override void Init(Mode mode, byte[] mk)
        {
            this.mode = mode;
            engine.Init(mode, mk);
        }

        protected override int ProcessBlock(byte[] @in, int inOff, byte[] @out, int outOff, int outlen)
        {
            if (outlen != blockSize)
            {
                throw new ArgumentException("outlen should be " + blockSize + " in " + GetAlgorithmName());
            }

            if ((inOff + blockSize) > @in.Length)
            {
                throw new InvalidOperationException("input data too short");
            }

            return engine.ProcessBlock(@in, inOff, @out, outOff);
        }
    }