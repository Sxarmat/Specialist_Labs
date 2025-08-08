using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Specialist_Lab_3_1_Service.Models;
using Specialist_Lab_3_1_Service.Context;
using Specialist_Lab_3_1_Service.Controllers.ControllersParams;

namespace Specialist_Lab_3_1_Service.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CourseController : ControllerBase
{
    private readonly AppDbContext db;

    public CourseController(AppDbContext db) => this.db = db;

    [HttpGet]
    public async Task<ActionResult> Index()
    {
        return Ok(await db.Courses
            .Select(course => new
            {
                course.Id,
                course.Title,
                course.Duration,
                course.Description,
            }).ToListAsync()
        );
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> Get(int id)
    {
        var course = await db.Courses
            .Select(course => new
            {
                course.Id,
                course.Title,
                course.Duration,
                course.Description,
            })
            .FirstOrDefaultAsync(c => c.Id == id);
        return course is null ? NotFound($"Unable to find course with id {id}") : Ok(course);
    }

    [HttpPost]
    public async Task<ActionResult> Create(CourseCreateParams courseData)
    {
        Course course = new()
        {
            Title = courseData.Title,
            Duration = courseData.Duration,
            Description = courseData.Description
        };
        db.Courses.Add(course);
        await db.SaveChangesAsync();
        return CreatedAtAction("Get", new { id = course.Id }, course);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, CourseUpdateParams courseData)
    {
        Course? course = await db.Courses.FindAsync(id);
        if (course is null) return NotFound($"Unable to find course with id {id}");
        course.Title = courseData.Title ?? course.Title;
        course.Duration = courseData.Duration ?? course.Duration;
        course.Description = courseData.Description ?? course.Description;
        try
        {
            await db.SaveChangesAsync();
            return CreatedAtAction("Get", new { id = course.Id }, course);
        }
        catch (DbUpdateConcurrencyException)
        {
            throw;
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        Course? course = db.Courses.FirstOrDefault(course => course.Id == id);
        if (course is null) return NotFound($"Unable to find course with id {id}");
        db.Remove(course);
        await db.SaveChangesAsync();
        return Ok($"Course ID {course.Id} is deleted");
    }
}