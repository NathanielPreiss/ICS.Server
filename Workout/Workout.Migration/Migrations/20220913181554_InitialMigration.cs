using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ICS.Workout.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Workout");

            migrationBuilder.CreateTable(
                name: "User",
                schema: "Workout",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Workout",
                schema: "Workout",
                columns: table => new
                {
                    WorkoutId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workout", x => x.WorkoutId)
                        .Annotation("SqlServer:Clustered", false);
                    table.UniqueConstraint("AK_Workout_WorkoutId_Position", x => new { x.WorkoutId, x.Position });
                    table.ForeignKey(
                        name: "FK_Workout_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Workout",
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Routine",
                schema: "Workout",
                columns: table => new
                {
                    RoutineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkoutId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    ExerciseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routine", x => x.RoutineId)
                        .Annotation("SqlServer:Clustered", false);
                    table.UniqueConstraint("AK_Routine_RoutineId_Position", x => new { x.RoutineId, x.Position });
                    table.ForeignKey(
                        name: "FK_Routine_Workout_WorkoutId",
                        column: x => x.WorkoutId,
                        principalSchema: "Workout",
                        principalTable: "Workout",
                        principalColumn: "WorkoutId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Set",
                schema: "Workout",
                columns: table => new
                {
                    SetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoutineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    Reps = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Set", x => x.SetId)
                        .Annotation("SqlServer:Clustered", false);
                    table.UniqueConstraint("AK_Set_SetId_Position", x => new { x.SetId, x.Position });
                    table.ForeignKey(
                        name: "FK_Set_Routine_RoutineId",
                        column: x => x.RoutineId,
                        principalSchema: "Workout",
                        principalTable: "Routine",
                        principalColumn: "RoutineId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Routine_WorkoutId_Position",
                schema: "Workout",
                table: "Routine",
                columns: new[] { "WorkoutId", "Position" })
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.CreateIndex(
                name: "IX_Set_RoutineId_Position",
                schema: "Workout",
                table: "Set",
                columns: new[] { "RoutineId", "Position" })
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.CreateIndex(
                name: "IX_Workout_UserId_Position",
                schema: "Workout",
                table: "Workout",
                columns: new[] { "UserId", "Position" })
                .Annotation("SqlServer:Clustered", true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Set",
                schema: "Workout");

            migrationBuilder.DropTable(
                name: "Routine",
                schema: "Workout");

            migrationBuilder.DropTable(
                name: "Workout",
                schema: "Workout");

            migrationBuilder.DropTable(
                name: "User",
                schema: "Workout");
        }
    }
}
