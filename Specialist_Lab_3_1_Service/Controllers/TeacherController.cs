using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Specialist_Lab_3_1_Service.Context;
using Specialist_Lab_3_1_Service.Models;

namespace Specialist_Lab_3_1_Service.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeacherController : ControllerBase
{
    private readonly AppDbContext db;

    public TeacherController(AppDbContext db) => this.db = db;

    [HttpGet]
    public async Task<ActionResult> Index()
    {
        return Ok(await db.Teachers
            .Select(teacher => new
            {
                teacher.Id,
                teacher.Name,
                Courses = teacher.Courses.Select(course => course.Title)
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
                teacher.Name
            })
            .FirstOrDefaultAsync(teacher => teacher.Id == id);
        return teacher is null ? NotFound($"Unable to find teacher with id {id}") : Ok(teacher);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] string name)
    {
        Teacher teacher = new() { Name = name };
        db.Add(teacher);
        await db.SaveChangesAsync();
        return Ok(teacher);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] string name)
    {
        Teacher? teacher = await db.Teachers.FindAsync(id);
        if (teacher is null) return NotFound($"Unable to find teacher with id {id}");
        teacher.Name = name ?? teacher.Name;
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

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        Teacher? teacher = await db.Teachers.FindAsync(id);
        if (teacher is null) return NotFound($"Unable to find teacher with id {id}");
        db.Remove(teacher);
        await db.SaveChangesAsync();
        return Ok($"Teacher ID {teacher.Id} is deleted");
    }
}