using System.Linq.Expressions;

namespace MyProperty.API.Core.Domain.Repositories.Common
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll(bool trackChanges = false);

        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false);

        void Create(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}