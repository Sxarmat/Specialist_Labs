namespace Specialist_Lab_2_2.Models;

public class Course
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required int Duration { get; set; }
    public string? Description { get; set; }

    public virtual List<Teacher> Teachers { get; set; } = [];
    public virtual List<Student> Students { get; set; } = [];
}