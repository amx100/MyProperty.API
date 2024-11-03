using Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace Presentation.Controllers
{
	[ApiController]
	[Route("api/transactions")]
	public class TransactionController(IServiceManager serviceManager) : ControllerBase
	{
		[Authorize]
		[HttpGet]
		public async Task<IActionResult> GetAllTransactions(CancellationToken cancellationToken)
		{
			var response = await serviceManager.TransactionService.GetAll(cancellationToken);
			return Ok(response);
		}

		[Authorize(Roles = "Admin")]
		[HttpDelete("delete/{transactionId}")]
		public async Task<IActionResult> Delete(int transactionId, CancellationToken cancellationToken)
		{
			await serviceManager.TransactionService.Delete(transactionId, cancellationToken);
			return NoContent();
		}

		[HttpPost("create")]
		public async Task<IActionResult> Create([FromBody] TransactionCreateDto transactionDto, CancellationToken cancellationToken)
		{
			var response = await serviceManager.TransactionService.Create(transactionDto, cancellationToken);
			return Ok(response);
		}

		[Authorize]
		[HttpGet("details/{transactionId}")]
		public async Task<IActionResult> GetTransactionById(int transactionId, CancellationToken cancellationToken)
		{
			var response = await serviceManager.TransactionService.GetById(transactionId, cancellationToken);
			return Ok(response);
		}
	}
}
