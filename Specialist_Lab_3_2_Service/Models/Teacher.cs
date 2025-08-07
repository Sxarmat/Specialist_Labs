namespace Specialist_Lab_3_2_Service.Models;

public class Teacher
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public List<Course> Courses { get; set; } = [];
}