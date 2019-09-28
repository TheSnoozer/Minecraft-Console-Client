using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using MinecraftClient.Protocol.Handlers;

namespace MinecraftClientProxy
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter Minecraft Server-IP (defaults to localhost):");
            var readLine = Console.ReadLine();
            string serverIp = String.IsNullOrEmpty(readLine) ? "localhost" : readLine;

            Console.WriteLine("Enter Minecraft Server-Port (defaults to 25565):");
            int serverPort = int.TryParse(Console.ReadLine(), out serverPort) ? serverPort : 25565;

            Console.WriteLine("Waiting for client on port 25565...");
            TcpListener listener = new TcpListener(IPAddress.Any, 25565);
            listener.Start();
            TcpClient client = listener.AcceptTcpClient();
            
            Console.WriteLine(String.Format("Connecting to server '{0}' on port {1}...", serverIp, serverPort));
            TcpClient server = new TcpClient(serverIp, serverPort);

            Console.WriteLine("Starting proxy...\n");
            new PacketProxy(client, server).Run();

            Console.ReadLine();
        }
    }
}
