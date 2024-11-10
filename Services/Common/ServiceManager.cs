global using Contract;
global using Mapster;
global using Microsoft.IdentityModel.Tokens;
global using Services.Abstractions;
global using System.IdentityModel.Tokens.Jwt;
global using System.Security.Claims;
global using System.Security.Cryptography;
global using System.Text;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using MyProperty.API.Core.Domain.Repositories.Common;


namespace Services
{
	public class ServiceManager(
		IRepositoryManager repositoryManager,
		UserManager<Account> userManager,
		RoleManager<AccountRole> roleManager,
		ITokenService tokenService) : IServiceManager
	{
		private readonly Lazy<IAccountService> _lazyAccountService = new(() => new AccountService(repositoryManager, userManager, roleManager, tokenService));

		private readonly Lazy<IPropertyService> _lazyPropertyService = new(() => new PropertyService(repositoryManager));

		private readonly Lazy<IPropertyImageService> _lazyPropertyImageService = new(() => new PropertyImageService(repositoryManager));

		private readonly Lazy<ITransactionService> _lazyTransactionService = new(() => new TransactionService(repositoryManager));

		private readonly Lazy<IReservationService> _lazyReservationService = new(() => new ReservationService(repositoryManager));







		public IAccountService AccountService => _lazyAccountService.Value;

		public IPropertyService PropertyService => _lazyPropertyService.Value;

		public IPropertyImageService PropertyImageService => _lazyPropertyImageService.Value;

		public ITransactionService TransactionService => _lazyTransactionService.Value;

		public IReservationService ReservationService => _lazyReservationService.Value;
	}
}