using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Sandbox.Server.DomainObjects.Interfaces.Models.Abstract;
using Sandbox.Server.DomainObjects.Interfaces.Repositories.Abstract;

namespace Sandbox.Server.DataAccess.Repositories.Abstract
{
    public abstract class EntityRepository<TE> : IEntityRepository<TE> where TE: IEntity
    {
        private static readonly Random RandomGenerator;

        private MongoCollectionHandler collectionHandler = new MongoCollectionHandler();

        static EntityRepository()
        {
          RandomGenerator = new Random();
        }

        public virtual async Task<TE> Create(TE instance)
        {
            // Increment revision
            instance.Revision = GenerateRevision();

            await collectionHandler.Write<TE>().InsertOneAsync(instance);
            return instance;
        }

        public virtual async Task<TE> Retrieve(Guid id)
        {
            var filter = Builders<TE>.Filter.Eq("_id", id);
            
            var list = await collectionHandler.ReadOnly<TE>().FindAsync(filter);
            return list.SingleOrDefault();
        }

        public virtual async Task<TE> Update(TE instance)
        {
            var filter = Builders<TE>.Filter.And(
                Builders<TE>.Filter.Eq("_id", instance.Id),
                Builders<TE>.Filter.Eq("Revision", instance.Revision));

            // Increment revision
            instance.Revision = GenerateRevision();

            // Concurrency check
            var previousInstance = await collectionHandler.Write<TE>().FindOneAndReplaceAsync(filter, instance);
            if (previousInstance == null)
            {
                throw new Exception("The entity was modified by another process");
            }
            
            return instance;
        }

        public virtual async void Delete(TE instance)
        {
            var filter = Builders<TE>.Filter.Eq("_id", instance.Id);

            await collectionHandler.Write<TE>().DeleteOneAsync(filter);
        }

        public virtual async Task<IEnumerable<TE>> RetrieveAll()
        {
            var filter = Builders<TE>.Filter.Empty;

            var list = await collectionHandler.ReadOnly<TE>().FindSync(filter).ToListAsync();
            return list;
        }

        protected Guid GenerateRevision()
        {
            var date = BitConverter.GetBytes(DateTime.Now.ToBinary());
            Array.Reverse(date);

            var random = new byte[8];
            RandomGenerator.NextBytes(random);

            var guid = new byte[16];
            Buffer.BlockCopy(date, 0, guid, 0, 8);
            Buffer.BlockCopy(random, 0, guid, 8, 8);

            return new Guid(guid);
        }
    }
}