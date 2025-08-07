using System.ComponentModel.DataAnnotations;

namespace Specialist_Lab_3_1_Service.Models;

public class Student
{
    public int Id { get; set; }
    public  required string Name { get; set; }
    public List<Course> Courses { get; set; } = [];
}