using System;
using static LEA.BlockCipher;

namespace LEA
{
	public abstract class BlockCipherModeBlock : BlockCipherModeCore
	{
		protected Padding padding;
		protected BlockCipherModeBlock(BlockCipher cipher) : base(cipher)
		{
		}

		public override int GetOutputSize(int length)
		{
			// TODO
			var size = (length + bufferOffset & blockmask) + blocksize;
			if (mode == Mode.ENCRYPT)
				return padding != null ? size : length;

			return length;
		}

		public override int GetUpdateOutputSize(int length)
		{
			if (mode == Mode.DECRYPT && padding != null)
				return length + bufferOffset - blocksize & blockmask;

			return length + bufferOffset & blockmask;
		}

		public override void Init(Mode mode, byte[] key) => throw new InvalidOperationException("This init method is not applicable to " + GetAlgorithmName());

		public override void Init(Mode mode, byte[] key, byte[] iv) => throw new InvalidOperationException("This init method is not applicable to " + GetAlgorithmName());

		public override void Reset()
		{
			bufferOffset = 0;
			buffer.FillBy((byte)0);
		}

		public override void SetPadding(Padding padding) => this.padding = padding;

		public override byte[] Update(byte[] message)
		{
			if (padding != null && mode == Mode.DECRYPT)
				return DecryptWithPadding(message);

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
			if (padding != null)
				return DoFinalWithPadding();

			if (bufferOffset == 0)
				return null;
			else if (bufferOffset != blocksize)
			{
				throw new InvalidOperationException("Bad padding");
			}

			var outBytes = new byte[blocksize];
			ProcessBlock(buffer, 0, outBytes, 0, blocksize);
			return outBytes;
		}

		/// <summary>
		/// 패딩 사용시 복호화 처리, 마지막 블록을 위해 데이터를 남겨둠
		/// </summary>
		/// <param name="message"></param>
		/// <returns></returns>
		private byte[] DecryptWithPadding(byte[] message)
		{
			if (message == null)
				return null;

			var len = message.Length;
			var gap = buffer.Length - bufferOffset;
			var inOffset = 0;
			var outOffset = 0;
			var outBytes = new byte[GetUpdateOutputSize(len)];
			if (len > gap)
			{
				Buffer.BlockCopy(message, inOffset, buffer, bufferOffset, gap);
				outOffset += ProcessBlock(buffer, 0, outBytes, outOffset);
				bufferOffset = 0;
				len -= gap;
				inOffset += gap;
				while (len > buffer.Length)
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

		/// <summary>
		/// 패딩 사용시 마지막 블록 처리
		/// </summary>
		/// <returns></returns>
		private byte[] DoFinalWithPadding()
		{
			byte[] outBytes;
			if (mode == Mode.ENCRYPT)
			{
				padding.Pad(buffer, bufferOffset);
				outBytes = new byte[GetOutputSize(0)];
				ProcessBlock(buffer, 0, outBytes, 0);
			}
			else
			{
				var blk = new byte[blocksize];
				ProcessBlock(buffer, 0, blk, 0);
				outBytes = padding.Unpad(blk);
			}

			return outBytes;
		}
	}
}