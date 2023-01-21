namespace LEA;

public abstract class BlockCipherModeBlock : BlockCipherModeImpl
{
	protected PaddingBase padding;
	public BlockCipherModeBlock(BlockCipher cipher) : base(cipher)
	{
	}

	public override int GetOutputSize(int length)
	{

		var size = ((length + bufferOffset) & blockMask) + blockSize;
		if (mode == OperatingMode.Encrypt)
		{
			return padding != null ? size : length;
		}

		return length;
	}

	public override int GetUpdateOutputSize(int length)
	{
		if (mode == OperatingMode.Decrypt && padding != null)
		{
			return (length + bufferOffset - blockSize) & blockMask;
		}

		return (length + bufferOffset) & blockMask;
	}

	public override void Init(OperatingMode mode, byte[] key) => throw new InvalidOperationException("This init method is not applicable to " + GetAlgorithmName());

	public override void Init(OperatingMode mode, byte[] key, byte[] iv) => throw new InvalidOperationException("This init method is not applicable to " + GetAlgorithmName());

	public override void Reset()
	{
		bufferOffset = 0;
		Array.Fill(buffer, (byte)0);
	}

	public override void SetPadding(PaddingBase padding) => this.padding = padding;

	public override byte[] Update(byte[] bytes)
	{
		if (padding != null && mode == OperatingMode.Decrypt)
		{
			return DecryptWithPadding(bytes);
		}

		if (bytes == null)
		{
			return null;
		}

		var length = bytes.Length;
		var gap = buffer.Length - bufferOffset;
		var inOffset = 0;
		var outOffset = 0;
		byte[] output = new byte[GetUpdateOutputSize(length)];
		if (length >= gap)
		{
			Array.Copy(bytes, inOffset, buffer, bufferOffset, gap);
			outOffset += ProcessBlock(buffer, 0, output, outOffset);
			bufferOffset = 0;
			length -= gap;
			inOffset += gap;
			while (length >= buffer.Length)
			{
				outOffset += ProcessBlock(bytes, inOffset, output, outOffset);
				length -= blockSize;
				inOffset += blockSize;
			}
		}

		if (length > 0)
		{
			Array.Copy(bytes, inOffset, buffer, bufferOffset, length);
			bufferOffset += length;
			//length = 0;
		}

		return output;
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

		byte[] output = new byte[blockSize];
		ProcessBlock(buffer, 0, output, 0, blockSize);
		return output;
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
		byte[] output = new byte[GetUpdateOutputSize(length)];
		if (length > gap)
		{
			Array.Copy(msg, inOffset, buffer, bufferOffset, gap);
			outOffset += ProcessBlock(buffer, 0, output, outOffset);
			bufferOffset = 0;
			length -= gap;
			inOffset += gap;
			while (length > buffer.Length)
			{
				outOffset += ProcessBlock(msg, inOffset, output, outOffset);
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

		return output;
	}

	/// <summary>
	/// 패딩 사용시 마지막 블록 처리
	/// </summary>
	/// <returns></returns>
	private byte[] DoFinalWithPadding()
	{
		byte[] output;
		if (mode == OperatingMode.Encrypt)
		{
			padding.Pad(buffer, bufferOffset);
			output = new byte[GetOutputSize(0)];
			ProcessBlock(buffer, 0, output, 0);
		}
		else
		{
			var block = new byte[blockSize];
			ProcessBlock(buffer, 0, block, 0);
			output = padding.Unpad(block);
		}

		return output;
	}
}