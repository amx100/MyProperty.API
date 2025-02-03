global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Persistence.Configurations;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using MyProperty.API.Core.Domain.Entities;
using MyProperty.API.Core.Domain.Entities.JointTable;

namespace Persistence
{
	public sealed class DataContext : IdentityDbContext<
		Account,
		AccountRole,
		string,
		IdentityUserClaim<string>,
		AccountIdentityUserRole,
		IdentityUserLogin<string>,
		IdentityRoleClaim<string>,
		IdentityUserToken<string>
	>
	{
		public DataContext(DbContextOptions<DataContext> options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Account>().ToTable("AspNetUsers");
			modelBuilder.Entity<AccountIdentityUserRole>().ToTable("AspNetUserRoles");
			modelBuilder.Entity<AccountRole>().ToTable("AspNetRoles");

			modelBuilder.Entity<AccountIdentityUserRole>()
				.HasOne(p => p.User)
				.WithMany(b => b.Roles)
				.HasForeignKey(p => p.UserId);

			modelBuilder.Entity<AccountIdentityUserRole>()
				.HasOne(x => x.Role)
				.WithMany(x => x.Roles)
				.HasForeignKey(p => p.RoleId);

			

        
            modelBuilder.Entity<PropertyImage>()
                .HasKey(pi => pi.ImageId);

            modelBuilder.Entity<PropertyImage>()
                .HasOne(pi => pi.Property)
                .WithMany(p => p.Images)
                .HasForeignKey(pi => pi.PropertyId)
                .OnDelete(DeleteBehavior.Cascade); 

         
            modelBuilder.Entity<Property>(entity =>
            {
                entity.HasKey(p => p.PropertyId);
                entity.Property(p => p.Title).IsRequired();
                entity.Property(p => p.Description).IsRequired();
                entity.Property(p => p.Address).IsRequired();
                entity.Property(p => p.Price).HasColumnType("decimal(18,2)").IsRequired();
                entity.Property(p => p.PropertyType).IsRequired();
                entity.Property(p => p.Status).IsRequired();
                entity.Property(p => p.Area).IsRequired();

                // Configure cascade delete for Images
                entity.HasMany(p => p.Images)
                      .WithOne(pi => pi.Property)
                      .HasForeignKey(pi => pi.PropertyId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Configure cascade delete for Reservations
                entity.HasMany(p => p.Reservations)
                      .WithOne(r => r.Property)
                      .HasForeignKey(r => r.PropertyId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                // Primary key
                entity.HasKey(r => r.ReservationId);

              
                entity.Property(r => r.Status)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(r => r.StartDate)
                    .IsRequired();

                entity.Property(r => r.EndDate)
                    .IsRequired();

              
                entity.Property(r => r.ReservationDate)
                    .IsRequired();

                entity.HasOne(r => r.Property)
        .WithMany(p => p.Reservations)
        .HasForeignKey(r => r.PropertyId)
        .OnDelete(DeleteBehavior.Cascade); // Promenjeno sa Restrict na Cascade

                // Configure the relationship with Account
                entity.HasOne(r => r.Account)
                    .WithMany(a => a.Reservations)
                    .HasForeignKey(r => r.AccountId)
                    .OnDelete(DeleteBehavior.Cascade); // Promenjeno sa Restrict na Cascade


            });


            modelBuilder.ApplyConfiguration(new RoleConfiguration());
			modelBuilder.ApplyConfiguration(new AccountConfiguration());
			modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
		}

		public DbSet<Property> Properties { get; set; }
		public DbSet<PropertyImage> PropertiesImages { get; set; }
		public DbSet<Reservation> Reservations { get; set; }
	}
}
