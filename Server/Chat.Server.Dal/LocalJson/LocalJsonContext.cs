using Chat.Server.Dal.LocalJson.Interfaces;
using Chat.Server.Dal.MongoDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Server.Dal.LocalJson
{
    public class LocalJsonContext
    {
        public LocalJsonContext(Encoding encoding)
        {
            ConnectedsUsers = new AsyncJson<ConnectedUser>(GetFileName<ConnectedUser>(), encoding);
        }
        private string BasePath => $"{Environment.CurrentDirectory}\\Data\\";

        public IAsyncJsonFile<ConnectedUser> ConnectedsUsers { get; set; }

        public string GetFileName<T>()
        {
            return $"{BasePath}{typeof(T).Name}.json";
        }
    }
}
