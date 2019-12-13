using Microsoft.EntityFrameworkCore.Migrations;

namespace aoc2019.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SpaceState",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PositionX1 = table.Column<int>(nullable: false),
                    PositionY1 = table.Column<int>(nullable: false),
                    PositionZ1 = table.Column<int>(nullable: false),
                    PositionX2 = table.Column<int>(nullable: false),
                    PositionY2 = table.Column<int>(nullable: false),
                    PositionZ2 = table.Column<int>(nullable: false),
                    PositionX3 = table.Column<int>(nullable: false),
                    PositionY3 = table.Column<int>(nullable: false),
                    PositionZ3 = table.Column<int>(nullable: false),
                    PositionX4 = table.Column<int>(nullable: false),
                    PositionY4 = table.Column<int>(nullable: false),
                    PositionZ4 = table.Column<int>(nullable: false),
                    VelocityX1 = table.Column<int>(nullable: false),
                    VelocityY1 = table.Column<int>(nullable: false),
                    VelocityZ1 = table.Column<int>(nullable: false),
                    VelocityX2 = table.Column<int>(nullable: false),
                    VelocityY2 = table.Column<int>(nullable: false),
                    VelocityZ2 = table.Column<int>(nullable: false),
                    VelocityX3 = table.Column<int>(nullable: false),
                    VelocityY3 = table.Column<int>(nullable: false),
                    VelocityZ3 = table.Column<int>(nullable: false),
                    VelocityX4 = table.Column<int>(nullable: false),
                    VelocityY4 = table.Column<int>(nullable: false),
                    VelocityZ4 = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpaceState", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SpaceState_PositionX1_PositionX2_PositionX3_PositionX4_PositionY1_PositionY2_PositionY3_PositionY4_PositionZ1_PositionZ2_PositionZ3_PositionZ4_VelocityX1_VelocityX2_VelocityX3_VelocityX4_VelocityY1_VelocityY2_VelocityY3_VelocityY4_VelocityZ1_VelocityZ2_VelocityZ3_VelocityZ4",
                table: "SpaceState",
                columns: new[] { "PositionX1", "PositionX2", "PositionX3", "PositionX4", "PositionY1", "PositionY2", "PositionY3", "PositionY4", "PositionZ1", "PositionZ2", "PositionZ3", "PositionZ4", "VelocityX1", "VelocityX2", "VelocityX3", "VelocityX4", "VelocityY1", "VelocityY2", "VelocityY3", "VelocityY4", "VelocityZ1", "VelocityZ2", "VelocityZ3", "VelocityZ4" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpaceState");
        }
    }
}
