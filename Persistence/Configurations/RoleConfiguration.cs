using Domain.Entities;
using System.Xml.Linq;

namespace Persistence.Configurations
{
	public class RoleConfiguration : IEntityTypeConfiguration<AccountRole>
	{
		public void Configure(EntityTypeBuilder<AccountRole> builder)
		{
			builder.HasData(
				new AccountRole
				{
					Id = "1",
					Name = "Admin",
					NormalizedName = "ADMIN"
				},
				new AccountRole
				{
					Id = "2",
					Name = "User",
					NormalizedName = "USER"
				},
				 new AccountRole
				 {
				Id = "3",
					   Name = "Owner",
					   NormalizedName = "OWNER"
				   },
				  new AccountRole
				  {
					  Id = "4",
					  Name = "Buyer",
					  NormalizedName = "BUYER"
				  }
			);
		}
	}
}
