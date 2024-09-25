using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GzReservation.Server.Migrations
{
    /// <inheritdoc />
    public partial class dbmessage_fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "title",
                table: "messages",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "title",
                table: "messages");
        }
    }
}
