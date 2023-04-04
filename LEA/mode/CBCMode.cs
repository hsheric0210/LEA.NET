using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kr.Re.Nsr.Crypto.Mode
{
    public class CBCMode : BlockCipherModeBlock
    {
        private byte[] iv;
        private byte[] feedback;
        public CBCMode(BlockCipher cipher): base(cipher)
        {
        }

        public override string GetAlgorithmName()
        {
            return engine.GetAlgorithmName() + "/CBC";
        }

        public override void Init(Mode mode, byte[] mk, byte[] iv)
        {
            this.mode = mode;
            engine.Init(mode, mk);
            this.iv = Clone(iv);
            this.feedback = new byte[blocksize];
            Reset();
        }

        public override void Reset()
        {
            base.Reset();
            System.Arraycopy(iv, 0, feedback, 0, blocksize);
        }

        protected override int ProcessBlock(byte[] @in, int inOff, byte[] @out, int outOff, int outlen)
        {
            if (outlen != blocksize)
            {
                throw new ArgumentException("outlen should be " + blocksize + " in " + GetAlgorithmName());
            }

            if (mode == Mode.ENCRYPT)
            {
                return EncryptBlock(@in, inOff, @out, outOff);
            }

            return DecryptBlock(@in, inOff, @out, outOff);
        }

        private int EncryptBlock(byte[] @in, int inOff, byte[] @out, int outOff)
        {
            if ((inOff + blocksize) > @in.length)
            {
                throw new InvalidOperationException("input data too short");
            }

            XOR(feedback, 0, @in, inOff, blocksize);
            engine.ProcessBlock(feedback, 0, @out, outOff);
            System.Arraycopy(@out, outOff, feedback, 0, blocksize);
            return blocksize;
        }

        private int DecryptBlock(byte[] @in, int inOff, byte[] @out, int outOff)
        {
            if ((inOff + blocksize) > @in.length)
            {
                throw new InvalidOperationException("input data too short");
            }

            engine.ProcessBlock(@in, inOff, @out, outOff);
            XOR(@out, outOff, feedback, 0, blocksize);
            System.Arraycopy(@in, inOff, feedback, 0, blocksize);
            return blocksize;
        }
    }
}