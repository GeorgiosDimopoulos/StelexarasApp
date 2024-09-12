using StelexarasApp.DataAccess.Helpers;

namespace StelexarasApp.Tests.OtherTests
{
    public class WordsConverterTests
    {
        [Theory]
        [InlineData("John Doe", "SkiniName", true)] // Positive case
        [InlineData("Jane", "SkiniName", false)] // Negative case: FullName has only one word
        [InlineData("John Doe", "", false)] // Negative case: SkiniName is empty
        [InlineData("", "SkiniName", false)] // Negative case: FullName is empty
        [InlineData("John Doe", " ", false)] // Negative case: SkiniName is whitespace
        [InlineData(" ", "SkiniName", false)] // Negative case: FullName is whitespace
        [InlineData("John Doe", "Skini Name", true)] // Positive case: SkiniName has multiple words
        public void IsValidInput_ShouldReturnExpectedResult(string fullName, string skiniName, bool expectedResult)
        {
            // Act
            var result = DataChecksAndConverters.IsValidFullNameInput(fullName);
            var result2 = DataChecksAndConverters.IsValidFullNameInput(skiniName);

            // Assert
            Assert.Equal(expectedResult, result);
            Assert.Equal(expectedResult, result2);
        }
    }
}