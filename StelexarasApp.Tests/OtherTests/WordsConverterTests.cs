using StelexarasApp.DataAccess.Helpers;

namespace StelexarasApp.Tests.OtherTests
{
    public class WordsConverterTests
    {
        [Theory]
        [InlineData("John Doe", true)] // Positive case
        [InlineData("Jane", false)] // Negative case: FullName has only one word
        [InlineData("", false)] // Negative case: FullName is empty
        [InlineData(" ", false)] // Negative case: FullName is whitespace
        public void IsValidInput_ShouldReturnExpectedResult(string fullName, bool expectedResult)
        {
            // Act
            var result = DataChecksAndConverters.IsValidFullNameInput(fullName);

            // Assert
            Assert.Equal(expectedResult, result);
        }
    }
}