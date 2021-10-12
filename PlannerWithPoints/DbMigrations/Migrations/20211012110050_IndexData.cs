using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

namespace DbMigrations.Migrations
{
    public partial class IndexData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "End",
                schema: "Agendes",
                table: "Diatari");

            migrationBuilder.DropColumn(
                name: "Start",
                schema: "Agendes",
                table: "Diatari");

            migrationBuilder.AlterColumn<int>(
                name: "Minuts",
                schema: "Agendes",
                table: "Diatari",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<Point>(
                name: "PEnd",
                schema: "Agendes",
                table: "Diatari",
                nullable: false);

            migrationBuilder.AddColumn<Point>(
                name: "PStart",
                schema: "Agendes",
                table: "Diatari",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_Diatari_DataInici",
                schema: "Agendes",
                table: "Diatari",
                column: "DataInici");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Diatari_DataInici",
                schema: "Agendes",
                table: "Diatari");

            migrationBuilder.DropColumn(
                name: "PEnd",
                schema: "Agendes",
                table: "Diatari");

            migrationBuilder.DropColumn(
                name: "PStart",
                schema: "Agendes",
                table: "Diatari");

            migrationBuilder.AlterColumn<long>(
                name: "Minuts",
                schema: "Agendes",
                table: "Diatari",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<Point>(
                name: "End",
                schema: "Agendes",
                table: "Diatari",
                type: "geography",
                nullable: false);

            migrationBuilder.AddColumn<Point>(
                name: "Start",
                schema: "Agendes",
                table: "Diatari",
                type: "geography",
                nullable: false);
        }
    }
}
