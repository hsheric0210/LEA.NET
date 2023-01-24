using LEA.Padding;

namespace LEA.Test.Padding;
public class Pkcs5PaddingTest
{
    [Fact]
    public void Padding_16bytes_WhenLengthShorterThanBlockSize()
    {
        // Arrange
        var impl = new Pkcs5Padding(16);
        var unpadded = new byte[] { 0xff };
        var paddedExpected = new byte[] { 0xff, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15 };

        // Act
        var paddedActual = impl.Pad(unpadded);

        // Assert
        Assert.Equal(paddedExpected.ToArray(), paddedActual.ToArray());
    }

    [Fact]
    public void Padding_16bytes_WhenLengthEqualToBlockSize()
    {
        // Arrange
        var impl = new Pkcs5Padding(16);
        var unpadded = new byte[] { 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff };

        // Act
        var paddedActual = impl.Pad(unpadded);

        // Assert
        Assert.Equal(unpadded.ToArray(), paddedActual.ToArray());
    }

    [Fact]
    public void Padding_16bytes_WhenLengthLongerThanBlockSize()
    {
        // Arrange
        var impl = new Pkcs5Padding(16);
        var unpadded = new byte[] { 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff };

        // Act
        // Assert
        Assert.Throws<ArgumentException>(() => _ = impl.Pad(unpadded));
    }

    [Fact]
    public void Padding_WithOffset_16bytes_WhenLengthShorterThanBlockSize()
    {
        // Arrange
        var impl = new Pkcs5Padding(16);
        var unpadded = new byte[] { 0xff, 0xff, 0xff, 0xff };
        var paddedExpected = new byte[] { 0xff, 0xff, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14 };
        var paddedActual = new byte[16];
        unpadded.CopyTo(paddedActual.AsSpan());

        // Act
        impl.Pad(paddedActual, 2);

        // Assert
        Assert.Equal(paddedExpected.ToArray(), paddedActual.ToArray());
    }

    [Fact]
    public void Padding_WithOffset_16bytes_OffsetOOB()
    {
        // Arrange
        var impl = new Pkcs5Padding(16);
        var unpadded = new byte[] { 0xff, 0xff, 0xff, 0xff, 0xfc, 0xfc, 0xfc, 0xfc, 0xfa, 0xfa, 0xfa, 0xfa };

        // Act
        // Assert
        Assert.Throws<ArgumentException>(() => impl.Pad(unpadded, 50));
    }


    [Fact]
    public void Unpadding_16bytes_WhenLengthShorterThanBlockSize()
    {
        // Arrange
        var impl = new Pkcs5Padding(16);
        var padded = new byte[] { 0xff, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15 };
        var unpaddedExpected = new byte[] { 0xff };

        // Act
        var unpaddedActual = impl.Unpad(padded);

        // Assert
        Assert.Equal(unpaddedExpected, unpaddedActual.ToArray());
    }

    [Fact]
    public void Unpadding_16bytes_WhenLengthEqualToBlockSize()
    {
        // Arrange
        var impl = new Pkcs5Padding(16);
        var padded = new byte[] { 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16 };
        var unpaddedExpected = new byte[] { 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff };

        // Act
        var unpaddedActual = impl.Unpad(padded);

        // Assert
        Assert.Equal(unpaddedExpected.ToArray(), unpaddedActual.ToArray());
    }

    [Fact]
    public void Unpadding_16bytes_BadPadding_LengthIsNotDividedByBlockSize()
    {
        // Arrange
        var impl = new Pkcs5Padding(16);
        var badPadded = new byte[] { 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff };

        // Act
        // Assert
        Assert.Throws<ArgumentException>(() => _ = impl.Unpad(badPadded));
    }

    [Fact]
    public void Unpadding_16bytes_BadPadding_InconsistentPaddingChar()
    {
        // Arrange
        var impl = new Pkcs5Padding(16);
        var badPadded = new byte[] { 0xff, 0xff, 0xff, 0xff, 12, 12, 12, 12, 12, 12, 12, 12, 10, 12, 12, 12 };

        // Act
        // Assert
        Assert.Throws<InvalidOperationException>(() => _ = impl.Unpad(badPadded));
    }

    [Fact]
    public void Unpadding_16bytes_BadPadding_PaddingCountOOB()
    {
        // Arrange
        var impl = new Pkcs5Padding(16);

        // it describes the padding length is 255, but actual byte array length is only 16, thus it causes the IndexOutOfRangeException
        var badPadded = new byte[] { 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff };

        // Act
        // Assert
        Assert.Throws<IndexOutOfRangeException>(() => _ = impl.Unpad(badPadded));
    }
}
