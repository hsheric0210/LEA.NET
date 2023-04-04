using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LEA
{
	public abstract class BlockCipherModeStream : BlockCipherModeImpl
	{
		public BlockCipherModeStream(BlockCipher cipher) : base(cipher)
		{
		}

		public override int GetOutputSize(int len)
		{
			return len + bufferOffset;
		}

		public override int GetUpdateOutputSize(int len)
		{
			return len + bufferOffset & blockmask;
		}

		public override void Init(Mode mode, byte[] mk)
		{
			throw new InvalidOperationException("This init method is not applicable to " + GetAlgorithmName());
		}

		public override void Init(Mode mode, byte[] mk, byte[] iv)
		{
			throw new InvalidOperationException("This init method is not applicable to " + GetAlgorithmName());
		}

		public override void Reset()
		{
			bufferOffset = 0;
			Arrays.Fill(buffer, (byte)0);
		}

		public override void SetPadding(Padding padding)
		{
		}

		public override byte[] Update(byte[] msg)
		{
			if (msg == null)
				return null;

			int len = msg.length;
			int gap = buffer.length - bufferOffset;
			var inOff = 0;
			var outOff = 0;
			byte[] out = new byte[GetUpdateOutputSize(len)];
			if (len >= gap)
			{
				System.Arraycopy(msg, inOff, buffer, bufferOffset, gap);
				outOff += ProcessBlock(buffer, 0, @out, outOff);
				bufferOffset = 0;
				len -= gap;
				inOff += gap;
				while (len >= buffer.length)
				{
					outOff += ProcessBlock(msg, inOff, @out, outOff);
					len -= blocksize;
					inOff += blocksize;
				}
			}

			if (len > 0)
			{
				System.Arraycopy(msg, inOff, buffer, bufferOffset, len);
				bufferOffset += len;
				len = 0;
			}

			return @out;
		}

		public override byte[] DoFinal()
		{
			if (bufferOffset == 0)
				return null;

			byte[] out = new byte[bufferOffset];
			ProcessBlock(buffer, 0, @out, 0, bufferOffset);
			return @out;
		}
	}
}