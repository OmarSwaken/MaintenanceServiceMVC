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

            


            // ServiceRequest -> Customer (Required)
            modelBuilder.Entity<ServiceRequest>()
                .HasOne(sr => sr.Customer)
                .WithMany(c => c.ServiceRequests)
                .HasForeignKey(sr => sr.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            // ServiceRequest -> Professional (Optional)
            modelBuilder.Entity<ServiceRequest>()
                .HasOne(sr => sr.Professional)
                .WithMany(p => p.ServiceRequests)
                .HasForeignKey(sr => sr.ProfessionalId)
                .OnDelete(DeleteBehavior.SetNull);

            // ServiceRequest -> Service (Required)
            modelBuilder.Entity<ServiceRequest>()
                .HasOne(sr => sr.Service)
                .WithMany(s => s.ServiceRequests)
                .HasForeignKey(sr => sr.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);

            // Review -> Customer
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Customer)
                .WithMany(c => c.Reviews)
                .HasForeignKey(r => r.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Review -> Professional
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Professional)
                .WithMany(p => p.Reviews)
                .HasForeignKey(r => r.ProfessionalId)
                .OnDelete(DeleteBehavior.Restrict);


            // Relationship: One User -> One Customer
            modelBuilder.Entity<Customer>()
                .HasOne(c => c.User)
                .WithOne(u => u.Customer)
                .HasForeignKey<Customer>(c => c.UserId);

            // Relationship: One User -> One Professional
            modelBuilder.Entity<Professional>()
                .HasOne(p => p.User)
                .WithOne(u => u.Professional)
                .HasForeignKey<Professional>(c => c.UserId);

            // Unique Service Name
            modelBuilder.Entity<Service>()
                .HasIndex(s => s.Name)
                .IsUnique();
                

            #endregion

           

        }

    }
    
}
