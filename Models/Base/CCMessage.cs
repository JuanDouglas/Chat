using Chat.Protocol.Base.Enums;
using Chat.Protocol.Base.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Protocol.Base
{
    public class CCMessage
    {
        public CCMessage(Encoding encoding)
        {
            Version = "1.0";
            Type = MessageType.NotDefined;
            Attributes = new();
            Encoding = encoding;
            Content = new(encoding, encoding.GetBytes("\0"));
        }

        private CCMessage(Encoding encoding, string header, byte[] content)
            : this(encoding)
        {
            string http = header.Substring(0, 4);
            if (http.ToLowerInvariant() == "http".ToLowerInvariant())
            {
                throw new HttpRequestException();
            }

            if (!http.ToLowerInvariant().Contains("CM".ToLowerInvariant()))
            {
                throw new HttpRequestException();
            }
        }

        public CCMessage(Encoding encoding, byte[] bytes) :
            this(encoding, encoding.GetString(bytes, 0, bytes.Length).Split(Separetor)[0] + Separetor, encoding.GetBytes(string.Empty))
        {
            string header = encoding.GetString(bytes, 0, bytes.Length).Split(Separetor)[0] + Separetor;
            header = header.Replace("\0", string.Empty);
            int bytesCount = encoding.GetByteCount(header);
            int length = bytes.Length - bytesCount;

            if (length < 1)
                return;

            byte[] content = new byte[length];

            for (int i = 0; i < content.Length; i++)
            {
                content[i] = bytes[i + bytesCount];
            }
            string contentEncondingName = Attributes.FirstOrDefault(fs => fs.Key == CCMContent.EncodingAttributeKey).Content;

            Content = new(Encoding.GetEncoding(contentEncondingName), content);
        }

        public string Version { get; private set; }
        public MessageType Type { get; protected internal set; }
        public CCMAttributes Attributes { get; set; }
        public CCMContent Content
        {
            get => _content; set
            {
                _content = value;
                int index = Attributes.IndexOf(CCMContent.EncodingAttributeKey);

                if (index != 0)
                    Attributes.RemoveAt(index);

                Attributes.Add(new(CCMContent.EncodingAttributeKey, Encoding.HeaderName));
            }
        }
        private CCMContent _content;
        public Encoding Encoding { get; set; }
        protected internal static Encoding DefaultEncoding = Encoding.UTF8;

        protected internal const string Separetor = "\n\n";
        private string HeaderString() => $"CCM\\{Version} {Type}" +
            $"{Attributes}{Separetor}";
        public virtual byte[] ContentBytes()
        {
            string header = HeaderString();
            List<byte> bytes = new(Encoding.GetBytes(header));
            bytes.AddRange(Content.ByteArray);

            return bytes.ToArray();
        }
    }
}
