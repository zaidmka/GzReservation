using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GzReservation.Server.Migrations
{
    /// <inheritdoc />
    public partial class init1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "entities",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    entity_name = table.Column<string>(type: "text", nullable: false),
                    entityName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_entities", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "forms",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    did = table.Column<int>(type: "integer", nullable: false),
                    doc_no = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    entity = table.Column<string>(type: "text", nullable: false),
                    father_work = table.Column<string>(type: "text", nullable: false),
                    mother_name = table.Column<string>(type: "text", nullable: false),
                    mother_work = table.Column<string>(type: "text", nullable: false),
                    wife_name = table.Column<string>(type: "text", nullable: false),
                    wife_work = table.Column<string>(type: "text", nullable: false),
                    bagde_color = table.Column<string>(type: "text", nullable: false),
                    actiondate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    username = table.Column<string>(type: "text", nullable: false),
                    info_book = table.Column<int>(type: "integer", nullable: false),
                    seq = table.Column<int>(type: "integer", nullable: false),
                    review_date = table.Column<DateOnly>(type: "date", nullable: false),
                    birthdate = table.Column<DateOnly>(type: "date", nullable: false),
                    state = table.Column<string>(type: "text", nullable: false),
                    nationalism = table.Column<string>(type: "text", nullable: false),
                    religion = table.Column<string>(type: "text", nullable: false),
                    place_govern = table.Column<string>(type: "text", nullable: false),
                    place_city = table.Column<string>(type: "text", nullable: false),
                    place_mhala = table.Column<string>(type: "text", nullable: false),
                    place_zuqaq = table.Column<string>(type: "text", nullable: false),
                    place_dar = table.Column<string>(type: "text", nullable: false),
                    place_point = table.Column<string>(type: "text", nullable: false),
                    phone1 = table.Column<int>(type: "integer", nullable: false),
                    phone2 = table.Column<int>(type: "integer", nullable: false),
                    work_place = table.Column<string>(type: "text", nullable: false),
                    work_place2 = table.Column<string>(type: "text", nullable: false),
                    study = table.Column<string>(type: "text", nullable: false),
                    grad_year = table.Column<DateOnly>(type: "date", nullable: false),
                    a1 = table.Column<string>(type: "text", nullable: false),
                    a2 = table.Column<string>(type: "text", nullable: false),
                    a3 = table.Column<string>(type: "text", nullable: false),
                    a4 = table.Column<string>(type: "text", nullable: false),
                    a5 = table.Column<string>(type: "text", nullable: false),
                    a6 = table.Column<string>(type: "text", nullable: false),
                    a7 = table.Column<string>(type: "text", nullable: false),
                    a8 = table.Column<string>(type: "text", nullable: false),
                    a9 = table.Column<string>(type: "text", nullable: false),
                    a10 = table.Column<string>(type: "text", nullable: false),
                    a11 = table.Column<string>(type: "text", nullable: false),
                    a12 = table.Column<string>(type: "text", nullable: false),
                    a13 = table.Column<string>(type: "text", nullable: false),
                    a14 = table.Column<string>(type: "text", nullable: false),
                    a15 = table.Column<string>(type: "text", nullable: false),
                    a16 = table.Column<string>(type: "text", nullable: false),
                    a17 = table.Column<string>(type: "text", nullable: false),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_forms", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "entities");

            migrationBuilder.DropTable(
                name: "forms");
        }
    }
}
