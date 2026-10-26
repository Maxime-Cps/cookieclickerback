using System.Text.Json.Serialization;
using CookieClicker.Model.Games;
using CookieClicker.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
builder.Services.AddControllers()
    .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddSingleton<Game>();
builder.Services.AddSingleton<GameService>();
builder.Services.AddSingleton<BoutiqueService>();
builder.Services.AddSingleton<PowerUpService>();
builder.Services.AddSingleton<BatimentService>();
builder.Services.AddSingleton<StatistiqueService>();

builder.Services.AddCors(o => o.AddDefaultPolicy(policy => policy
    .SetIsOriginAllowed(origin => new Uri(origin).IsLoopback)
    .AllowAnyHeader()
    .AllowAnyMethod()));

var app = builder.Build();



app.UseHttpsRedirection();
app.UseCors();
app.UseDefaultFiles();
app.UseStaticFiles();
app.MapControllers();
app.Run();
