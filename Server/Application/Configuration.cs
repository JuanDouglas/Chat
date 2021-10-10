using System;
using System.IO;
using Newtonsoft.Json;
using System.Text;

namespace Chat.Server.Application
{
    public class Configuration
    {
        public int Port { get; set; }
        public string ResponseIP { get; set; }
        public int MaxConnections { get; set; }
        public int BufferLength { get; set; }
        public string Encoding { get; set; }
        public string MongoConnectionString { get; set; }
        public string MongoDBName { get; set; }
        public Configuration()
        {

        }
        public static Configuration LoadByJson(string text)
        {
            Configuration config = JsonConvert.DeserializeObject<Configuration>(text);
            return config;
        }

        public static Configuration LoadByByteArray(byte[] array)
        {
            return LoadByJson(System.Text.Encoding.UTF8.GetString(array));
        }

        public static Configuration LoadByPath(string path)
        {
            return LoadByByteArray(File.ReadAllBytes(path));
        }

        public override string ToString()
        {
            return $"Encoding: {Encoding}\n" +
                $"Buffer Length: {BufferLength}\n" +
                $"Max Connections: {MaxConnections}\n" +
                $"Response IP: {ResponseIP}\n" + 
                $"Port: {Port}\n" +
                $"Mongo Connection: {MongoConnectionString} | {MongoDBName}";
        }
    }
}