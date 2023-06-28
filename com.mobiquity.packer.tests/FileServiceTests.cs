using com.mobiquity.packer.Services;
using System;
using System.IO;
using Xunit;

namespace com.mobiquity.packer.tests
{
    // Tests for the FileService class.

    public class FileServiceTests
    {
        private readonly FileService _sut;

        public FileServiceTests()
        {
            // Create an instance of the FileService to be tested
            _sut = new FileService();
        }

        [Theory]
        [InlineData("input")]
        [InlineData("input2")]
        [InlineData("input3")]
        [InlineData("example_output")]
        public void CorrectFileNameShouldReturnTrue(string fileName)
        {
            // Arrange
            var fullPath = Path.Combine(Environment.CurrentDirectory, "Resources", fileName);

            // Act
            var result = _sut.IsExist(fullPath);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData("WrongFileName1")]
        [InlineData("WrongFileName2")]
        public void WrongFileNameShouldReturnFalse(string fileName)
        {
            // Arrange
            var fullPath = Path.Combine(Environment.CurrentDirectory, "Resources", fileName);

            // Act
            var result = _sut.IsExist(fullPath);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void EmptyFilePathShouldReturnException()
        {
            // Arrange
            string emptyFilePath = "";

            // Assert
            Assert.Throws<APIException>(() => _sut.IsExist(emptyFilePath));
        }

        [Theory]
        [InlineData("input", 4)]
        [InlineData("input2", 2)]
        public void NumberOfLinesCountShouldMatch(string fileName, int expectedNumberOfLines)
        {
            // Arrange
            var fullPath = Path.Combine(Environment.CurrentDirectory, "Resources", fileName);

            // Act
            var lines = _sut.ReadFile(fullPath);

            // Assert
            Assert.Equal(expectedNumberOfLines, lines.Length);
        }

        [Theory]
        [InlineData("input", "81 : (1,53.38,€45) (2,88.62,€98) (3,78.48,€3) (4,72.30,€76) (5,30.18,€9) (6,46.34,€48)")]
        [InlineData("input2", "75 : (1,85.31,€29) (2,14.55,€74) (3,3.98,€16) (4,26.24,€55) (5,63.69,€52) (6,76.25,€75) (7,60.02,€74) (8,93.18,€35) (9,89.95,€78)")]
        public void FileLineContentShouldMatch(string fileName, string firstLineContent)
        {
            // Arrange
            var fullPath = Path.Combine(Environment.CurrentDirectory, "Resources", fileName);

            // Act
            var lines = _sut.ReadFile(fullPath);

            // Assert
            Assert.Equal(firstLineContent, lines[0]);
        }

        [Theory]
        [InlineData("empty_file_input")]
        public void EmptyFileShouldReturnEmptyArray(string fileName)
        {
            // Arrange
            var fullPath = Path.Combine(Environment.CurrentDirectory, "Resources", fileName);

            // Act
            var lines = _sut.ReadFile(fullPath);

            // Assert
            Assert.Empty(lines);
        }
    }
}