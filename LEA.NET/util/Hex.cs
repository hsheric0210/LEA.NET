using System.Text;

namespace LEA.NET.Util;

public class Hex
    {
	private Hex() => throw new AssertionError("Can't create an instance of class Hex");

	public static byte[] DecodeHexString(string hexString)
        {
            if (hexString == null)
            {
                return null;
            }

            var buf = new byte[hexString.Length() / 2];
            for (var i = 0; i < buf.Length; ++i)
            {
                buf[i] = (byte)(16 * DecodeHexChar(hexString.CharAt(i * 2)));
                buf[i] += DecodeHexChar(hexString.CharAt(i * 2 + 1));
            }

            return buf;
        }

        private static byte DecodeHexChar(char ch)
        {
            if (ch >= '0' && ch <= '9')
            {
                return (byte)(ch - '0');
            }

            if (ch >= 'a' && ch <= 'f')
            {
                return (byte)(ch - 'a' + 10);
            }

            if (ch >= 'A' && ch <= 'F')
            {
                return (byte)(ch - 'A' + 10);
            }

            return 0;
        }

        public static byte[] ToBytes(int value, int len)
        {
            var buf = new byte[len];
            ToBytes(value, buf, 0, len);
            return buf;
        }

        public static void ToBytes(long value, byte[] buf, int offset, int len)
        {
            if (len <= 0)
            {
                throw new ArgumentException("len should be positive integer");
            }

            for (int i = offset + len - 1, shift = 0; i >= offset; --i, shift += 8)
            {
                buf[i] = (byte)((value >> shift) & 0xff);
            }
        }

        /// <summary>
        /// print the content of a byte buffer to the system output as a hex string
        /// </summary>
        /// <param name="title"></param>
        /// <param name="buf"></param>
        public static void PrintHex(string title, byte[] buf)
        {
            if (buf == null)
            {
                throw new NullPointerException("input array shoud not be null");
            }

            System.@out.Print(title + "(" + buf.Length + ") ");
            System.@out.Println(ToHexString(buf));
        }

        /// <summary>
        /// print the content of an int buffer to the system output as a hex string
        /// </summary>
        /// <param name="title"></param>
        /// <param name="buf"></param>
        public static void PrintHex(string title, int[] buf)
        {
            if (buf == null)
            {
                throw new NullPointerException("input array shoud not be null");
            }

            System.@out.Print(title + "(" + buf.Length + ") ");
            System.@out.Println(ToHexString(buf));
        }

        /// <summary>
        /// convert byte buffer to hex string
        /// </summary>
        /// <param name="buf"></param>
        /// <returns></returns>
        public static string ToHexString(byte[] buf)
        {
            if (buf == null)
            {
                return null;
            }

            return ToHexString(buf, 0, buf.Length, 0);
        }

        public static string ToHexString(byte[] buf, int indent)
        {
            if (buf == null)
            {
                throw null;
            }

            return ToHexString(buf, 0, buf.Length, indent);
        }

        public static string ToHexString(byte[] buf, int offset, int len, int indent)
        {
            if (buf == null)
            {
                return null;
            }

            if (buf.Length < offset + len)
            {
                throw new ArgumentException("buffer length is not enough");
            }

            var sb = new StringBuilder();
            var index = 0;
            for (var i = offset; i < offset + len; ++i)
            {
                sb.Append(String.Format("%02x", buf[i]));
                ++index;
                if (index != len && indent != 0 && (index % indent) == 0)
                {
                    sb.Append(" ");
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// convert int buffer to hex string
        /// </summary>
        /// <param name="buf"></param>
        /// <returns></returns>
        public static string ToHexString(int[] buf)
        {
            if (buf == null)
            {
                return null;
            }

            var sb = new StringBuilder();
            foreach (var ch in buf)
            {
                sb.Append(String.Format("%08x", ch));
                sb.Append("  ");
            }

            return sb.ToString();
        }

        public static string ToBitString(byte[] @in)
        {
            if (@in == null)
            {
                throw new NullPointerException("input array shoud not be null");
            }

            var sb = new StringBuilder();
            foreach (var i in @in)
            {
                sb.Append(ToBitString(i));
            }

            return sb.ToString();
        }

        public static string ToBitString(byte @in)
        {
            var sb = new StringBuilder();
            for (var i = 7; i >= 0; --i)
            {
                sb.Append(@in >> i & 1);
            }

            return sb.ToString();
        }

        public static string ToBitString(int[] @in)
        {
            if (@in == null)
            {
                throw new NullPointerException("input array shoud not be null");
            }

            var sb = new StringBuilder();
            foreach (var i in @in)
            {
                sb.Append(ToBitString(i));
            }

            return sb.ToString();
        }

        public static string ToBitString(int @in)
        {
            var sb = new StringBuilder();
            for (var i = 31; i >= 0; --i)
            {
                sb.Append(@in >> i & 1);
            }

            return sb.ToString();
        }
    }