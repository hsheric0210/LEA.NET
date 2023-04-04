using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kr.Re.Nsr.Crypto.Util
{
    public abstract class Pack
    {
        private Pack()
        {
            throw new AssertionError("Can't create an instance of class Pack");
        }

        public static int BigEndianToInt(byte[] bs, int off)
        {
            int n = bs[off] << 24;
            n |= (bs[++off] & 0xff) << 16;
            n |= (bs[++off] & 0xff) << 8;
            n |= (bs[++off] & 0xff);
            return n;
        }

        public static void BigEndianToInt(byte[] bs, int off, int[] ns)
        {
            for (int i = 0; i < ns.length; ++i)
            {
                ns[i] = BigEndianToInt(bs, off);
                off += 4;
            }
        }

        public static byte[] IntToBigEndian(int n)
        {
            byte[] bs = new byte[4];
            IntToBigEndian(n, bs, 0);
            return bs;
        }

        public static void IntToBigEndian(int n, byte[] bs, int off)
        {
            bs[off] = (byte)(n >> 24);
            bs[++off] = (byte)(n >> 16);
            bs[++off] = (byte)(n >> 8);
            bs[++off] = (byte)(n);
        }

        public static byte[] IntToBigEndian(int[] ns)
        {
            byte[] bs = new byte[4 * ns.length];
            IntToBigEndian(ns, bs, 0);
            return bs;
        }

        public static void IntToBigEndian(int[] ns, byte[] bs, int off)
        {
            for (int i = 0; i < ns.length; ++i)
            {
                IntToBigEndian(ns[i], bs, off);
                off += 4;
            }
        }

        public static long BigEndianToLong(byte[] bs, int off)
        {
            int hi = BigEndianToInt(bs, off);
            int lo = BigEndianToInt(bs, off + 4);
            return ((long)(hi & 4294967295L) << 32) | (long)(lo & 4294967295L);
        }

        public static void BigEndianToLong(byte[] bs, int off, long[] ns)
        {
            for (int i = 0; i < ns.length; ++i)
            {
                ns[i] = BigEndianToLong(bs, off);
                off += 8;
            }
        }

        public static byte[] LongToBigEndian(long n)
        {
            byte[] bs = new byte[8];
            LongToBigEndian(n, bs, 0);
            return bs;
        }

        public static void LongToBigEndian(long n, byte[] bs, int off)
        {
            IntToBigEndian((int)(n >> 32), bs, off);
            IntToBigEndian((int)(n & 4294967295L), bs, off + 4);
        }

        public static byte[] LongToBigEndian(long[] ns)
        {
            byte[] bs = new byte[8 * ns.length];
            LongToBigEndian(ns, bs, 0);
            return bs;
        }

        public static void LongToBigEndian(long[] ns, byte[] bs, int off)
        {
            for (int i = 0; i < ns.length; ++i)
            {
                LongToBigEndian(ns[i], bs, off);
                off += 8;
            }
        }

        public static int LittleEndianToInt(byte[] bs, int off)
        {
            int n = bs[off] & 0xff;
            n |= (bs[++off] & 0xff) << 8;
            n |= (bs[++off] & 0xff) << 16;
            n |= bs[++off] << 24;
            return n;
        }

        public static void LittleEndianToInt(byte[] bs, int off, int[] ns)
        {
            for (int i = 0; i < ns.length; ++i)
            {
                ns[i] = LittleEndianToInt(bs, off);
                off += 4;
            }
        }

        public static void LittleEndianToInt(byte[] bs, int bOff, int[] ns, int nOff, int count)
        {
            for (int i = 0; i < count; ++i)
            {
                ns[nOff + i] = LittleEndianToInt(bs, bOff);
                bOff += 4;
            }
        }

        public static byte[] IntToLittleEndian(int n)
        {
            byte[] bs = new byte[4];
            IntToLittleEndian(n, bs, 0);
            return bs;
        }

        public static void IntToLittleEndian(int n, byte[] bs, int off)
        {
            bs[off] = (byte)(n);
            bs[++off] = (byte)(n >> 8);
            bs[++off] = (byte)(n >> 16);
            bs[++off] = (byte)(n >> 24);
        }

        public static byte[] IntToLittleEndian(int[] ns)
        {
            byte[] bs = new byte[4 * ns.length];
            IntToLittleEndian(ns, bs, 0);
            return bs;
        }

        public static void IntToLittleEndian(int[] ns, byte[] bs, int off)
        {
            for (int i = 0; i < ns.length; ++i)
            {
                IntToLittleEndian(ns[i], bs, off);
                off += 4;
            }
        }

        public static long LittleEndianToLong(byte[] bs, int off)
        {
            int lo = LittleEndianToInt(bs, off);
            int hi = LittleEndianToInt(bs, off + 4);
            return ((long)(hi & 4294967295L) << 32) | (long)(lo & 4294967295L);
        }

        public static void LittleEndianToLong(byte[] bs, int off, long[] ns)
        {
            for (int i = 0; i < ns.length; ++i)
            {
                ns[i] = LittleEndianToLong(bs, off);
                off += 8;
            }
        }

        public static byte[] LongToLittleEndian(long n)
        {
            byte[] bs = new byte[8];
            LongToLittleEndian(n, bs, 0);
            return bs;
        }

        public static void LongToLittleEndian(long n, byte[] bs, int off)
        {
            IntToLittleEndian((int)(n & 4294967295L), bs, off);
            IntToLittleEndian((int)(n >> 32), bs, off + 4);
        }

        public static byte[] LongToLittleEndian(long[] ns)
        {
            byte[] bs = new byte[8 * ns.length];
            LongToLittleEndian(ns, bs, 0);
            return bs;
        }

        public static void LongToLittleEndian(long[] ns, byte[] bs, int off)
        {
            for (int i = 0; i < ns.length; ++i)
            {
                LongToLittleEndian(ns[i], bs, off);
                off += 8;
            }
        }
    }
}