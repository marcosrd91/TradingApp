using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCMOVIES.Migrations
{
    public partial class _4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Operations_User_UserId",
                table: "Operations");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "Operations",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Operations_User_UserId",
                table: "Operations",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Operations_User_UserId",
                table: "Operations");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "Operations",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_Operations_User_UserId",
                table: "Operations",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
