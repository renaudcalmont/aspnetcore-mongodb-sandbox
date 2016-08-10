using MongoDB.Driver;

namespace Sandbox.Server.DataAccess.Repositories.Abstract
{
    public class MongoCollectionHandler
    {
        private static IMongoClient readOnlyAccess;
        private static IMongoClient writeAccess;
        
        private static readonly object readOnlyLock = new object();
        private static readonly object writeLock = new object();

        public IMongoCollection<TE> ReadOnly<TE, TC>()
            where TE : TC
            where TC : DomainObjects.Interfaces.Models.Abstract.IEntity
        {
            lock (readOnlyLock)
            {
                if (readOnlyAccess == null)
                {
                    readOnlyAccess = new MongoClient("mongodb://127.0.0.1:32768?slaveOk=true");
                }
            }

            return readOnlyAccess.GetDatabase("sandbox").GetCollection<TE>(typeof(TC).Name);
        }

        public IMongoCollection<TE> Write<TE, TC>()
            where TE : TC
            where TC : DomainObjects.Interfaces.Models.Abstract.IEntity
        {
            lock (writeLock)
            {
                if (writeAccess == null)
                {
                    writeAccess = new MongoClient("mongodb://127.0.0.1:32768");
                }
            }

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