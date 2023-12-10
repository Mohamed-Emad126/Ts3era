using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ts3era.Migrations
{
    /// <inheritdoc />
    public partial class fff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "National_Id",
                table: "AspNetUsers",
                type: "nvarchar(14)",
                maxLength: 14,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 14);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "National_Id",
                table: "AspNetUsers",
                type: "int",
                maxLength: 14,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(14)",
                oldMaxLength: 14);
        }
    }
}
