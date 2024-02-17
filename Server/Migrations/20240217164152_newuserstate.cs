using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GzReservation.Server.Migrations
{
    /// <inheritdoc />
    public partial class newuserstate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "new_user",
                table: "usersentity",
                type: "text",
                nullable: false,
                defaultValue: "yes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "new_user",
                table: "usersentity");
        }
    }
}
