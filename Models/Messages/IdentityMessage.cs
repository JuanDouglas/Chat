using Chat.Protocol.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Protocol.Messages
{
    public class IdentityMessage : CCMessage
    {
        public string Username { get => _username; set { _username = value; Content.StringContent = Username; } }
        private string _username;
        public IdentityMessage(string username) : base(DefaultEncoding)
        {
            Username = username;
            
        }
        public IdentityMessage(Encoding encoding, byte[] vs) : base(encoding, vs)
        {
            Username = Content.StringContent;
        }
    }
}
