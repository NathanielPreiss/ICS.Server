using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ICS.Exercise.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "exercise");

            migrationBuilder.CreateTable(
                name: "Exercise",
                schema: "exercise",
                columns: table => new
                {
                    ExerciseId = table.Column<int>(type: "int", nullable: false),
                    ExerciseName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercise", x => x.ExerciseId);
                });

            migrationBuilder.CreateTable(
                name: "Mechanic",
                schema: "exercise",
                columns: table => new
                {
                    MechanicId = table.Column<int>(type: "int", nullable: false),
                    MechanicName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mechanic", x => x.MechanicId);
                });

            migrationBuilder.CreateTable(
                name: "Muscle",
                schema: "exercise",
                columns: table => new
                {
                    MuscleId = table.Column<int>(type: "int", nullable: false),
                    MuscleName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Muscle", x => x.MuscleId);
                });

            migrationBuilder.CreateTable(
                name: "Utility",
                schema: "exercise",
                columns: table => new
                {
                    UtilityId = table.Column<int>(type: "int", nullable: false),
                    UtilityName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utility", x => x.UtilityId);
                });

            migrationBuilder.CreateTable(
                name: "Classification",
                schema: "exercise",
                columns: table => new
                {
                    ExerciseId = table.Column<int>(type: "int", nullable: false),
                    MechanicId = table.Column<int>(type: "int", nullable: false),
                    UtilityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classification", x => x.ExerciseId);
                    table.ForeignKey(
                        name: "FK_Classification_Exercise_ExerciseId",
                        column: x => x.ExerciseId,
                        principalSchema: "exercise",
                        principalTable: "Exercise",
                        principalColumn: "ExerciseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "exercise",
                table: "Exercise",
                columns: new[] { "ExerciseId", "ExerciseName" },
                values: new object[,]
                {
                    { 111001, "Neck Flexion" },
                    { 112001, "Neck Extension" },
                    { 113001, "Front Raise" },
                    { 114001, "Upright Row" },
                    { 115001, "Rear Row" },
                    { 116001, "Front Lateral Raise" },
                    { 131000, "Chest Press" }
                });

            migrationBuilder.InsertData(
                schema: "exercise",
                table: "Mechanic",
                columns: new[] { "MechanicId", "MechanicName" },
                values: new object[,]
                {
                    { 1, "Compound" },
                    { 2, "Isolated" }
                });

            migrationBuilder.InsertData(
                schema: "exercise",
                table: "Muscle",
                columns: new[] { "MuscleId", "MuscleName" },
                values: new object[,]
                {
                    { 111000, "Sternocleidomastoid" },
                    { 112000, "Splenius" },
                    { 121000, "Anterior Deltoid" },
                    { 122000, "Lateral Deltoid" },
                    { 123000, "Posterior Deltoid" },
                    { 124000, "Supraspinatus" },
                    { 131000, "Pectoralis Major" },
                    { 132000, "Pectoralis Minor" },
                    { 133000, "Serratus Anterior" },
                    { 141000, "Latissimus Dorsi" },
                    { 142000, "Teres" },
                    { 143000, "Trapezius" },
                    { 145000, "Levator Scapulae" },
                    { 146000, "Rhomboids" },
                    { 147000, "Infraspinatus" },
                    { 148000, "Subscapularis" },
                    { 151000, "Triceps Brachii" },
                    { 152000, "Biceps Brachii" },
                    { 153000, "Brachialis" },
                    { 161000, "Brachioradialis" },
                    { 162000, "Wrist Flexors" },
                    { 163000, "Wrist Extendors" },
                    { 164000, "Wrist Pronator" },
                    { 165000, "Wrist Supinator" },
                    { 211000, "Quadratus Lumborum" },
                    { 212000, "Erector Spinae" },
                    { 221000, "Rectus Abdominis" },
                    { 222000, "Transverse Abdominis" },
                    { 223000, "Obliques" },
                    { 311000, "Gluteus Maximus" },
                    { 312000, "Abductors" },
                    { 313000, "Flexors" },
                    { 321000, "Quadriceps" }
                });

            migrationBuilder.InsertData(
                schema: "exercise",
                table: "Muscle",
                columns: new[] { "MuscleId", "MuscleName" },
                values: new object[,]
                {
                    { 322000, "Hamstrings" },
                    { 323000, "Adductors" },
                    { 331000, "Gastrocnemius" },
                    { 332000, "Soleus" },
                    { 333000, "Tibialis Anterior" }
                });

            migrationBuilder.InsertData(
                schema: "exercise",
                table: "Utility",
                columns: new[] { "UtilityId", "UtilityName" },
                values: new object[,]
                {
                    { 1, "Basic" },
                    { 2, "Auxiliary" },
                    { 3, "Basic or Auxiliary" }
                });

            migrationBuilder.InsertData(
                schema: "exercise",
                table: "Classification",
                columns: new[] { "ExerciseId", "MechanicId", "UtilityId" },
                values: new object[,]
                {
                    { 111001, 2, 3 },
                    { 112001, 2, 3 },
                    { 113001, 2, 2 },
                    { 114001, 1, 1 },
                    { 115001, 1, 3 },
                    { 116001, 2, 2 },
                    { 131000, 1, 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Classification",
                schema: "exercise");

            migrationBuilder.DropTable(
                name: "Mechanic",
                schema: "exercise");

            migrationBuilder.DropTable(
                name: "Muscle",
                schema: "exercise");

            migrationBuilder.DropTable(
                name: "Utility",
                schema: "exercise");

            migrationBuilder.DropTable(
                name: "Exercise",
                schema: "exercise");
        }
    }
}
