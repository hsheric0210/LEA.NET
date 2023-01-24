namespace LEA;

/// <summary>
/// Interface for single block encryption or decryption of block cipher
/// </summary>
public abstract class BlockCipher
{
    /// <summary>
    /// Initialization method
    /// </summary>
    /// <param name="mode"><see cref="BlockCipher.Mode"/></param>
    /// <param name="key">Encryption key</param>
    public abstract void Init(Mode mode, ReadOnlySpan<byte> key);

    /// <summary>
    /// Reset the internal state to process new data
    /// </summary>
    public abstract void Reset();

    /// <summary>
    /// Returns the name of algorithm
    /// </summary>
    /// <returns>The name of algorithm</returns>
    public abstract string GetAlgorithmName();

    /// <summary>
    /// Returns the size of single block
    /// </summary>
    /// <returns>The size of single block</returns>
    public abstract int GetBlockSize();

    /// <summary>
    /// Perform encryption for single block
    /// </summary>
    /// <param name="inBlock">Input byte array</param>
    /// <param name="inOffset">Offset of <paramref name="inBlock"/></param>
    /// <param name="outBlock">Output byte array</param>
    /// <param name="outOffset">Offset of <paramref name="outBlock"/></param>
    /// <returns>Processed length</returns>
    public abstract int ProcessBlock(ReadOnlySpan<byte> inBlock, int inOffset, Span<byte> outBlock, int outOffset);
}