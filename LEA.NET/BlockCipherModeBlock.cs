namespace LEA.NET;

public abstract class BlockCipherModeBlock : BlockCipherModeImpl
{
	protected Padding padding;
	public BlockCipherModeBlock(BlockCipher cipher) : base(cipher)
	{
	}

	public override int GetOutputSize(int len)
	{

		var size = ((len + bufferOffset) & blockMask) + blockSize;
		if (mode == Mode.ENCRYPT)
		{
			return padding != null ? size : len;
		}

		return len;
	}

	public override int GetUpdateOutputSize(int len)
	{
		if (mode == Mode.DECRYPT && padding != null)
		{
			return (len + bufferOffset - blockSize) & blockMask;
		}

		return (len + bufferOffset) & blockMask;
	}

	public override void Init(Mode mode, byte[] mk) => throw new InvalidOperationException("This init method is not applicable to " + GetAlgorithmName());

	public override void Init(Mode mode, byte[] mk, byte[] iv) => throw new InvalidOperationException("This init method is not applicable to " + GetAlgorithmName());

	public override void Reset()
	{
		bufferOffset = 0;
		Array.Fill(buffer, (byte)0);
	}

	public override void SetPadding(Padding padding) => this.padding = padding;

	public override byte[] Update(byte[] msg)
	{
		if (padding != null && mode == Mode.DECRYPT)
		{
			return DecryptWithPadding(msg);
		}

		if (msg == null)
		{
			return null;
		}

		var length = msg.Length;
		var gap = buffer.Length - bufferOffset;
		var inOffset = 0;
		var outOffset = 0;
		byte[] outBytes = new byte[GetUpdateOutputSize(length)];
		if (length >= gap)
		{
			Array.Copy(msg, inOffset, buffer, bufferOffset, gap);
			outOffset += ProcessBlock(buffer, 0, outBytes, outOffset);
			bufferOffset = 0;
			length -= gap;
			inOffset += gap;
			while (length >= buffer.Length)
			{
				outOffset += ProcessBlock(msg, inOffset, outBytes, outOffset);
				length -= blockSize;
				inOffset += blockSize;
			}
		}

		if (length > 0)
		{
			Array.Copy(msg, inOffset, buffer, bufferOffset, length);
			bufferOffset += length;
			//length = 0;
		}

		return outBytes;
	}

	public override byte[] DoFinal()
	{
		if (padding != null)
		{
			return DoFinalWithPadding();
		}

		if (bufferOffset == 0)
		{
			return null;
		}
		else if (bufferOffset != blockSize)
		{
			throw new InvalidOperationException("Bad padding");
		}

		byte[] outBytes = new byte[blockSize];
		ProcessBlock(buffer, 0, outBytes, 0, blockSize);
		return outBytes;
	}

	/// <summary>
	/// 패딩 사용시 복호화 처리, 마지막 블록을 위해 데이터를 남겨둠
	/// </summary>
	/// <param name="msg"></param>
	/// <returns></returns>
	private byte[] DecryptWithPadding(byte[] msg)
	{
		if (msg == null)
			return Array.Empty<byte>();

		var length = msg.Length;
		var gap = buffer.Length - bufferOffset;
		var inOffset = 0;
		var outOffset = 0;
		byte[] outBytes = new byte[GetUpdateOutputSize(length)];
		if (length > gap)
		{
			Array.Copy(msg, inOffset, buffer, bufferOffset, gap);
			outOffset += ProcessBlock(buffer, 0, outBytes, outOffset);
			bufferOffset = 0;
			length -= gap;
			inOffset += gap;
			while (length > buffer.Length)
			{
				outOffset += ProcessBlock(msg, inOffset, outBytes, outOffset);
				length -= blockSize;
				inOffset += blockSize;
			}
		}

		if (length > 0)
		{
			Array.Copy(msg, inOffset, buffer, bufferOffset, length);
			bufferOffset += length;
			//length = 0;
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
			var block = new byte[blockSize];
			ProcessBlock(buffer, 0, block, 0);
			outBytes = padding.Unpad(block);
		}

		return outBytes;
	}
}