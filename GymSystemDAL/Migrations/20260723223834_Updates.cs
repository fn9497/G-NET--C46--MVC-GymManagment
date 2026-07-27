using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymSystemDAL.Migrations
{
    /// <inheritdoc />
    public partial class Updates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_HealthRecords_memberId",
                table: "HealthRecords");

            migrationBuilder.CreateIndex(
                name: "IX_HealthRecords_memberId",
                table: "HealthRecords",
                column: "memberId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_HealthRecords_memberId",
                table: "HealthRecords");

            migrationBuilder.CreateIndex(
                name: "IX_HealthRecords_memberId",
                table: "HealthRecords",
                column: "memberId");
        }
    }
}
