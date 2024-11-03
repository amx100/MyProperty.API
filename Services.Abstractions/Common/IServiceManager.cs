namespace Services.Abstractions
{
	public interface IServiceManager
    {
        IAccountService AccountService { get; }

        IPropertyService PropertyService { get; }
        
        IPropertyImageService PropertyImageService { get; }

        ITransactionService TransactionService { get; }

        IReservationService ReservationService { get; }
    }
}