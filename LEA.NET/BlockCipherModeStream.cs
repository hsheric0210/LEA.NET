namespace LEA.NET;

public abstract class BlockCipherModeStream : BlockCipherModeImpl
{
	public BlockCipherModeStream(BlockCipher cipher) : base(cipher)
	{
	}

	public override int GetOutputSize(int len) => len + bufferOffset;

	public override int GetUpdateOutputSize(int len) => (len + bufferOffset) & blockMask;

	public override void Init(Mode mode, byte[] mk) => throw new InvalidOperationException("This init method is not applicable to " + GetAlgorithmName());

	public override void Init(Mode mode, byte[] mk, byte[] iv) => throw new InvalidOperationException("This init method is not applicable to " + GetAlgorithmName());

	public override void Reset()
	{
		bufferOffset = 0;
		Array.Fill(buffer, (byte)0);
	}

	public override void SetPadding(Padding padding)
	{
	}

	public override byte[] Update(byte[] msg)
	{
		if (msg == null)
			return Array.Empty<byte>();

		var len = msg.Length;
		var gap = buffer.Length - bufferOffset;
		var inOff = 0;
		var outOff = 0;
		var outBytes = new byte[GetUpdateOutputSize(len)];
		if (len >= gap)
		{
			Array.Copy(msg, inOff, buffer, bufferOffset, gap);
			outOff += ProcessBlock(buffer, 0, outBytes, outOff);
			bufferOffset = 0;
			len -= gap;
			inOff += gap;
			while (len >= buffer.Length)
			{
				outOff += ProcessBlock(msg, inOff, outBytes, outOff);
				len -= blockSize;
				inOff += blockSize;
			}
		}

		if (len > 0)
		{
			Array.Copy(msg, inOff, buffer, bufferOffset, len);
			bufferOffset += len;
			len = 0;
		}

		return outBytes;
	}

	public override byte[] DoFinal()
	{
		if (bufferOffset == 0)
			return Array.Empty<byte>();

		var outBytes = new byte[bufferOffset];
		ProcessBlock(buffer, 0, outBytes, 0, bufferOffset);
		return outBytes;
	}
}