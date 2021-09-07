using System;
using System.IO;
using Newtonsoft.Json;

namespace Chat.Server
{
    public class Configuration
    {
        public int Port { get; set; }
        public string ResponseIP { get; set; }
        public int MaxConnections { get; set; }
        public int BufferLength { get; set; }
        public string Encoding { get; set; }
        public Configuration()
        {
            
        }
        public static Configuration LoadByJson(string path)
        {
            string text = File.ReadAllText(path);
            Configuration config = JsonConvert.DeserializeObject<Configuration>(text);
            return config;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}