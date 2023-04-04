using System;
using System.Security.Cryptography;

namespace LEA.Paddings
{
	public class Iso10126Padding : Padding
	{
		public Iso10126Padding(int blocksize) : base(blocksize)
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

			if (inBytes.Length <= inOffset)
				return;

			var code = (byte)(inBytes.Length - inOffset);
			var random = new byte[inBytes.Length - inOffset];
			RandomNumberGenerator.Create().GetBytes(random);
			Buffer.BlockCopy(random, 0, inBytes, inOffset, random.Length);
			inBytes[inBytes.Length - 1] = code;
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

			return inBytes[inBytes.Length - 1] & 0xff;
		}
	}
}