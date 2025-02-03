using MyProperty.API.Core.Domain.Entities;
using MyProperty.API.Core.Domain.Repositories;
using MyProperty.API.Infrastructure.Persistence.Persistence;
using MyProperty.API.Infrastructure.Persistence.Persistence.Repositories.Common;
using Persistence;

namespace MyProperty.API.Infrastructure.Persistence.Persistence.Repositories
{
	public class ReservationRepository(DataContext dataContext) : RepositoryBase<Reservation>(dataContext), IReservationRepository
	{
		public void CreateReservation(Reservation reservation, CancellationToken cancellationToken = default) => Create(reservation);

		public void DeleteReservation(Reservation reservation, CancellationToken cancellationToken = default) => Delete(reservation);

		public void UpdateReservation(Reservation reservation, CancellationToken cancellationToken = default) => Update(reservation);

		public async Task<IEnumerable<Reservation>> GetAllReservations(CancellationToken cancellationToken = default)
		{
			return await FindAll(trackChanges: false)
				.Include(r => r.Property)
				.Include(r => r.Account)
				.ToListAsync(cancellationToken);
		}

   

        public async Task<Reservation> GetById(int reservationId, CancellationToken cancellationToken = default)
        {
            return await FindByCondition(r => r.ReservationId == reservationId)
                .Include(r => r.Property)
                .Include(r => r.Account)
                .FirstOrDefaultAsync(cancellationToken);
        }



        public async Task<IEnumerable<Reservation>> GetReservationsByUserId(string accountId, CancellationToken cancellationToken = default)
		{
			return await FindByCondition(r => r.AccountId == accountId).ToListAsync(cancellationToken);
		}

		public async Task<IEnumerable<Reservation>> GetReservationsByPropertyId(int propertyId, CancellationToken cancellationToken = default)
		{
			return await FindByCondition(r => r.PropertyId == propertyId).ToListAsync(cancellationToken);
		}

		public Task<IEnumerable<Reservation>> GetReservationsByUserId(int userId, CancellationToken cancellationToken = default)
		{
			throw new NotImplementedException();
		}

		public async Task<Reservation?> GetExistingReservation(string accountId, int propertyId, CancellationToken cancellationToken)
		{
			return await FindByCondition(r => r.AccountId == accountId && r.PropertyId == propertyId)
				.FirstOrDefaultAsync(cancellationToken);
		}
	}
}
