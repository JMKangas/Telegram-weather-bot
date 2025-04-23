namespace TelegramBotAPI.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;

        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Weather> GetWeatherAsync()
        {
            string url = "https://api.open-meteo.com/v1/forecast?latitude=62.2415&longitude=25.7209&hourly=temperature_2m&forecast_days=1";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();

            return Weather.FromJson(json); // use QuickType-generated static method
        }
    }
    //public class WeatherService
    //{
    //    private readonly HttpClient _httpClient;

    //    public WeatherService(HttpClient httpClient)
    //    {
    //        _httpClient = httpClient;
    //    }

    //    public async Task<string> GetWeatherAsync()
    //    {
    //        string url = "https://api.open-meteo.com/v1/forecast?latitude=62.2415&longitude=25.7209&hourly=temperature_2m&forecast_days=1";
    //        HttpResponseMessage response = await _httpClient.GetAsync(url);

    //        response.EnsureSuccessStatusCode();

    //        return await response.Content.ReadAsStringAsync();
    //    }
    //}
}
