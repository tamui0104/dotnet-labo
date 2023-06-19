using dotnet_labo;
using dotnet_labo.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.VisualBasic;

var name = Name.Parse("tommy");
Person p1 = new(new("tommy"), new(45));
p1.Greet();
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
var connectionString = builder.Configuration.GetConnectionString("Sample") ?? "Data Source=Sample.db";
builder.Services.AddSqlite<SampleDbContext>(connectionString);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/person", async (SampleDbContext db) =>
{
    await DbInitializer.SeedingAsync(db);
    return await db.PersonDB
        .Select(p => p.ToJson().To<Person>())
        .ToListAsync();
})
    .WithName("GetPerson");

app.Run();
