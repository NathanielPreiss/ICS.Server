using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ICS.Muscle.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "muscle");

            migrationBuilder.CreateTable(
                name: "BodyArea",
                schema: "muscle",
                columns: table => new
                {
                    BodyAreaId = table.Column<int>(type: "int", nullable: false),
                    BodyAreaName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BodyArea", x => x.BodyAreaId);
                });

            migrationBuilder.CreateTable(
                name: "Joint",
                schema: "muscle",
                columns: table => new
                {
                    JointId = table.Column<int>(type: "int", nullable: false),
                    JointName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Joint", x => x.JointId);
                });

            migrationBuilder.CreateTable(
                name: "MuscleGroup",
                schema: "muscle",
                columns: table => new
                {
                    MuscleGroupId = table.Column<int>(type: "int", nullable: false),
                    MuscleGroupName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    BodyAreaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MuscleGroup", x => x.MuscleGroupId);
                    table.ForeignKey(
                        name: "FK_MuscleGroup_BodyArea_BodyAreaId",
                        column: x => x.BodyAreaId,
                        principalSchema: "muscle",
                        principalTable: "BodyArea",
                        principalColumn: "BodyAreaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JointMuscleGroupMap",
                schema: "muscle",
                columns: table => new
                {
                    JointId = table.Column<int>(type: "int", nullable: false),
                    MuscleGroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JointMuscleGroupMap", x => new { x.JointId, x.MuscleGroupId });
                    table.ForeignKey(
                        name: "FK_JointMuscleGroupMap_Joint_JointId",
                        column: x => x.JointId,
                        principalSchema: "muscle",
                        principalTable: "Joint",
                        principalColumn: "JointId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JointMuscleGroupMap_MuscleGroup_MuscleGroupId",
                        column: x => x.MuscleGroupId,
                        principalSchema: "muscle",
                        principalTable: "MuscleGroup",
                        principalColumn: "MuscleGroupId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Muscle",
                schema: "muscle",
                columns: table => new
                {
                    MuscleId = table.Column<int>(type: "int", nullable: false),
                    MuscleName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    MuscleGroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Muscle", x => x.MuscleId);
                    table.ForeignKey(
                        name: "FK_Muscle_MuscleGroup_MuscleGroupId",
                        column: x => x.MuscleGroupId,
                        principalSchema: "muscle",
                        principalTable: "MuscleGroup",
                        principalColumn: "MuscleGroupId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "muscle",
                table: "BodyArea",
                columns: new[] { "BodyAreaId", "BodyAreaName" },
                values: new object[,]
                {
                    { 100000, "Upper Body" },
                    { 200000, "Core" },
                    { 300000, "Lower Body" }
                });

            migrationBuilder.InsertData(
                schema: "muscle",
                table: "Joint",
                columns: new[] { "JointId", "JointName" },
                values: new object[,]
                {
                    { 1, "Neck" },
                    { 2, "Shoulder" },
                    { 3, "Elbow" },
                    { 4, "Wrist" },
                    { 5, "Upper Back" },
                    { 6, "Lower Back" },
                    { 7, "Hip" },
                    { 8, "Knee" },
                    { 9, "Ankle" }
                });

            migrationBuilder.InsertData(
                schema: "muscle",
                table: "MuscleGroup",
                columns: new[] { "MuscleGroupId", "BodyAreaId", "MuscleGroupName" },
                values: new object[,]
                {
                    { 110000, 100000, "Neck" },
                    { 120000, 100000, "Shoulder" },
                    { 130000, 100000, "Chest" },
                    { 140000, 100000, "Upper Back" },
                    { 150000, 100000, "Upper Arm" },
                    { 160000, 100000, "Forearm" },
                    { 210000, 200000, "Lower Back" },
                    { 220000, 200000, "Abdomen" },
                    { 310000, 300000, "Hip" },
                    { 320000, 300000, "Thigh" },
                    { 330000, 300000, "Calve" }
                });

            migrationBuilder.InsertData(
                schema: "muscle",
                table: "JointMuscleGroupMap",
                columns: new[] { "JointId", "MuscleGroupId" },
                values: new object[,]
                {
                    { 1, 110000 },
                    { 1, 120000 },
                    { 2, 110000 },
                    { 2, 120000 },
                    { 2, 130000 },
                    { 2, 140000 },
                    { 2, 150000 },
                    { 3, 150000 },
                    { 3, 160000 },
                    { 4, 160000 },
                    { 5, 110000 },
                    { 5, 120000 },
                    { 5, 140000 },
                    { 5, 210000 },
                    { 6, 210000 },
                    { 6, 220000 },
                    { 7, 310000 },
                    { 7, 320000 },
                    { 8, 320000 },
                    { 8, 330000 },
                    { 9, 330000 }
                });

            migrationBuilder.InsertData(
                schema: "muscle",
                table: "Muscle",
                columns: new[] { "MuscleId", "MuscleGroupId", "MuscleName" },
                values: new object[,]
                {
                    { 111000, 110000, "Sternocleidomastoid" },
                    { 112000, 110000, "Splenius" },
                    { 121000, 120000, "Anterior Deltoid" },
                    { 122000, 120000, "Lateral Deltoid" },
                    { 123000, 120000, "Posterior Deltoid" },
                    { 124000, 120000, "Supraspinatus" },
                    { 131000, 130000, "Pectoralis Major" },
                    { 132000, 130000, "Pectoralis Minor" },
                    { 133000, 130000, "Serratus Anterior" },
                    { 141000, 140000, "Latissimus Dorsi" },
                    { 142000, 140000, "Teres" },
                    { 143000, 140000, "Trapezius" },
                    { 145000, 140000, "Levator Scapulae" },
                    { 146000, 140000, "Rhomboids" },
                    { 147000, 140000, "Infraspinatus" },
                    { 148000, 140000, "Subscapularis" },
                    { 151000, 150000, "Triceps Brachii" },
                    { 152000, 150000, "Biceps Brachii" },
                    { 153000, 150000, "Brachialis" },
                    { 161000, 160000, "Brachioradialis" },
                    { 162000, 160000, "Wrist Flexors" }
                });

            migrationBuilder.InsertData(
                schema: "muscle",
                table: "Muscle",
                columns: new[] { "MuscleId", "MuscleGroupId", "MuscleName" },
                values: new object[,]
                {
                    { 163000, 160000, "Wrist Extendors" },
                    { 164000, 160000, "Wrist Pronator" },
                    { 165000, 160000, "Wrist Supinator" },
                    { 211000, 210000, "Quadratus Lumborum" },
                    { 212000, 210000, "Erector Spinae" },
                    { 221000, 220000, "Rectus Abdominis" },
                    { 222000, 220000, "Transverse Abdominis" },
                    { 223000, 220000, "Obliques" },
                    { 311000, 310000, "Gluteus Maximus" },
                    { 312000, 310000, "Abductors" },
                    { 313000, 310000, "Flexors" },
                    { 321000, 320000, "Quadriceps" },
                    { 322000, 320000, "Hamstrings" },
                    { 323000, 320000, "Adductors" },
                    { 331000, 330000, "Gastrocnemius" },
                    { 332000, 330000, "Soleus" },
                    { 333000, 330000, "Tibialis Anterior" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_JointMuscleGroupMap_MuscleGroupId",
                schema: "muscle",
                table: "JointMuscleGroupMap",
                column: "MuscleGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Muscle_MuscleGroupId",
                schema: "muscle",
                table: "Muscle",
                column: "MuscleGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_MuscleGroup_BodyAreaId",
                schema: "muscle",
                table: "MuscleGroup",
                column: "BodyAreaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JointMuscleGroupMap",
                schema: "muscle");

            migrationBuilder.DropTable(
                name: "Muscle",
                schema: "muscle");

            migrationBuilder.DropTable(
                name: "Joint",
                schema: "muscle");

            migrationBuilder.DropTable(
                name: "MuscleGroup",
                schema: "muscle");

            migrationBuilder.DropTable(
                name: "BodyArea",
                schema: "muscle");
        }
    }
}
