using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaintenanceServiceMVC.Migrations
{
    /// <inheritdoc />
    public partial class EditProfessional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExperienceYears",
                table: "Professionals");

            migrationBuilder.DropColumn(
                name: "HourlyRate",
                table: "Professionals");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExperienceYears",
                table: "Professionals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "HourlyRate",
                table: "Professionals",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
