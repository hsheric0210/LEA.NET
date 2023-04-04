using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEA
{
	public static class ArrayCompatExtension
	{
		public static T[] CopyOf<T>(this T[] array) => array.CopyOf(0, array.Length);

		public static T[] CopyOf<T>(this T[] array, int length) => array.CopyOf(0, length);

		public static T[] CopyOf<T>(this T[] array, int offset, int length)
		{
			var newArray = new T[length];
			if (typeof(T) == typeof(byte))
				Buffer.BlockCopy(array, offset, newArray, 0, length);
			else
				Array.Copy(array, offset, newArray, 0, length);
			return newArray;
		}

		public static void FillBy<T>(this T[] array, T value) => array.FillBy(array.Length, value);

		public static void FillBy<T>(this T[] array, int length, T value) => array.FillBy(0, length, value);

		public static void FillBy<T>(this T[] array, int offset, int length, T value)
		{
#if NETSTANDARD2_1_OR_GREATER
			Array.Fill(array, value, offset, length);
#else
			for (var i = offset; i < length; i++)
				array[i] = value;
#endif
		}

		public static void FillRange<T>(this T[] array, int startIndexInclusive, int endIndexExclusive, T value) => array.FillBy(startIndexInclusive, endIndexExclusive - startIndexInclusive, value);
	}
}
