using System;
using static LEA.BlockCipher;

namespace LEA
{
	public abstract class BlockCipherModeStream : BlockCipherModeCore
	{
		protected BlockCipherModeStream(BlockCipher cipher) : base(cipher)
		{
		}

		public override int GetOutputSize(int length) => length + bufferOffset;

		public override int GetUpdateOutputSize(int length) => length + bufferOffset & blockmask;

		public override void Init(Mode mode, byte[] key) => throw new InvalidOperationException("This init method is not applicable to " + GetAlgorithmName());

		public override void Init(Mode mode, byte[] key, byte[] iv) => throw new InvalidOperationException("This init method is not applicable to " + GetAlgorithmName());

		public override void Reset()
		{
			bufferOffset = 0;
			buffer.FillBy((byte)0);
		}

		public override void SetPadding(Padding padding)
		{
		}

		public override byte[] Update(byte[] message)
		{
			if (message == null)
				return null;

			var len = message.Length;
			var gap = buffer.Length - bufferOffset;
			var inOffset = 0;
			var outOffset = 0;
			var outBytes = new byte[GetUpdateOutputSize(len)];
			if (len >= gap)
			{
				Buffer.BlockCopy(message, inOffset, buffer, bufferOffset, gap);
				outOffset += ProcessBlock(buffer, 0, outBytes, outOffset);
				bufferOffset = 0;
				len -= gap;
				inOffset += gap;
				while (len >= buffer.Length)
				{
					outOffset += ProcessBlock(message, inOffset, outBytes, outOffset);
					len -= blocksize;
					inOffset += blocksize;
				}
			}

			if (len > 0)
			{
				Buffer.BlockCopy(message, inOffset, buffer, bufferOffset, len);
				bufferOffset += len;
			}

			return outBytes;
		}

		public override byte[] DoFinal()
		{
			if (bufferOffset == 0)
				return null;

			var outBytes = new byte[bufferOffset];
			ProcessBlock(buffer, 0, outBytes, 0, bufferOffset);
			return outBytes;
		}
	}
}