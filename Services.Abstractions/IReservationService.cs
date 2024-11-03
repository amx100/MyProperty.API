using Contract;

namespace Services.Abstractions;

public interface IReservationService
{
	Task<IEnumerable<ReservationDto>> GetAll(CancellationToken cancellationToken = default);

	Task<ReservationDto> GetById(int reservationId, CancellationToken cancellationToken = default);

	Task<GeneralResponseDto> Create(ReservationCreateDto reservationDto, CancellationToken cancellationToken = default);

	Task<GeneralResponseDto> Update(int reservationId, ReservationUpdateDto reservationDto, CancellationToken cancellationToken = default);

	Task Delete(int reservationId, CancellationToken cancellationToken = default);
}
