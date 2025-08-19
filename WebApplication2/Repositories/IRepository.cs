using System.Linq.Expressions;

namespace MaintenanceServiceMVC.Repositories
{
    public interface IRepository<T> where T : class
    {
        // Get all entities
        Task<IEnumerable<T>> GetAllAsync();

        // Get by Id
        Task<T> GetByIdAsync(object id);

        // Find by condition (e.g. with predicates)
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

        // Add new entity
        Task AddAsync(T entity);

        // Update existing entity
        void Update(T entity);

        // Delete entity
        void Delete(T entity);

        // Save changes (optional, depends on your UnitOfWork pattern)
        Task<int> SaveChangesAsync();
    }
}
