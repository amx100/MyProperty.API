using MyProperty.API.Core.Domain.Repositories;
using MyProperty.API.Core.Domain.Repositories.Common;
using Persistence;

namespace MyProperty.API.Infrastructure.Persistence.Persistence.Repositories.Common
{
	public class RepositoryManager(DataContext dbContext) : IRepositoryManager
    {
        private readonly Lazy<IAccountRepository> _lazyAccountRepository = new(() => new AccountRepository(dbContext));

        private readonly Lazy<IUnitOfWork> _lazyUnitOfWork = new(() => new UnitOfWork(dbContext));

        private readonly Lazy<IPropertyRepository> _lazyPropertyRepository = new(() => new PropertyRepository(dbContext));

        private readonly Lazy<IPropertyImageRepository> _lazyPropertyImageRepository = new(() => new PropertyImageRepository(dbContext));

        private readonly Lazy<IReservationRepository> _lazyReservationRepository = new(() => new ReservationRepository(dbContext));

        private readonly Lazy<ITransactionRepository> _lazyTransactionRepository = new(() => new TransactionRepository(dbContext));








        public IAccountRepository AccountRepository => _lazyAccountRepository.Value;

        public IUnitOfWork UnitOfWork => _lazyUnitOfWork.Value;

        public IPropertyRepository PropertyRepository => _lazyPropertyRepository.Value;

        public IPropertyImageRepository PropertyImageRepository => _lazyPropertyImageRepository.Value;

        public IReservationRepository ReservationRepository => _lazyReservationRepository.Value;

        public ITransactionRepository TransactionRepository => _lazyTransactionRepository.Value;




    }
}