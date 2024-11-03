using MyProperty.API.Core.Domain.Entities;
using MyProperty.API.Core.Domain.Repositories.Common;

namespace MyProperty.API.Core.Domain.Repositories
{
    public interface ITransactionRepository : IRepositoryBase<Transaction>
    {
        Task<IEnumerable<Transaction>> GetAllTransactions(CancellationToken cancellationToken = default);
        Task<Transaction> GetById(int transactionId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Transaction>> GetTransactionsByBuyerId(int buyerId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Transaction>> GetTransactionsByOwnerId(int OwnerId, CancellationToken cancellationToken = default);

        void CreateTransaction(Transaction transaction, CancellationToken cancellationToken = default);
        void DeleteTransaction(Transaction transaction, CancellationToken cancellationToken = default);
        void UpdateTransaction(Transaction transaction, CancellationToken cancellationToken = default);
    }
}
