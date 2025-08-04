using System.Net.Sockets;
using System.Text;

namespace Specialist_Lab_1_4_Client;

public class Program
{
    static void Main(string[] args)
    {
        const int CLIENTS = 100;
        ThreadPool.SetMinThreads(CLIENTS, CLIENTS);
        ThreadPool.SetMaxThreads(CLIENTS, CLIENTS);

        Task[] clients = new Task[CLIENTS];

        for (int i = 0; i < CLIENTS; i++)
        {
            int clientNum = i + 1;
            clients[i] = new Task(() =>
            {
                try
                {
                    using var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    socket.Connect("127.0.0.1", 1111);
                    using var stream = new NetworkStream(socket);
                    using var reader = new StreamReader(stream, Encoding.UTF8);
                    using var writer = new StreamWriter(stream, Encoding.UTF8);
                    writer.WriteLine($"Client {clientNum} from task {Task.CurrentId} into thread {Thread.CurrentThread.ManagedThreadId}");
                    writer.Flush();
                    string? result = reader.ReadLine();
                    Console.WriteLine(result);
                }
                catch (Exception exeption)
                {
                    Console.WriteLine(exeption.Message);
                }
            });
        }
        foreach (Task client in clients) client.Start();
        Task.WaitAll(clients);
    }
}