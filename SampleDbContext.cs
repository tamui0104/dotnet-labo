
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace dotnet_labo;

public class SampleDbContext : DbContext
{
    public DbSet<PersonDB> PersonDB { get; internal set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
    {
        var connectionString =
            new SqliteConnectionStringBuilder { DataSource = "/Users/tommy/git/dotnet-labo/dotnet-labo/sample.db" }.ToString();
        optionBuilder.UseSqlite(new SqliteConnection(connectionString));
    }

}
