using System.Diagnostics;


namespace Specialist_Lab_1_6;

internal class Program
{
    private static void Main(string[] args)
    {
        const int STEPS = 100_000_000;

        double Single(Func<double, double> f, double a, double b, int steps = STEPS)
        {
            double w = (b - a) / steps;
            double summa = 0d;

            for (int i = 0; i < steps; i++)
            {
                double x = a + i * w + w / 2;
                double h = f(x);
                summa += h * w;
            }

            return summa;
        }

        double SingleParallel(Func<double, double> f, double a, double b, int steps = STEPS)
        {
            double w = (b - a) / 10;
            object locker = new();
            double summa2 = 0d;

            Parallel.For(0, 10, (loop) =>
            {
                double start = a + loop * w;
                double finish = a + (loop + 1) * w;
                double result = Single(f, start, finish, steps / 10);
                lock (locker)
                {
                    summa2 += result;
                }
            });
            return summa2;
        }

        Stopwatch t1 = new();
        t1.Start();
        double r1 = Single(Math.Sin, 0, Math.PI / 2);
        t1.Stop();
        Console.WriteLine($"Single result : {r1} Time Sync: {t1.ElapsedMilliseconds}");

        t1.Reset();

        Stopwatch t = new();
        t1.Start();
        r1 = SingleParallel(Math.Sin, 0, Math.PI / 2);
        t1.Stop();
        Console.WriteLine($"Single result : {r1} Time Parallel: {t1.ElapsedMilliseconds}");
    }
}