using static LEA.Utils.Xor;

namespace LEA.OpMode
{
	public class CbcMode : BlockCipherModeBlock
	{
		private byte[] iv;
		private byte[] feedback;

		public CbcMode(BlockCipher cipher) : base(cipher)
		{
		}

		public override string GetAlgorithmName() => Engine.GetAlgorithmName() + "/CBC";

		public override void Init(Mode mode, ReadOnlySpan<byte> key, ReadOnlySpan<byte> iv)
		{
			Mode = mode;
			Engine.Init(mode, key);
			this.iv = iv.ToArray();
			feedback = new byte[BlockSizeBytes];
			Reset();
		}

		public override void Reset()
		{
			base.Reset();
			Buffer.BlockCopy(iv, 0, feedback, 0, BlockSizeBytes);
		}

		protected override int ProcessBlock(ReadOnlySpan<byte> inBlock, int inOffset, Span<byte> outBlock, int outOffset, int outLength)
		{
			if (outLength != BlockSizeBytes)
				throw new ArgumentException("outlen should be " + BlockSizeBytes + " in " + GetAlgorithmName());

			if (Mode == Mode.Encrypt)
				return EncryptBlock(inBlock, inOffset, outBlock, outOffset);

			return DecryptBlock(inBlock, inOffset, outBlock, outOffset);
		}

		private int EncryptBlock(ReadOnlySpan<byte> input, int inOffset, Span<byte> output, int outOffset)
		{
			if (inOffset + BlockSizeBytes > input.Length)
				throw new InvalidOperationException("input data too short");

			XOR(feedback, 0, input, inOffset, BlockSizeBytes);
			Engine.ProcessBlock(feedback, 0, output, outOffset);
			output.Slice(outOffset, BlockSizeBytes).CopyTo(feedback);
			return BlockSizeBytes;
		}

		private int DecryptBlock(ReadOnlySpan<byte> input, int inOffset, Span<byte> output, int outOffset)
		{
			if (inOffset + BlockSizeBytes > input.Length)
				throw new InvalidOperationException("input data too short");

			Engine.ProcessBlock(input, inOffset, output, outOffset);
			XOR(output, outOffset, feedback, 0, BlockSizeBytes);
			input.Slice(inOffset, BlockSizeBytes).CopyTo(feedback);
			return BlockSizeBytes;
		}
	}
}