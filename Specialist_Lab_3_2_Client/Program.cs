using Grpc.Net.Client;
using Specialist_Lab_3_2_Client;
using static Specialist_Lab_3_2_Client.CourseService;

internal class Program
{
    private static async Task Main(string[] args)
    {
        using GrpcChannel grpcChannel = GrpcChannel.ForAddress("http://localhost:5153");
        CourseServiceClient client = new CourseServiceClient(grpcChannel);
        ListCourseReply reply = await client.IndexCourseAsync(new Google.Protobuf.WellKnownTypes.Empty());

        foreach (CourseReply course in reply.Courses)
        {
            Console.WriteLine($"Курс {course.Id} - {course.Title} - {course.Duration}ч. - {course.Decription}");
        }

    }
}