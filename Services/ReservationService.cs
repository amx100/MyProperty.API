using MyProperty.API.Core.Domain.Entities;
using MyProperty.API.Core.Domain.Repositories.Common;

namespace Services
{
	public class ReservationService(IRepositoryManager repositoryManager) : IReservationService
	{
		public async Task<GeneralResponseDto> Create(ReservationCreateDto reservationDto, CancellationToken cancellationToken = default)
		{
			try
			{
				// Check if account exists
				var account = await repositoryManager.AccountRepository.GetById(reservationDto.AccountId, cancellationToken);
				if (account == null)
				{
					return new GeneralResponseDto
					{
						IsSuccess = false,
						Message = "Account not found."
					};
				}

				// Check if property exists and is available
				var property = await repositoryManager.PropertyRepository.GetById(reservationDto.PropertyId, cancellationToken);
				if (property == null || property.Status != "Available")
				{
					return new GeneralResponseDto
					{
						IsSuccess = false,
						Message = "Property not found or is unavailable."
					};
				}

				// Check for existing reservations
				var existingReservation = await repositoryManager.ReservationRepository
					.GetExistingReservation(reservationDto.AccountId, reservationDto.PropertyId, cancellationToken);
				if (existingReservation != null)
				{
					return new GeneralResponseDto
					{
						IsSuccess = false,
						Message = "You already have a pending or confirmed reservation for this property."
					};
				}

				// Create new reservation and set properties manually instead of using Adapt
				var reservation = new Reservation
				{
					PropertyId = reservationDto.PropertyId,
					AccountId = reservationDto.AccountId,
					StartDate = reservationDto.StartDate,
					EndDate = reservationDto.EndDate,
					Status = "Pending",
					ReservationDate = DateTime.UtcNow
				};

				repositoryManager.ReservationRepository.Create(reservation);
				await repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);

				return new GeneralResponseDto { Message = "Reservation created successfully.", IsSuccess = true };
			}
			catch (Exception ex)
			{
				return new GeneralResponseDto
				{
					IsSuccess = false,
					Message = ex.Message
				};
			}
		}


		public async Task Delete(int reservationId, CancellationToken cancellationToken = default)
		{
			var reservation = await repositoryManager.ReservationRepository.GetById(reservationId, cancellationToken);
			if (reservation != null)
			{
				repositoryManager.ReservationRepository.Delete(reservation);
				await repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
			}
		}

		public async Task<IEnumerable<ReservationDto>> GetAll(CancellationToken cancellationToken = default)
		{
			var reservations = await repositoryManager.ReservationRepository.GetAllReservations(cancellationToken);
			
			var reservationDtos = reservations.Select(r => new ReservationDto
			{
				Id = r.ReservationId,
				PropertyId = r.PropertyId,
				AccountId = r.AccountId,
				Status = r.Status,
				StartDate = r.StartDate,
				EndDate = r.EndDate
			});

			return reservationDtos;
		}

		public async Task<ReservationDto> GetById(int reservationId, CancellationToken cancellationToken = default)
		{
			var reservation = await repositoryManager.ReservationRepository.GetById(reservationId, cancellationToken);
			return reservation.Adapt<ReservationDto>();
		}

        public async Task<GeneralResponseDto> Update(int reservationId, ReservationUpdateDto reservationDto, CancellationToken cancellationToken = default)
        {
            try
            {
                var existingReservation = await repositoryManager.ReservationRepository.GetById(reservationId, cancellationToken);
                if (existingReservation == null)
                {
                    return new GeneralResponseDto
                    {
                        IsSuccess = false,
                        Message = "Reservation not found."
                    };
                }

                // The Property is already included in the existingReservation
                var property = existingReservation.Property;
                if (property == null)
                {
                    return new GeneralResponseDto
                    {
                        IsSuccess = false,
                        Message = "Associated property not found."
                    };
                }

                // Update reservation status
                existingReservation.Status = reservationDto.Status;

                // If reservation is confirmed, update property status to Reserved
                if (reservationDto.Status == "Confirmed")
                {
                    property.Status = "Reserved";
                }
                // If reservation is declined or cancelled, ensure property is Available
                else if (reservationDto.Status == "Declined" || reservationDto.Status == "Cancelled")
                {
                    property.Status = "Available";
                }

                // Save changes
                repositoryManager.ReservationRepository.Update(existingReservation);
                var result = await repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);

                if (result <= 0)
                {
                    return new GeneralResponseDto
                    {
                        IsSuccess = false,
                        Message = "Failed to save changes to database."
                    };
                }

                return new GeneralResponseDto
                {
                    IsSuccess = true,
                    Message = $"Reservation status updated to {reservationDto.Status} successfully. Property status updated accordingly."
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Update: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                return new GeneralResponseDto
                {
                    IsSuccess = false,
                    Message = $"Error updating reservation: {ex.Message}"
                };
            }
        }
    }
}