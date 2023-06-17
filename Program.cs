

using dotnet_labo;
using Microsoft.VisualBasic;

var name = Name.Parse("tommy");
Person p1 = new(new("tommy"), new(45));
Person p2 = new(new("tommy"), new(45));
System.Console.WriteLine(p1 == p2);
Console.WriteLine(p1.ToJson());
Person? person = $$"""{"Name":"{{name}}", "Age":45 }""".To<Person>();
Console.WriteLine(person?.ToJson());

Item? item = $$"""{"Name":"A1"}""".To<Item>();
System.Console.WriteLine(item?.ToJson());


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<SampleDbContext>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateTime.Now.AddDays(index),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
    .WithName("GetWeatherForecast");

app.Run();



record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}


