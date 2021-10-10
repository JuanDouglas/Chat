using Chat.Server.Dal.MongoDB;
using Chat.Server.Dal.MongoDB.Models;
using Chat.Server.Dal.LocalJson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.Server.Dal.SqlServer;

namespace Chat.Server.Application
{
    /// <summary>
    /// Chat server context
    /// </summary>
    internal class ChatServerContext
    {
        protected internal MongoDBContext MongoContext { get; set; }
        protected internal LocalJsonContext LocalContext { get; set; }
        protected internal SqlServerContext SqlServerContext { get; set; }
        /// <summary>
        /// Start server context with configuration
        /// </summary>
        /// <param name="config">Server configuration</param>
        public ChatServerContext(Configuration config)
        {
            MongoContext = new(config.MongoConnectionString,config.MongoDBName);
            LocalContext = new();
            SqlServerContext = new(config.SqlConnectionString);
        }

        //public async Task<Message[]> GetMessagesAsync(string username)
        //{
        //    var messages = await MongoContext.Messages.FindAsync(fs => fs.TargetUser == username);
        //    var listAsync = await messages.ToListAsync();
        //    return listAsync.ToArray();
        //}

        public async Task NotifyUserConnection(string username, string userIP)
        {
            IMongoCollection<ConnectedUser> connecteds = MongoContext.ConnectedUsers;
            List<ConnectedUser> conByUsername = await (await connecteds.FindAsync(x => x.Username == username)).ToListAsync();
            if (conByUsername.Count < 1)
            {
                await connecteds.InsertOneAsync(new()
                {
                    Username = username,
                    LastUpdateTime = DateTime.UtcNow
                });
            }
            else
            {

            }



        }
    }
}
