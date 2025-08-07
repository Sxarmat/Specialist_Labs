using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Specialist_Lab_3_2_Service.Models;

namespace Specialist_Lab_3_2_Service.Context;

public class AppDbContext : DbContext
{
    public DbSet<Course> Courses { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Teacher> Teachers { get; set; }

    public AppDbContext(DbContextOptions options)
    : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        EntityTypeBuilder<Teacher> teacher = modelBuilder.Entity<Teacher>();
        EntityTypeBuilder<Student> student = modelBuilder.Entity<Student>();
        EntityTypeBuilder<Course> course = modelBuilder.Entity<Course>();

        teacher
            .HasMany((teacher) => teacher.Courses)
            .WithMany((course) => course.Teachers)
            .UsingEntity<CourseTeacher>();

        student
            .HasMany((student) => student.Courses)
            .WithMany((course) => course.Students)
            .UsingEntity<CourseStudent>();
    }
}