using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace LEA.util
{
	public abstract class Pack
	{
		private Pack()
		{
			Debug.Assert(true, "Can't create an instance of class Pack");
		}

		public static int BigEndianToInt(byte[] bs, int off)
		{
			var n = bs[off] << 24;
			n |= (bs[++off] & 0xff) << 16;
			n |= (bs[++off] & 0xff) << 8;
			n |= bs[++off] & 0xff;
			return n;
		}

		public static void BigEndianToInt(byte[] bs, int off, int[] ns)
		{
			for (var i = 0; i < ns.Length; ++i)
			{
				ns[i] = BigEndianToInt(bs, off);
				off += 4;
			}
		}

		public static byte[] IntToBigEndian(int n)
		{
			var bs = new byte[4];
			IntToBigEndian(n, bs, 0);
			return bs;
		}

		public static void IntToBigEndian(int n, byte[] bs, int off)
		{
			bs[off] = (byte)(n >> 24);
			bs[++off] = (byte)(n >> 16);
			bs[++off] = (byte)(n >> 8);
			bs[++off] = (byte)n;
		}

		public static byte[] IntToBigEndian(int[] ns)
		{
			var bs = new byte[4 * ns.Length];
			IntToBigEndian(ns, bs, 0);
			return bs;
		}

		public static void IntToBigEndian(int[] ns, byte[] bs, int off)
		{
			for (var i = 0; i < ns.Length; ++i)
			{
				IntToBigEndian(ns[i], bs, off);
				off += 4;
			}
		}

		public static long BigEndianToLong(byte[] bs, int off)
		{
			var hi = BigEndianToInt(bs, off);
			var lo = BigEndianToInt(bs, off + 4);
			return (hi & 4294967295L) << 32 | lo & 4294967295L;
		}

		public static void BigEndianToLong(byte[] bs, int off, long[] ns)
		{
			for (var i = 0; i < ns.Length; ++i)
			{
				ns[i] = BigEndianToLong(bs, off);
				off += 8;
			}
		}

		public static byte[] LongToBigEndian(long n)
		{
			var bs = new byte[8];
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
			var bs = new byte[8 * ns.Length];
			LongToBigEndian(ns, bs, 0);
			return bs;
		}

		public static void LongToBigEndian(long[] ns, byte[] bs, int off)
		{
			for (var i = 0; i < ns.Length; ++i)
			{
				LongToBigEndian(ns[i], bs, off);
				off += 8;
			}
		}

		public static int LittleEndianToInt(byte[] bs, int off)
		{
			var n = bs[off] & 0xff;
			n |= (bs[++off] & 0xff) << 8;
			n |= (bs[++off] & 0xff) << 16;
			n |= bs[++off] << 24;
			return n;
		}

		public static void LittleEndianToInt(byte[] bs, int off, int[] ns)
		{
			for (var i = 0; i < ns.Length; ++i)
			{
				ns[i] = LittleEndianToInt(bs, off);
				off += 4;
			}
		}

		public static void LittleEndianToInt(byte[] bs, int bOff, int[] ns, int nOff, int count)
		{
			for (var i = 0; i < count; ++i)
			{
				ns[nOff + i] = LittleEndianToInt(bs, bOff);
				bOff += 4;
			}
		}

		public static byte[] IntToLittleEndian(int n)
		{
			var bs = new byte[4];
			IntToLittleEndian(n, bs, 0);
			return bs;
		}

		public static void IntToLittleEndian(int n, byte[] bs, int off)
		{
			bs[off] = (byte)n;
			bs[++off] = (byte)(n >> 8);
			bs[++off] = (byte)(n >> 16);
			bs[++off] = (byte)(n >> 24);
		}

		public static byte[] IntToLittleEndian(int[] ns)
		{
			var bs = new byte[4 * ns.Length];
			IntToLittleEndian(ns, bs, 0);
			return bs;
		}

		public static void IntToLittleEndian(int[] ns, byte[] bs, int off)
		{
			for (var i = 0; i < ns.Length; ++i)
			{
				IntToLittleEndian(ns[i], bs, off);
				off += 4;
			}
		}

		public static long LittleEndianToLong(byte[] bs, int off)
		{
			var lo = LittleEndianToInt(bs, off);
			var hi = LittleEndianToInt(bs, off + 4);
			return (hi & 4294967295L) << 32 | lo & 4294967295L;
		}

		public static void LittleEndianToLong(byte[] bs, int off, long[] ns)
		{
			for (var i = 0; i < ns.Length; ++i)
			{
				ns[i] = LittleEndianToLong(bs, off);
				off += 8;
			}
		}

		public static byte[] LongToLittleEndian(long n)
		{
			var bs = new byte[8];
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
			var bs = new byte[8 * ns.Length];
			LongToLittleEndian(ns, bs, 0);
			return bs;
		}

		public static void LongToLittleEndian(long[] ns, byte[] bs, int off)
		{
			for (var i = 0; i < ns.Length; ++i)
			{
				LongToLittleEndian(ns[i], bs, off);
				off += 8;
			}
		}
	}
}