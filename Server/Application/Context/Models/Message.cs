﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Server.Application.Context.Models
{
    public class Message
    {
        internal const string CollectionName = "Messages";
        public string TargetUser { get; set; }

    }
}
