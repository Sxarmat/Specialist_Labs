using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Specialist_Lab_2_2.Context;
using Specialist_Lab_2_2.Models;

namespace Specialist_Lab_2_2;

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
            List<Teacher> teachers = db.Teachers.Include(t => t.Courses).ThenInclude(c => c.Students).ToList();
            teachers.ForEach(teacher =>
            {
                Console.WriteLine($"Преподаватель {teacher.Name} ведет следующие курсы:");
                teacher?.Courses?.ForEach(course =>
                {
                    Console.WriteLine($"    Курс {course.Title}. На курс записаны студенты:");
                    course?.Students?.ForEach(student =>
                    {
                        Console.WriteLine($"        - {student.Name}");
                    });
                });
            });
        }

        using (AppDbContext db = new())
        {
            List<Student> students = db.Students.ToList();
            students.ForEach((student) =>
            {
                if (student.Name == "Бондаренко Александр Вячеславович")
                {
                    Console.WriteLine($"Студент {student.Name} записан на следующие курсы:");
                    db.Entry(student).Collection(student => student.Courses).Load();
                    student.Courses.ForEach((cource) =>
                    {
                        Console.WriteLine($"    Курс: {cource.Title}. Ведут преподаватели");
                        db.Entry(cource).Collection(cource => cource.Teachers).Load();
                        cource.Teachers.ForEach((teacher) => Console.WriteLine($"        - {teacher.Name}"));
                    });
                }
            });
        }

        using (AppDbContext db = new())
        {
            List<Course> courses = db.Courses.ToList();
            courses.ForEach((course) =>
            {
                Console.WriteLine($"Курс {course.Title}");
                Console.WriteLine($"    Ведут преподаватели:");
                course.Teachers.ForEach((teacher) => { Console.WriteLine($"        -{teacher.Name}"); });
                Console.WriteLine($"    Записаны студенты:");
                course.Students.ForEach((student) => { Console.WriteLine($"        -{student.Name}"); });
            });
        }
    }
}