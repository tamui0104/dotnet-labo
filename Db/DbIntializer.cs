namespace dotnet_labo.Db;

public class DbInitializer
{
    public static async Task SeedingAsync(SampleDbContext context)
    {
        await context.Database.EnsureCreatedAsync();
        if (context.PersonDB.Any()) return;
        await context.PersonDB.AddRangeAsync(
         new PersonDB(1, "tomita", 45),
         new PersonDB(2, "miki", 29)
        );
        await context.SaveChangesAsync();
    }
}
