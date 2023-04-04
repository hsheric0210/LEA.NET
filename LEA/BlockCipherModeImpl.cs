using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kr.Re.Nsr.Crypto
{
    public abstract class BlockCipherModeImpl : BlockCipherMode
    {
        protected Mode mode;
        protected BlockCipher engine;
        protected byte[] buffer;
        protected int bufferOffset;
        protected int blocksize;
        protected int blockmask;
        public BlockCipherModeImpl(BlockCipher cipher)
        {
            engine = cipher;
            blocksize = engine.GetBlockSize();
            blockmask = GetBlockmask(blocksize);
            buffer = new byte[blocksize];
        }

        public override byte[] DoFinal(byte[] msg)
        {
            byte[] part1 = Update(msg);
            byte[] part2 = DoFinal();
            int len1 = part1 == null ? 0 : part1.length;
            int len2 = part2 == null ? 0 : part2.length;
            byte[] out = new byte[len1 + len2];
            if (len1 > 0)
            {
                System.Arraycopy(part1, 0, @out, 0, len1);
            }

            if (len2 > 0)
            {
                System.Arraycopy(part2, 0, @out, len1, len2);
            }

            return @out;
        }

        protected abstract int ProcessBlock(byte[] @in, int inOff, byte[] @out, int outOff, int length);
        protected virtual int ProcessBlock(byte[] @in, int inOff, byte[] @out, int outOff)
        {
            return ProcessBlock(@in, inOff, @out, outOff, blocksize);
        }

        protected static int GetBlockmask(int blocksize)
        {
            int mask = 0;
            switch (blocksize)
            {
                case 8:
                    mask = 0xfffffff7;
                    break;
                case 16:
                    mask = 0xfffffff0;
                    break;
                case 32:
                    mask = 0xffffffe0;
                    break;
            }

            return mask;
        }

        protected static byte[] Clone(byte[] array)
        {
            if (array == null)
            {
                return null;
            }

            byte[] clone = new byte[array.length];
            System.Arraycopy(array, 0, clone, 0, clone.length);
            return clone;
        }
    }
}