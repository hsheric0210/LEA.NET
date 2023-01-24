using LEA.Padding;

namespace LEA.Test.Padding;
public class Iso10126PaddingTest
{
    [Fact]
    public void Padding_16bytes_WhenLengthShorterThanBlockSize()
    {
        // Arrange
        var impl = new Iso10126Padding(16);
        var unpadded = new byte[] { 0xff };
        var unpaddedLength = unpadded.Length;
        var paddedExpected = new byte[] { 0xff, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 15 };

        // Act
        var paddedActual = impl.Pad(unpadded);

        // Assert
        Assert.Equal(paddedExpected[..unpaddedLength], paddedExpected[..unpaddedLength]);
        Assert.True(paddedExpected[^1] == paddedActual[^1], "Padding length mismatch");
    }

    [Fact]
    public void Padding_16bytes_WhenLengthEqualToBlockSize()
    {
        // Arrange
        var impl = new Iso10126Padding(16);
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
        var impl = new Iso10126Padding(16);
        var unpadded = new byte[] { 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff };

        // Act
        // Assert
        Assert.Throws<ArgumentException>(() => _ = impl.Pad(unpadded));
    }

    [Fact]
    public void Padding_WithOffset_16bytes_WhenLengthShorterThanBlockSize()
    {
        // Arrange
        var impl = new Iso10126Padding(16);
        var unpadded = new byte[] { 0xff, 0xff, 0xff, 0xff };
        var unpaddedLength = 2;
        var paddedExpected = new byte[] { 0xff, 0xff, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14 };
        var paddedActual = new byte[16];
        unpadded.CopyTo(paddedActual.AsSpan());

        // Act
        impl.Pad(paddedActual, unpaddedLength);

        // Assert
        Assert.Equal(paddedExpected[..unpaddedLength], paddedActual[..unpaddedLength]);
        Assert.True(paddedExpected[^1] == paddedActual[^1], "Padding length mismatch");
    }

    [Fact]
    public void Padding_WithOffset_16bytes_OffsetOOB()
    {
        // Arrange
        var impl = new Iso10126Padding(16);
        var unpadded = new byte[] { 0xff, 0xff, 0xff, 0xff, 0xfc, 0xfc, 0xfc, 0xfc, 0xfa, 0xfa, 0xfa, 0xfa };

        // Act
        // Assert
        Assert.Throws<ArgumentException>(() => impl.Pad(unpadded, 50));
    }


    [Fact]
    public void Unpadding_16bytes_WhenLengthShorterThanBlockSize()
    {
        // Arrange
        var impl = new Iso10126Padding(16);
        var padded = new byte[] { 0xff, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
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
        var impl = new Iso10126Padding(16);
        var padded = new byte[] { 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 1, 0, 2, 4, 2, 0, 4, 8, 4, 0, 9, 6, 8, 1, 9, 16 };
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
        var impl = new Iso10126Padding(16);
        var badPadded = new byte[] { 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff };

        // Act
        // Assert
        Assert.Throws<ArgumentException>(() => _ = impl.Unpad(badPadded));
    }

    [Fact]
    public void Unpadding_16bytes_BadPadding_PaddingCountOOB()
    {
        // Arrange
        var impl = new Iso10126Padding(16);

        // it describes the padding length is 255, but actual byte array length is only 16, thus it causes the IndexOutOfRangeException
        var badPadded = new byte[] { 76, 1, 83, 54, 27, 56, 76, 45, 65, 23, 54, 0xff, 12, 45, 78, 0xff };

        // Act
        // Assert
        Assert.Throws<InvalidOperationException>(() => _ = impl.Unpad(badPadded));
    }
}
