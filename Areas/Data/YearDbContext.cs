using Microsoft.EntityFrameworkCore;
using PS6.Areas.Data.Models;

namespace PS6.Areas.YearDb;

public class YearDbContext : DbContext
{
    public DbSet<YearValidationResult>? YearValidationResult { get; set; }

    public YearDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<YearValidationResult>().HasKey(x => x.Id);
    }

}
