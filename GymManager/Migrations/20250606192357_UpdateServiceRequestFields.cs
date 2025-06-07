using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymManager.Migrations
{
    /// <inheritdoc />
    public partial class UpdateServiceRequestFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "ServiceRequests");

            migrationBuilder.AddColumn<bool>(
                name: "IsResolved",
                table: "ServiceRequests",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsResolved",
                table: "ServiceRequests");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "ServiceRequests",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
