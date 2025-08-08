namespace Specialist_Lab_3_1_Service.Controllers.ControllersParams;

public class CourseCreateParams
{
    public required string Title { get; set; }
    public required int Duration { get; set; }
    public string? Description { get; set; }
}