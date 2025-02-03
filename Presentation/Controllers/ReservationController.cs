using Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using System.Text.Json;

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

	
		[HttpDelete("delete/{reservationId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int reservationId, CancellationToken cancellationToken)
		{
			await serviceManager.ReservationService.Delete(reservationId, cancellationToken);
			return NoContent();
		}

		[HttpPost("create")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Create([FromBody] ReservationCreateDto reservationDto, CancellationToken cancellationToken)
		{
			var response = await serviceManager.ReservationService.Create(reservationDto, cancellationToken);
			return Ok(response);
		}

	
		[HttpGet("details/{reservationId}")]
		public async Task<IActionResult> GetReservationById(int reservationId, CancellationToken cancellationToken)
		{
			var response = await serviceManager.ReservationService.GetById(reservationId, cancellationToken);
			return Ok(response);
		}

        
        [HttpPut("update/{reservationId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateReservation(int reservationId, [FromBody] ReservationUpdateDto reservationDto, CancellationToken cancellationToken)
        {
            try
            {
                // Log received data
                Console.WriteLine($"Received update request for reservation {reservationId}");
                Console.WriteLine($"Request body: {JsonSerializer.Serialize(reservationDto)}");

                var response = await serviceManager.ReservationService.Update(reservationId, reservationDto, cancellationToken);

                Console.WriteLine($"Update response: {JsonSerializer.Serialize(response)}");

                if (!response.IsSuccess)
                {
                    return BadRequest(response);
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateReservation: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                return StatusCode(500, new GeneralResponseDto
                {
                    IsSuccess = false,
                    Message = $"Internal server error: {ex.Message}"
                });
            }
        }
    }
}
