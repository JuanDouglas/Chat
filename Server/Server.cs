using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Chat.Server
{
    public class Server
    {
        public Configuration Configuration { get; set; }
        public Encoding Encoding { get; set; }

        public Server(Configuration configuration)
        {
            Configuration = configuration;
            Encoding = Encoding.GetEncoding(configuration.Encoding);
        }

        /// <summary>
        /// Inicia o servidor usando as configurações definidas anteriormente.
        /// </summary>
        public async Task StartAsync()
        {
            // Obtem o endereço IP do HOST pela DNS  
            IPHostEntry entry = Dns.GetHostEntry(Configuration.ResponseIP);
            IPAddress address = entry.AddressList[entry.AddressList.Length - 1];

            // Cria um EndPoint que ira responder pelo IP obtido
            // anteriormente na porta definida em Configuration.json
            IPEndPoint endPoint = new(address, Configuration.Port);
            Console.WriteLine($"Starting server in {endPoint} host.");
            try
            {
                TcpListener listener = new(endPoint);
                listener.Start(Configuration.MaxConnections);

                Console.WriteLine("Awaiting client connection...");
                while (true)
                {
                    TcpClient client = listener.AcceptTcpClient();
                    Task task = Connect(client);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private async Task Connect(TcpClient handler)
        {
            try
            {
                // Obtém o stream do cliente.
                NetworkStream stream = handler.GetStream();

                int i;
                byte[] bytes = new byte[Configuration.BufferLength];
                string data;
                Encoding encoding = Encoding.GetEncoding(Configuration.Encoding);

                // Loop para ler todo o conteudo da mensagem.
                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    // Traduz os dados em bytes para o enconding especificados na configuração 
                    data = encoding.GetString(bytes, 0, i);
                    Console.WriteLine("Received: {0}", data);

                    // Process the data sent by the client.
                    byte[] msg = encoding.GetBytes("HTTP/1.1 404 Not Found\r\nDate: Sun, 18 Oct 2012 10:36:20 GMT\r\nServer: Apache/2.2.14 (Win32)\r\nContent-Length: 230\r\nConnection: Closed\r\nContent-Type: text/html; charset=iso-8859-1\r\n\r\nResposta HTTP");

                    // Send back a response.
                    stream.Write(msg, 0, msg.Length);

                    Task.Run(() => {
                        for (int i = 0; i < int.MaxValue; i++)
                        {
                            Thread.Sleep(2000);

                        msg = encoding.GetBytes($"Resposta HTTP {i}");
                        }
                    });
                    // Send back a response.
                    stream.Write(msg, 0, msg.Length);
                    Console.WriteLine("Sent: {0}", data);
                }

            }
            catch (SocketException e)
            {
                Console.WriteLine();
                handler.Close();
            }
        }
    }
}
