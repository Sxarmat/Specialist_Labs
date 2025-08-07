namespace Specialist_Lab_3_1_Service.Controllers.ControllersParams;

public class StudentCreateParams
{
    public required string Name { get; set; }
    public List<int>? CoursesId { get; set; }
}