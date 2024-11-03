using Microsoft.AspNetCore.Identity;
using MyProperty.API.Core.Domain.Entities.JointTable;

namespace MyProperty.API.Core.Domain.Entities;


public class Account : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;

    public string? LastName { get; set; }

    public string? PasswordResetToken { get; set; }

    public string? EmailConfirmationToken { get; set; }

    public string RefreshToken { get; set; } = string.Empty;

    public DateTime RefreshTokenExpiryTime { get; set; }

    public string? MobileNumber { get; set; }

    public virtual ICollection<AccountIdentityUserRole> Roles { get; } = [];

	public class AccountRole : IdentityRole
	{
		public virtual ICollection<AccountIdentityUserRole> Roles { get; } = [];
	}

	//public ICollection<Reservation> Reservations { get; set; }
	//public ICollection<Transaction> TransactionsAsBuyer { get; set; }
	//public ICollection<Transaction> TransactionsAsOwner { get; set; }
}

