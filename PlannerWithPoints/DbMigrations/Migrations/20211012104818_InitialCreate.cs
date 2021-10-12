using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

namespace DbMigrations.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Agendes");

            migrationBuilder.CreateTable(
                name: "Diatari",
                schema: "Agendes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Agenda = table.Column<string>(maxLength: 10, nullable: false),
                    DataInici = table.Column<DateTime>(type: "date", nullable: false),
                    HoraInici = table.Column<TimeSpan>(type: "time", nullable: false),
                    Minuts = table.Column<long>(nullable: false),
                    Start = table.Column<Point>(nullable: false),
                    End = table.Column<Point>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diatari", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Diatari",
                schema: "Agendes");
        }
    }
}
