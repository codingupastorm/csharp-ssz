using System;
using Xunit;

namespace CodingBlocks.SimpleSerialize.Tests
{
    public class UintTests
    {
        [Theory]
        [InlineData(29, 8, new byte[]{ 29 })]
        [InlineData(29, 16, new byte[] { 29, 0 })]
        public void EncodeUints(ulong value, int bits, byte[] expectedBytes)
        {
            byte[] encoded = SSZ.EncodeUint(value, bits);
            Assert.Equal(expectedBytes, encoded);
        }

        [Fact]
        public void Uint_BitLengthNot8_ThrowsException()
        {
            Assert.ThrowsAny<Exception>(() => SSZ.EncodeUint(100, 10));
        }

        [Fact]
        public void Uint_DoesntFit_ThrowsException()
        {
            Assert.ThrowsAny<Exception>(() => SSZ.EncodeUint(257, 8));
        }
    }
}
