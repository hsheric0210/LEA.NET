using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kr.Re.Nsr.Crypto.Mode
{
    public class CCMMode : BlockCipherModeAE
    {
        private byte[] ctr;
        private byte[] mac;
        private byte[] tag;
        private byte[] block;
        private ByteArrayOutputStream aadBytes;
        private ByteArrayOutputStream inputBytes;
        private int msglen;
        private int taglen;
        private int noncelen;
        public CCMMode(BlockCipher cipher): base(cipher)
        {
            ctr = new byte[blocksize];
            mac = new byte[blocksize];
            block = new byte[blocksize];
        }

        public override void Init(Mode mode, byte[] mk, byte[] nonce, int taglen)
        {
            this.mode = mode;
            engine.Init(Mode.ENCRYPT, mk);
            aadBytes = new ByteArrayOutputStream();
            inputBytes = new ByteArrayOutputStream();
            SetTaglen(taglen);
            SetNonce(nonce);
        }

        public override void UpdateAAD(byte[] aad)
        {
            if (aad == null || aad.length == 0)
            {
                return;
            }

            aadBytes.Write(aad, 0, aad.length);
        }

        public override byte[] Update(byte[] msg)
        {
            inputBytes.Write(msg, 0, msg.length);
            return null;
        }

        public override byte[] DoFinal()
        {
            Close(aadBytes);
            Close(inputBytes);
            if (aadBytes.Count > 0)
            {
                block[0] |= (byte)0x40;
            }

            msglen = inputBytes.ToByteArray().length;
            if (mode == Mode.DECRYPT)
            {
                msglen -= taglen;
            }

            ToBytes(msglen, block, noncelen + 1, 15 - noncelen);
            engine.ProcessBlock(block, 0, mac, 0);
            byte[] out;
            ProcessAAD();
            if (mode == Mode.ENCRYPT)
            {
                @out = new byte[msglen + taglen];
                EncryptData(@out, 0);
            }
            else
            {
                @out = new byte[msglen];
                DecryptData(@out, 0);
            }

            ResetCounter();
            engine.ProcessBlock(ctr, 0, block, 0);
            if (mode == Mode.ENCRYPT)
            {
                XOR(mac, block);
                System.Arraycopy(mac, 0, tag, 0, taglen);
                System.Arraycopy(mac, 0, @out, @out.length - taglen, taglen);
            }
            else
            {
                mac = Arrays.CopyOf(mac, taglen);
                if (Arrays.Equals(tag, mac) == false)
                {
                    Arrays.Fill(@out, (byte)0);
                }
            }

            return @out;
        }

        public override int GetOutputSize(int len)
        {
            int outSize = len + bufOff;
            if (mode == Mode.ENCRYPT)
            {
                return outSize + taglen;
            }

            return outSize < taglen ? 0 : outSize - taglen;
        }

        private void SetNonce(byte[] nonce)
        {
            if (nonce == null)
            {
                throw new NullPointerException("nonce is null");
            }

            noncelen = nonce.length;
            if (noncelen < 7 || noncelen > 13)
            {
                throw new ArgumentException("length of nonce should be 7 ~ 13 bytes");
            }


            // init counter
            ctr[0] = (byte)(14 - noncelen);
            System.Arraycopy(nonce, 0, ctr, 1, noncelen);

            // init b0
            int tagfield = (taglen - 2) / 2;
            block[0] = (byte)((tagfield << 3) & 0xff);
            block[0] |= (byte)((14 - noncelen) & 0xff);
            System.Arraycopy(nonce, 0, block, 1, noncelen);
        }

        private void SetTaglen(int taglen)
        {
            if (taglen < 4 || taglen > 16 || (taglen & 0x01) != 0)
            {
                throw new ArgumentException("length of tag should be 4, 6, 8, 10, 12, 14, 16 bytes");
            }

            this.taglen = taglen;
            tag = new byte[taglen];
        }

        private void ResetCounter()
        {
            Arrays.Fill(ctr, noncelen + 1, ctr.length, (byte)0);
        }

        private void IncreaseCounter()
        {
            int i = ctr.length - 1;
            while (++ctr[i] == 0)
            {
                --i;
                if (i < noncelen + 1)
                {
                    throw new InvalidOperationException("exceed maximum counter");
                }
            }
        }

        private void ProcessAAD()
        {
            byte[] aad = aadBytes.ToByteArray();
            Arrays.Fill(block, (byte)0);
            int alen = 0;
            if (aad.length < 0xff00)
            {
                alen = 2;
                ToBytes(aad.length, block, 0, 2);
            }
            else
            {
                alen = 6;
                block[0] = (byte)0xff;
                block[1] = (byte)0xfe;
                ToBytes(aad.length, block, 2, 4);
            }

            if (aad.length == 0)
            {
                return;
            }

            int i = 0;
            int remained = aad.length;
            int processed = remained > blocksize - alen ? blocksize - alen : aad.length;
            i += processed;
            remained -= processed;
            System.Arraycopy(aad, 0, block, alen, processed);
            XOR(mac, block);
            engine.ProcessBlock(mac, 0, mac, 0);
            while (remained > 0)
            {
                processed = remained >= blocksize ? blocksize : remained;
                XOR(mac, 0, mac, 0, aad, i, processed);
                engine.ProcessBlock(mac, 0, mac, 0);
                i += processed;
                remained -= processed;
            }
        }

        private void EncryptData(byte[] @out, int offset)
        {
            int inIdx = 0;
            int remained = 0;
            int processed = 0;
            int outIdx = offset;
            byte[] in = inputBytes.ToByteArray();
            remained = msglen;
            while (remained > 0)
            {
                processed = remained >= blocksize ? blocksize : remained;
                XOR(mac, 0, mac, 0, @in, inIdx, processed);
                engine.ProcessBlock(mac, 0, mac, 0);
                IncreaseCounter();
                engine.ProcessBlock(ctr, 0, block, 0);
                XOR(@out, outIdx, block, 0, @in, inIdx, processed);
                inIdx += processed;
                outIdx += processed;
                remained -= processed;
            }
        }

        private void DecryptData(byte[] @out, int offset)
        {
            int i = 0;
            int remained = 0;
            int processed = 0;
            int outIdx = offset;
            byte[] in = inputBytes.ToByteArray();
            System.Arraycopy(@in, msglen, tag, 0, taglen);
            engine.ProcessBlock(ctr, 0, block, 0);
            XOR(tag, 0, block, 0, taglen);
            remained = msglen;
            while (remained > 0)
            {
                processed = remained >= blocksize ? blocksize : remained;
                IncreaseCounter();
                engine.ProcessBlock(ctr, 0, block, 0);
                XOR(@out, outIdx, block, 0, @in, i, processed);
                XOR(mac, 0, mac, 0, @out, outIdx, processed);
                engine.ProcessBlock(mac, 0, mac, 0);
                i += processed;
                outIdx += processed;
                remained -= processed;
            }
        }

        private static void Close(Closeable obj)
        {
            if (obj == null)
            {
                return;
            }

            try
            {
                obj.Close();
            }
            catch (Exception e)
            {
                e.PrintStackTrace();
            }
        }
    }
}