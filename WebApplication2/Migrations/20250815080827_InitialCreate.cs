using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MaintenanceServiceMVC.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Professionals",
                columns: table => new
                {
                    ProfessionalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Specialty = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false),
                    Rating = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ExperienceYears = table.Column<int>(type: "int", nullable: false),
                    HourlyRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    JoinDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professionals", x => x.ProfessionalId);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    ServiceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    BasePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.ServiceId);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ReviewId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    ProfessionalId = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ReviewDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK_Reviews_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reviews_Professionals_ProfessionalId",
                        column: x => x.ProfessionalId,
                        principalTable: "Professionals",
                        principalColumn: "ProfessionalId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProfessionalServices",
                columns: table => new
                {
                    ProfessionalId = table.Column<int>(type: "int", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    IsCertified = table.Column<bool>(type: "bit", nullable: false),
                    CertificationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessionalServices", x => new { x.ProfessionalId, x.ServiceId });
                    table.ForeignKey(
                        name: "FK_ProfessionalServices_Professionals_ProfessionalId",
                        column: x => x.ProfessionalId,
                        principalTable: "Professionals",
                        principalColumn: "ProfessionalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfessionalServices_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "ServiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceRequests",
                columns: table => new
                {
                    ServiceRequestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    ProfessionalId = table.Column<int>(type: "int", nullable: true),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ScheduledDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FinalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceRequests", x => x.ServiceRequestId);
                    table.ForeignKey(
                        name: "FK_ServiceRequests_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceRequests_Professionals_ProfessionalId",
                        column: x => x.ProfessionalId,
                        principalTable: "Professionals",
                        principalColumn: "ProfessionalId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ServiceRequests_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "ServiceId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Address", "Email", "Name", "Phone" },
                values: new object[,]
                {
                    { 1, "123 Main St, NY", "john.smith@example.com", "John Smith", "+1-555-1234" },
                    { 2, "45 Park Ave, LA", "emily.j@example.com", "Emily Johnson", "+1-555-5678" },
                    { 3, "78 Oak Rd, TX", "michael.b@example.com", "Michael Brown", "+1-555-9012" },
                    { 4, "12 Lakeview Dr, FL", "sarah.d@example.com", "Sarah Davis", "+1-555-3456" },
                    { 5, "90 Sunset Blvd, CA", "david.w@example.com", "David Wilson", "+1-555-7890" }
                });

            migrationBuilder.InsertData(
                table: "Professionals",
                columns: new[] { "ProfessionalId", "Email", "ExperienceYears", "HourlyRate", "IsAvailable", "JoinDate", "Name", "Phone", "Rating", "Specialty" },
                values: new object[,]
                {
                    { 1, "james.m@example.com", 8, 50m, true, new DateTime(2022, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "James Miller", "+1-555-1111", 4.5m, "Plumbing" },
                    { 2, "olivia.g@example.com", 10, 60m, true, new DateTime(2020, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Olivia Garcia", "+1-555-2222", 4.8m, "Electrical" },
                    { 3, "ethan.m@example.com", 6, 45m, false, new DateTime(2021, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ethan Martinez", "+1-555-3333", 4.3m, "Carpentry" },
                    { 4, "sophia.r@example.com", 7, 40m, true, new DateTime(2023, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sophia Rodriguez", "+1-555-4444", 4.9m, "Cleaning" },
                    { 5, "liam.l@example.com", 5, 55m, true, new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Liam Lee", "+1-555-5555", 4.2m, "Painting" }
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "ServiceId", "BasePrice", "CreatedDate", "Description", "IsActive", "Name" },
                values: new object[,]
                {
                    { 1, 80m, new DateTime(2024, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fix leaks, replace pipes, and other plumbing services.", true, "Plumbing Repair" },
                    { 2, 100m, new DateTime(2024, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Install, repair, and maintain electrical systems.", true, "Electrical Work" },
                    { 3, 120m, new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Custom furniture, repairs, and woodwork.", true, "Carpentry" },
                    { 4, 60m, new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Deep cleaning for houses and offices.", true, "House Cleaning" },
                    { 5, 150m, new DateTime(2025, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Interior and exterior painting services.", true, "Painting" }
                });

            migrationBuilder.InsertData(
                table: "ProfessionalServices",
                columns: new[] { "ProfessionalId", "ServiceId", "CertificationDate", "IsCertified" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 2, 2, new DateTime(2022, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 3, 3, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 4, 4, new DateTime(2023, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 5, 5, new DateTime(2024, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), true }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "ReviewId", "Comment", "CustomerId", "ProfessionalId", "Rating", "ReviewDate" },
                values: new object[,]
                {
                    { 1, "Excellent work!", 1, 1, 5, new DateTime(2025, 8, 4, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Good service, on time.", 2, 2, 4, new DateTime(2025, 8, 7, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "Average quality.", 3, 3, 3, new DateTime(2025, 8, 8, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "Highly recommend!", 4, 4, 5, new DateTime(2025, 8, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "Nice work, but a bit slow.", 5, 5, 4, new DateTime(2025, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "ServiceRequests",
                columns: new[] { "ServiceRequestId", "Address", "CompletedDate", "CustomerId", "Description", "FinalPrice", "Notes", "ProfessionalId", "RequestDate", "ScheduledDate", "ServiceId", "Status" },
                values: new object[,]
                {
                    { 1, "123 Main St, NY", new DateTime(2025, 8, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Fix kitchen sink leak", 90m, "Quick repair.", 1, new DateTime(2025, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Completed" },
                    { 2, "45 Park Ave, LA", null, 2, "Install new ceiling fan", null, null, 2, new DateTime(2025, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "InProgress" },
                    { 3, "78 Oak Rd, TX", null, 3, "Repair wardrobe door", null, "Needs urgent fix.", 3, new DateTime(2025, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 3, "Pending" },
                    { 4, "12 Lakeview Dr, FL", null, 4, "Weekly cleaning service", null, null, 4, new DateTime(2025, 8, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Assigned" },
                    { 5, "90 Sunset Blvd, CA", null, 5, "Paint living room", null, "Prefer light blue.", 5, new DateTime(2025, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 5, "Pending" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProfessionalServices_ServiceId",
                table: "ProfessionalServices",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CustomerId",
                table: "Reviews",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ProfessionalId",
                table: "Reviews",
                column: "ProfessionalId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequests_CustomerId",
                table: "ServiceRequests",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequests_ProfessionalId",
                table: "ServiceRequests",
                column: "ProfessionalId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequests_ServiceId",
                table: "ServiceRequests",
                column: "ServiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProfessionalServices");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "ServiceRequests");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Professionals");

            migrationBuilder.DropTable(
                name: "Services");
        }
    }
}
