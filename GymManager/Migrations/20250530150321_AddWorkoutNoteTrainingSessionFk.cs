using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymManager.Migrations
{
    /// <inheritdoc />
    public partial class AddWorkoutNoteTrainingSessionFk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutNotes_TrainingSessions_TrainingSessionId",
                table: "WorkoutNotes",
                column: "TrainingSessionId",
                principalTable: "TrainingSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutNotes_TrainingSessions_TrainingSessionId",
                table: "WorkoutNotes");
        }
    }
}
