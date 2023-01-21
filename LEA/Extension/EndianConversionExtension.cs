namespace LEA.Extension;

public static class EndianConversionExtension
{
	public static int BigEndianToInt(this byte[] bytes, int offset)
	{
		var n = bytes[offset++] << 24;
		n |= (bytes[offset++] & 0xff) << 16;
		n |= (bytes[offset++] & 0xff) << 8;
		n |= bytes[offset] & 0xff;
		return n;
	}

	public static void BigEndianToInt(this byte[] bytes, int offset, int[] numbers)
	{
		for (var i = 0; i < numbers.Length; ++i)
		{
			numbers[i] = BigEndianToInt(bytes, offset);
			offset += 4;
		}
	}

	public static byte[] IntToBigEndian(this int n)
	{
		var bytes = new byte[4];
		IntToBigEndian(n, bytes, 0);
		return bytes;
	}

	public static void IntToBigEndian(this int n, byte[] bytes, int offset)
	{
		bytes[offset++] = (byte)(n >> 24);
		bytes[offset++] = (byte)(n >> 16);
		bytes[offset++] = (byte)(n >> 8);
		bytes[offset] = (byte)n;
	}

	public static byte[] IntToBigEndian(this int[] numbers)
	{
		var bytes = new byte[4 * numbers.Length];
		IntToBigEndian(numbers, bytes, 0);
		return bytes;
	}

	public static void IntToBigEndian(this int[] numbers, byte[] bytes, int offset)
	{
		for (var i = 0; i < numbers.Length; ++i)
		{
			IntToBigEndian(numbers[i], bytes, offset);
			offset += 4;
		}
	}

	public static long BigEndianToLong(this byte[] bytes, int offset)
	{
		var hi = BigEndianToInt(bytes, offset);
		var lo = BigEndianToInt(bytes, offset + 4);
		return (hi & 4294967295L) << 32 | lo & 4294967295L;
	}

	public static void BigEndianToLong(this byte[] bytes, int offset, long[] numbers)
	{
		for (var i = 0; i < numbers.Length; ++i)
		{
			numbers[i] = BigEndianToLong(bytes, offset);
			offset += 8;
		}
	}

	public static byte[] LongToBigEndian(this long n)
	{
		var bytes = new byte[8];
		LongToBigEndian(n, bytes, 0);
		return bytes;
	}

	public static void LongToBigEndian(this long n, byte[] bytes, int offset)
	{
		IntToBigEndian((int)(n >> 32), bytes, offset);
		IntToBigEndian((int)(n & 4294967295L), bytes, offset + 4);
	}

	public static byte[] LongToBigEndian(this long[] numbers)
	{
		var bytes = new byte[8 * numbers.Length];
		LongToBigEndian(numbers, bytes, 0);
		return bytes;
	}

	public static void LongToBigEndian(this long[] numbers, byte[] bytes, int offset)
	{
		for (var i = 0; i < numbers.Length; ++i)
		{
			LongToBigEndian(numbers[i], bytes, offset);
			offset += 8;
		}
	}

	public static int LittleEndianToInt(this byte[] bytes, int offset)
	{
		var n = bytes[offset++] & 0xff;
		n |= (bytes[offset++] & 0xff) << 8;
		n |= (bytes[offset++] & 0xff) << 16;
		n |= bytes[offset] << 24;
		return n;
	}

	public static void LittleEndianToInt(this byte[] bytes, int offset, int[] numbers)
	{
		for (var i = 0; i < numbers.Length; ++i)
		{
			numbers[i] = LittleEndianToInt(bytes, offset);
			offset += 4;
		}
	}

	public static void LittleEndianToInt(this byte[] bytes, int bOff, int[] numbers, int numberOffset, int count)
	{
		for (var i = 0; i < count; ++i)
		{
			numbers[numberOffset + i] = LittleEndianToInt(bytes, bOff);
			bOff += 4;
		}
	}

	public static byte[] IntToLittleEndian(this int n)
	{
		var bytes = new byte[4];
		IntToLittleEndian(n, bytes, 0);
		return bytes;
	}

	public static void IntToLittleEndian(this int n, byte[] bytes, int offset)
	{
		bytes[offset++] = (byte)n;
		bytes[offset++] = (byte)(n >> 8);
		bytes[offset++] = (byte)(n >> 16);
		bytes[offset] = (byte)(n >> 24);
	}

	public static byte[] IntToLittleEndian(this int[] numbers)
	{
		var bytes = new byte[4 * numbers.Length];
		IntToLittleEndian(numbers, bytes, 0);
		return bytes;
	}

	public static void IntToLittleEndian(this int[] numbers, byte[] bytes, int offset)
	{
		for (var i = 0; i < numbers.Length; ++i)
		{
			IntToLittleEndian(numbers[i], bytes, offset);
			offset += 4;
		}
	}

	public static long LittleEndianToLong(byte[] bytes, int offset)
	{
		var lo = LittleEndianToInt(bytes, offset);
		var hi = LittleEndianToInt(bytes, offset + 4);
		return (hi & 4294967295L) << 32 | lo & 4294967295L;
	}

	public static void LittleEndianToLong(byte[] bytes, int offset, long[] numbers)
	{
		for (var i = 0; i < numbers.Length; ++i)
		{
			numbers[i] = LittleEndianToLong(bytes, offset);
			offset += 8;
		}
	}

	public static byte[] LongToLittleEndian(this long n)
	{
		var bytes = new byte[8];
		LongToLittleEndian(n, bytes, 0);
		return bytes;
	}

	public static void LongToLittleEndian(this long n, byte[] bytes, int offset)
	{
		IntToLittleEndian((int)(n & 4294967295L), bytes, offset);
		IntToLittleEndian((int)(n >> 32), bytes, offset + 4);
	}

	public static byte[] LongToLittleEndian(this long[] numbers)
	{
		var bytes = new byte[8 * numbers.Length];
		LongToLittleEndian(numbers, bytes, 0);
		return bytes;
	}

	public static void LongToLittleEndian(this long[] numbers, byte[] bytes, int offset)
	{
		for (var i = 0; i < numbers.Length; ++i)
		{
			LongToLittleEndian(numbers[i], bytes, offset);
			offset += 8;
		}
	}
}