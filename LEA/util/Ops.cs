using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LEA.util
{
	public class Ops
	{
		private Ops()
		{
			throw new AssertionError("Can't create an instance of class Ops");
		}

		/// <summary>
		/// lhs ^= rhs
		/// </summary>
		/// <param name="lhs">
		///            [out]</param>
		/// <param name="rhs">
		///            [in]</param>
		public static void XOR(byte[] lhs, byte[] rhs)
		{
			if (lhs == null || rhs == null)
				throw new NullPointerException("any of input arrarys should not be null");

			if (lhs.length != rhs.length)
				throw new ArgumentException("the length of two arrays should be same");

			for (var i = 0; i < lhs.length; ++i)
			{
				lhs[i] ^= rhs[i];
			}
		}

		/// <summary>
		/// </summary>
		/// <param name="lhs"></param>
		/// <param name="lhsOff"></param>
		/// <param name="rhs"></param>
		/// <param name="rhsOff"></param>
		/// <param name="len"></param>
		public static void XOR(byte[] lhs, int lhsOff, byte[] rhs, int rhsOff, int len)
		{
			if (lhs == null || rhs == null)
				throw new NullPointerException("any of input arrarys should not be null");

			if (lhs.length < lhsOff + len || rhs.length < rhsOff + len)
				throw new ArrayIndexOutOfBoundsException();

			for (var i = 0; i < len; ++i)
			{
				lhs[lhsOff + i] ^= rhs[rhsOff + i];
			}
		}

		/// <summary>
		/// lhs = rhs1 ^ rhs2
		/// </summary>
		/// <param name="lhs">
		///            [out]</param>
		/// <param name="rhs1">
		///            [in]</param>
		/// <param name="rhs2">
		///            [in]</param>
		public static void XOR(byte[] lhs, byte[] rhs1, byte[] rhs2)
		{
			if (lhs == null || rhs1 == null || rhs2 == null)
				throw new NullPointerException("any of input arrarys should not be null");

			if (lhs.length != rhs1.length || lhs.length != rhs2.length)
				throw new ArgumentException("the length of arrays should be same");

			for (var i = 0; i < lhs.length; ++i)
			{
				lhs[i] = (byte)(rhs1[i] ^ rhs2[i]);
			}
		}

		/// <summary>
		/// </summary>
		/// <param name="out"></param>
		/// <param name="outOff"></param>
		/// <param name="len"></param>
		/// <param name="lhs"></param>
		/// <param name="lhs1Off"></param>
		/// <param name="rhs"></param>
		/// <param name="rhsOff"></param>
		public static void XOR(byte[] @out, int outOff, byte[] lhs, int lhs1Off, byte[] rhs, int rhsOff, int len)
		{
			if (@out == null || lhs == null || rhs == null)
				throw new NullPointerException("any of input arrarys should not be null");

			if (@out.length < outOff + len || lhs.length < lhs1Off + len || rhs.length < rhsOff + len)
				throw new ArrayIndexOutOfBoundsException();

			for (var i = 0; i < len; ++i)
			{
				@out[outOff + i] = (byte)(lhs[lhs1Off + i] ^ rhs[rhsOff + i]);
			}
		}

		/// <summary>
		/// lhs ^= rhs
		/// </summary>
		/// <param name="lhs">
		///            [out]</param>
		/// <param name="rhs">
		///            [in]</param>
		public static void XOR(int[] lhs, int[] rhs)
		{
			if (lhs == null || rhs == null)
				throw new NullPointerException("any of input arrarys should not be null");

			if (lhs.length != rhs.length)
				throw new ArgumentException("the length of two arrays should be same");

			for (var i = 0; i < lhs.length; ++i)
			{
				lhs[i] ^= rhs[i];
			}
		}

		public static void XOR(int[] lhs, int lhsOff, int[] rhs, int rhsOff, int len)
		{
			if (lhs == null || rhs == null)
				throw new NullPointerException("any of input arrarys should not be null");

			if (lhs.length < lhsOff + len || rhs.length < rhsOff + len)
				throw new ArrayIndexOutOfBoundsException();

			for (var i = 0; i < len; ++i)
			{
				lhs[lhsOff + i] ^= rhs[rhsOff + i];
			}
		}

		/// <summary>
		/// lhs = rhs1 ^ rhs2
		/// </summary>
		/// <param name="lhs">
		///            [out]</param>
		/// <param name="rhs1">
		///            [in]</param>
		/// <param name="rhs2">
		///            [in]</param>
		public static void XOR(int[] lhs, int[] rhs1, int[] rhs2)
		{
			if (lhs == null || rhs1 == null || rhs2 == null)
				throw new NullPointerException("any of input arrarys should not be null");

			if (lhs.length != rhs1.length || lhs.length != rhs2.length)
				throw new ArgumentException("the length of arrays should be same");

			for (var i = 0; i < lhs.length; ++i)
			{
				lhs[i] = rhs1[i] ^ rhs2[i];
			}
		}

		/// <summary>
		/// </summary>
		/// <param name="lhs"></param>
		/// <param name="lhsOff"></param>
		/// <param name="len"></param>
		/// <param name="rhs1"></param>
		/// <param name="rhs1Off"></param>
		/// <param name="rhs2"></param>
		/// <param name="rhs2Off"></param>
		public static void XOR(int[] lhs, int lhsOff, int[] rhs1, int rhs1Off, int[] rhs2, int rhs2Off, int len)
		{
			if (lhs == null || rhs1 == null || rhs2 == null)
				throw new NullPointerException("any of input arrarys should not be null");

			if (lhs.length < lhsOff + len || rhs1.length < rhs1Off + len || rhs2.length < rhs2Off + len)
				throw new ArrayIndexOutOfBoundsException();

			for (var i = 0; i < len; ++i)
			{
				lhs[lhsOff + i] = rhs1[rhs1Off + i] ^ rhs2[rhs2Off + i];
			}
		}

		public static void ShiftLeft(byte[] bytes, int shift)
		{
			if (bytes == null)
				throw new NullPointerException("input array should not be null");

			if (shift < 1 || shift > 7)
				throw new ArgumentException("the allowed shift amount is 1~7");

			int tmp = bytes[0];
			for (var i = 1; i < bytes.length; ++i)
			{
				tmp = tmp << 8 | bytes[i] & 0xff;
				bytes[i - 1] = (byte)((tmp << shift & 0xff00) >> 8);
			}

			bytes[bytes.length - 1] <<= shift;
		}

		public static void ShiftRight(byte[] bytes, int shift)
		{
			if (bytes == null)
				throw new NullPointerException("input array should not be null");

			if (shift < 1 || shift > 7)
				throw new ArgumentException("the allowed shift amount is 1~7");

			int tmp = bytes[bytes.length - 1];
			for (int i = bytes.length - 1; i > 0; --i)
			{
				tmp = bytes[i - 1] << 8 | bytes[i] & 0xff;
				bytes[i] = (byte)(tmp >> shift);
			}

			tmp = bytes[0] & 0xff;
			bytes[0] = (byte)(tmp >> shift);
		}

		/// <summary>
		/// byte array to int array
		/// </summary>
		public static void Pack(byte[] @in, int[] @out)
		{
			if (@in == null || @out == null)
				throw new NullPointerException();

			if (@in.length != @out.length * 4)
				throw new ArrayIndexOutOfBoundsException();

			var outIdx = 0;
			for (var inIdx = 0; inIdx < @in.length; ++inIdx, ++outIdx)
			{
				@out[outIdx] = @in[inIdx] & 0xff;
				@out[outIdx] |= (@in[++inIdx] & 0xff) << 8;
				@out[outIdx] |= (@in[++inIdx] & 0xff) << 16;
				@out[outIdx] |= (@in[++inIdx] & 0xff) << 24;
			}
		}

		public static void Pack(byte[] @in, int inOff, int[] @out, int outOff, int inlen)
		{
			if (@in == null || @out == null)
				throw new NullPointerException();

			if ((inlen & 3) != 0)
				throw new ArgumentException("length should be multiple of 4");

			if (@in.length < inOff + inlen || @out.length < outOff + inlen / 4)
				throw new ArrayIndexOutOfBoundsException();

			var outIdx = outOff;
			var endInIdx = inOff + inlen;
			for (var inIdx = inOff; inIdx < endInIdx; ++inIdx, ++outIdx)
			{
				@out[outIdx] = @in[inIdx] & 0xff;
				@out[outIdx] |= (@in[++inIdx] & 0xff) << 8;
				@out[outIdx] |= (@in[++inIdx] & 0xff) << 16;
				@out[outIdx] |= (@in[++inIdx] & 0xff) << 24;
			}
		}

		/// <summary>
		/// int array to byte array
		/// </summary>
		public static void Unpack(int[] @in, byte[] @out)
		{
			if (@in == null || @out == null)
				throw new NullPointerException();

			if (@in.length * 4 != @out.length)
				throw new ArrayIndexOutOfBoundsException();

			var outIdx = 0;
			for (var inIdx = 0; inIdx < @in.length; ++inIdx, ++outIdx)
			{
				@out[outIdx] = (byte)@in[inIdx];
				@out[++outIdx] = (byte)(@in[inIdx] >> 8);
				@out[++outIdx] = (byte)(@in[inIdx] >> 16);
				@out[++outIdx] = (byte)(@in[inIdx] >> 24);
			}
		}

		public static void Unpack(int[] @in, int inOff, byte[] @out, int outOff, int inlen)
		{
			if (@in == null || @out == null)
				throw new NullPointerException();

			if (@in.length < inOff + inlen || @out.length < outOff + inlen * 4)
				throw new ArrayIndexOutOfBoundsException();

			var outIdx = outOff;
			var endInIdx = inOff + inlen;
			for (var inIdx = inOff; inIdx < endInIdx; ++inIdx, ++outIdx)
			{
				@out[outIdx] = (byte)@in[inIdx];
				@out[++outIdx] = (byte)(@in[inIdx] >> 8);
				@out[++outIdx] = (byte)(@in[inIdx] >> 16);
				@out[++outIdx] = (byte)(@in[inIdx] >> 24);
			}
		}

		public static void XOR(long[] lhs, int lhsOff, long[] rhs, int rhsOff, int len)
		{
			if (lhs == null || rhs == null)
				throw new NullPointerException("any of input arrarys should not be null");

			if (lhs.length < lhsOff + len || rhs.length < rhsOff + len)
				throw new ArrayIndexOutOfBoundsException();

			for (var i = 0; i < len; ++i)
			{
				lhs[lhsOff + i] ^= rhs[rhsOff + i];
			}
		}
	}
}