namespace LEA.NET.Padding;

public class PKCS5Padding : Padding
    {
        public PKCS5Padding(int blocksize): base(blocksize)
        {
        }

        public override byte[] Pad(byte[] @in)
        {
            if (@in == null)
            {
                throw new NullPointerException();
            }

            if (@in.Length < 0 || @in.Length > blocksize)
            {
                throw new InvalidOperationException("input should be shorter than blocksize");
            }

            byte[] out = new byte[blocksize];
            Array.Copy(@in, 0, @out, 0, @in.Length);
            Pad(@out, @in.Length);
            return @out;
        }

        public override void Pad(byte[] @in, int inOff)
        {
            if (@in == null)
            {
                throw new NullPointerException();
            }

            if (@in.Length < inOff)
            {
                throw new ArgumentException();
            }

            var code = (byte)(@in.Length - inOff);
            Array.Fill(@in, inOff, @in.Length, code);
        }

        public override byte[] Unpad(byte[] @in)
        {
            if (@in == null || @in.Length < 1)
            {
                throw new NullPointerException();
            }

            if (@in.Length % blocksize != 0)
            {
                throw new ArgumentException("Bad padding");
            }

            var cnt = @in.Length - GetPadCount(@in);
            if (cnt == 0)
            {
                return null;
            }

            byte[] out = new byte[cnt];
            Array.Copy(@in, 0, @out, 0, @out.Length);
            return @out;
        }

        public override int GetPadCount(byte[] @in)
        {
            if (@in == null || @in.Length < 1)
            {
                throw new NullPointerException();
            }

            if (@in.Length % blocksize != 0)
            {
                throw new ArgumentException("Bad padding");
            }

            var count = @in[@in.Length - 1] & 0xff;
            var isBadPadding = false;
            var lower_bound = @in.Length - count;
            for (int i = in.Length - 1; i > lower_bound; --i)
            {
                if (@in[i] != count)
                {
                    isBadPadding = true;
                }
            }

            if (isBadPadding)
            {
                throw new InvalidOperationException("Bad Padding");
            }

            return count;
        }
    }