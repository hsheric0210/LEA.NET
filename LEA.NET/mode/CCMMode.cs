namespace LEA.NET.Mode;

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
            ctr = new byte[blockSize];
            mac = new byte[blockSize];
            block = new byte[blockSize];
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
            if (aad == null || aad.Length == 0)
            {
                return;
            }

            aadBytes.Write(aad, 0, aad.Length);
        }

        public override byte[] Update(byte[] msg)
        {
            inputBytes.Write(msg, 0, msg.Length);
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

            msglen = inputBytes.ToByteArray().Length;
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
                Array.Copy(mac, 0, tag, 0, taglen);
                Array.Copy(mac, 0, @out, @out.Length - taglen, taglen);
            }
            else
            {
                mac = Arrays.CopyOf(mac, taglen);
                if (Arrays.Equals(tag, mac) == false)
                {
                    Array.Fill(@out, (byte)0);
                }
            }

            return @out;
        }

        public override int GetOutputSize(int len)
        {
            var outSize = len + bufferOffset;
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

            noncelen = nonce.Length;
            if (noncelen < 7 || noncelen > 13)
            {
                throw new ArgumentException("length of nonce should be 7 ~ 13 bytes");
            }


            // init counter
            ctr[0] = (byte)(14 - noncelen);
            Array.Copy(nonce, 0, ctr, 1, noncelen);

            // init b0
            var tagfield = (taglen - 2) / 2;
            block[0] = (byte)((tagfield << 3) & 0xff);
            block[0] |= (byte)((14 - noncelen) & 0xff);
            Array.Copy(nonce, 0, block, 1, noncelen);
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

	private void ResetCounter() => Array.Fill(ctr, noncelen + 1, ctr.Length, (byte)0);

	private void IncreaseCounter()
        {
            var i = ctr.Length - 1;
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
            Array.Fill(block, (byte)0);
            var alen = 0;
            if (aad.Length < 0xff00)
            {
                alen = 2;
                ToBytes(aad.Length, block, 0, 2);
            }
            else
            {
                alen = 6;
                block[0] = (byte)0xff;
                block[1] = (byte)0xfe;
                ToBytes(aad.Length, block, 2, 4);
            }

            if (aad.Length == 0)
            {
                return;
            }

            var i = 0;
            var remained = aad.Length;
            var processed = remained > blockSize - alen ? blockSize - alen : aad.Length;
            i += processed;
            remained -= processed;
            Array.Copy(aad, 0, block, alen, processed);
            XOR(mac, block);
            engine.ProcessBlock(mac, 0, mac, 0);
            while (remained > 0)
            {
                processed = remained >= blockSize ? blockSize : remained;
                XOR(mac, 0, mac, 0, aad, i, processed);
                engine.ProcessBlock(mac, 0, mac, 0);
                i += processed;
                remained -= processed;
            }
        }

        private void EncryptData(byte[] @out, int offset)
        {
            var inIdx = 0;
            var remained = 0;
            var processed = 0;
            var outIdx = offset;
            byte[] in = inputBytes.ToByteArray();
            remained = msglen;
            while (remained > 0)
            {
                processed = remained >= blockSize ? blockSize : remained;
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
            var i = 0;
            var remained = 0;
            var processed = 0;
            var outIdx = offset;
            byte[] in = inputBytes.ToByteArray();
            Array.Copy(@in, msglen, tag, 0, taglen);
            engine.ProcessBlock(ctr, 0, block, 0);
            XOR(tag, 0, block, 0, taglen);
            remained = msglen;
            while (remained > 0)
            {
                processed = remained >= blockSize ? blockSize : remained;
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