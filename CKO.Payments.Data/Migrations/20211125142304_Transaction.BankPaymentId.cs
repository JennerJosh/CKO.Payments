using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CKO.Payments.Data.Migrations
{
    public partial class TransactionBankPaymentId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BankPaymentId",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BankPaymentId",
                table: "Transactions");
        }
    }
}
