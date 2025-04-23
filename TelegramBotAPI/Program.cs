
using TelegramBotAPI.Services;

namespace TelegramBotAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddHttpClient();
            builder.Services.AddScoped<WeatherService>();
            builder.Services.AddScoped<TelegramBotAPI.TelegramBot.Bot>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            var scope = app.Services.CreateScope();
            var bot = scope.ServiceProvider.GetRequiredService<TelegramBotAPI.TelegramBot.Bot>();
            app.Run();
        }
    }
}
