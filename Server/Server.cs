using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Chat.Protocol;
using Chat.Protocol.Base;
using System.IO;
using Chat.Protocol.Base.Exceptions;

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
        public async Task Start()
        {
            // Obtém o endereço IP do HOST pela DNS  
            IPHostEntry entry = Dns.GetHostEntry(Configuration.ResponseIP);
            IPAddress address = entry.AddressList[^1];

            // Cria um EndPoint que irá responder pelo IP obtido
            // anteriormente na porta definida em Configuration.json
            IPEndPoint endPoint = new(address, Configuration.Port);
            Console.WriteLine($"Starting server in {endPoint} host.");
            try
            {
                //Inicia o listener no EndPoint especificado
                TcpListener listener = new(endPoint);

                //Define o máximo de conexões que este servirdor pode ter.
                listener.Start(Configuration.MaxConnections);

                //Mostra mensagem avisando que espera conexão
                Console.WriteLine("Awaiting client connection...");

                //Loop para esperar conexão
                while (true)
                {
                    //Aceita a conexão 
                    TcpClient client = listener.AcceptTcpClient();

                    //Cria um novo thread para continuar a comunicação 
                    Thread th = new(() =>
                    {
                        Connect(client);
                    });

                    //Inicia o thread criado
                    th.Start();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void Connect(TcpClient handler)
        {
            int i;
            byte[] buffer = new byte[Configuration.BufferLength];
            string data;
            Encoding encoding = Encoding.GetEncoding(Configuration.Encoding);

            // Obtém o stream do cliente.
            NetworkStream stream = handler.GetStream();

            try
            {
                // Loop para ler todo o conteudo da mensagem.
                while ((i = stream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    // Traduz os dados em bytes para o enconding especificados na configuração 
                    data = encoding.GetString(buffer, 0, i);

                    //Reduz o tamanho do buffer
                    buffer = buffer.RelockBuffer(0, i);

                    // Mostra mensagem avisando que a conexão foi aberta
                    Console.WriteLine($"Open connection to {handler.Client.RemoteEndPoint}");

                    CCMessage connect = new(encoding, buffer);

                }
            }
            catch (SocketException e)
            {
                Console.WriteLine(e.ToString());
                handler.Close();
            }
            catch (HttpRequestException e)
            {
                // Codifica a mensagem http a ser enviado para o cliente 
                byte[] msg = encoding.GetBytes($"HTTP/1.1 400 Bad Request\r\nDate: {DateTime.UtcNow}\r\nServer: CCM Server\r\nContent-Length: 110\r\nConnection: Closed\r\nContent-Type: text/html; charset=iso-8859-1\r\n\r\nThis server uses the CCM (Chat Comunitcation message) protocol to communicate and I need to use this protocol!");

                // Manda a mensagem para o cliente
                stream.Write(msg, 0, msg.Length);

                // Mostra mensagem avisando que a conexão foi fechada
                Console.WriteLine($"Close connection to {handler.Client.RemoteEndPoint} with protocol error.");

                // Fecha efetivamente a conexão. 
                handler.Close();
            }
        }
    }


    public static class Extension
    {
        public static byte[] RelockBuffer(this byte[] bytes, int offset, int newLength)
        {
            byte[] buffer = bytes;
            bytes = new byte[newLength];
            for (int i = 0; i < newLength; i++)
            {
                bytes[i] = buffer[i + offset + 1];
            }
            return bytes;
        }

        public static byte[] RelockBuffer(this byte[] bytes, byte[] addBytes, int offset, int size)
        {
            byte[] buffer = bytes;

            bytes = new byte[buffer.Length + size + 1];
            for (int i = 0; i < buffer.Length; i++)
            {
                bytes[i] = buffer[i];
            }

            for (int i = 0; i < size; i++)
            {
                bytes[i + buffer.Length] = addBytes[offset + i];
            }

            return bytes;
        }
    }
}
