using Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/accounts")] 
    public class AccountController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllAccounts(CancellationToken cancellationToken)
        {
            var response = await serviceManager.AccountService.GetAll(cancellationToken);
            return Ok(response);
        }
        
        [HttpPost("login")] 
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto, CancellationToken cancellationToken)
        {
            var response = await serviceManager.AccountService.Login(loginDto, cancellationToken);
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("deleteAccount/{accountId}")] 
        public async Task<IActionResult> DeleteAccount(string accountId, CancellationToken cancellationToken)
        {
            await serviceManager.AccountService.Delete(accountId, cancellationToken);
            return NoContent();
        }

        [HttpPost("registration")]
        public async Task<IActionResult> Register([FromBody] RegistrationDto registrationDto, CancellationToken cancellationToken)
        {
            var response = await serviceManager.AccountService.Register(registrationDto, cancellationToken);
            return Ok(response);
        }

        [Authorize]
        [HttpGet("details/{accountId}")] 
        public async Task<IActionResult> GetAccountById(string accountId, CancellationToken cancellationToken)
        {
            var response = await serviceManager.AccountService.GetById(accountId, cancellationToken);
            return Ok(response);
        }

        [Authorize]
        [HttpPut("update/{accountId}")]
        public async Task<IActionResult> UpdateAccount(string accountId, [FromBody] AccountUpdateDto accountDto, CancellationToken cancellationToken)
        {
            var response = await serviceManager.AccountService.Update(accountId, accountDto, cancellationToken);
            return Ok(response);
        }
    }
}
