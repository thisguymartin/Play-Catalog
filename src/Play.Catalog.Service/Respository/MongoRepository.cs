using MongoDB.Driver;
using Play.Catalog.Service.Entities;

namespace Play.Catalog.Service.Repository
{

    public class MongoRepository<T> : IRepository<T> where T : IEntity
    {
        private readonly IMongoCollection<T> dbCollection;
        private readonly FilterDefinitionBuilder<T> filterBuilder = Builders<T>.Filter;

        public MongoRepository(IMongoDatabase database, string collectionName)
        {

            dbCollection = database.GetCollection<T>(collectionName);

        }

        public async Task<IReadOnlyCollection<T>> GetAllASync()
        {
            return await dbCollection.Find(filterBuilder.Empty).ToListAsync();
        }

        public async Task<T> GetAsync(Guid id)
        {
            FilterDefinition<T> filter = filterBuilder.Eq(entity => entity.Id, id);
            return await dbCollection.Find(filter).FirstAsync();
        }

        public async Task CreateAsync(T entity)
        {

            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await dbCollection.InsertOneAsync(entity);

        }

        public async Task UpdateAsync(T entity)
        {

            FilterDefinition<T> filter = filterBuilder.Eq(dbEntity => dbEntity.Id, entity.Id);

            await dbCollection.ReplaceOneAsync(filter, entity);
        }

        public async Task DeleteOneAsync(Guid id)
        {

            FilterDefinition<T> filter = filterBuilder.Eq(dbEntity => dbEntity.Id, id);
            await dbCollection.DeleteOneAsync(filter);
        }
    }
}