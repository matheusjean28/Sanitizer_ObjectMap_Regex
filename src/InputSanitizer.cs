using System;
using System.Text.RegularExpressions;

namespace sanetizerUserInput
{
    public class InputSanitizer
    {

        //you can change the regex as you needed
        private static readonly Regex SanitizeRegex = new Regex("[^a-zA-Z0-9 @.,;:!?-]");

        public string SanitizeInput(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }
            return SanitizeRegex.Replace(input, string.Empty);
        }
    }
}
