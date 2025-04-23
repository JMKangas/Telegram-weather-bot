using System.Text.Json;
using System.Text.Json.Serialization;
using System.Globalization;

namespace TelegramBotAPI
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public partial class Weather
    {
        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }

        [JsonPropertyName("hourly_units")]
        public HourlyUnits? HourlyUnits { get; set; }

        [JsonPropertyName("hourly")]
        public Hourly? Hourly { get; set; }
    }

    public partial class Hourly
    {
        [JsonPropertyName("time")]
        public List<string> Time { get; set; }

        [JsonPropertyName("temperature_2m")]
        public List<double> Temperature2M { get; set; }
    }

    public partial class HourlyUnits
    {
        [JsonPropertyName("time")]
        public string Time { get; set; }

        [JsonPropertyName("temperature_2m")]
        public string Temperature2M { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    }

    public partial class Weather
    {
#pragma warning disable CS8603 // Possible null reference return.
        public static Weather FromJson(string json) => JsonSerializer.Deserialize<Weather>(json);
#pragma warning restore CS8603 // Possible null reference return.

    }

}

