using Chat.Protocol.Base;
using System;
using System.Text;

namespace Chat.Protocol
{
    public class ConnectMessage : CCMessage
    {
        public ConnectMessage(): base(Encoding.ASCII){ }
    }

    public class Authentication
    {

    }
}
