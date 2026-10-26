using System.Text.Json.Serialization;
using CookieClicker.Model.Games;
using CookieClicker.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
builder.Services.AddControllers()
    .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddSingleton<Game>();
builder.Services.AddSingleton<PlayerService>();
builder.Services.AddSingleton<GameService>();
builder.Services.AddSingleton<BoutiqueService>();
builder.Services.AddSingleton<PowerUpService>();
builder.Services.AddSingleton<BatimentService>();
builder.Services.AddSingleton<StatistiqueService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();
app.MapControllers();
app.Run();
