using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Specialist_Lab_3_1_Service.Context;
using Specialist_Lab_3_1_Service.Controllers.ControllersParams;
using Specialist_Lab_3_1_Service.Models;

namespace Specialist_Lab_3_1_Service.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeacherController : ControllerBase
{
    private readonly AppDbContext db;

    public TeacherController(AppDbContext db)
    {
        this.db = db;
    }

    [HttpGet]
    public async Task<ActionResult> Index()
    {
        return Ok(await db.Teachers
            .Select(teacher => new
            {
                teacher.Id,
                teacher.Name,
                Courses = teacher.Courses.Select(course => new
                {
                    course.Id,
                    course.Title,
                    course.Duration,
                    course.Description
                })
            })
            .ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> Get(int id)
    {
        var teacher = await db.Teachers
            .Select(teacher => new
            {
                teacher.Id,
                teacher.Name,
                Courses = teacher.Courses.Select(course => new
                {
                    course.Id,
                    course.Title,
                    course.Duration,
                    course.Description
                })
            })
            .FirstOrDefaultAsync(teacher => teacher.Id == id);
        return teacher is null ? NotFound($"Unable to find teacher with id {id}") : Ok(teacher);
    }

    [HttpPost]
    public async Task<ActionResult> Create(TeacherCreateParams teacherData)
    {
        Teacher teacher = new()
        {
            Name = teacherData.Name
        };

        if (teacherData.CoursesId is not null)
        {
            teacher.Courses = await db.Courses
                .Where(course => teacherData.CoursesId.Contains(course.Id))
                .ToListAsync();
        }

        db.Add(teacher);
        await db.SaveChangesAsync();
        return CreatedAtAction("Get", new { id = teacher.Id }, teacher);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, TeacherUpdateParams teacherData)
    {
        Teacher? teacher = await db.Teachers.FindAsync(id);
        if (teacher is null) return NotFound($"Unable to find teacher with id {id}");

        teacher.Name = teacherData.Name ?? teacher.Name;

        if (teacherData.CoursesId is not null)
        {
            List<Course> courses = await db.Courses.Where(cource => teacherData.CoursesId.Contains(cource.Id)).ToListAsync();
            await db.Entry(teacher).Collection(teacher => teacher.Courses).LoadAsync();
            teacher.Courses = courses;
        }

        try
        {
            await db.SaveChangesAsync();
            return CreatedAtAction("Get", new { id = teacher.Id }, teacher);
        }
        catch (DbUpdateConcurrencyException)
        {
            throw;
        }
    }

    [HttpDelete("id")]
    public async Task<ActionResult> Delete(int id)
    {
        Teacher? teacher = await db.Teachers.FindAsync(id);
        if (teacher is null) return NotFound($"Unable to find teacher with id {id}");
        db.Remove(teacher);
        await db.SaveChangesAsync();
        return Ok($"Teacher ID {teacher.Id} is deleted");
    }
}