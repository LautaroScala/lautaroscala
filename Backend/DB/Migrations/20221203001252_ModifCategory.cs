using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DB.Migrations
{
    /// <inheritdoc />
    public partial class ModifCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Things_Categories_CategoryId",
                table: "Things");

            migrationBuilder.DropIndex(
                name: "IX_Things_CategoryId",
                table: "Things");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Things_CategoryId",
                table: "Things",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Things_Categories_CategoryId",
                table: "Things",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
