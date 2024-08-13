using BookingRoom.Domain.Entities;
using System.Linq.Expressions;

namespace BookingRoom.Domain.Interfaces
{
    public interface IBaseRepository<TEntity> : IDisposable where TEntity : BaseDomain
    {
        Task Adcionar(TEntity entity);
        Task Adcionar(IList<TEntity> entities);
        Task<TEntity> GetById(Guid id);
        Task<List<TEntity>> GetAll();
        Task Update(TEntity entity);
        Task Remover(Guid id);
        Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate);
        Task<int> SaveChanges();
    }
}
