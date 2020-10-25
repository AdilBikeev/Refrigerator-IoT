using Microsoft.EntityFrameworkCore.Migrations;

namespace RefrigeratorServerSide.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Places",
                columns: table => new
                {
                    placeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(nullable: false),
                    locationId = table.Column<int>(nullable: false),
                    pressure = table.Column<float>(nullable: false),
                    foodType = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Places", x => x.placeId);
                });

            migrationBuilder.CreateTable(
                name: "Refrigerator",
                columns: table => new
                {
                    RefrigeratorUUID = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Refrigerator", x => x.RefrigeratorUUID);
                });

            migrationBuilder.CreateTable(
                name: "RefrigeratorBlock",
                columns: table => new
                {
                    BlockUUID = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    RefrigeratorUUID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefrigeratorBlock", x => x.BlockUUID);
                    table.ForeignKey(
                        name: "FK_RefrigeratorBlock_Refrigerator_RefrigeratorUUID",
                        column: x => x.RefrigeratorUUID,
                        principalTable: "Refrigerator",
                        principalColumn: "RefrigeratorUUID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SensorData",
                columns: table => new
                {
                    SensorUUID = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    BlockUUID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SensorData", x => x.SensorUUID);
                    table.ForeignKey(
                        name: "FK_SensorData_RefrigeratorBlock_BlockUUID",
                        column: x => x.BlockUUID,
                        principalTable: "RefrigeratorBlock",
                        principalColumn: "BlockUUID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RefrigeratorBlock_RefrigeratorUUID",
                table: "RefrigeratorBlock",
                column: "RefrigeratorUUID");

            migrationBuilder.CreateIndex(
                name: "IX_SensorData_BlockUUID",
                table: "SensorData",
                column: "BlockUUID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Places");

            migrationBuilder.DropTable(
                name: "SensorData");

            migrationBuilder.DropTable(
                name: "RefrigeratorBlock");

            migrationBuilder.DropTable(
                name: "Refrigerator");
        }
    }
}
