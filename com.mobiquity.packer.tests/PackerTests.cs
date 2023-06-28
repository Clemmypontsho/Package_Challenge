using System;
using System.IO;
using Xunit;

namespace com.mobiquity.packer.tests
{
    // Tests for the Packer class
    public class PackerTests
    {
        [Theory]
        [InlineData("input")]
        public void PackerShouldReturnString(string fileName)
        {
            // Act
            var returnValue = Packer.Pack(Path.Combine(Environment.CurrentDirectory, "Resources", fileName));

            // Assert
            Assert.True(typeof(string) == returnValue.GetType());
        }

        [Theory]
        [InlineData("WrongFileName")]
        public void WrongFileNameShouldReturnException(string fileName)
        {
            // Assert
            Assert.Throws<APIException>(() => Packer.Pack(Path.Combine(Environment.CurrentDirectory, "Resources", fileName)));
        }

        [Theory]
        [InlineData("input", "4\r\n-\r\n2,7\r\n8,9\r\n")]
        [InlineData("input2", "2,7\r\n8,9\r\n")]
        [InlineData("input3", "4\r\n1\r\n2,3\r\n8,9\r\n")]
        public void PackerResponseShouldMatch(string fileName, string expected)
        {
            // Act
            var returnValue = Packer.Pack(Path.Combine(Environment.CurrentDirectory, "Resources", fileName));

            // Assert
            Assert.Equal(expected, returnValue);
        }

        [Theory]
        [InlineData("input_wrongPackageItem")]
        public void WrongPackageItemRecordShouldThrowException(string fileName)
        {
            // Assert
            Assert.Throws<APIException>(() => Packer.Pack(Path.Combine(Environment.CurrentDirectory, "Resources", fileName)));
        }
    }
}