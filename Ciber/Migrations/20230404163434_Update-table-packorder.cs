using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ciber.Migrations
{
    public partial class Updatetablepackorder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PackOrderId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PackOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackOrders", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PackOrderId",
                table: "Orders",
                column: "PackOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_PackOrders_PackOrderId",
                table: "Orders",
                column: "PackOrderId",
                principalTable: "PackOrders",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_PackOrders_PackOrderId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "PackOrders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_PackOrderId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PackOrderId",
                table: "Orders");
        }
    }
}
