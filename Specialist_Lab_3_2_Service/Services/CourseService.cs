using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using Specialist_Lab_3_2_Service.Context;
using Specialist_Lab_3_2_Service.Models;

namespace Specialist_Lab_3_2_Service.Services;

public class CourseApiService : CourseService.CourseServiceBase
{
    private readonly AppDbContext db;

    public CourseApiService(AppDbContext db)
    {
        this.db = db;
    }

    public override async Task<ListCourseReply> IndexCourse(Empty request, ServerCallContext context)
    {
        ListCourseReply reply = new();

        reply.Courses.AddRange(
            await db.Courses.Select(course => new CourseReply()
            {
                Id = course.Id,
                Title = course.Title,
                Duration = course.Duration,
                Decription = course.Description
            })
            .ToListAsync()
        );

        return reply;
    }

    public override async Task<CourseReply> GetCourse(GetCourseRequest request, ServerCallContext context)
    {
        Course? course = await db.Courses.FindAsync(request.Id)
            ?? throw new RpcException(new Status(StatusCode.NotFound, "Course not found"));

        return new CourseReply()
        {
            Id = course.Id,
            Title = course.Title,
            Duration = course.Duration,
            Decription = course.Description
        };
    }

    public override async Task<CourseReply> UpdateCourse(UpdateCourseRequest request, ServerCallContext context)
    {
        Course? course = await db.Courses.FindAsync(request.Id)
            ?? throw new RpcException(new Status(StatusCode.NotFound, "Course not found"));

        course.Title = request.Title;
        course.Duration = request.Duration;
        course.Description = request.Description;

        await db.SaveChangesAsync();

        return new CourseReply()
        {
            Title = course.Title,
            Duration = course.Duration,
            Decription = course.Description
        };
    }

    public override async Task<CourseReply> DeleteCourse(DeleteCourseRequest request, ServerCallContext context)
    {
        Course? course = await db.Courses.FindAsync(request.Id)
            ?? throw new RpcException(new Status(StatusCode.NotFound, "Course not found"));

        db.Courses.Remove(course);
        await db.SaveChangesAsync();
        return new CourseReply()
        {
            Title = course.Title,
            Duration = course.Duration,
            Decription = course.Description
        };
    }
}
