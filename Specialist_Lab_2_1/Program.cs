using Microsoft.Extensions.Configuration;
using Specialist_Lab_2_1.Context;
using Specialist_Lab_2_1.Models;

namespace Specialist_Lab_2_1;

internal class Program
{
    internal static IConfiguration Config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();

    private static void Main(string[] args)
    {

        using (AppDbContext db = new())
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            List<Course> newCourses = [
                new() { Title = "C# Клиент-Сервер", Duration = 40 },
                new() { Title = "Java 2 OOP", Duration = 40 }
            ];

            db.Courses.AddRange(newCourses);
            db.SaveChanges();
        }

        using (AppDbContext db = new())
        {
            db.Courses.ToList().ForEach(
                (course) => Console.WriteLine($"Course - ID: {course.Id}, Name: {course.Title}, {course.Duration}")
            );
        }

        Course? course;
        using (AppDbContext db = new())
        {
            course = db.Courses.Where((course) => course.Title == "C# Клиент-Сервер").FirstOrDefault();
        }

        using (AppDbContext db = new())
        {
            if (course is not null)
            {
                course.Description = "На курсе рассматриваются вопросы создания современных серверных приложений, работа с базами данных, веб-сервисы и многопоточное программирование, а также разбираются возможности и преимущества платформы .Net. Курс является практико-ориентированным, что позволяет слушателям применять полученные знания и навыки сразу после окончания обучения.";
                db.Update(course);
                db.SaveChanges();
            }
        }

        using (AppDbContext db = new())
        {
            course = db.Courses.Where((course) => course.Title == "Java 2 OOP").FirstOrDefault();
            if (course is not null)
            {
                db.Remove(course);
                db.SaveChanges();
            }
        }
    }
}