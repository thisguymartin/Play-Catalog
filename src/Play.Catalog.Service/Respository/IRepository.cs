using Play.Catalog.Service.Entities;

namespace Play.Catalog.Service.Repository
{
    public interface IRepository<T> where T : IEntity
    {
        Task CreateAsync(T entity);
        Task DeleteOneAsync(Guid id);
        Task<IReadOnlyCollection<T>> GetAllASync();
        Task<T> GetAsync(Guid id);
        Task UpdateAsync(T entity);
    }
}