using Chat.Protocol.Base;
using Chat.Protocol.Base.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Protocol.Messages
{
    public class ChatMessage : CCMessage
    {
        private const string attrMessageType = "Message-Type";
        public ChatMessageType MessageType
        {
            get
            {
                return (ChatMessageType)Enum.Parse(typeof(ChatMessageType), Attributes.GetByKey(attrMessageType).Content);
            }
            set { 
                Attributes.Add(new(attrMessageType, value.ToString()), true); 
            }
        }
        public ChatMessage(ChatMessageType messageType) : base(DefaultEncoding)
        {
            MessageType = messageType;
        }
    }
}
