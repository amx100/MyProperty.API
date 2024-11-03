using MyProperty.API.Core.Domain.Entities;
using MyProperty.API.Core.Domain.Repositories;
using MyProperty.API.Infrastructure.Persistence.Persistence.Repositories.Common;
using Persistence;

namespace MyProperty.API.Infrastructure.Persistence.Persistence.Repositories;

public class TransactionRepository(DataContext dataContext) : RepositoryBase<Transaction>(dataContext), ITransactionRepository
{
    public void CreateTransaction(Transaction transaction, CancellationToken cancellationToken = default) => Create(transaction);

    public void DeleteTransaction(Transaction transaction, CancellationToken cancellationToken = default) => Delete(transaction);

    public void UpdateTransaction(Transaction transaction, CancellationToken cancellationToken = default) => Update(transaction);

    public async Task<IEnumerable<Transaction>> GetAllTransactions(CancellationToken cancellationToken = default)
    {
        return await FindAll().ToListAsync(cancellationToken);
    }

    public async Task<Transaction> GetById(int transactionId, CancellationToken cancellationToken = default)
    {
        return await FindByCondition(t => t.TransactionId == transactionId).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<Transaction>> GetTransactionsByBuyerId(string buyerId, CancellationToken cancellationToken = default)
    {
        return await FindByCondition(t => t.BuyerId == buyerId).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Transaction>> GetTransactionsByOwnerId(string OwnerId, CancellationToken cancellationToken = default)
    {
        return await FindByCondition(t => t.OwnerId == OwnerId).ToListAsync(cancellationToken);
    }

	public Task<IEnumerable<Transaction>> GetTransactionsByBuyerId(int buyerId, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public Task<IEnumerable<Transaction>> GetTransactionsByOwnerId(int OwnerId, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}
}

