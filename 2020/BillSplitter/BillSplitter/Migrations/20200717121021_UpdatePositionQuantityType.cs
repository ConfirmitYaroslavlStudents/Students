using Microsoft.EntityFrameworkCore.Migrations;

namespace BillSplitter.Migrations
{
    public partial class UpdatePositionQuantityType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Quantity",
                table: "Position",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "Position",
                type: "int",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
