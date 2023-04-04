using System;
using static LEA.BlockCipher;

namespace LEA
{
	public abstract class BlockCipherModeStream : BlockCipherModeImpl
	{
		public BlockCipherModeStream(BlockCipher cipher) : base(cipher)
		{
		}

		public override int GetOutputSize(int len) => len + bufferOffset;

		public override int GetUpdateOutputSize(int len) => len + bufferOffset & blockmask;

		public override void Init(Mode mode, byte[] mk) => throw new InvalidOperationException("This init method is not applicable to " + GetAlgorithmName());

		public override void Init(Mode mode, byte[] mk, byte[] iv) => throw new InvalidOperationException("This init method is not applicable to " + GetAlgorithmName());

		public override void Reset()
		{
			bufferOffset = 0;
			buffer.FillBy((byte)0);
		}

		public override void SetPadding(Padding padding)
		{
		}

		public override byte[] Update(byte[] msg)
		{
			if (msg == null)
				return null;

			var len = msg.Length;
			var gap = buffer.Length - bufferOffset;
			var inOff = 0;
			var outOff = 0;
			var @out = new byte[GetUpdateOutputSize(len)];
			if (len >= gap)
			{
				Buffer.BlockCopy(msg, inOff, buffer, bufferOffset, gap);
				outOff += ProcessBlock(buffer, 0, @out, outOff);
				bufferOffset = 0;
				len -= gap;
				inOff += gap;
				while (len >= buffer.Length)
				{
					outOff += ProcessBlock(msg, inOff, @out, outOff);
					len -= blocksize;
					inOff += blocksize;
				}
			}

			if (len > 0)
			{
				Buffer.BlockCopy(msg, inOff, buffer, bufferOffset, len);
				bufferOffset += len;
				len = 0;
			}

			return @out;
		}

		public override byte[] DoFinal()
		{
			if (bufferOffset == 0)
				return null;

			var @out = new byte[bufferOffset];
			ProcessBlock(buffer, 0, @out, 0, bufferOffset);
			return @out;
		}
	}
}