using Microsoft.EntityFrameworkCore.Migrations;

namespace ExpenseTracker.Migrations
{
    public partial class AddTypeColumnToTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Incomes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Expenses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Incomes");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Expenses");
        }
    }
}
