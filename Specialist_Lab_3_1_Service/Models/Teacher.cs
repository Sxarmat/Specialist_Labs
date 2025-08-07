using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Specialist_Lab_3_1_Service.Models;

public class Teacher
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public List<Course> Courses { get; set; } = [];
}