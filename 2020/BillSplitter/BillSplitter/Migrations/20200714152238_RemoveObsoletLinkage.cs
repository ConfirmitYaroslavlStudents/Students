using Microsoft.EntityFrameworkCore.Migrations;

namespace BillSplitter.Migrations
{
    public partial class RemoveObsoletLinkage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Position_Customer_CustomerId",
                table: "Position");

            migrationBuilder.DropIndex(
                name: "IX_Position_CustomerId",
                table: "Position");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Position");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Position",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Position_CustomerId",
                table: "Position",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Position_Customer_CustomerId",
                table: "Position",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
