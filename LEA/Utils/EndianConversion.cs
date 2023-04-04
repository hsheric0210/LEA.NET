namespace LEA.Utils
{
	public static class EndianConversion
	{
		public static int BigEndianToInt(ReadOnlySpan<byte> bytes, int offset)
		{
			var n = bytes[offset++] << 24;
			n |= (bytes[offset++] & 0xff) << 16;
			n |= (bytes[offset++] & 0xff) << 8;
			n |= bytes[offset] & 0xff;
			return n;
		}

		public static void BigEndianToInt(ReadOnlySpan<byte> bytes, int offset, int[] numbers)
		{
			for (var i = 0; i < numbers.Length; ++i)
			{
				numbers[i] = BigEndianToInt(bytes, offset);
				offset += 4;
			}
		}

		public static ReadOnlySpan<byte> IntToBigEndian(int n)
		{
			var bytes = new byte[4];
			IntToBigEndian(n, bytes, 0);
			return bytes;
		}

		public static void IntToBigEndian(int n, Span<byte> bytes, int offset)
		{
			bytes[offset++] = (byte)(n >> 24);
			bytes[offset++] = (byte)(n >> 16);
			bytes[offset++] = (byte)(n >> 8);
			bytes[offset] = (byte)n;
		}

		public static ReadOnlySpan<byte> IntToBigEndian(int[] numbers)
		{
			var bytes = new byte[4 * numbers.Length];
			IntToBigEndian(numbers, bytes, 0);
			return bytes;
		}

		public static void IntToBigEndian(int[] numbers, Span<byte> bytes, int offset)
		{
			for (var i = 0; i < numbers.Length; ++i)
			{
				IntToBigEndian(numbers[i], bytes, offset);
				offset += 4;
			}
		}

		public static long BigEndianToLong(ReadOnlySpan<byte> bytes, int offset)
		{
			var high = BigEndianToInt(bytes, offset);
			var low = BigEndianToInt(bytes, offset + 4);
			return (high & 4294967295L) << 32 | low & 4294967295L;
		}

		public static void BigEndianToLong(ReadOnlySpan<byte> bytes, int offset, long[] numbers)
		{
			for (var i = 0; i < numbers.Length; ++i)
			{
				numbers[i] = BigEndianToLong(bytes, offset);
				offset += 8;
			}
		}

		public static ReadOnlySpan<byte> LongToBigEndian(long n)
		{
			var bytes = new byte[8];
			LongToBigEndian(n, bytes, 0);
			return bytes;
		}

		public static void LongToBigEndian(long n, Span<byte> bytes, int offset)
		{
			IntToBigEndian((int)(n >> 32), bytes, offset);
			IntToBigEndian((int)(n & 4294967295L), bytes, offset + 4);
		}

		public static ReadOnlySpan<byte> LongToBigEndian(long[] numbers)
		{
			var bytes = new byte[8 * numbers.Length];
			LongToBigEndian(numbers, bytes, 0);
			return bytes;
		}

		public static void LongToBigEndian(long[] numbers, Span<byte> bytes, int offset)
		{
			for (var i = 0; i < numbers.Length; ++i)
			{
				LongToBigEndian(numbers[i], bytes, offset);
				offset += 8;
			}
		}

		public static int LittleEndianToInt(ReadOnlySpan<byte> bytes, int offset)
		{
			var n = bytes[offset++] & 0xff;
			n |= (bytes[offset++] & 0xff) << 8;
			n |= (bytes[offset++] & 0xff) << 16;
			n |= bytes[offset] << 24;
			return n;
		}

		public static void LittleEndianToInt(ReadOnlySpan<byte> bytes, int offset, int[] numbers)
		{
			for (var i = 0; i < numbers.Length; ++i)
			{
				numbers[i] = LittleEndianToInt(bytes, offset);
				offset += 4;
			}
		}

		public static void LittleEndianToInt(ReadOnlySpan<byte> bytes, int bOff, int[] numbers, int numberOffset, int count)
		{
			for (var i = 0; i < count; ++i)
			{
				numbers[numberOffset + i] = LittleEndianToInt(bytes, bOff);
				bOff += 4;
			}
		}

		public static ReadOnlySpan<byte> IntToLittleEndian(int n)
		{
			var bytes = new byte[4];
			IntToLittleEndian(n, bytes, 0);
			return bytes;
		}

		public static void IntToLittleEndian(int n, Span<byte> bytes, int offset)
		{
			bytes[offset++] = (byte)n;
			bytes[offset++] = (byte)(n >> 8);
			bytes[offset++] = (byte)(n >> 16);
			bytes[offset] = (byte)(n >> 24);
		}

		public static ReadOnlySpan<byte> IntToLittleEndian(int[] numbers)
		{
			var bytes = new byte[4 * numbers.Length];
			IntToLittleEndian(numbers, bytes, 0);
			return bytes;
		}

		public static void IntToLittleEndian(int[] numbers, Span<byte> bytes, int offset)
		{
			for (var i = 0; i < numbers.Length; ++i)
			{
				IntToLittleEndian(numbers[i], bytes, offset);
				offset += 4;
			}
		}

		public static long LittleEndianToLong(ReadOnlySpan<byte> bytes, int offset)
		{
			var lo = LittleEndianToInt(bytes, offset);
			var hi = LittleEndianToInt(bytes, offset + 4);
			return (hi & 4294967295L) << 32 | lo & 4294967295L;
		}

		public static void LittleEndianToLong(ReadOnlySpan<byte> bytes, int offset, long[] numbers)
		{
			for (var i = 0; i < numbers.Length; ++i)
			{
				numbers[i] = LittleEndianToLong(bytes, offset);
				offset += 8;
			}
		}

		public static ReadOnlySpan<byte> LongToLittleEndian(long n)
		{
			var bytes = new byte[8];
			LongToLittleEndian(n, bytes, 0);
			return bytes;
		}

		public static void LongToLittleEndian(long n, Span<byte> bytes, int offset)
		{
			IntToLittleEndian((int)(n & 0xFFFFFFFFL), bytes, offset);
			IntToLittleEndian((int)(n >> 32), bytes, offset + 4);
		}

		public static ReadOnlySpan<byte> LongToLittleEndian(long[] numbers)
		{
			var bytes = new byte[8 * numbers.Length];
			LongToLittleEndian(numbers, bytes, 0);
			return bytes;
		}

		public static void LongToLittleEndian(long[] numbers, Span<byte> bytes, int offset)
		{
			for (var i = 0; i < numbers.Length; ++i)
			{
				LongToLittleEndian(numbers[i], bytes, offset);
				offset += 8;
			}
		}
	}
}