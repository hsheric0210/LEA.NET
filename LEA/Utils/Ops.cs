using System;
using System.Diagnostics;

namespace LEA.Utils
{
	public static class Ops
	{
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
				throw new ArgumentNullException("any of input arrarys should not be null");

			if (lhs.Length != rhs.Length)
				throw new ArgumentException("the length of two arrays should be same");

			for (var i = 0; i < lhs.Length; ++i)
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
				throw new ArgumentNullException("any of input arrarys should not be null");

			if (lhs.Length < lhsOff + len || rhs.Length < rhsOff + len)
				throw new IndexOutOfRangeException();

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
				throw new ArgumentNullException("any of input arrarys should not be null");

			if (lhs.Length != rhs1.Length || lhs.Length != rhs2.Length)
				throw new ArgumentException("the length of arrays should be same");

			for (var i = 0; i < lhs.Length; ++i)
			{
				lhs[i] = (byte)(rhs1[i] ^ rhs2[i]);
			}
		}

		/// <summary>
		/// </summary>
		/// <param name="out"></param>
		/// <param name="outOffset"></param>
		/// <param name="len"></param>
		/// <param name="lhs"></param>
		/// <param name="lhs1Off"></param>
		/// <param name="rhs"></param>
		/// <param name="rhsOff"></param>
		public static void XOR(byte[] outBytes, int outOffset, byte[] lhs, int lhs1Off, byte[] rhs, int rhsOff, int len)
		{
			if (outBytes == null || lhs == null || rhs == null)
				throw new ArgumentNullException("any of input arrarys should not be null");

			if (outBytes.Length < outOffset + len || lhs.Length < lhs1Off + len || rhs.Length < rhsOff + len)
				throw new IndexOutOfRangeException();

			for (var i = 0; i < len; ++i)
			{
				outBytes[outOffset + i] = (byte)(lhs[lhs1Off + i] ^ rhs[rhsOff + i]);
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
				throw new ArgumentNullException("any of input arrarys should not be null");

			if (lhs.Length != rhs.Length)
				throw new ArgumentException("the length of two arrays should be same");

			for (var i = 0; i < lhs.Length; ++i)
			{
				lhs[i] ^= rhs[i];
			}
		}

		public static void XOR(int[] lhs, int lhsOff, int[] rhs, int rhsOff, int len)
		{
			if (lhs == null || rhs == null)
				throw new ArgumentNullException("any of input arrarys should not be null");

			if (lhs.Length < lhsOff + len || rhs.Length < rhsOff + len)
				throw new IndexOutOfRangeException();

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
				throw new ArgumentNullException("any of input arrarys should not be null");

			if (lhs.Length != rhs1.Length || lhs.Length != rhs2.Length)
				throw new ArgumentException("the length of arrays should be same");

			for (var i = 0; i < lhs.Length; ++i)
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
				throw new ArgumentNullException("any of input arrarys should not be null");

			if (lhs.Length < lhsOff + len || rhs1.Length < rhs1Off + len || rhs2.Length < rhs2Off + len)
				throw new IndexOutOfRangeException();

			for (var i = 0; i < len; ++i)
			{
				lhs[lhsOff + i] = rhs1[rhs1Off + i] ^ rhs2[rhs2Off + i];
			}
		}

		public static void XOR(long[] lhs, int lhsOff, long[] rhs, int rhsOff, int len)
		{
			if (lhs == null || rhs == null)
				throw new ArgumentNullException("any of input arrarys should not be null");

			if (lhs.Length < lhsOff + len || rhs.Length < rhsOff + len)
				throw new IndexOutOfRangeException();

			for (var i = 0; i < len; ++i)
			{
				lhs[lhsOff + i] ^= rhs[rhsOff + i];
			}
		}

		public static void ShiftLeft(byte[] bytes, int shift)
		{
			if (bytes == null)
				throw new ArgumentNullException("input array should not be null");

			if (shift < 1 || shift > 7)
				throw new ArgumentException("the allowed shift amount is 1~7");

			int tmp = bytes[0];
			for (var i = 1; i < bytes.Length; ++i)
			{
				tmp = tmp << 8 | bytes[i] & 0xff;
				bytes[i - 1] = (byte)((tmp << shift & 0xff00) >> 8);
			}

			bytes[bytes.Length - 1] <<= shift;
		}

		public static void ShiftRight(byte[] bytes, int shift)
		{
			if (bytes == null)
				throw new ArgumentNullException("input array should not be null");

			if (shift < 1 || shift > 7)
				throw new ArgumentException("the allowed shift amount is 1~7");

			int tmp = bytes[bytes.Length - 1];
			for (var i = bytes.Length - 1; i > 0; --i)
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
		public static void Pack(byte[] inBytes, uint[] outBytes)
		{
			if (inBytes == null || outBytes == null)
				throw new ArgumentNullException();

			if (inBytes.Length != outBytes.Length * 4)
				throw new IndexOutOfRangeException();

			var outIdx = 0;
			for (var inIdx = 0; inIdx < inBytes.Length; ++inIdx, ++outIdx)
			{
				outBytes[outIdx] = inBytes[inIdx] & 0xffu;
				outBytes[outIdx] |= (inBytes[++inIdx] & 0xffu) << 8;
				outBytes[outIdx] |= (inBytes[++inIdx] & 0xffu) << 16;
				outBytes[outIdx] |= (inBytes[++inIdx] & 0xffu) << 24;
			}
		}

		public static void Pack(byte[] inBytes, int inOffset, uint[] outBytes, int outOffset, int inlen)
		{
			if (inBytes == null || outBytes == null)
				throw new ArgumentNullException();

			if ((inlen & 3) != 0)
				throw new ArgumentException("length should be multiple of 4");

			if (inBytes.Length < inOffset + inlen || outBytes.Length < outOffset + inlen / 4)
				throw new IndexOutOfRangeException();

			var outIdx = outOffset;
			var endInIdx = inOffset + inlen;
			for (var inIdx = inOffset; inIdx < endInIdx; ++inIdx, ++outIdx)
			{
				outBytes[outIdx] = inBytes[inIdx] & 0xffu;
				outBytes[outIdx] |= (inBytes[++inIdx] & 0xffu) << 8;
				outBytes[outIdx] |= (inBytes[++inIdx] & 0xffu) << 16;
				outBytes[outIdx] |= (inBytes[++inIdx] & 0xffu) << 24;
			}
		}

		/// <summary>
		/// int array to byte array
		/// </summary>
		public static void Unpack(uint[] inBytes, byte[] outBytes)
		{
			if (inBytes == null || outBytes == null)
				throw new ArgumentNullException();

			if (inBytes.Length * 4 != outBytes.Length)
				throw new IndexOutOfRangeException();

			var outIdx = 0;
			for (var inIdx = 0; inIdx < inBytes.Length; ++inIdx, ++outIdx)
			{
				outBytes[outIdx] = (byte)inBytes[inIdx];
				outBytes[++outIdx] = (byte)(inBytes[inIdx] >> 8);
				outBytes[++outIdx] = (byte)(inBytes[inIdx] >> 16);
				outBytes[++outIdx] = (byte)(inBytes[inIdx] >> 24);
			}
		}

		public static void Unpack(uint[] inBytes, int inOffset, byte[] outBytes, int outOffset, int inlen)
		{
			if (inBytes == null || outBytes == null)
				throw new ArgumentNullException();

			if (inBytes.Length < inOffset + inlen || outBytes.Length < outOffset + inlen * 4)
				throw new IndexOutOfRangeException();

			var outIdx = outOffset;
			var endInIdx = inOffset + inlen;
			for (var inIdx = inOffset; inIdx < endInIdx; ++inIdx, ++outIdx)
			{
				outBytes[outIdx] = (byte)inBytes[inIdx];
				outBytes[++outIdx] = (byte)(inBytes[inIdx] >> 8);
				outBytes[++outIdx] = (byte)(inBytes[inIdx] >> 16);
				outBytes[++outIdx] = (byte)(inBytes[inIdx] >> 24);
			}
		}
	}
}