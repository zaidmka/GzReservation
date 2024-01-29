using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GzReservation.Server.Migrations
{
    /// <inheritdoc />
    public partial class bugfixaddpassport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "phone1",
                table: "forms",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<long>(
                name: "id_number",
                table: "forms",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "old_place",
                table: "forms",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "passport_no",
                table: "forms",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "old_place",
                table: "forms");

            migrationBuilder.DropColumn(
                name: "passport_no",
                table: "forms");

            migrationBuilder.AlterColumn<int>(
                name: "phone1",
                table: "forms",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "id_number",
                table: "forms",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
