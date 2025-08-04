using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Specialist_Lab_1_4_Server;

public class Program
{
    static void Main(string[] args)
    {
        const int MAX_CONNECTION_IN_QUEUE = 10;

        IPEndPoint iPEndPoint = new(IPAddress.Any, 1111);
        using Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        socket.Bind(iPEndPoint);
        socket.Listen(MAX_CONNECTION_IN_QUEUE);

        int request = 0;
        object locker = new();

        while (true)
        {
            Socket client = socket.Accept();
            Task.Run(() =>
            {
                using var stream = new NetworkStream(client);
                using var reader = new StreamReader(stream, Encoding.UTF8);
                using var writer = new StreamWriter(stream, Encoding.UTF8);
                string? result = reader.ReadLine();
                int clientNum;
                lock (locker)
                {
                    clientNum = ++request;
                }
                Console.WriteLine($"Remote client: {client.RemoteEndPoint} - Received: {result}, Requests: {clientNum}");
                Thread.Sleep(100);
                writer.WriteLine($"CLIENT {clientNum}: {client.RemoteEndPoint}. ANSWER FROM SERVERTASK {Task.CurrentId} INTO SERVERTHREAD {Thread.CurrentThread.ManagedThreadId}");
                writer.Flush();
                client.Dispose();
            });
        }
    }
}