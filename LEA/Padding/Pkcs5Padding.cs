namespace LEA.Padding;

public class Pkcs5Padding : PaddingBase
{
	public Pkcs5Padding(int blocksize) : base(blocksize)
	{
	}

	public override byte[] Pad(byte[] input)
	{
		if (input == null)
			throw new ArgumentNullException(nameof(input));

		if (input.Length > blocksize)
			throw new InvalidOperationException("input should be shorter than blocksize");

		var output = new byte[blocksize];
		Array.Copy(input, 0, output, 0, input.Length);
		Pad(output, input.Length);
		return output;
	}

	public override void Pad(byte[] input, int inOffset)
	{
		if (input == null)
			throw new ArgumentNullException(nameof(input));

		if (input.Length < inOffset)
			throw new ArgumentException("Index out of bounds: " + nameof(input));

		var code = (byte)(input.Length - inOffset);
		Array.Fill(input, code, inOffset, input.Length);
	}

	public override byte[] Unpad(byte[] input)
	{
		if (input == null)
			throw new ArgumentNullException(nameof(input));
		if (input.Length < 1)
			throw new ArgumentException("Empty array: " + nameof(input));
		if (input.Length % blocksize != 0)
			throw new ArgumentException("Bad padding"); // FIXME: Padding oracle attack

		var cnt = input.Length - GetPadCount(input);
		if (cnt == 0)
			return Array.Empty<byte>();

		var output = new byte[cnt];
		Array.Copy(input, 0, output, 0, output.Length);
		return output;
	}

	public override int GetPadCount(byte[] input)
	{
		if (input == null)
			throw new ArgumentNullException(nameof(input));

		if (input.Length < 1)
			throw new ArgumentException("Empty array: " + nameof(input));

		if (input.Length % blocksize != 0)
			throw new ArgumentException("Bad padding");

		var count = input[input.Length - 1] & 0xff;
		var isBadPadding = false;
		var lower_bound = input.Length - count;
		for (var i = input.Length - 1; i > lower_bound; --i)
		{
			if (input[i] != count)
				isBadPadding = true;
		}

		if (isBadPadding)
			throw new InvalidOperationException("Bad Padding"); // FIXME: Padding oracle attack

		return count;
	}
}