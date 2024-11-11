using Domain.Entities;

namespace Persistence.Configurations
{
	public class AccountConfiguration : IEntityTypeConfiguration<Account>
	{
		public void Configure(EntityTypeBuilder<Account> builder)
		{
			var admin = new Account
			{
				FirstName = "Admin",
				LastName = "Admin",
				Id = "595af844-b3f7-4d70-87ca-eb9c08a2368a",
				Email = "admin@test.com",
				NormalizedEmail = "ADMIN@TEST.COM",
				UserName = "admin@test.com",
				NormalizedUserName = "ADMIN@TEST.COM",
				EmailConfirmed = true
			};

			var user = new Account
			{
				FirstName = "User",
				LastName = "User",
				Id = "4334dd38-cdd9-4ba8-99c6-856220356d4a",
				Email = "user@test.com",
				NormalizedEmail = "USER@TEST.COM",
				UserName = "user@test.com",
				NormalizedUserName = "USER@TEST.COM",
				EmailConfirmed = true
			};

			var owner = new Account
			{
				FirstName = "Owner",
				LastName = "Owner",
				Id = "4cda5ea1-47a4-4383-9a6c-b581d13cc961", // Zameniti stvarnim ID-em
				Email = "owner@test.com",
				NormalizedEmail = "OWNER@TEST.COM",
				UserName = "owner@test.com",
				NormalizedUserName = "OWNER@TEST.COM",
				EmailConfirmed = true
			};

			var buyer = new Account
			{
				FirstName = "Buyer",
				LastName = "Buyer",
				Id = "7ada92d0-de96-45f7-a0f8-dafba1830724", // Zameniti stvarnim ID-em
				Email = "buyer@test.com",
				NormalizedEmail = "BUYER@TEST.COM",
				UserName = "buyer@test.com",
				NormalizedUserName = "BUYER@TEST.COM",
				EmailConfirmed = true
			};

			builder.HasData(admin, user, owner, buyer);
		}
	}
}
/*
 * 
 * var owner = new Account
			{
				FirstName = "Owner",
				LastName = "Owner",
				Id = "4cda5ea1-47a4-4383-9a6c-b581d13cc961", // Zameniti stvarnim ID-em
				Email = "owner@test.com",
				NormalizedEmail = "OWNER@TEST.COM",
				UserName = "owner@test.com",
				NormalizedUserName = "OWNER@TEST.COM",
				EmailConfirmed = true
			};

			var buyer = new Account
			{
				FirstName = "Buyer",
				LastName = "Buyer",
				Id = "7ada92d0-de96-45f7-a0f8-dafba1830724", // Zameniti stvarnim ID-em
				Email = "buyer@test.com",
				NormalizedEmail = "BUYER@TEST.COM",
				UserName = "buyer@test.com",
				NormalizedUserName = "BUYER@TEST.COM",
				EmailConfirmed = true
			};

			builder.HasData(admin, user, owner, buyer);
 */