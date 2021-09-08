using System;
using System.Threading.Tasks;

namespace Chat.Server
{
    public class Program
    {
        static Configuration configuration;
        static Server server;
        public static void Main(string[] args)
        {
            Console.WriteLine("Start configuration!\n");
            configuration = Configuration.LoadByPath(Environment.CurrentDirectory + "\\Configuration.json");
            Console.WriteLine(configuration.ToString());

            server = new(configuration);
            Task task = server.StartAsync();

            if (task.Status == TaskStatus.Created)
                task.Start();


            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}
