using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ciber.Migrations
{
    public partial class AddColumForTableUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "PackOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "PackOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CustomerPhone",
                table: "PackOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "PackOrders");

            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "PackOrders");

            migrationBuilder.DropColumn(
                name: "CustomerPhone",
                table: "PackOrders");
        }
    }
}
