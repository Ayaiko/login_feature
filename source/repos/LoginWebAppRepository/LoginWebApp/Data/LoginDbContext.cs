using LoginWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LoginWebApp.Data
{
    public class LoginDbContext : IdentityDbContext
    {
        private readonly DbContextOptions _options;

        public LoginDbContext(DbContextOptions options) : base(options)
        {
            _options = options;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppointmentModel>()
                .HasOne(u => u.User)
                .WithMany()
                .HasForeignKey(u => u.UserId)
                .IsRequired();

        }

        public DbSet<AppointmentModel> Appointment { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
    }
}
