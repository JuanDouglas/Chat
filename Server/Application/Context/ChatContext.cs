using Chat.Server.Application.Context.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Server.Application.Context
{
    internal class ChatContext
    {
        protected internal MongoDBContext MongoContext { get; set; }
        protected internal LocalDBContext LocalContext { get; set; }
        public ChatContext(Configuration configuration)
        {
            MongoContext = new(configuration);
        }

        public async Task<Message[]> GetMessagesAsync(string username)
        {
            var messages = await MongoContext.Messages.FindAsync(fs => fs.TargetUser == username);
            var listAsync = await messages.ToListAsync();
            return listAsync.ToArray();
        }

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
