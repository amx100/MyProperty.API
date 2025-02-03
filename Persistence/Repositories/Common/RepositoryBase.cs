using MyProperty.API.Core.Domain.Repositories.Common;
using MyProperty.API.Infrastructure.Persistence.Persistence;
using Persistence;
using System.Linq.Expressions;

namespace MyProperty.API.Infrastructure.Persistence.Persistence.Repositories.Common
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected DataContext RepositoryContext;

        protected RepositoryBase(DataContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }

        public IQueryable<T> FindAll(bool trackChanges = false) =>
            !trackChanges ? 
                RepositoryContext.Set<T>().AsNoTracking() : 
                RepositoryContext.Set<T>();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) => RepositoryContext.Set<T>().Where(expression).AsNoTracking();

        public void Create(T entity) => RepositoryContext.Set<T>().Add(entity);

        public void Update(T entity) => RepositoryContext.Set<T>().Update(entity);

        public void Delete(T entity) => RepositoryContext.Set<T>().Remove(entity);
    }
}