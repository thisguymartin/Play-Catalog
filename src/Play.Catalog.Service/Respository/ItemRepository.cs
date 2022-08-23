using MongoDB.Driver;
using Play.Catalog.Service.Entities;

namespace Play.Catalog.Service.Repository
{

  public class ItemRepository : IItemRepository
  {
    private const string collectionName = "items";
    private readonly IMongoCollection<Item> dbCollection;
    private readonly FilterDefinitionBuilder<Item> filterBuilder = Builders<Item>.Filter;

    public ItemRepository()
    {
      var mongoClient = new MongoClient("mongodb://localhost:27017");
      var database = mongoClient.GetDatabase("Catalog");
      dbCollection = database.GetCollection<Item>(collectionName);

    }

    public async Task<IReadOnlyCollection<Item>> GetAllASync()
    {
      return await dbCollection.Find(filterBuilder.Empty).ToListAsync();
    }

    public async Task<Item> GetAsync(Guid id)
    {
      FilterDefinition<Item> filter = filterBuilder.Eq(entity => entity.Id, id);
      return await dbCollection.Find(filter).FirstAsync();
    }

    public async Task CreateAsync(Item entity)
    {

      if (entity == null)
      {
        throw new ArgumentNullException(nameof(entity));
      }

      await dbCollection.InsertOneAsync(entity);

    }

    public async Task UpdateAsync(Item entity)
    {

      FilterDefinition<Item> filter = filterBuilder.Eq(dbEntity => dbEntity.Id, entity.Id);

      await dbCollection.ReplaceOneAsync(filter, entity);
    }

    public async Task DeleteOneAsync(Guid id)
    {

      FilterDefinition<Item> filter = filterBuilder.Eq(dbEntity => dbEntity.Id, id);
      await dbCollection.DeleteOneAsync(filter);
    }
  }
}