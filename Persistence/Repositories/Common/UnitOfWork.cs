using MyProperty.API.Core.Domain.Repositories;
using MyProperty.API.Core.Domain.Repositories.Common;
using MyProperty.API.Infrastructure.Persistence.Persistence;
using Persistence;

namespace MyProperty.API.Infrastructure.Persistence.Persistence.Repositories.Common;

internal sealed class UnitOfWork(DataContext dbContext) : IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => dbContext.SaveChangesAsync(cancellationToken);
}