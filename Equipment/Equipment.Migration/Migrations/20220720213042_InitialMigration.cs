using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ICS.Equipment.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "equipment");

            migrationBuilder.CreateTable(
                name: "EquipmentGroup",
                schema: "equipment",
                columns: table => new
                {
                    EquipmentGroupId = table.Column<int>(type: "int", nullable: false),
                    EquipmentGroupName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentGroup", x => x.EquipmentGroupId);
                });

            migrationBuilder.InsertData(
                schema: "equipment",
                table: "EquipmentGroup",
                columns: new[] { "EquipmentGroupId", "EquipmentGroupName" },
                values: new object[,]
                {
                    { 1, "Barbell" },
                    { 2, "Cable" },
                    { 3, "Dumbbell" },
                    { 4, "Lever" },
                    { 5, "Sled" },
                    { 6, "Smith" },
                    { 7, "Band" },
                    { 8, "Suspension" },
                    { 9, "Calisthenics" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EquipmentGroup",
                schema: "equipment");
        }
    }
}
