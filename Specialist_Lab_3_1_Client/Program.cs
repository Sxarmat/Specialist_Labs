using System.Threading.Tasks;

namespace Specialist_Lab_3_1_Client;

internal class Program
{
    private static async Task Main(string[] args)
    {
        Client client = new("http://localhost:5186");
        var courses = await client.CourseAllAsync();

        foreach (var cource in courses)
        {
            Console.WriteLine($"Course {cource.Id} - {cource.Title} - {cource.Description} - {cource.Duration}");
            Console.WriteLine($"\tTeachers");
            foreach (var teacher in cource.Teachers)
            {
                Console.WriteLine($"\t- {teacher.Id} - {teacher.Name}");
            }
            Console.WriteLine($"\tStudents");
            foreach (var student in cource.Students)
            {
                Console.WriteLine($"\t- {student.Id} - {student.Name}");
            }
        }       
    }
}