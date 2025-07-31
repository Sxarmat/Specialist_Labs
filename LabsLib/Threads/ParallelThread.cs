namespace LabsLib.Threads;

public class ParallelThread
{
    private readonly Thread threadInstance;
    private int StepStart { get; }
    private int StepEnd { get; }

    private ParallelThread(int stepStart, int stepEnd, string? threadName)
    {
        this.StepStart = stepStart;
        this.StepEnd = stepEnd;
        threadInstance = new Thread(this.Run);
        threadInstance.Name = $"Thread {threadName ?? threadInstance.ManagedThreadId.ToString()}";
    }

    public static void Start(int start, int end, string? name = null)
    {
        new ParallelThread(start, end, name).threadInstance.Start();
    }

    private void Run()
    {
        for (int stepNow = this.StepStart; stepNow <= this.StepEnd; stepNow++)
        {
            Console.WriteLine($"{this.threadInstance.Name}: Step {stepNow}");
            Thread.Sleep(100);
        }
    }
}


