using System.Reflection;
using Microsoft.EntityFrameworkCore;
using NewsD.Model;

namespace NewsD.DataAccess;

public class NewsDDataContext : DbContext
{
    public static string DBO_SCHEMA = "Dbo";
    private readonly IConfiguration _configuration;

    public NewsDDataContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public DbSet<User>? Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly, t => t?.Namespace?.EndsWith("Configuration") ?? false);
        base.OnModelCreating(modelBuilder);
    }


    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        var connectionStr = _configuration.GetConnectionString("Database");
        options.UseSqlServer(connectionStr);
    }
}