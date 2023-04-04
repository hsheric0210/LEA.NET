using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LEA.padding
{
	public class PKCS5Padding : Padding
	{
		public PKCS5Padding(int blocksize) : base(blocksize)
		{
		}

		public override byte[] Pad(byte[] @in)
		{
			if (@in == null)
				throw new ArgumentNullException();

			if (@in.Length < 0 || @in.Length > blocksize)
				throw new InvalidOperationException("input should be shorter than blocksize");

			byte[] @out = new byte[blocksize];
			Buffer.BlockCopy(@in, 0, @out, 0, @in.Length);
			Pad(@out, @in.Length);
			return @out;
		}

		public override void Pad(byte[] @in, int inOff)
		{
			if (@in == null)
				throw new ArgumentNullException();

			if (@in.Length < inOff)
				throw new ArgumentException();

			var code = (byte)(@in.Length - inOff);
			@in.FillRange(inOff, @in.Length, code);
		}

		public override byte[] Unpad(byte[] @in)
		{
			if (@in == null || @in.Length < 1)
				throw new ArgumentNullException();

			if (@in.Length % blocksize != 0)
				throw new ArgumentException("Bad padding");

			int cnt = @in.Length - GetPadCount(@in);
			if (cnt == 0)
				return null;

			byte[] @out = new byte[cnt];
			Buffer.BlockCopy(@in, 0, @out, 0, @out.Length);
			return @out;
		}

		public override int GetPadCount(byte[] @in)
		{
			if (@in == null || @in.Length < 1)
				throw new ArgumentNullException();

			if (@in.Length % blocksize != 0)
				throw new ArgumentException("Bad padding");

			int count = @in[@in.Length - 1] & 0xff;
			var isBadPadding = false;
			int lower_bound = @in.Length - count;
			for (int i = @in.Length - 1; i > lower_bound; --i)
			{
				if (@in[i] != count)
					isBadPadding = true;
			}

			if (isBadPadding)
				throw new InvalidOperationException("Bad Padding");

			return count;
		}
	}
}