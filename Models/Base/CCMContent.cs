using System.Text;

namespace Chat.Protocol.Base
{
    public class CCMContent
    {
        public CCMContent()
        {
            Encoding = Encoding.Default;
        }

        public CCMContent(Encoding encoding, byte[] byteArray)
        {
            Encoding = encoding;
            ByteArray = byteArray;
        }

        public Encoding Encoding { get; set; }

        public byte[] ByteArray { get; set; }

        public string StringContent { get { return Encoding.GetString(ByteArray); } set { ByteArray = Encoding.GetBytes(value)} }

        internal static string EncodingAttributeKey => "Content-Encoding";

        public override string ToString()
        {
            return Encoding.GetString(ByteArray);
        }
    }
}
