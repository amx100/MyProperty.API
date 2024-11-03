namespace MyProperty.API.Core.Domain.Repositories.Common
{
	public interface IRepositoryManager
    {
        IPropertyRepository PropertyRepository { get; }
        IAccountRepository AccountRepository { get; }
        IPropertyImageRepository PropertyImageRepository { get; }
        IReservationRepository ReservationRepository { get; }
        ITransactionRepository TransactionRepository { get; }
        IUnitOfWork UnitOfWork { get; }
    }
}