namespace LEA
{
	public abstract class BlockCipherModeCore : BlockCipherMode
	{
		protected BlockCipher Engine { get; }
		protected Mode Mode { get; set; }

		protected byte[] BlockBuffer { get; }
		protected int BlockBufferOffset { get; set; }

		protected int BlockSizeBytes { get; }
		protected int BlockMask { get; }

		protected BlockCipherModeCore(BlockCipher cipher)
		{
			Engine = cipher;
			BlockSizeBytes = Engine.BlockSizeBytes;
			BlockMask = GetBlockmask(BlockSizeBytes);
			BlockBuffer = new byte[BlockSizeBytes];
		}

		public override ReadOnlySpan<byte> DoFinal(ReadOnlySpan<byte> message)
		{
			ReadOnlySpan<byte> part1 = Update(message);
			ReadOnlySpan<byte> part2 = DoFinal();
			var output = new byte[part1.Length + part2.Length];
			if (part1.Length > 0)
				part1.CopyTo(output);
			if (part2.Length > 0)
				part2.CopyTo(output.AsSpan()[part1.Length..]);
			return output;
		}

		protected abstract int ProcessBlock(ReadOnlySpan<byte> inBlock, int inOffset, Span<byte> outBlock, int outOffset, int outLength);
		protected virtual int ProcessBlock(ReadOnlySpan<byte> inBlock, int inOffset, Span<byte> outBlock, int outOffset) => ProcessBlock(inBlock, inOffset, outBlock, outOffset, BlockSizeBytes);

		protected static int GetBlockmask(int blockSize)
		{
			return unchecked((int)(blockSize switch
			{
				8 => 0xfffffff7u,
				16 => 0xfffffff0u,
				32 => 0xffffffe0u,
				_ => 0u,
			}));
		}
	}
}