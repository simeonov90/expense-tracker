using Microsoft.EntityFrameworkCore.Migrations;

namespace ExpenseTracker.Migrations
{
    public partial class ChangePropertyName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IncomeFrom",
                table: "Incomes");

            migrationBuilder.DropColumn(
                name: "ExpenseFrom",
                table: "Expenses");

            migrationBuilder.AddColumn<string>(
                name: "From",
                table: "Incomes",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "From",
                table: "Expenses",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "From",
                table: "Incomes");

            migrationBuilder.DropColumn(
                name: "From",
                table: "Expenses");

            migrationBuilder.AddColumn<string>(
                name: "IncomeFrom",
                table: "Incomes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ExpenseFrom",
                table: "Expenses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
