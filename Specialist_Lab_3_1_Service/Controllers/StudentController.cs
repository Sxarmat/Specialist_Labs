using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Specialist_Lab_3_1_Service.Context;
using Specialist_Lab_3_1_Service.Models;

namespace Specialist_Lab_3_1_Service.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentController : ControllerBase
{
    private readonly AppDbContext db;

    public StudentController(AppDbContext db) => this.db = db;

    [HttpGet]
    public async Task<ActionResult> Index()
    {
        return Ok(await db.Students
            .Select(student => new
            {
                student.Id,
                student.Name,
            })
            .ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> Get(int id)
    {
        var student = await db.Students
            .Select(student => new
            {
                student.Id,
                student.Name
            })
            .FirstOrDefaultAsync(student => student.Id == id);
        return student is null ? NotFound($"Unable to find student with id {id}") : Ok(student);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] string name)
    {
        Student student = new()
        {
            Name = name
        };
        db.Add(student);
        await db.SaveChangesAsync();
        return CreatedAtAction("Get", new { id = student.Id }, student);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] string name)
    {
        Student? student = await db.Students.FindAsync(id);
        if (student is null) return NotFound($"Unable to find student with id {id}");
        student.Name = name ?? student.Name;
        try
        {
            await db.SaveChangesAsync();
            return CreatedAtAction("Get", new { id = student.Id }, student);
        }
        catch (DbUpdateConcurrencyException)
        {
            throw;
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        Student? student = await db.Students.FindAsync(id);
        if (student is null) return NotFound($"Unable to find student with id {id}");
        db.Remove(student);
        await db.SaveChangesAsync();
        return Ok($"Student ID {student.Id} is deleted");
    }
}