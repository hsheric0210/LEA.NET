namespace LEA.NET.Mode;

public class CBCMode : BlockCipherModeBlock
    {
        private byte[] iv;
        private byte[] feedback;
        public CBCMode(BlockCipher cipher): base(cipher)
        {
        }

	public override string GetAlgorithmName() => engine.GetAlgorithmName() + "/CBC";

	public override void Init(Mode mode, byte[] mk, byte[] iv)
        {
            this.mode = mode;
            engine.Init(mode, mk);
            this.iv = Clone(iv);
            this.feedback = new byte[blockSize];
            Reset();
        }

        public override void Reset()
        {
            base.Reset();
            Array.Copy(iv, 0, feedback, 0, blockSize);
        }

        protected override int ProcessBlock(byte[] @in, int inOff, byte[] @out, int outOff, int outlen)
        {
            if (outlen != blockSize)
            {
                throw new ArgumentException("outlen should be " + blockSize + " in " + GetAlgorithmName());
            }

            if (mode == Mode.ENCRYPT)
            {
                return EncryptBlock(@in, inOff, @out, outOff);
            }

            return DecryptBlock(@in, inOff, @out, outOff);
        }

        private int EncryptBlock(byte[] @in, int inOff, byte[] @out, int outOff)
        {
            if ((inOff + blockSize) > @in.Length)
            {
                throw new InvalidOperationException("input data too short");
            }

            XOR(feedback, 0, @in, inOff, blockSize);
            engine.ProcessBlock(feedback, 0, @out, outOff);
            Array.Copy(@out, outOff, feedback, 0, blockSize);
            return blockSize;
        }

        private int DecryptBlock(byte[] @in, int inOff, byte[] @out, int outOff)
        {
            if ((inOff + blockSize) > @in.Length)
            {
                throw new InvalidOperationException("input data too short");
            }

            engine.ProcessBlock(@in, inOff, @out, outOff);
            XOR(@out, outOff, feedback, 0, blockSize);
            Array.Copy(@in, inOff, feedback, 0, blockSize);
            return blockSize;
        }
    }