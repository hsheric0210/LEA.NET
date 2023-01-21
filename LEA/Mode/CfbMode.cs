namespace LEA.Mode;

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

        protected override int ProcessBlock(byte[] input, int inOffset, byte[] output, int outOffset, int length)
        {
            var length = engine.ProcessBlock(feedback, 0, block, 0);
            XOR(output, outOffset, input, inOffset, block, 0, length);
            if (mode == Mode.ENCRYPT)
            {
                Array.Copy(output, outOffset, feedback, 0, blockSize);
            }
            else
            {
                Array.Copy(input, inOffset, feedback, 0, blockSize);
            }

            return length;
        }
    }