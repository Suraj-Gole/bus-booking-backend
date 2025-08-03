using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCityEntityAndRouteCityRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndCity",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "StartCity",
                table: "Routes");

            migrationBuilder.AlterColumn<decimal>(
                name: "DistanceInKm",
                table: "Routes",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<Guid>(
                name: "FromCityId",
                table: "Routes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ToCityId",
                table: "Routes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Routes_FromCityId",
                table: "Routes",
                column: "FromCityId");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_ToCityId",
                table: "Routes",
                column: "ToCityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Cities_FromCityId",
                table: "Routes",
                column: "FromCityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Cities_ToCityId",
                table: "Routes",
                column: "ToCityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Cities_FromCityId",
                table: "Routes");

            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Cities_ToCityId",
                table: "Routes");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_Routes_FromCityId",
                table: "Routes");

            migrationBuilder.DropIndex(
                name: "IX_Routes_ToCityId",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "FromCityId",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "ToCityId",
                table: "Routes");

            migrationBuilder.AlterColumn<double>(
                name: "DistanceInKm",
                table: "Routes",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<string>(
                name: "EndCity",
                table: "Routes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StartCity",
                table: "Routes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
