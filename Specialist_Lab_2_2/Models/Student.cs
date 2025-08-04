namespace Specialist_Lab_2_2.Models;

public class Student
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public virtual List<Course> Courses { get; set; } = [];
}