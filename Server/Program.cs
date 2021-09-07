using System;

namespace Chat.Server
{
    public class Program
    {
        static Configuration configuration;
        static Server server;
        public static void Main(string[] args)
        {
            configuration = Configuration.LoadByJson(Environment.CurrentDirectory + "\\Configuration.json");
            server = new(configuration);
            Console.WriteLine("Hello World!");
        }
    }
}
