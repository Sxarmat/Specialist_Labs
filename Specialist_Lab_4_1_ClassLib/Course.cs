namespace Specialist_Lab_4_1_ClassLib;

public class Course
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required int Duration { get; set; }
    public string? Description { get; set; }

    public List<Teacher> Teachers { get; set; } = [];
    public List<Student> Students { get; set; } = [];
}