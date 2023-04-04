using System;

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

		public static void FillBy<T>(this T[] array, T value) =>
#if NETSTANDARD2_1_OR_GREATER
			Array.Fill(array, value);
#else
			for (int i = 0, j = array.Length; i < j; i++)
				array[i] = value;
#endif


		public static void FillBy<T>(this T[] array, int startIndexInclusive, int endIndexExclusive, T value) =>
#if NETSTANDARD2_1_OR_GREATER
			array.AsSpan()[startIndexInclusive..endIndexExclusive].Fill(value);
#else
			for (var i = startIndexInclusive; i < endIndexExclusive; i++)
				array[i] = value;
#endif

	}
}
