namespace MyProperty.API.Core.Domain.Repositories.Common
{
	public interface IRepositoryManager
    {
        IPropertyRepository PropertyRepository { get; }
        IAccountRepository AccountRepository { get; }
        IPropertyImageRepository PropertyImageRepository { get; }
        IReservationRepository ReservationRepository { get; }
        IUnitOfWork UnitOfWork { get; }
    }
}