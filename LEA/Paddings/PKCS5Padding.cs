using System;

namespace LEA.Paddings
{
	public class Pkcs5Padding : Padding
	{
		public Pkcs5Padding(int blocksize) : base(blocksize)
		{
		}

		public override byte[] Pad(byte[] inBytes)
		{
			if (inBytes == null)
				throw new ArgumentNullException(nameof(inBytes));

			if (inBytes.Length > blocksize)
				throw new InvalidOperationException("input should be shorter than blocksize");

			var outBytes = new byte[blocksize];
			Buffer.BlockCopy(inBytes, 0, outBytes, 0, inBytes.Length);
			Pad(outBytes, inBytes.Length);
			return outBytes;
		}

		public override void Pad(byte[] inBytes, int inOffset)
		{
			if (inBytes == null)
				throw new ArgumentNullException(nameof(inBytes));

			if (inBytes.Length < inOffset)
				throw new ArgumentException();

			var code = (byte)(inBytes.Length - inOffset);
			inBytes.FillBy(inOffset, inBytes.Length, code);
		}

		public override byte[] Unpad(byte[] inBytes)
		{
			if (inBytes == null)
				throw new ArgumentNullException(nameof(inBytes));

			if (inBytes.Length < 1 || inBytes.Length % blocksize != 0)
				throw new ArgumentException("Bad padding");

			var cnt = inBytes.Length - GetPadCount(inBytes);
			if (cnt == 0)
				return null;

			var outBytes = new byte[cnt];
			Buffer.BlockCopy(inBytes, 0, outBytes, 0, outBytes.Length);
			return outBytes;
		}

		public override int GetPadCount(byte[] inBytes)
		{
			if (inBytes == null)
				throw new ArgumentNullException(nameof(inBytes));

			if (inBytes.Length < 1 || inBytes.Length % blocksize != 0)
				throw new ArgumentException("Bad padding");

			var count = inBytes[inBytes.Length - 1] & 0xff;
			var isBadPadding = false;
			var lower_bound = inBytes.Length - count;
			for (var i = inBytes.Length - 1; i > lower_bound; --i)
			{
				if (inBytes[i] != count)
					isBadPadding = true;
			}

			if (isBadPadding)
				throw new InvalidOperationException("Bad Padding");

			return count;
		}
	}
}