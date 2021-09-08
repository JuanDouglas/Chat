using Chat.Protocol.Base.Enums;
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
        }

        public CCMessage(Encoding encoding, string header, byte[] content)
            : this(encoding)
        {

        }

        public CCMessage(Encoding encoding, byte[] bytes) :
            this(encoding, encoding.GetString(bytes, 0, bytes.Length).Split(Separetor)[0] + Separetor, encoding.GetBytes(string.Empty))
        {
            string header = encoding.GetString(bytes, 0, bytes.Length).Split(Separetor)[0] + Separetor;
            int bytesCount = encoding.GetByteCount(header);

            byte[] content = new byte[bytes.Length - bytesCount];

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

                Attributes.Add(new(CCMContent.EncodingAttributeKey, Encoding.EncodingName));
            }
        }
        private CCMContent _content;
        public Encoding Encoding { get; set; }

        public const string Separetor = "\n\n";
        private string HeaderString() => $"CCM\\{Version} {Type}" +
            $"{Attributes}{Separetor}";
        public byte[] ContentBytes()
        {
            string header = HeaderString();
            List<byte> bytes = new(Encoding.GetBytes(header));
            bytes.AddRange(Content.ByteArray);

            return bytes.ToArray();
        }
    }
}
