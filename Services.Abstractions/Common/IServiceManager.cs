namespace Services.Abstractions
{
	public interface IServiceManager
    {
        IAccountService AccountService { get; }

        IPropertyService PropertyService { get; }
        
        IPropertyImageService PropertyImageService { get; }

        IReservationService ReservationService { get; }
    }
}