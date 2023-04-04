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

		public override int GetOutputSize(int len)
		{
			// TODO
			var size = (len + bufferOffset & blockmask) + blocksize;
			if (mode == Mode.ENCRYPT)
				return padding != null ? size : len;

			return len;
		}

		public override int GetUpdateOutputSize(int len)
		{
			if (mode == Mode.DECRYPT && padding != null)
				return len + bufferOffset - blocksize & blockmask;

			return len + bufferOffset & blockmask;
		}

		public override void Init(Mode mode, byte[] mk) => throw new InvalidOperationException("This init method is not applicable to " + GetAlgorithmName());

		public override void Init(Mode mode, byte[] mk, byte[] iv) => throw new InvalidOperationException("This init method is not applicable to " + GetAlgorithmName());

		public override void Reset()
		{
			bufferOffset = 0;
			buffer.FillBy((byte)0);
		}

		public override void SetPadding(Padding padding) => this.padding = padding;

		public override byte[] Update(byte[] msg)
		{
			if (padding != null && mode == Mode.DECRYPT)
				return DecryptWithPadding(msg);

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
			}

			return @out;
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

			var @out = new byte[blocksize];
			ProcessBlock(buffer, 0, @out, 0, blocksize);
			return @out;
		}

		/// <summary>
		/// 패딩 사용시 복호화 처리, 마지막 블록을 위해 데이터를 남겨둠
		/// </summary>
		/// <param name="msg"></param>
		/// <returns></returns>
		private byte[] DecryptWithPadding(byte[] msg)
		{
			if (msg == null)
				return null;

			var len = msg.Length;
			var gap = buffer.Length - bufferOffset;
			var inOff = 0;
			var outOff = 0;
			var @out = new byte[GetUpdateOutputSize(len)];
			if (len > gap)
			{
				Buffer.BlockCopy(msg, inOff, buffer, bufferOffset, gap);
				outOff += ProcessBlock(buffer, 0, @out, outOff);
				bufferOffset = 0;
				len -= gap;
				inOff += gap;
				while (len > buffer.Length)
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
			}

			return @out;
		}

		/// <summary>
		/// 패딩 사용시 마지막 블록 처리
		/// </summary>
		/// <returns></returns>
		private byte[] DoFinalWithPadding()
		{
			byte[] @out;
			if (mode == Mode.ENCRYPT)
			{
				padding.Pad(buffer, bufferOffset);
				@out = new byte[GetOutputSize(0)];
				ProcessBlock(buffer, 0, @out, 0);
			}
			else
			{
				var blk = new byte[blocksize];
				ProcessBlock(buffer, 0, blk, 0);
				@out = padding.Unpad(blk);
			}

			return @out;
		}
	}
}