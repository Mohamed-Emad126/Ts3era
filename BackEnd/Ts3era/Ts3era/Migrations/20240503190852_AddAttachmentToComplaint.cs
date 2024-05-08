using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ts3era.Migrations
{
    /// <inheritdoc />
    public partial class AddAttachmentToComplaint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Attachment",
                table: "Complaints",
                type: "varbinary(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attachment",
                table: "Complaints");
        }
    }
}
