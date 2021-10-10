using Chat.Protocol.Base;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Chat.Cli
{
    public static class Program
    {
        static string Host ="127.0.0.1";
        const int Port = 5454;
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            IPEndPoint endPoint = new(IPAddress.Parse(Host), Port);
            TcpClient client = new();
            client.Connect(endPoint);


            NetworkStream serverStream = client.GetStream();

            CCMessage message = new(Encoding.UTF8)
            {
                Content = new CCMContent(Encoding.UTF8, Encoding.UTF8.GetBytes("Hello Server, get started?"))
            };

            byte[] outStream = message.ContentBytes();
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();

            byte[] buffer = new byte[4096];

            serverStream.Read(buffer,0,buffer.Length);

            
        }
    }
}
