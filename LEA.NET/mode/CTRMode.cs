namespace LEA.NET.Mode;

public class CTRMode : BlockCipherModeStream
    {
        private byte[] iv;
        private byte[] ctr;
        private byte[] block;
        public CTRMode(BlockCipher cipher): base(cipher)
        {
        }

	public override string GetAlgorithmName() => engine.GetAlgorithmName() + "/CTR";

	public override void Init(Mode mode, byte[] mk, byte[] iv)
        {
            this.mode = mode;
            engine.Init(Mode.ENCRYPT, mk);
            this.iv = iv.Clone();
            ctr = new byte[blockSize];
            block = new byte[blockSize];
            Reset();
        }

        public override void Reset()
        {
            base.Reset();
            Array.Copy(iv, 0, ctr, 0, ctr.Length);
        }

        protected override int ProcessBlock(byte[] @in, int inOff, byte[] @out, int outOff, int outlen)
        {
            var length = engine.ProcessBlock(ctr, 0, block, 0);
            AddCounter();
            XOR(@out, outOff, @in, inOff, block, 0, outlen);
            return length;
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