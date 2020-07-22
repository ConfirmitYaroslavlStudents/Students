using Microsoft.EntityFrameworkCore.Migrations;

namespace BillSplitter.Migrations
{
    public partial class AddQuantity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Position",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Quantity",
                table: "Orders",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Position");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Orders");
        }
    }
}
