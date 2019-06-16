using MongoDB.Bson;

namespace Motoristas.Core.Data.MongoDB
{
    public class MongoDbIdentityGenerator : IIdentityGenerator<string>
    {
        public string Create()
        {
            return ObjectId.GenerateNewId().ToString();
        }
    }
}
