using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Motoristas.Core.States;

namespace Motoristas.Core.Data.MongoDB
{
    public abstract class MongoDbContext<T> where T : IStateIdentity<string>
    {
        private readonly IMongoCollection<T> _mongoCollection;

        static MongoDbContext()
        {
            var pack = new ConventionPack
                       {
                           new CamelCaseElementNameConvention(),
                           new IgnoreIfDefaultConvention(false),
                           new IgnoreIfNullConvention(true)
                       };
            ConventionRegistry.Register("Default conventions", pack, t => true);

            BsonClassMap.RegisterClassMap<T>(cm =>
            {
                cm.AutoMap();
                cm.MapIdProperty(c => c.Id)
                  .SetSerializer(new StringSerializer(BsonType.ObjectId));
            });

            BsonSerializer.RegisterSerializer(typeof(decimal), new DecimalSerializer(BsonType.Decimal128));
            BsonSerializer.RegisterSerializer(typeof(decimal?), new NullableSerializer<decimal>(new DecimalSerializer(BsonType.Decimal128)));
        }

        protected MongoDbContext(string connectionString, string collectionName)
        {
            var url = MongoUrl.Create(connectionString);
            var client = new MongoClient(url);
            Database = client.GetDatabase(url.DatabaseName);
            _mongoCollection = Database.GetCollection<T>(collectionName);
        }

        protected IMongoDatabase Database { get; }

        public IQueryable<T> Collection => _mongoCollection.AsQueryable();

        public Task Save(T state)
        {
            var filter = Builders<T>.Filter.Eq(s => s.Id, state.Id);
            var updateOptions = new UpdateOptions {IsUpsert = true};
            var result = _mongoCollection.ReplaceOneAsync(filter, state, updateOptions);
            return result;
        }

        public T Load(string id)
        {
            var filter = Builders<T>.Filter.Eq(s => s.Id, id);
            var result = _mongoCollection.Find(filter).FirstOrDefault();
            return result;
        }

        public T Delete(string id)
        {
            var filter = Builders<T>.Filter.Eq(s => s.Id, id);
            var result = _mongoCollection.FindOneAndDelete(filter);
            return result;
        }

        public List<T> Find(string sort)
        {
            return _mongoCollection.Find(FilterDefinition<T>.Empty)
                .Sort(Builders<T>.Sort.Ascending(sort))
                .ToList();
                
        }
    }
}
