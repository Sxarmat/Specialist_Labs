namespace Specialist_Lab_1_2;

using LabsLib.Threads;

public class Program
{
    public static void Main(string[] args)
    {
        SyncThread thread1 = new("1");
        SyncThread thread2 = new("2");
        thread2.Start(thread1);
        Thread.Sleep(1000);
        thread1.Start();
    }
}