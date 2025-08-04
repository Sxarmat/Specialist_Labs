namespace Specialist_Lab_2_1.Models;

public class Course
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required int Duration { get; set; }
    public string? Description { get; set; }
}