using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaintenanceServiceMVC.Migrations
{
    /// <inheritdoc />
    public partial class OneServicePerProf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProfessionalServices");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Services",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "HourlyRate",
                table: "Professionals",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "Professionals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Professionals_ServiceId",
                table: "Professionals",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Professionals_Services_ServiceId",
                table: "Professionals",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "ServiceId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Professionals_Services_ServiceId",
                table: "Professionals");

            migrationBuilder.DropIndex(
                name: "IX_Professionals_ServiceId",
                table: "Professionals");

            migrationBuilder.DropColumn(
                name: "HourlyRate",
                table: "Professionals");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "Professionals");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Services",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.CreateTable(
                name: "ProfessionalServices",
                columns: table => new
                {
                    ProfessionalId = table.Column<int>(type: "int", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HourlyRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_ProfessionalServices_ServiceId",
                table: "ProfessionalServices",
                column: "ServiceId");
        }
    }
}
