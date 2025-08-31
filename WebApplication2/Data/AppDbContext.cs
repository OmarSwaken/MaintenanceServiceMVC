using MaintenanceServiceMVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace MaintenanceServiceMVC.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options){}
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Professional> Professionals { get; set; }
        
        //Dropped table
        //public DbSet<ProfessionalService> ProfessionalServices { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceRequest> ServiceRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Fluent API Configurations
            // Relationships
            modelBuilder.Entity<Professional>()
                .HasOne(p => p.Service)
                .WithMany(s => s.Professionals)
                .HasForeignKey(p => p.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);

            


            modelBuilder.Entity<ServiceRequest>()
                .HasOne(sr => sr.Customer)
                .WithMany(c => c.ServiceRequests)
                .HasForeignKey(sr => sr.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ServiceRequest>()
                .HasOne(sr => sr.Professional)
                .WithMany(p => p.ServiceRequests)
                .HasForeignKey(sr => sr.ProfessionalId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<ServiceRequest>()
                .HasOne(sr => sr.Service)
                .WithMany(s => s.ServiceRequests)
                .HasForeignKey(sr => sr.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Customer)
                .WithMany(c => c.Reviews)
                .HasForeignKey(r => r.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Professional)
                .WithMany(p => p.Reviews)
                .HasForeignKey(r => r.ProfessionalId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Customer>()
                .HasOne(c => c.User)
                .WithOne(u => u.Customer)
                .HasForeignKey<Customer>(c => c.UserId);

            modelBuilder.Entity<Professional>()
                .HasOne(p => p.User)
                .WithOne(u => u.Professional)
                .HasForeignKey<Professional>(c => c.UserId);

            modelBuilder.Entity<Service>()
                .HasIndex(s => s.Name)
                .IsUnique();

            modelBuilder.Entity<Professional>()
                .Property(p => p.ProfessionalId)
                .UseIdentityColumn(seed: 1000, increment: 1);

            modelBuilder.Entity<Customer>()
                .Property(c => c.CustomerId)
                .UseIdentityColumn(seed: 1000, increment: 1);

            modelBuilder.Entity<ServiceRequest>()
                .Property(sr => sr.ServiceRequestId)
                .UseIdentityColumn(seed: 1000, increment: 1);

            modelBuilder.Entity<Review>()
                .Property(r => r.ReviewId)
                .UseIdentityColumn(seed: 1000, increment: 1);

            modelBuilder.Entity<Service>()
                .Property(s => s.ServiceId) // or ServiceId depending on your class
                .UseIdentityColumn(seed: 1000, increment: 1);


            #endregion



        }

    }
    
}
