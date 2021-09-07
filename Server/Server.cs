using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

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
            IPAddress address = entry.AddressList[0];

            // Cria um EndPoint que ira responder pelo IP obtido
            // anteriormente na porta definida em Configuration.json
            IPEndPoint endPoint = new(address, Configuration.Port);

            try
            {
                //Inicia o socket usando o protocolo TCP
                Socket socket = new(address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                //Defini o endPoit qe o socket ira responder.
                socket.Bind(endPoint);

                //Defini quantas solicitações um socket pode ouvir antes de dar uma resposta ocupada.
                socket.Listen(Configuration.MaxConnections);

                while (true)
                {
                    await Connect(await socket.AcceptAsync());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
        }

        private async Task Connect(Socket handler)
        {
            // Incoming data from the client.    
            string data = string.Empty;
            byte[] bytes = Array.Empty<byte>();

            while (true)
            {
                bytes = new byte[Configuration.BufferLength];
                int bytesRec = handler.Receive(bytes);
                data += Encoding.GetString(bytes, 0, bytesRec);
                if (data.IndexOf("<EOF>") > -1)
                {
                    break;
                }
            }
        }
    }
}
