﻿using Chat.Server.Dal.MongoDB.Models;
using MongoDB.Driver;

namespace Chat.Server.Dal.MongoDB
{
    public class MongoDBContext
    {
        private MongoClient mongoClient;
        private IMongoDatabase mongoDatabase;
        public MongoDBContext(string connectionString, string dbName)
        {
            mongoClient = new(connectionString);
            mongoDatabase = mongoClient.GetDatabase(dbName);
        }

        public IMongoCollection<ConnectedUser> ConnectedUsers { get { return mongoDatabase.GetCollection<ConnectedUser>(ConnectedUser.CollectionName); } }
    }
}
