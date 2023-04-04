using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kr.Re.Nsr.Crypto.Mode
{
    public class CTRMode : BlockCipherModeStream
    {
        private byte[] iv;
        private byte[] ctr;
        private byte[] block;
        public CTRMode(BlockCipher cipher): base(cipher)
        {
        }

        public override string GetAlgorithmName()
        {
            return engine.GetAlgorithmName() + "/CTR";
        }

        public override void Init(Mode mode, byte[] mk, byte[] iv)
        {
            this.mode = mode;
            engine.Init(Mode.ENCRYPT, mk);
            this.iv = iv.Clone();
            ctr = new byte[blocksize];
            block = new byte[blocksize];
            Reset();
        }

        public override void Reset()
        {
            base.Reset();
            System.Arraycopy(iv, 0, ctr, 0, ctr.length);
        }

        protected override int ProcessBlock(byte[] @in, int inOff, byte[] @out, int outOff, int outlen)
        {
            int length = engine.ProcessBlock(ctr, 0, block, 0);
            AddCounter();
            XOR(@out, outOff, @in, inOff, block, 0, outlen);
            return length;
        }

        private void AddCounter()
        {
            for (int i = ctr.length - 1; i >= 0; --i)
            {
                if (++ctr[i] != 0)
                {
                    break;
                }
            }
        }
    }
}