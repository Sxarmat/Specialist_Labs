namespace Specialist_Lab_4_1_ClassLib;

public class Teacher
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public List<Course> Courses { get; set; } = [];
}