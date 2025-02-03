using Microsoft.AspNetCore.Identity;
using MyProperty.API.Core.Domain.Entities;
using MyProperty.API.Core.Domain.Entities.JointTable;

namespace Domain.Entities;

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

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}

public class AccountRole : IdentityRole
{
	public virtual ICollection<AccountIdentityUserRole> Roles { get; } = [];
}