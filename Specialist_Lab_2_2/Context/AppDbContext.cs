using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using Specialist_Lab_2_2.Models;

namespace Specialist_Lab_2_2.Context;

public class AppDbContext : DbContext
{
    public DbSet<Course> Courses { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder
            .UseSqlServer(Program.Config.GetConnectionString("MSSQl"))
            .UseLazyLoadingProxies()
            .LogTo(log => Debug.WriteLine(log));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        EntityTypeBuilder<Teacher> teacher = modelBuilder.Entity<Teacher>();
        EntityTypeBuilder<Student> student = modelBuilder.Entity<Student>();
        EntityTypeBuilder<Course> course = modelBuilder.Entity<Course>();

        teacher.HasMany((teacher) => teacher.Courses)
            .WithMany((courses) => courses.Teachers)
            .UsingEntity<CourseTeacher>();
        teacher.HasData(
            [
                new Teacher() {Id = 1, Name= "Шуйков Сергей Юрьевич"},
                new Teacher() {Id = 2, Name= "Кораблин Александр Игоревич"},
                new Teacher() {Id = 3, Name= "Герасименко Сергей Валерьевич"}
            ]
        );

        student.HasMany((student) => student.Courses)
            .WithMany((course) => course.Students)
            .UsingEntity<CourseStudent>();
        student.HasData(
            [
                new Student() {Id = 1, Name= "Бондаренко Александр Вячеславович"},
                new Student() {Id = 2, Name= "Иванов Иван Иванович"},
                new Student() {Id = 3, Name= "Петров Петр Петрович"},
                new Student() {Id = 4, Name= "Сергеев Сергей Сергеевич"},
                new Student() {Id = 5, Name= "Ибрагимов Ибрагим Ибрагимович"},
                new Student() {Id = 6, Name= "Васильев Василий Васильевич"},
                new Student() {Id = 7, Name= "Алексеев Алексей Алексеевич"},
                new Student() {Id = 8, Name= "Викторов Виктор Викторович"},
                new Student() {Id = 9, Name= "Ильин Илья Ильич"}
            ]
        );

        course.HasData(
            [
                new Course() {
                    Id = 1,
                    Title = "Net",
                    Duration = 40,
                    Description ="Клиент - серверная разработка под .Net на языке C# ",
                },

                new Course() {
                    Id = 2,
                    Title = "Java",
                    Duration = 20,
                    Description ="Программирование на Java. Уровень 2. Объектно - ориентированное программирование",
                },

                new Course() {
                    Id = 3,
                    Title = "C#",
                    Duration = 40,
                    Description ="Язык программирования C#",
                }
            ]
        );

        modelBuilder.Entity<CourseTeacher>().HasData(
            new CourseTeacher() { Id = 1, CourseId = 1, TeacherId = 1 },
            new CourseTeacher() { Id = 2, CourseId = 2, TeacherId = 2 },
            new CourseTeacher() { Id = 3, CourseId = 3, TeacherId = 3 }
        );

        modelBuilder.Entity<CourseStudent>().HasData(
            new CourseStudent() { Id = 1, CourseId = 1, StudentId = 1 },
            new CourseStudent() { Id = 2, CourseId = 1, StudentId = 2 },
            new CourseStudent() { Id = 3, CourseId = 1, StudentId = 3 },
            new CourseStudent() { Id = 4, CourseId = 2, StudentId = 4 },
            new CourseStudent() { Id = 5, CourseId = 2, StudentId = 5 },
            new CourseStudent() { Id = 6, CourseId = 2, StudentId = 6 },
            new CourseStudent() { Id = 7, CourseId = 3, StudentId = 7 },
            new CourseStudent() { Id = 8, CourseId = 3, StudentId = 8 },
            new CourseStudent() { Id = 9, CourseId = 3, StudentId = 9 },
            new CourseStudent() { Id = 10, CourseId = 2, StudentId = 1 },
            new CourseStudent() { Id = 11, CourseId = 3, StudentId = 1 }
        );
    }
}