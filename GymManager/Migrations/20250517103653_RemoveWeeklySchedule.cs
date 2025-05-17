using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymManager.Migrations
{
    /// <inheritdoc />
    public partial class RemoveWeeklySchedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingSessions_WeeklySchedules_WeeklyScheduleId",
                table: "TrainingSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingSessions_WorkoutNotes_WorkoutNoteId",
                table: "TrainingSessions");

            migrationBuilder.DropTable(
                name: "WeeklySchedules");

            migrationBuilder.DropIndex(
                name: "IX_TrainingSessions_WeeklyScheduleId",
                table: "TrainingSessions");

            migrationBuilder.DropIndex(
                name: "IX_TrainingSessions_WorkoutNoteId",
                table: "TrainingSessions");

            migrationBuilder.DropColumn(
                name: "WeeklyScheduleId",
                table: "TrainingSessions");

            migrationBuilder.DropColumn(
                name: "WorkoutNoteId",
                table: "TrainingSessions");

            migrationBuilder.AddColumn<int>(
                name: "TrainingSessionId",
                table: "WorkoutNotes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "MembershipTypes",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutNotes_TrainingSessionId",
                table: "WorkoutNotes",
                column: "TrainingSessionId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutNotes_TrainingSessions_TrainingSessionId",
                table: "WorkoutNotes",
                column: "TrainingSessionId",
                principalTable: "TrainingSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutNotes_TrainingSessions_TrainingSessionId",
                table: "WorkoutNotes");

            migrationBuilder.DropIndex(
                name: "IX_WorkoutNotes_TrainingSessionId",
                table: "WorkoutNotes");

            migrationBuilder.DropColumn(
                name: "TrainingSessionId",
                table: "WorkoutNotes");

            migrationBuilder.AddColumn<int>(
                name: "WeeklyScheduleId",
                table: "TrainingSessions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WorkoutNoteId",
                table: "TrainingSessions",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "MembershipTypes",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2);

            migrationBuilder.CreateTable(
                name: "WeeklySchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeeklySchedules", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrainingSessions_WeeklyScheduleId",
                table: "TrainingSessions",
                column: "WeeklyScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingSessions_WorkoutNoteId",
                table: "TrainingSessions",
                column: "WorkoutNoteId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingSessions_WeeklySchedules_WeeklyScheduleId",
                table: "TrainingSessions",
                column: "WeeklyScheduleId",
                principalTable: "WeeklySchedules",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingSessions_WorkoutNotes_WorkoutNoteId",
                table: "TrainingSessions",
                column: "WorkoutNoteId",
                principalTable: "WorkoutNotes",
                principalColumn: "Id");
        }
    }
}
