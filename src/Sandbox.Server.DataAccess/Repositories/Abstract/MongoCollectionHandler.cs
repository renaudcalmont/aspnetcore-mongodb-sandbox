using MongoDB.Driver;

namespace Sandbox.Server.DataAccess.Repositories.Abstract
{
    public class MongoCollectionHandler
    {
        private static IMongoClient readOnlyAccess;
        private static IMongoClient writeAccess;

        public static void OpenConnections(string readOnlyAccess, string writeAccess)
        {
            if (readOnlyAccess.StartsWith("mongodb://"))
            {
                MongoCollectionHandler.readOnlyAccess = new MongoClient(readOnlyAccess);
            }

            if (writeAccess.StartsWith("mongodb://"))
            {
                MongoCollectionHandler.writeAccess = new MongoClient(writeAccess);
            }
        }

        public IMongoCollection<TE> ReadOnly<TE, TC>()
            where TE : TC
            where TC : DomainObjects.Interfaces.Models.Abstract.IEntity
        {
            return readOnlyAccess.GetDatabase("sandbox").GetCollection<TE>(typeof(TC).Name);
        }

        public IMongoCollection<TE> Write<TE, TC>()
            where TE : TC
            where TC : DomainObjects.Interfaces.Models.Abstract.IEntity
        {
            return writeAccess.GetDatabase("sandbox").GetCollection<TE>(typeof(TC).Name);
        }

        public IMongoCollection<TE> ReadOnly<TE>()
            where TE : DomainObjects.Interfaces.Models.Abstract.IEntity
        {
            return this.ReadOnly<TE, TE>();
        }

        public IMongoCollection<TE> Write<TE>()
            where TE : DomainObjects.Interfaces.Models.Abstract.IEntity
        {
            return this.Write<TE, TE>();
        }
    }
}