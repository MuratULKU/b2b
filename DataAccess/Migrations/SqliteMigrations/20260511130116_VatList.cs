using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations.SqliteMigrations
{
    /// <inheritdoc />
    public partial class VatList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.CreateTable(
                name: "VatList",
                columns: table => new
                {
                    VatNo = table.Column<byte>(type: "INTEGER", nullable: false),
                    VatPer = table.Column<double>(type: "REAL", nullable: false),
                    VatName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VatList", x => x.VatNo);
                });

           
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VatList");

           
        }
    }
}
