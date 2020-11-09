using Microsoft.EntityFrameworkCore.Migrations;

namespace ASPAssignment2.Data.Migrations
{
    public partial class RenameCreatorIdToAuthorId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Article_Author",
                table: "Article");

            migrationBuilder.DropIndex(
                name: "IX_Article_CreatorId",
                table: "Article");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Article");

            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "Article",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Article_AuthorId",
                table: "Article",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Article_Author",
                table: "Article",
                column: "AuthorId",
                principalTable: "Author",
                principalColumn: "AuthorId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Article_Author",
                table: "Article");

            migrationBuilder.DropIndex(
                name: "IX_Article_AuthorId",
                table: "Article");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Article");

            migrationBuilder.AddColumn<int>(
                name: "CreatorId",
                table: "Article",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Article_CreatorId",
                table: "Article",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Article_Author",
                table: "Article",
                column: "CreatorId",
                principalTable: "Author",
                principalColumn: "AuthorId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
