using MyProperty.API.Core.Domain.Entities;
using MyProperty.API.Core.Domain.Repositories.Common;

namespace MyProperty.API.Core.Domain.Repositories
{
    public interface IReservationRepository : IRepositoryBase<Reservation>
    {
        Task<IEnumerable<Reservation>> GetAllReservations(CancellationToken cancellationToken = default);
        Task<Reservation> GetById(int reservationId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Reservation>> GetReservationsByUserId(int userId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Reservation>> GetReservationsByPropertyId(int propertyId, CancellationToken cancellationToken = default);

		Task<Reservation?> GetExistingReservation(string accountId, int propertyId, CancellationToken cancellationToken);

		void CreateReservation(Reservation reservation, CancellationToken cancellationToken = default);
        void DeleteReservation(Reservation reservation, CancellationToken cancellationToken = default);
        void UpdateReservation(Reservation reservation, CancellationToken cancellationToken = default);
    }
}
