using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Specialist_Lab_3_1_Service.Context;
using Specialist_Lab_3_1_Service.Models;

namespace Specialist_Lab_3_1_Service.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class CourseAdminController : ControllerBase
{
    private readonly AppDbContext db;

    public CourseAdminController(AppDbContext db) => this.db = db;

    [HttpGet]
    public async Task<ActionResult> GetAllRelations()
    {
        return Ok(await db.Courses
            .Select(course => new
            {
                course.Id,
                course.Title,
                Teachers = course.Teachers.Select(teacher => new { teacher.Id, teacher.Name }).ToList(),
                Students = course.Students.Select(student => new { student.Id, student.Name }).ToList()
            }).ToListAsync()
        );
    }

    [HttpGet]
    public async Task<ActionResult> GetCourseRelations(int id)
    {
        var course = await db.Courses
            .Select(course => new
            {
                course.Id,
                course.Title,
                Teachers = course.Teachers.Select(teacher => new { teacher.Id, teacher.Name }).ToList(),
                Students = course.Students.Select(student => new { student.Id, student.Name }).ToList()
            })
            .FirstOrDefaultAsync(c => c.Id == id);
        return course is null ? NotFound($"Unable to find course with id {id}") : Ok(course);
    }

    [HttpPost]
    public async Task<ActionResult> AssignTeachers(int courseId, int[] teachersId)
    {
        Course? course = await db.Courses.FindAsync(courseId);
        if (course is null) return NotFound($"Unable to find teacher with id {courseId}");
        await db.Entry(course).Collection(course => course.Teachers).LoadAsync();
        int[] currentTeachers = course.Teachers.Select(course => course.Id).ToArray();
        course.Teachers.AddRange(
            db.Teachers.Where(
                teacher => teachersId.Contains(teacher.Id) && !currentTeachers.Contains(teacher.Id)
            )
        );
        await db.SaveChangesAsync();
        return Ok(new
        {
            Message = "Success",
            Data = new
            {
                course.Id,
                course.Title,
                Teachers = course.Teachers.Select(teacher => new { teacher.Id, teacher.Name }).OrderBy(teacher => teacher.Id)
            }
        });
    }

    [HttpPost]
    public async Task<ActionResult> RemoveTeachers(int courseId, int[] teachersId)
    {
        Course? course = await db.Courses.FindAsync(courseId);
        if (course is null) return NotFound($"Unable to find teacher with id {courseId}");
        await db.Entry(course).Collection(course => course.Teachers).LoadAsync();
        course.Teachers.RemoveAll(teacher => teachersId.Contains(teacher.Id));
        await db.SaveChangesAsync();
        return Ok(new
        {
            Message = "Success",
            Data = new
            {
                course.Id,
                course.Title,
                Teachers = course.Teachers.Select(teacher => new { teacher.Id, teacher.Name }).OrderBy(teacher => teacher.Id)
            }
        });
    }

    [HttpPost]
    public async Task<ActionResult> EnrollStudent(int courseId, int[] studentsId)
    {
        Course? course = await db.Courses.FindAsync(courseId);
        if (course is null) return NotFound($"Unable to find teacher with id {courseId}");
        await db.Entry(course).Collection(course => course.Students).LoadAsync();
        int[] currentStudents = course.Students.Select(course => course.Id).ToArray();
        course.Students.AddRange(
            db.Students.Where(
                student => studentsId.Contains(student.Id) && !currentStudents.Contains(student.Id)
            )
        );
        await db.SaveChangesAsync();
        return Ok(new
        {
            Message = "Success",
            Data = new
            {
                course.Id,
                course.Title,
                Students = course.Students.Select(student => new { student.Id, student.Name }).OrderBy(student => student.Id)
            }
        });
    }

    [HttpPost]
    public async Task<ActionResult> ExpelStudents(int courseId, int[] studentsId)
    {
        Course? course = await db.Courses.FindAsync(courseId);
        if (course is null) return NotFound($"Unable to find teacher with id {courseId}");
        await db.Entry(course).Collection(course => course.Students).LoadAsync();
        course.Students.RemoveAll(student => studentsId.Contains(student.Id));
        await db.SaveChangesAsync();
        return Ok(new
        {
            Message = "Success",
            Data = new
            {
                course.Id,
                course.Title,
                Student = course.Students.Select(student => new { student.Id, student.Name }).OrderBy(student => student.Id)
            }
        });
    }
}