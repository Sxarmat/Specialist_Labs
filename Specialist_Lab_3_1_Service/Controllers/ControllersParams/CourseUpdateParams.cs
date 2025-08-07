namespace Specialist_Lab_3_1_Service.Controllers.ControllersParams;

public class CourseUpdateParams
{
    public string? Title { get; set; }
    public int? Duration { get; set; }
    public string? Description { get; set; }
    public List<int>? TeachersId { get; set; }
    public List<int>? StudentsId { get; set; }
}