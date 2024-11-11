using MyProperty.API.Core.Domain.Entities.JointTable;

namespace Persistence.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<AccountIdentityUserRole>
    {
        public void Configure(EntityTypeBuilder<AccountIdentityUserRole> builder)
        {
			builder.HasData(
			   new AccountIdentityUserRole
			   {
				   RoleId = "1", //ADMIN
				   UserId = "595af844-b3f7-4d70-87ca-eb9c08a2368a" //admin@test.com
			   },
			   new AccountIdentityUserRole
			   {
				   RoleId = "2", //User
				   UserId = "4334dd38-cdd9-4ba8-99c6-856220356d4a" //user@test.com
			   },
			   // Dodajte nove role za Owner i Buyer
			   new AccountIdentityUserRole
			   {
				   RoleId = "3", //Owner
				   UserId = "4cda5ea1-47a4-4383-9a6c-b581d13cc961"// Zamenite sa stvarnim UserId za vlasnika
			   },
			   new AccountIdentityUserRole
			   {
				   RoleId = "4", //Buyer
				   UserId = "7ada92d0-de96-45f7-a0f8-dafba1830724"
			   });
		}
    }
}