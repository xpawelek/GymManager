using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymManager.Migrations
{
    /// <inheritdoc />
    public partial class IsActiveToDbFromMembership : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PersonalTrainingsPerWeek",
                table: "MembershipTypes",
                newName: "PersonalTrainingsPerMonth");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Memberships",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Memberships");

            migrationBuilder.RenameColumn(
                name: "PersonalTrainingsPerMonth",
                table: "MembershipTypes",
                newName: "PersonalTrainingsPerWeek");
        }
    }
}
