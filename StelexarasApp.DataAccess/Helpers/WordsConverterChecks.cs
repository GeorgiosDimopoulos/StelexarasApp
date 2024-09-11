namespace StelexarasApp.DataAccess.Helpers
{
    public static class WordsConverterChecks
    {
        public static bool IsValidFullNameInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return false;
            }
            
            var parts = input.Trim().Split(' ');
            return parts.Length >= 2;
        }
    }
}
