using Microsoft.EntityFrameworkCore.Migrations;

namespace ASPAssignment2.Data.Migrations
{
    public partial class AddAccountIdToAuthor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccountID",
                table: "Author",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountID",
                table: "Author");
        }
    }
}
