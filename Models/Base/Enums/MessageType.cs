using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Protocol.Base.Enums
{
    public enum MessageType:uint
    {
        NotDefined,
        Connect,
        ConnectVoice,
        Identity
    }
}
