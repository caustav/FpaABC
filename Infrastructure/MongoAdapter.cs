using Application.Common;
using MongoDB.Driver;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using Mapster;
using Microsoft.Extensions.Logging;
using System.Security;

namespace Infrastructure
{
    public class MongoAdapter
    {
        public IConfiguration Configuration { get; private set; } = default!;
        public MongoClient MongoClient { get; private set; } = default!;
        public IMongoDatabase MongoDatabase { get; private set; } = default!;

        private readonly ILogger<MongoAdapter> logger; 

        private SecureString ToSecureString(string plainString)
        {        
            SecureString secureString = new SecureString();
            foreach (char c in plainString.ToCharArray())
            {
                secureString.AppendChar(c);
            }
            return secureString;
        }

        public MongoAdapter(IConfiguration configuration, ILogger<MongoAdapter> logger)
        {
            MongoClient = new MongoClient("mongodb://adminuser:password123@10.99.104.16:27017");            
            MongoDatabase = MongoClient.GetDatabase("FpaABC");
            
            this.logger = logger;
        }

        public void Insert<TModel>(string collectionName, TModel doc)
        {
            if (String.IsNullOrEmpty(collectionName) || doc == null)
            {
                throw new Exception("Invalid input is supplied");
            }

            try
            {
                var collection = MongoDatabase.GetCollection<TModel>(collectionName);
                collection.InsertOne(doc);
            }
            catch (System.Exception e)
            {
                logger.LogError(e.Message);
            }
        }

        public async Task Update<TModel>(string collectionName, TModel model, string filter)
        {
            if (String.IsNullOrEmpty(collectionName) || model == null || String.IsNullOrEmpty(filter))
            {
                throw new Exception("Invalid input is supplied");
            }

            var result = await MongoDatabase.GetCollection<TModel>(collectionName).ReplaceOneAsync(filter, model);

            if (result.MatchedCount == 0)
            {
                throw new Exception("Error : Fail to update");
            }
        }

        public async Task<T> Read<T>(string collectionName, string filter)
        {
            T? tValue = default;
            try
            {
                if (String.IsNullOrEmpty(collectionName) || String.IsNullOrEmpty(filter))
                {
                    throw new Exception("Invalid input is supplied");
                }

                var collection = MongoDatabase.GetCollection<BsonDocument>(collectionName);

                var findOptions = new FindOptions<MongoDB.Bson.BsonDocument, T>();
                findOptions.Projection = "{'_id': 0}";

                if (collection != null)
                {
                    var tValues =  await collection.FindAsync<T>(filter, findOptions); 
                    tValue = await tValues.FirstAsync();
                }            
            }
            catch (System.Exception e)
            {
                 System.Console.WriteLine(e.Message);
            }
            return tValue!;
        }

        public IEnumerable<T> ReadAll<T>(string collectionName)
        {
            if (String.IsNullOrEmpty(collectionName))
            {
                throw new Exception("Invalid input is supplied");
            }

            var collection = MongoDatabase.GetCollection<T>(collectionName);
            var documents = collection.FindSync("{}").ToList<T>();
            return documents;
        }

    }
}
