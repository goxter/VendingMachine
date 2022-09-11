using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVP.VendingMachine.DataModel.Migrations
{
    public partial class AddRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "44eda852-f157-4826-86bf-1f44e447d654", "08252f36-1387-4027-b31c-01b1c8ada3ae", "Seller", "SELLER" },
                    { "866765b1-91d9-47ae-96b2-85dacd72bad2", "99de036f-6a3e-446e-83a3-7cd6440e32d8", "Buyer", "BUYER" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "44eda852-f157-4826-86bf-1f44e447d654");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "866765b1-91d9-47ae-96b2-85dacd72bad2");
        }
    }
}
