using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCMOVIES.Migrations
{
    public partial class _5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Date",
                table: "Operations",
                nullable: true,
                oldClrType: typeof(DateTime));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Operations",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
