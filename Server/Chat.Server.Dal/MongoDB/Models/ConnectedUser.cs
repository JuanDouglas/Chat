using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Server.Dal.MongoDB.Models
{
    public class ConnectedUser
    {
        internal const string CollectionName = "ConnectedsUsers";
        [Required]
        public DateTime LastUpdateTime { get; set; }
        [Required]
        public string Username { get; set; }
    }
}
