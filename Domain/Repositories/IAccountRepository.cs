using MyProperty.API.Core.Domain.Entities;
using MyProperty.API.Core.Domain.Repositories.Common;

namespace MyProperty.API.Core.Domain.Repositories;

public interface IAccountRepository : IRepositoryBase<Account>
{
	Task<IEnumerable<Account>> GetAll(CancellationToken cancellationToken = default);

	Task<Account> GetById(string accountId, CancellationToken cancellationToken = default);
}
