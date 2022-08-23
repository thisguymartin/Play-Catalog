using Play.Catalog.Service.Entities;

namespace Play.Catalog.Service.Repository
{
  public interface IItemRepository
  {
    Task CreateAsync(Item entity);
    Task DeleteOneAsync(Guid id);
    Task<IReadOnlyCollection<Item>> GetAllASync();
    Task<Item> GetAsync(Guid id);
    Task UpdateAsync(Item entity);
  }
}