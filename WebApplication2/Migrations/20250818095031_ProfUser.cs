using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MaintenanceServiceMVC.Migrations
{
    /// <inheritdoc />
    public partial class ProfUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_AspNetUsers_UserId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_UserId",
                table: "Customers");

            migrationBuilder.DeleteData(
                table: "ProfessionalServices",
                keyColumns: new[] { "ProfessionalId", "ServiceId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "ProfessionalServices",
                keyColumns: new[] { "ProfessionalId", "ServiceId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "ProfessionalServices",
                keyColumns: new[] { "ProfessionalId", "ServiceId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "ProfessionalServices",
                keyColumns: new[] { "ProfessionalId", "ServiceId" },
                keyValues: new object[] { 4, 4 });

            migrationBuilder.DeleteData(
                table: "ProfessionalServices",
                keyColumns: new[] { "ProfessionalId", "ServiceId" },
                keyValues: new object[] { 5, 5 });

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ServiceRequests",
                keyColumn: "ServiceRequestId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ServiceRequests",
                keyColumn: "ServiceRequestId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ServiceRequests",
                keyColumn: "ServiceRequestId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ServiceRequests",
                keyColumn: "ServiceRequestId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ServiceRequests",
                keyColumn: "ServiceRequestId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Professionals",
                keyColumn: "ProfessionalId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Professionals",
                keyColumn: "ProfessionalId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Professionals",
                keyColumn: "ProfessionalId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Professionals",
                keyColumn: "ProfessionalId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Professionals",
                keyColumn: "ProfessionalId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "ServiceId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "ServiceId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "ServiceId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "ServiceId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "ServiceId",
                keyValue: 5);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Professionals",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Customers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Professionals_UserId",
                table: "Professionals",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_UserId",
                table: "Customers",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_AspNetUsers_UserId",
                table: "Customers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Professionals_AspNetUsers_UserId",
                table: "Professionals",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_AspNetUsers_UserId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Professionals_AspNetUsers_UserId",
                table: "Professionals");

            migrationBuilder.DropIndex(
                name: "IX_Professionals_UserId",
                table: "Professionals");

            migrationBuilder.DropIndex(
                name: "IX_Customers_UserId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Professionals");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Customers",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Address", "Email", "Name", "Phone", "UserId" },
                values: new object[,]
                {
                    { 1, "123 Main St, NY", "john.smith@example.com", "John Smith", "+1-555-1234", null },
                    { 2, "45 Park Ave, LA", "emily.j@example.com", "Emily Johnson", "+1-555-5678", null },
                    { 3, "78 Oak Rd, TX", "michael.b@example.com", "Michael Brown", "+1-555-9012", null },
                    { 4, "12 Lakeview Dr, FL", "sarah.d@example.com", "Sarah Davis", "+1-555-3456", null },
                    { 5, "90 Sunset Blvd, CA", "david.w@example.com", "David Wilson", "+1-555-7890", null }
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
                name: "IX_Customers_UserId",
                table: "Customers",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_AspNetUsers_UserId",
                table: "Customers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
