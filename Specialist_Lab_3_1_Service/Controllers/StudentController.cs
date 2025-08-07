using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Specialist_Lab_3_1_Service.Context;
using Specialist_Lab_3_1_Service.Controllers.ControllersParams;
using Specialist_Lab_3_1_Service.Models;

namespace Specialist_Lab_3_1_Service.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentController : ControllerBase
{
    private readonly AppDbContext db;

    public StudentController(AppDbContext db)
    {
        this.db = db;
    }

    [HttpGet]
    public ActionResult Index()
    {
        return Ok( db.Students.Include(student => student.Courses).ToList());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> Get(int id)
    {
        Student? student = await db.Students
            .Include(student => student.Courses)
            .FirstOrDefaultAsync(student => student.Id == id);
        return student is null ? NotFound($"Unable to find student with id {id}") : Ok(student);
    }

    [HttpPost]
    public async Task<ActionResult> Create(StudentCreateParams studentData)
    {
        Student student = new()
        {
            Name = studentData.Name
        };

        if (studentData.CoursesId is not null)
        {
            student.Courses = await db.Courses
                .Where(course => studentData.CoursesId.Contains(course.Id))
                .ToListAsync();
        }

        db.Add(student);
        await db.SaveChangesAsync();
        return CreatedAtAction("Get", new { id = student.Id }, student);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, StudentUpdateParams studentData)
    {
        Student? student = await db.Students.FindAsync(id);
        if (student is null) return NotFound($"Unable to find student with id {id}");

        student.Name = studentData.Name ?? student.Name;

        if (studentData.CoursesId is not null)
        {
            List<Course> courses = await db.Courses.Where(cource => studentData.CoursesId.Contains(cource.Id)).ToListAsync();
            await db.Entry(student).Collection(student => student.Courses).LoadAsync();
            student.Courses = courses;
        }

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

    [HttpDelete("id")]
    public async Task<ActionResult> Delete(int id)
    {
        Student? student = await db.Students.FindAsync(id);
        if (student is null) return NotFound($"Unable to find student with id {id}");
        db.Remove(student);
        await db.SaveChangesAsync();
        return Ok($"Student ID {student.Id} is deleted");
    }
}