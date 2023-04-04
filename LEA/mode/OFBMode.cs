using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kr.Re.Nsr.Crypto.Mode
{
    // DONE: block vs buffer
    public class OFBMode : BlockCipherModeStream
    {
        private byte[] iv;
        private byte[] block;
        public OFBMode(BlockCipher cipher): base(cipher)
        {
        }

        public override string GetAlgorithmName()
        {
            return engine.GetAlgorithmName() + "/OFB";
        }

        public override void Init(Mode mode, byte[] mk, byte[] iv)
        {
            this.mode = mode;
            engine.Init(Mode.ENCRYPT, mk);
            this.iv = iv.Clone();
            block = new byte[blocksize];
            Reset();
        }

        public override void Reset()
        {
            base.Reset();
            System.Arraycopy(iv, 0, block, 0, blocksize);
        }

        protected override int ProcessBlock(byte[] @in, int inOff, byte[] @out, int outOff, int outlen)
        {
            int length = engine.ProcessBlock(block, 0, block, 0);
            XOR(@out, outOff, @in, inOff, block, 0, outlen);
            return length;
        }
    }
}