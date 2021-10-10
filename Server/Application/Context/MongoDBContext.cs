using Chat.Server.Application.Context.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Server.Application.Context
{
    internal class MongoDBContext
    {
        private MongoClient mongoClient;
        private IMongoDatabase mongoDatabase;
        private Configuration Configuration { get; set; }
        public MongoDBContext(Configuration configuration)
        {
            Configuration = configuration;
            mongoClient = new(Configuration.MongoConnectionString);
            mongoDatabase = mongoClient.GetDatabase(Configuration.MongoDBName);
        }

        public IMongoCollection<Message> Messages { get { return mongoDatabase.GetCollection<Message>(Message.CollectionName); } }
        public IMongoCollection<ConnectedUser> ConnectedUsers { get { return mongoDatabase.GetCollection<ConnectedUser>(ConnectedUser.CollectionName); } }
    }
}
