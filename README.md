🌤️ Jyväskylä Weather Telegram Bot

A simple and efficient Telegram bot built with .NET 9 Web API that provides current weather, hourly forecast, and daily average for the city of Jyväskylä, Finland.
🚀 Features

    🔸 Get the current weather in Jyväskylä

    🔸 Request a forecast for a specific hour

    🔸 View the daily weather average

    🔸 Works via Telegram bot and also responds to HTTP API calls in a browser

🛠️ Setup Guide

Follow these steps to set up and run the Telegram weather bot.
1. 🔧 Create a Bot with BotFather

    Open Telegram and search for @BotFather

    Start a chat and run the command /newbot

    Follow the prompts:

        Name your bot (e.g., Jyväskylä Weather Bot)

        Choose a username ending with bot (e.g., jyvaskyla_weather_bot)

    BotFather will give you a Bot Token — copy it for the next step.

2. 🧪 Configure and Run the Bot
Clone this repository

git clone https://github.com/JMKangas/Telegram-weather-bot

Add your Telegram Bot Token

Place your token inside appsettings.Development.json:

{
  "TelegramBot": {
    "Token": "YOUR_TELEGRAM_BOT_TOKEN"
  }
}

✅ Or alternatively, set it as an environment variable:

export TelegramBot__Token=YOUR_TELEGRAM_BOT_TOKEN

Your bot constructor should load the token like this:

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
    .Build();

var token = configuration["TelegramBot:Token"];
var _botClient = new TelegramBotClient(token);

    ℹ️ Make sure the appsettings.Development.json file is set to "Copy if newer" in file properties.

Run the bot

dotnet run

The bot will start polling for messages and respond to your commands.
💬 Using the Bot

Once your bot is live:

    Search for your bot in Telegram (e.g., @jyvaskyla_weather_bot)

    Start a chat

    Use the following commands:

Command	Description
Lämpötila	Shows current weather in Jyväskylä
Ennuste 2	Shows forecast for two hours ahead
Keskiarvo	Shows today’s average temperature

    🔤 Commands are case-insensitive

🌐 Web API Access

You can also get current weather directly via a browser:

  https://localhost:7281/api/weather

This returns a string of current weather for Jyväskylä. Check which port is being listened.
📦 Technologies Used

    .NET 9 Web API

    Telegram.Bot

    Open-Meteo Free Weather API

    C#

📝 License

This project is licensed under the GNU Affero General Public License v3.0 (AGPL-3.0).
See the LICENSE file for more details.