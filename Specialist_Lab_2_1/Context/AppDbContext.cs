using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Specialist_Lab_2_1.Models;

namespace Specialist_Lab_2_1.Context;

public class AppDbContext : DbContext
{
    public DbSet<Course> Courses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer(Program.Config.GetConnectionString("MSSQl"));
    }
}