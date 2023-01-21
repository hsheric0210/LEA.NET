namespace LEA.NET.Mode;

// DONE: block vs buffer
public class CFBMode : BlockCipherModeStream
    {
        private byte[] iv;
        private byte[] block;
        private byte[] feedback;
        public CFBMode(BlockCipher cipher): base(cipher)
        {
        }

	public override string GetAlgorithmName() => engine.GetAlgorithmName() + "/CFB";

	public override void Init(Mode mode, byte[] mk, byte[] iv)
        {
            this.mode = mode;
            engine.Init(Mode.ENCRYPT, mk);
            this.iv = iv.Clone();
            block = new byte[blockSize];
            feedback = new byte[blockSize];
            Reset();
        }

        public override void Reset()
        {
            base.Reset();
            Array.Copy(iv, 0, feedback, 0, blockSize);
        }

        protected override int ProcessBlock(byte[] @in, int inOff, byte[] @out, int outOff, int outlen)
        {
            var length = engine.ProcessBlock(feedback, 0, block, 0);
            XOR(@out, outOff, @in, inOff, block, 0, outlen);
            if (mode == Mode.ENCRYPT)
            {
                Array.Copy(@out, outOff, feedback, 0, blockSize);
            }
            else
            {
                Array.Copy(@in, inOff, feedback, 0, blockSize);
            }

            return length;
        }
    }