namespace LabsLib.Threads;

public class AntiphaseThread
{
    private static double Num = 1;
    private static bool Phase = true;
    private static readonly object locker = new();
    private Operations Operation { get; }

    public AntiphaseThread(Operations operation)
    {
        if (operation != Operations.Cos && operation != Operations.ArcCos)
        {
            throw new ArgumentException($"Enum Operations does not contain the value \"{operation}\"");
        }
        this.Operation = operation;
    }

    public static void Start()
    {
        new Thread(new AntiphaseThread(Operations.Cos).DoOperation).Start();
        new Thread(new AntiphaseThread(Operations.ArcCos).DoOperation).Start();
    }
    
    private void DoOperation()
    {
        for (int i = 1; i <= 10; i++)
        {
            lock (locker)
            {
                if (this.Operation == Operations.Cos)
                {
                    while (!Phase)
                    {
                        Monitor.Wait(locker);
                    }
                    Num = Math.Cos(Num);
                    Console.Write($"{Num} ");
                }
                else if (this.Operation == Operations.ArcCos)
                {
                    while (Phase)
                    {
                        Monitor.Wait(locker);
                    }
                    Num = Math.Acos(Num);
                    Console.WriteLine($"{Num}");
                }
                Phase = !Phase;
                Monitor.Pulse(locker);
            }
        }
    }
}