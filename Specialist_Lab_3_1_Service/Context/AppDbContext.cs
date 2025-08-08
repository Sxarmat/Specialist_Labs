using Microsoft.EntityFrameworkCore;
using Specialist_Lab_3_1_Service.Models;

namespace Specialist_Lab_3_1_Service.Context;

public class AppDbContext : DbContext
{
    public DbSet<Course> Courses { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Teacher> Teachers { get; set; }

    public AppDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Teacher>()
            .HasMany((teacher) => teacher.Courses)
            .WithMany((course) => course.Teachers)
            .UsingEntity<CourseTeacher>();

        modelBuilder.Entity<Student>()
            .HasMany((student) => student.Courses)
            .WithMany((course) => course.Students)
            .UsingEntity<CourseStudent>();
    }
}