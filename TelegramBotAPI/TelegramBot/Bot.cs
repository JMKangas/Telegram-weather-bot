using System.Text.RegularExpressions;
using Telegram.Bot.Types;
using Telegram.Bot;
using TelegramBotAPI.Enums;
using TelegramBotAPI.Services;

namespace TelegramBotAPI.TelegramBot
{
    public class Bot
    {
        private readonly WeatherService _weatherService;
        private readonly TelegramBotClient _botClient;

        public Bot(WeatherService weatherService)
        {
            _weatherService = weatherService;
            _botClient = new TelegramBotClient("1234567890"); //HOX!! Add your own api token as string parameter!!
            _botClient.StartReceiving(UpdateHandler, ErrorHandler);
        }

        public async Task UpdateHandler(ITelegramBotClient bot, Update update, CancellationToken token)
        {
            if (update.Message is null || string.IsNullOrWhiteSpace(update.Message?.Text)) { return; }

            double? forecastHour = null;
            var input = Validators.InputValidator.ValidateUserInput(update.Message.Text);

            var weather = await _weatherService.GetWeatherAsync();

            string response = CreateBotMessage(input, update.Message.Text, weather);

            await bot.SendMessage(
                chatId: update.Message.Chat.Id,
                text: response,
                parseMode: Telegram.Bot.Types.Enums.ParseMode.Html,
                cancellationToken: token
            );

        }

        private string CreateBotMessage(UserInputType input, string userInput, Weather weather)
        {
            double temp = 0;
            TimeOnly time = TimeOnly.FromDateTime(DateTime.Now);
            switch (input)
            {
                case UserInputType.None:
                    //forecastHours = null;
                    return $"Kirjoittamalla\n<b>Lämpötila</b>: Saat tämän hetkisen lämpötilan \n<b>Ennuste</b> '<b>x</b>': Saat ennusteen lämpötilasta x-tunnin päähän esim. Ennuste 4 \n<b>Keskiarvo</b>: Saat kuluvan vuorokauden keskiarvon";
                case UserInputType.Invalid:
                    return $"Kirjoittamalla\n<b>Lämpötila</b>: Saat tämän hetkisen lämpötilan \n<b>Ennuste</b> '<b>x</b>': Saat ennusteen lämpötilasta x-tunnin päähän esim. Ennuste 4 \n<b>Keskiarvo</b>: Saat kuluvan vuorokauden keskiarvon";
                case UserInputType.Forecast:
                    Match match = Regex.Match(userInput, @"^\s*\w+\b.*?\b(-?\d+)", RegexOptions.IgnoreCase);
                    if (match.Success)
                    {
                        int forecastHours = Math.Abs(int.Parse(match.Groups[1].Value));
                        time = time.AddHours(forecastHours);
                        temp = GenerateTemperature(weather, input, time.Hour);
                        return $"Jyväskylän lämpötilaennuste klo.{time} on {temp}°C";
                    }
                    else
                    {
                        //Jotain lokitusta
                        return $"unable to tell";
                    }
                case UserInputType.CurrentTemp:
                    temp = GenerateTemperature(weather, input, time.Hour);
                    return $"Jyväskylän lämpötila klo.{time} on {temp}°C";

                case UserInputType.DailyAverage:
                    temp = GenerateTemperature(weather, input, time.Hour);
                    return $"Jyväskylän vuorokauden keskiarvoennuste on {temp.ToString("F1")}°C";
                default:
                    return $"unable to tell";
            }
        }

        private double GenerateTemperature(Weather weather, UserInputType input, int forecastHour = 0)
        {
            double currentTemp = 0;
            double forecast = 0;
            double dailyAverage = 0;
            TimeOnly time = TimeOnly.FromDateTime(DateTime.Now);

            for (int i = 0; i < weather.Hourly.Time.Count; i++)
            {
                if (DateTime.TryParse(weather.Hourly.Time[i], out DateTime forecastTime) &&
                forecastTime.Hour == forecastHour)
                {
                    forecast = weather.Hourly.Temperature2M[i];
                }
                if (i == time.Hour)
                {
                    currentTemp = weather.Hourly.Temperature2M[i];
                }
                dailyAverage += weather.Hourly.Temperature2M[i];
            }
            switch (input)
            {
                case UserInputType.Forecast:
                    return forecast;
                case UserInputType.CurrentTemp:
                    return currentTemp;
                case UserInputType.DailyAverage:
                    dailyAverage = dailyAverage / weather.Hourly.Temperature2M.Count;
                    return dailyAverage;
                default:
                    return 0;
            }
        }

        public Task ErrorHandler(ITelegramBotClient bot, Exception ex, CancellationToken token)
        {
            Console.WriteLine($"Telegram Bot Error: {ex.Message}");
            return Task.CompletedTask;
        }
    }
}
