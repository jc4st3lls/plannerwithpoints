using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

namespace DbMigrations.Migrations
{
    public partial class Geometry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PEnd",
                schema: "Agendes",
                table: "Diatari");

            migrationBuilder.DropColumn(
                name: "PStart",
                schema: "Agendes",
                table: "Diatari");

            migrationBuilder.AddColumn<Point>(
                name: "PuntFi",
                schema: "Agendes",
                table: "Diatari",
                type: "geometry",
                nullable: false);

            migrationBuilder.AddColumn<Point>(
                name: "PuntInici",
                schema: "Agendes",
                table: "Diatari",
                type: "geometry",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PuntFi",
                schema: "Agendes",
                table: "Diatari");

            migrationBuilder.DropColumn(
                name: "PuntInici",
                schema: "Agendes",
                table: "Diatari");

            migrationBuilder.AddColumn<Point>(
                name: "PEnd",
                schema: "Agendes",
                table: "Diatari",
                type: "geography",
                nullable: false);

            migrationBuilder.AddColumn<Point>(
                name: "PStart",
                schema: "Agendes",
                table: "Diatari",
                type: "geography",
                nullable: false);
        }
    }
}
