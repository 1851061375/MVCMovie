using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcMovie.Migrations
{
    /// <inheritdoc />
    public partial class FixMenu2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuCustom_MenuCustom_MenuCustomId",
                table: "MenuCustom");

            migrationBuilder.DropIndex(
                name: "IX_MenuCustom_MenuCustomId",
                table: "MenuCustom");

            migrationBuilder.DropColumn(
                name: "MenuCustomId",
                table: "MenuCustom");

            migrationBuilder.CreateIndex(
                name: "IX_MenuCustom_ParentId",
                table: "MenuCustom",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuCustom_MenuCustom_ParentId",
                table: "MenuCustom",
                column: "ParentId",
                principalTable: "MenuCustom",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuCustom_MenuCustom_ParentId",
                table: "MenuCustom");

            migrationBuilder.DropIndex(
                name: "IX_MenuCustom_ParentId",
                table: "MenuCustom");

            migrationBuilder.AddColumn<int>(
                name: "MenuCustomId",
                table: "MenuCustom",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MenuCustom_MenuCustomId",
                table: "MenuCustom",
                column: "MenuCustomId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuCustom_MenuCustom_MenuCustomId",
                table: "MenuCustom",
                column: "MenuCustomId",
                principalTable: "MenuCustom",
                principalColumn: "Id");
        }
    }
}
