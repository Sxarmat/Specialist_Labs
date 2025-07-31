namespace LabsLib.Threads;

public class SyncThread
{
    private readonly Thread threadInstance;
    private SyncThread? previousThread;

    public SyncThread(string threadName = "")
    {
        threadInstance = new Thread(this.Run);
        threadInstance.Name = $"Thread {threadName ?? threadInstance.ManagedThreadId.ToString()}";
    }

    public void Start(SyncThread? previous = null)
    {
        this.previousThread = previous;
        this.threadInstance.Start();
    }
    
    public void Join()
    {
        threadInstance.Join();
    }

    public bool Join(int timeout)
    {
        return threadInstance.Join(timeout);
    }

    private void Run()
    {
        if (this.previousThread is not null)
        {
            while (this.previousThread.threadInstance.ThreadState == ThreadState.Unstarted) Thread.Yield();
            this.previousThread.Join(10000);
        }
        for (int stepNow = 1; stepNow <= 100; stepNow++)
        {
            Console.WriteLine($"{this.threadInstance.Name}: Step {stepNow}");
        }
    }
}