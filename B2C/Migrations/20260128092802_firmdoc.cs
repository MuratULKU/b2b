using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace B2C.Migrations
{
    /// <inheritdoc />
    public partial class firmdoc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          

            migrationBuilder.AddColumn<int>(
                name: "Inforef",
                table: "FirmDocs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Lref",
                table: "FirmDocs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

           
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
          

            migrationBuilder.DropColumn(
                name: "Inforef",
                table: "FirmDocs");

            migrationBuilder.DropColumn(
                name: "Lref",
                table: "FirmDocs");

            
        }
    }
}
