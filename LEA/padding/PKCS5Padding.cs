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
				throw new NullPointerException();

			if (@in.length < 0 || @in.length > blocksize)
				throw new InvalidOperationException("input should be shorter than blocksize");

			byte[] out = new byte[blocksize];
			System.Arraycopy(@in, 0, @out, 0, @in.length);
			Pad(@out, @in.length);
			return @out;
		}

		public override void Pad(byte[] @in, int inOff)
		{
			if (@in == null)
				throw new NullPointerException();

			if (@in.length < inOff)
				throw new ArgumentException();

			var code = (byte)(@in.length - inOff);
			Arrays.Fill(@in, inOff, @in.length, code);
		}

		public override byte[] Unpad(byte[] @in)
		{
			if (@in == null || @in.length < 1)
				throw new NullPointerException();

			if (@in.length % blocksize != 0)
				throw new ArgumentException("Bad padding");

			int cnt = @in.length - GetPadCount(@in);
			if (cnt == 0)
				return null;

			byte[] out = new byte[cnt];
			System.Arraycopy(@in, 0, @out, 0, @out.length);
			return @out;
		}

		public override int GetPadCount(byte[] @in)
		{
			if (@in == null || @in.length < 1)
				throw new NullPointerException();

			if (@in.length % blocksize != 0)
				throw new ArgumentException("Bad padding");

			int count = @in[@in.length - 1] & 0xff;
			var isBadPadding = false;
			int lower_bound = @in.length - count;
			for (int i = in.length - 1; i > lower_bound;
			--i)
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