using System;
using System.Text;

namespace LEA.Utils;

public static class HexCoder
{
    public static ReadOnlySpan<byte> DecodeHexString(string hexString)
    {
        if (hexString == null)
            return Array.Empty<byte>();

        var buffer = new byte[hexString.Length / 2];
        for (var i = 0; i < buffer.Length; ++i)
        {
            buffer[i] = (byte)(16 * DecodeHexChar(hexString[i * 2]));
            buffer[i] += DecodeHexChar(hexString[i * 2 + 1]);
        }

        return buffer;
    }

    private static byte DecodeHexChar(char ch)
    {
        if (ch is >= '0' and <= '9')
            return (byte)(ch - '0');
        if (ch is >= 'a' and <= 'f')
            return (byte)(ch - 'a' + 10);
        if (ch is >= 'A' and <= 'F')
            return (byte)(ch - 'A' + 10);
        return 0;
    }

    public static ReadOnlySpan<byte> ToBytes(int value, int length)
    {
        var buffer = new byte[length];
        ToBytes(value, buffer, 0, length);
        return buffer;
    }

    public static void ToBytes(long value, Span<byte> buffer, int offset, int length)
    {
        if (length <= 0)
            throw new ArgumentException("length should be positive integer");

        for (int i = offset + length - 1, shift = 0; i >= offset; --i, shift += 8)
            buffer[i] = (byte)(value >> shift & 0xff);
    }

    /// <summary>
    /// print the content of a byte buffer to the system output as a hex string
    /// </summary>
    /// <param name="title"></param>
    /// <param name="bytes"></param>
    public static void PrintHex(string title, ReadOnlySpan<byte> bytes)
    {
        Console.Out.Write(title + "(" + bytes.Length + ") ");
        Console.Out.WriteLine(ToHexString(bytes));
    }

    /// <summary>
    /// print the content of an int buffer to the system output as a hex string
    /// </summary>
    /// <param name="title"></param>
    /// <param name="bytes"></param>
    public static void PrintHex(string title, ReadOnlySpan<int> bytes)
    {
        Console.Out.Write(title + "(" + bytes.Length + ") ");
        Console.Out.WriteLine(ToHexString(bytes));
    }

    /// <summary>
    /// convert byte buffer to hex string
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns></returns>
    public static string ToHexString(ReadOnlySpan<byte> bytes) => ToHexString(bytes, 0, bytes.Length, 0);

    public static string ToHexString(ReadOnlySpan<byte> bytes, int indent) => ToHexString(bytes, 0, bytes.Length, indent);

    public static string ToHexString(ReadOnlySpan<byte> bytes, int offset, int length, int indent)
    {
        if (bytes.Length < offset + length)
            throw new ArgumentException("buffer length is not enough");

        var sb = new StringBuilder();
        var index = 0;
        for (var i = offset; i < offset + length; ++i)
        {
            sb.AppendFormat("{0,2:x}", bytes[i]);
            ++index;
            if (index != length && indent != 0 && index % indent == 0)
                sb.Append(' ');
        }
        return sb.ToString();
    }

    /// <summary>
    /// convert int buffer to hex string
    /// </summary>
    /// <param name="buffer"></param>
    /// <returns></returns>
    public static string ToHexString(ReadOnlySpan<int> buffer)
    {
        var sb = new StringBuilder();
        foreach (var ch in buffer)
        {
            sb.AppendFormat("{0,8:x}", ch);
            sb.Append("  ");
        }
        return sb.ToString();
    }

    public static string ToBitString(ReadOnlySpan<byte> input)
    {
        var sb = new StringBuilder();
        foreach (var i in input)
            sb.Append(ToBitString(i));
        return sb.ToString();
    }

    public static string ToBitString(byte input)
    {
        var sb = new StringBuilder();
        for (var i = 7; i >= 0; --i)
        {
            sb.Append(input >> i & 1);
        }
        return sb.ToString();
    }

    public static string ToBitString(ReadOnlySpan<int> input)
    {
        var sb = new StringBuilder();
        foreach (var i in input)
        {
            sb.Append(ToBitString(i));
        }
        return sb.ToString();
    }

    public static string ToBitString(int input)
    {
        var sb = new StringBuilder();
        for (var i = 31; i >= 0; --i)
        {
            sb.Append(input >> i & 1);
        }
        return sb.ToString();
    }
}