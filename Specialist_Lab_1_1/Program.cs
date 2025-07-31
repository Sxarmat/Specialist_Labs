namespace Specialist_Lab_1_1;

using LabsLib.Threads;

public class Program
{
    public static void Main(string[] args)
    {
        ParallelThread.Start(1, 9, "MyThread");
        ParallelThread.Start(55, 63);
    }
}