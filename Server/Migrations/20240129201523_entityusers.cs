using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GzReservation.Server.Migrations
{
    /// <inheritdoc />
    public partial class entityusers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EntityId",
                table: "usersentity",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_usersentity_EntityId",
                table: "usersentity",
                column: "EntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_usersentity_entities_EntityId",
                table: "usersentity",
                column: "EntityId",
                principalTable: "entities",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_usersentity_entities_EntityId",
                table: "usersentity");

            migrationBuilder.DropIndex(
                name: "IX_usersentity_EntityId",
                table: "usersentity");

            migrationBuilder.DropColumn(
                name: "EntityId",
                table: "usersentity");
        }
    }
}
