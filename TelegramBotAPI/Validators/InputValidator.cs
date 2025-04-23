using TelegramBotAPI.Enums;

namespace TelegramBotAPI.Validators
{
    public static class InputValidator
    {
        public static UserInputType ValidateUserInput(string input)
        {
            return IsValidUserInput(input);
        }

        private static UserInputType IsValidUserInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) { return UserInputType.None; }
            else if (input.StartsWith("lämpötila", StringComparison.OrdinalIgnoreCase))
            {
                return UserInputType.CurrentTemp;
            }
            else if (input.StartsWith("ennuste", StringComparison.OrdinalIgnoreCase))
            {
                return UserInputType.Forecast;
            }
            else if (input.Contains("keskiarvo", StringComparison.OrdinalIgnoreCase))
            {
                return UserInputType.DailyAverage;
            }
            else
            {
                return UserInputType.Invalid;
            }
        }

    }
}
