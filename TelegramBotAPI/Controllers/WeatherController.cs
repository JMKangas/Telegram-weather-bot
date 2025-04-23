using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TelegramBotAPI.Services;

namespace TelegramBotAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly WeatherService _weatherService;

        public WeatherController(WeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet]
        public async Task<IActionResult> GetWeather()
        {
            var data = await _weatherService.GetWeatherAsync();

            var aika = DateTime.Now;

            var temp = (double)1;
            for (int i = 0; i < 23; i++)
            {
                aika = DateTime.Now;
                if (aika.Hour == i)
                {
                    temp = data.Hourly.Temperature2M[i];
                }
            }

            return Ok($"Lämpötila on; {temp}°C Jyväskylässä klo.{aika.ToShortTimeString()}");
        }
    }
}
