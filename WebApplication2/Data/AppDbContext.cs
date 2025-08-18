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
        public DbSet<ProfessionalService> ProfessionalServices { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceRequest> ServiceRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Fluent API Configurations
            // Composite key for ProfessionalService
            modelBuilder.Entity<ProfessionalService>()
                .HasKey(ps => new { ps.ProfessionalId, ps.ServiceId });

            // Relationships
            modelBuilder.Entity<ProfessionalService>()
                .HasOne(ps => ps.Professional)
                .WithMany(p => p.ProfessionalServices)
                .HasForeignKey(ps => ps.ProfessionalId);

            modelBuilder.Entity<ProfessionalService>()
                .HasOne(ps => ps.Service)
                .WithMany(s => s.ProfessionalServices)
                .HasForeignKey(ps => ps.ServiceId);

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

            #endregion

            //#region Seed Data
            //// Customers
            //modelBuilder.Entity<Customer>().HasData(
            //    new Customer { CustomerId = 1, Name = "John Smith", Email = "john.smith@example.com", Phone = "+1-555-1234", Address = "123 Main St, NY" },
            //    new Customer { CustomerId = 2, Name = "Emily Johnson", Email = "emily.j@example.com", Phone = "+1-555-5678", Address = "45 Park Ave, LA" },
            //    new Customer { CustomerId = 3, Name = "Michael Brown", Email = "michael.b@example.com", Phone = "+1-555-9012", Address = "78 Oak Rd, TX" },
            //    new Customer { CustomerId = 4, Name = "Sarah Davis", Email = "sarah.d@example.com", Phone = "+1-555-3456", Address = "12 Lakeview Dr, FL" },
            //    new Customer { CustomerId = 5, Name = "David Wilson", Email = "david.w@example.com", Phone = "+1-555-7890", Address = "90 Sunset Blvd, CA" }
            //);

            //// Professionals
            //modelBuilder.Entity<Professional>().HasData(
            //    new Professional { ProfessionalId = 1, Name = "James Miller", Specialty = "Plumbing", Phone = "+1-555-1111", Email = "james.m@example.com", Rating = 4.5m, ExperienceYears = 8, HourlyRate = 50m, IsAvailable = true, JoinDate = new DateTime(2022, 5, 1) },
            //    new Professional { ProfessionalId = 2, Name = "Olivia Garcia", Specialty = "Electrical", Phone = "+1-555-2222", Email = "olivia.g@example.com", Rating = 4.8m, ExperienceYears = 10, HourlyRate = 60m, IsAvailable = true, JoinDate = new DateTime(2020, 3, 15) },
            //    new Professional { ProfessionalId = 3, Name = "Ethan Martinez", Specialty = "Carpentry", Phone = "+1-555-3333", Email = "ethan.m@example.com", Rating = 4.3m, ExperienceYears = 6, HourlyRate = 45m, IsAvailable = false, JoinDate = new DateTime(2021, 8, 10) },
            //    new Professional { ProfessionalId = 4, Name = "Sophia Rodriguez", Specialty = "Cleaning", Phone = "+1-555-4444", Email = "sophia.r@example.com", Rating = 4.9m, ExperienceYears = 7, HourlyRate = 40m, IsAvailable = true, JoinDate = new DateTime(2023, 1, 5) },
            //    new Professional { ProfessionalId = 5, Name = "Liam Lee", Specialty = "Painting", Phone = "+1-555-5555", Email = "liam.l@example.com", Rating = 4.2m, ExperienceYears = 5, HourlyRate = 55m, IsAvailable = true, JoinDate = new DateTime(2024, 2, 20) }
            //);

            //// Services
            //modelBuilder.Entity<Service>().HasData(
            //    new Service { ServiceId = 1, Name = "Plumbing Repair", Description = "Fix leaks, replace pipes, and other plumbing services.", BasePrice = 80m, IsActive = true, CreatedDate = new DateTime(2024, 8, 1) },
            //    new Service { ServiceId = 2, Name = "Electrical Work", Description = "Install, repair, and maintain electrical systems.", BasePrice = 100m, IsActive = true, CreatedDate = new DateTime(2024, 10, 15) },
            //    new Service { ServiceId = 3, Name = "Carpentry", Description = "Custom furniture, repairs, and woodwork.", BasePrice = 120m, IsActive = true, CreatedDate = new DateTime(2024, 12, 1) },
            //    new Service { ServiceId = 4, Name = "House Cleaning", Description = "Deep cleaning for houses and offices.", BasePrice = 60m, IsActive = true, CreatedDate = new DateTime(2025, 2, 1) },
            //    new Service { ServiceId = 5, Name = "Painting", Description = "Interior and exterior painting services.", BasePrice = 150m, IsActive = true, CreatedDate = new DateTime(2025, 4, 10) }
            //);

            //// ProfessionalServices (junction table)
            //modelBuilder.Entity<ProfessionalService>().HasData(
            //    new ProfessionalService { ProfessionalId = 1, ServiceId = 1, IsCertified = true, CertificationDate = new DateTime(2023, 6, 1) },
            //    new ProfessionalService { ProfessionalId = 2, ServiceId = 2, IsCertified = true, CertificationDate = new DateTime(2022, 9, 15) },
            //    new ProfessionalService { ProfessionalId = 3, ServiceId = 3, IsCertified = false, CertificationDate = new DateTime(2024, 3, 1) },
            //    new ProfessionalService { ProfessionalId = 4, ServiceId = 4, IsCertified = true, CertificationDate = new DateTime(2023, 1, 20) },
            //    new ProfessionalService { ProfessionalId = 5, ServiceId = 5, IsCertified = true, CertificationDate = new DateTime(2024, 5, 5) }
            //);

            //// ServiceRequests
            //modelBuilder.Entity<ServiceRequest>().HasData(
            //    new ServiceRequest { ServiceRequestId = 1, CustomerId = 1, ProfessionalId = 1, ServiceId = 1, Description = "Fix kitchen sink leak", Address = "123 Main St, NY", RequestDate = new DateTime(2025, 8, 1), ScheduledDate = new DateTime(2025, 8, 3), CompletedDate = new DateTime(2025, 8, 4), FinalPrice = 90m, Status = "Completed", Notes = "Quick repair." },
            //    new ServiceRequest { ServiceRequestId = 2, CustomerId = 2, ProfessionalId = 2, ServiceId = 2, Description = "Install new ceiling fan", Address = "45 Park Ave, LA", RequestDate = new DateTime(2025, 8, 5), ScheduledDate = new DateTime(2025, 8, 7), Status = "InProgress", Notes = null },
            //    new ServiceRequest { ServiceRequestId = 3, CustomerId = 3, ProfessionalId = 3, ServiceId = 3, Description = "Repair wardrobe door", Address = "78 Oak Rd, TX", RequestDate = new DateTime(2025, 8, 6), Status = "Pending", Notes = "Needs urgent fix." },
            //    new ServiceRequest { ServiceRequestId = 4, CustomerId = 4, ProfessionalId = 4, ServiceId = 4, Description = "Weekly cleaning service", Address = "12 Lakeview Dr, FL", RequestDate = new DateTime(2025, 8, 8), ScheduledDate = new DateTime(2025, 8, 11), Status = "Assigned", Notes = null },
            //    new ServiceRequest { ServiceRequestId = 5, CustomerId = 5, ProfessionalId = 5, ServiceId = 5, Description = "Paint living room", Address = "90 Sunset Blvd, CA", RequestDate = new DateTime(2025, 8, 10), Status = "Pending", Notes = "Prefer light blue." }
            //);

            //// Reviews
            //modelBuilder.Entity<Review>().HasData(
            //    new Review { ReviewId = 1, CustomerId = 1, ProfessionalId = 1, Rating = 5, Comment = "Excellent work!", ReviewDate = new DateTime(2025, 8, 4) },
            //    new Review { ReviewId = 2, CustomerId = 2, ProfessionalId = 2, Rating = 4, Comment = "Good service, on time.", ReviewDate = new DateTime(2025, 8, 7) },
            //    new Review { ReviewId = 3, CustomerId = 3, ProfessionalId = 3, Rating = 3, Comment = "Average quality.", ReviewDate = new DateTime(2025, 8, 8) },
            //    new Review { ReviewId = 4, CustomerId = 4, ProfessionalId = 4, Rating = 5, Comment = "Highly recommend!", ReviewDate = new DateTime(2025, 8, 11) },
            //    new Review { ReviewId = 5, CustomerId = 5, ProfessionalId = 5, Rating = 4, Comment = "Nice work, but a bit slow.", ReviewDate = new DateTime(2025, 8, 10) }
            //);
            //#endregion

        }

    }
    
}
