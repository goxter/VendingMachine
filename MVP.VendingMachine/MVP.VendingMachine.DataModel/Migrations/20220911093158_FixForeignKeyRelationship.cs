using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVP.VendingMachine.DataModel.Migrations
{
    public partial class FixForeignKeyRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_User_SellerId",
                table: "Product");

            migrationBuilder.AlterColumn<string>(
                name: "SellerId",
                table: "Product",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_User_SellerId",
                table: "Product",
                column: "SellerId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_User_SellerId",
                table: "Product");

            migrationBuilder.AlterColumn<string>(
                name: "SellerId",
                table: "Product",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_User_SellerId",
                table: "Product",
                column: "SellerId",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
