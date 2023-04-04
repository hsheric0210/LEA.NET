using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kr.Re.Nsr.Crypto
{
    public abstract class BlockCipherModeBlock : BlockCipherModeImpl
    {
        protected Padding padding;
        public BlockCipherModeBlock(BlockCipher cipher): base(cipher)
        {
        }

        public override int GetOutputSize(int len)
        {

            // TODO
            int size = ((len + bufferOffset) & blockmask) + blocksize;
            if (mode == Mode.ENCRYPT)
            {
                return padding != null ? size : len;
            }

            return len;
        }

        public override int GetUpdateOutputSize(int len)
        {
            if (mode == Mode.DECRYPT && padding != null)
            {
                return (len + bufferOffset - blocksize) & blockmask;
            }

            return (len + bufferOffset) & blockmask;
        }

        public override void Init(Mode mode, byte[] mk)
        {
            throw new InvalidOperationException("This init method is not applicable to " + GetAlgorithmName());
        }

        public override void Init(Mode mode, byte[] mk, byte[] iv)
        {
            throw new InvalidOperationException("This init method is not applicable to " + GetAlgorithmName());
        }

        public override void Reset()
        {
            bufferOffset = 0;
            Arrays.Fill(buffer, (byte)0);
        }

        public override void SetPadding(Padding padding)
        {
            this.padding = padding;
        }

        public override byte[] Update(byte[] msg)
        {
            if (padding != null && mode == Mode.DECRYPT)
            {
                return DecryptWithPadding(msg);
            }

            if (msg == null)
            {
                return null;
            }

            int len = msg.length;
            int gap = buffer.length - bufferOffset;
            int inOff = 0;
            int outOff = 0;
            byte[] out = new byte[GetUpdateOutputSize(len)];
            if (len >= gap)
            {
                System.Arraycopy(msg, inOff, buffer, bufferOffset, gap);
                outOff += ProcessBlock(buffer, 0, @out, outOff);
                bufferOffset = 0;
                len -= gap;
                inOff += gap;
                while (len >= buffer.length)
                {
                    outOff += ProcessBlock(msg, inOff, @out, outOff);
                    len -= blocksize;
                    inOff += blocksize;
                }
            }

            if (len > 0)
            {
                System.Arraycopy(msg, inOff, buffer, bufferOffset, len);
                bufferOffset += len;
                len = 0;
            }

            return @out;
        }

        public override byte[] DoFinal()
        {
            if (padding != null)
            {
                return DoFinalWithPadding();
            }

            if (bufferOffset == 0)
            {
                return null;
            }
            else if (bufferOffset != blocksize)
            {
                throw new InvalidOperationException("Bad padding");
            }

            byte[] out = new byte[blocksize];
            ProcessBlock(buffer, 0, @out, 0, blocksize);
            return @out;
        }

        /// <summary>
        /// 패딩 사용시 복호화 처리, 마지막 블록을 위해 데이터를 남겨둠
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        private byte[] DecryptWithPadding(byte[] msg)
        {
            if (msg == null)
            {
                return null;
            }

            int len = msg.length;
            int gap = buffer.length - bufferOffset;
            int inOff = 0;
            int outOff = 0;
            byte[] out = new byte[GetUpdateOutputSize(len)];
            if (len > gap)
            {
                System.Arraycopy(msg, inOff, buffer, bufferOffset, gap);
                outOff += ProcessBlock(buffer, 0, @out, outOff);
                bufferOffset = 0;
                len -= gap;
                inOff += gap;
                while (len > buffer.length)
                {
                    outOff += ProcessBlock(msg, inOff, @out, outOff);
                    len -= blocksize;
                    inOff += blocksize;
                }
            }

            if (len > 0)
            {
                System.Arraycopy(msg, inOff, buffer, bufferOffset, len);
                bufferOffset += len;
                len = 0;
            }

            return @out;
        }

        /// <summary>
        /// 패딩 사용시 마지막 블록 처리
        /// </summary>
        /// <returns></returns>
        private byte[] DoFinalWithPadding()
        {
            byte[] out = null;
            if (mode == Mode.ENCRYPT)
            {
                padding.Pad(buffer, bufferOffset);
                @out = new byte[GetOutputSize(0)];
                ProcessBlock(buffer, 0, @out, 0);
            }
            else
            {
                byte[] blk = new byte[blocksize];
                ProcessBlock(buffer, 0, blk, 0);
                @out = padding.Unpad(blk);
            }

            return @out;
        }
    }
}