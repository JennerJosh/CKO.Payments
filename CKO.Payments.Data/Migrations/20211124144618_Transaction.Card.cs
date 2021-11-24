using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CKO.Payments.Data.Migrations
{
    public partial class TransactionCard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Customers_CustomerId",
                table: "Cards");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Cards",
                newName: "TransactionId");

            migrationBuilder.RenameIndex(
                name: "IX_Cards_CustomerId",
                table: "Cards",
                newName: "IX_Cards_TransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Transactions_TransactionId",
                table: "Cards",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Transactions_TransactionId",
                table: "Cards");

            migrationBuilder.RenameColumn(
                name: "TransactionId",
                table: "Cards",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Cards_TransactionId",
                table: "Cards",
                newName: "IX_Cards_CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Customers_CustomerId",
                table: "Cards",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
