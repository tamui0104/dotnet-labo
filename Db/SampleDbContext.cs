
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace dotnet_labo;

public class SampleDbContext : DbContext
{
    public SampleDbContext(DbContextOptions options) : base(options) { }
    public DbSet<PersonDB> PersonDB { get; internal set; }
}
