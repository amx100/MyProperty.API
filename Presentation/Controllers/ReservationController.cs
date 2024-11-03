using Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace Presentation.Controllers
{
	[ApiController]
	[Route("api/reservations")]
	public class ReservationController(IServiceManager serviceManager) : ControllerBase
	{

		[HttpGet]
		public async Task<IActionResult> GetAllReservations(CancellationToken cancellationToken)
		{
			var response = await serviceManager.ReservationService.GetAll(cancellationToken);
			return Ok(response);
		}

		[Authorize(Roles = "Admin")]
		[HttpDelete("delete/{reservationId}")]
		public async Task<IActionResult> Delete(int reservationId, CancellationToken cancellationToken)
		{
			await serviceManager.ReservationService.Delete(reservationId, cancellationToken);
			return NoContent();
		}

		[HttpPost("create")]
		public async Task<IActionResult> Create([FromBody] ReservationCreateDto reservationDto, CancellationToken cancellationToken)
		{
			var response = await serviceManager.ReservationService.Create(reservationDto, cancellationToken);
			return Ok(response);
		}

		[Authorize]
		[HttpGet("details/{reservationId}")]
		public async Task<IActionResult> GetReservationById(int reservationId, CancellationToken cancellationToken)
		{
			var response = await serviceManager.ReservationService.GetById(reservationId, cancellationToken);
			return Ok(response);
		}

		[HttpPut("update/{reservationId}")]
		public async Task<IActionResult> UpdateReservation(int reservationId, [FromBody] ReservationUpdateDto reservationDto, CancellationToken cancellationToken)
		{
			var response = await serviceManager.ReservationService.Update(reservationId, reservationDto, cancellationToken);
			return Ok(response);
		}
	}
}
