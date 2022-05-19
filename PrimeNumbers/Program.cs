using Microsoft.AspNetCore.Mvc;
using PrimeNumbers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/doIt/{filter}/{sort}", (string filter, string sort, [FromQuery] string numStr) =>
{
    var f = filter switch
    {
        "prime" => new PrimeFilter(),
        _ => throw new InvalidProgramException("Wrong filter"),
    };

    var s = sort switch
    {
        "up" => true,
        "down" => false,
        _ => false,
    };

    var nums = numStr.Split(";").Select(int.Parse).ToArray();

    var result = nums.Where(n => f.IsPrime(n));
    result = s ? result.OrderBy(n => n) : result.OrderByDescending(n => n);

    return string.Join("; ", result);
});

//var summaries = new[]
//{
//    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//};

//app.MapGet("/weatherforecast", () =>
//{
//    var forecast = Enumerable.Range(1, 5).Select(index =>
//        new WeatherForecast
//        (
//            DateTime.Now.AddDays(index),
//            Random.Shared.Next(-20, 55),
//            summaries[Random.Shared.Next(summaries.Length)]
//        ))
//        .ToArray();
//    return forecast;
//})
//.WithName("GetWeatherForecast");

app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}