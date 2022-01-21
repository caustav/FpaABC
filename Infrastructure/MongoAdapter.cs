using Application.Common;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public class MongoAdapter
    {
        public IConfiguration Configuration { get; private set; } = default!;
        public MongoClient MongoClient { get; private set; } = default!;
        public IMongoDatabase MongoDatabase { get; private set; } = default!;

        public MongoAdapter(IConfiguration configuration)
        {
            MongoClient = new MongoClient("mongodb://127.0.0.1:27017");
            MongoDatabase = MongoClient.GetDatabase("FpaABC");
        }

        // private void LoadDatabase()
        // {
        //     if (MongoDatabase == null)
        //     {
        //         MongoClient = new MongoClient(Configuration.DatabaseConnectionString);
        //         MongoDatabase = MongoClient.GetDatabase(Configuration.DatabaseName);
        //     }
        // }

        public void Insert<TModel>(string collectionName, TModel doc)
        {
            if (String.IsNullOrEmpty(collectionName) || doc == null)
            {
                throw new Exception("Invalid input is supplied");
            }

            var collection = MongoDatabase.GetCollection<TModel>(collectionName);
            collection.InsertOne(doc);
        }

        public void Update<TModel>(string collectionName, TModel model, string filter)
        {
            if (String.IsNullOrEmpty(collectionName) || model == null || String.IsNullOrEmpty(filter))
            {
                throw new Exception("Invalid input is supplied");
            }

            var result = MongoDatabase.GetCollection<TModel>(collectionName).ReplaceOne(filter, model);

            if (result.MatchedCount == 0)
            {
                throw new Exception("Error : Fail to update");
            }
        }

        public T Read<T>(string collectionName, string filter)
        {
            if (String.IsNullOrEmpty(collectionName) || String.IsNullOrEmpty(filter))
            {
                throw new Exception("Invalid input is supplied");
            }

            var collection = MongoDatabase.GetCollection<T>(collectionName);

            T tValue = collection.FindSync(filter).FirstOrDefault();
            return tValue;
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
