using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace B2B.Migrations
{
    /// <inheritdoc />
    public partial class init1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CODE = table.Column<string>(type: "TEXT", nullable: false),
                    NAME = table.Column<string>(type: "TEXT", nullable: false),
                    UNIT = table.Column<string>(type: "TEXT", nullable: false),
                    UNIT1 = table.Column<string>(type: "TEXT", nullable: true),
                    UNIT1RATE = table.Column<decimal>(type: "TEXT", nullable: true),
                    UNIT2 = table.Column<string>(type: "TEXT", nullable: true),
                    UNIT2RATE = table.Column<decimal>(type: "TEXT", nullable: true),
                    UNIT3 = table.Column<string>(type: "TEXT", nullable: true),
                    UNIT3RATE = table.Column<decimal>(type: "TEXT", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreateUser = table.Column<Guid>(type: "TEXT", nullable: false),
                    UpdateUser = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    RoleName = table.Column<string>(type: "TEXT", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreateUser = table.Column<Guid>(type: "TEXT", nullable: false),
                    UpdateUser = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreateUser = table.Column<Guid>(type: "TEXT", nullable: false),
                    UpdateUser = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    RoleId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CODE", "CreateDate", "CreateUser", "NAME", "UNIT", "UNIT1", "UNIT1RATE", "UNIT2", "UNIT2RATE", "UNIT3", "UNIT3RATE", "UpdateDate", "UpdateUser" },
                values: new object[,]
                {
                    { new Guid("01339499-2821-4943-89d1-9813d7945269"), "st36", new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7724), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0"), "1 nolu urun", "adet", null, null, null, null, null, null, new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7725), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0") },
                    { new Guid("101b7ea8-ca48-4372-9f38-aa7ba22c7ea8"), "st32", new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7702), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0"), "1 nolu urun", "adet", null, null, null, null, null, null, new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7704), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0") },
                    { new Guid("1861d5b1-2f47-498a-8a94-32904b5f7cef"), "st7", new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7522), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0"), "1 nolu urun", "adet", null, null, null, null, null, null, new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7524), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0") },
                    { new Guid("3ecec644-9adc-4b78-a4b9-e011f8106a1a"), "st1", new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7456), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0"), "1 nolu urun", "adet", null, null, null, null, null, null, new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7458), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0") },
                    { new Guid("41496638-3ca5-403a-adc4-dd5bf09203e6"), "st22", new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7630), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0"), "1 nolu urun", "adet", null, null, null, null, null, null, new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7632), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0") },
                    { new Guid("4164da50-e6cf-4638-b51b-45eea1802ec0"), "st33", new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7709), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0"), "1 nolu urun", "adet", null, null, null, null, null, null, new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7710), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0") },
                    { new Guid("4b328075-2f27-4035-a871-02b260a18024"), "st12", new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7566), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0"), "1 nolu urun", "adet", null, null, null, null, null, null, new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7568), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0") },
                    { new Guid("4bf39c76-1771-4be1-97be-a366559215ab"), "st31", new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7695), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0"), "1 nolu urun", "adet", null, null, null, null, null, null, new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7697), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0") },
                    { new Guid("5ea8289b-1056-4b06-8b4f-7568c63b384c"), "st15", new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7587), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0"), "1 nolu urun", "adet", null, null, null, null, null, null, new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7588), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0") },
                    { new Guid("622b34f3-86eb-4380-a0e0-190eee1bd337"), "st11", new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7552), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0"), "1 nolu urun", "adet", null, null, null, null, null, null, new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7554), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0") },
                    { new Guid("631239c4-3dc0-4464-9c01-243169a4400e"), "st6", new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7514), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0"), "1 nolu urun", "adet", null, null, null, null, null, null, new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7516), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0") },
                    { new Guid("6766539f-c507-449e-a112-7820180db4fb"), "st10", new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7545), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0"), "1 nolu urun", "adet", null, null, null, null, null, null, new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7547), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0") },
                    { new Guid("6c37b46f-1c2f-48b3-864b-325374c079c7"), "st24", new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7643), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0"), "1 nolu urun", "adet", null, null, null, null, null, null, new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7645), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0") },
                    { new Guid("74129ace-6c50-495b-93eb-72c4095a923c"), "st28", new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7675), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0"), "1 nolu urun", "adet", null, null, null, null, null, null, new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7676), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0") },
                    { new Guid("7479a2cf-f7f0-47af-9be2-8aad88f66cb0"), "st27", new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7663), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0"), "1 nolu urun", "adet", null, null, null, null, null, null, new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7665), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0") },
                    { new Guid("776b080b-d9c2-4715-ab94-0320c9325af5"), "st18", new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7599), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0"), "1 nolu urun", "adet", null, null, null, null, null, null, new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7601), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0") },
                    { new Guid("7c209914-590e-4e06-89c0-4e48d5d8ebae"), "st19", new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7606), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0"), "1 nolu urun", "adet", null, null, null, null, null, null, new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7607), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0") },
                    { new Guid("8b9b962a-f91a-454a-9759-29181f355441"), "st3", new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7474), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0"), "1 nolu urun", "adet", null, null, null, null, null, null, new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7476), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0") },
                    { new Guid("9127af94-11d7-46a7-8f33-776b21b7c760"), "st16", new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7591), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0"), "1 nolu urun", "adet", null, null, null, null, null, null, new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7592), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0") },
                    { new Guid("9fd74662-54bb-4f45-8a5e-081b89cfdeba"), "st14", new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7581), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0"), "1 nolu urun", "adet", null, null, null, null, null, null, new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7582), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0") },
                    { new Guid("a251f52e-fad0-433a-9189-0743e640ffa7"), "st35", new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7717), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0"), "1 nolu urun", "adet", null, null, null, null, null, null, new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7718), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0") },
                    { new Guid("ab7b129e-8b4e-4ef4-816a-548552ea2d89"), "st13", new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7574), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0"), "1 nolu urun", "adet", null, null, null, null, null, null, new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7576), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0") },
                    { new Guid("af23ebc6-1982-4607-9b0a-ced0561b87dc"), "st23", new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7636), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0"), "1 nolu urun", "adet", null, null, null, null, null, null, new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7638), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0") },
                    { new Guid("c026830d-ba47-40ae-8aef-7e3773040818"), "st4", new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7498), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0"), "1 nolu urun", "adet", null, null, null, null, null, null, new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7500), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0") },
                    { new Guid("c904e213-59b4-42f5-bdaf-ea262a6b43ad"), "st34", new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7715), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0"), "1 nolu urun", "adet", null, null, null, null, null, null, new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7715), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0") },
                    { new Guid("c9269bf1-0efa-41c2-ae95-1eb60edcb93c"), "st20", new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7616), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0"), "1 nolu urun", "adet", null, null, null, null, null, null, new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7618), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0") },
                    { new Guid("c991f8a1-9f68-4a0a-92b2-eb26d4b3ca16"), "st26", new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7656), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0"), "1 nolu urun", "adet", null, null, null, null, null, null, new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7658), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0") },
                    { new Guid("cbc0bfae-5338-4ba2-8cf0-72326e53738d"), "st17", new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7594), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0"), "1 nolu urun", "adet", null, null, null, null, null, null, new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7595), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0") },
                    { new Guid("cdcd6401-22eb-4939-9559-6b9b6c2699d0"), "st29", new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7682), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0"), "1 nolu urun", "adet", null, null, null, null, null, null, new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7683), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0") },
                    { new Guid("dc4e0bfa-30f7-42ff-be09-1af025078c64"), "st8", new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7530), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0"), "1 nolu urun", "adet", null, null, null, null, null, null, new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7532), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0") },
                    { new Guid("e3d755b7-0c53-41e2-a132-7b78b4cfd877"), "st30", new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7688), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0"), "1 nolu urun", "adet", null, null, null, null, null, null, new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7690), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0") },
                    { new Guid("e9f6655c-a514-45fc-993c-a4f8664abb53"), "st9", new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7538), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0"), "1 nolu urun", "adet", null, null, null, null, null, null, new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7540), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0") },
                    { new Guid("eb12a87f-abe7-4d96-b155-60fa17447015"), "st25", new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7649), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0"), "1 nolu urun", "adet", null, null, null, null, null, null, new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7651), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0") },
                    { new Guid("eecbd0a4-8864-4cac-9a43-9fb9327bb48b"), "st2", new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7466), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0"), "2 nolu urun", "adet", null, null, null, null, null, null, new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7468), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0") },
                    { new Guid("f4672cf5-707a-45db-8cf5-18eea38a4275"), "st21", new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7623), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0"), "1 nolu urun", "adet", null, null, null, null, null, null, new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7625), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0") },
                    { new Guid("f6a9ba55-07d4-44e5-913f-3981e5cad11f"), "st5", new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7506), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0"), "1 nolu urun", "adet", null, null, null, null, null, null, new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7508), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0") }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "CreateUser", "RoleName", "UpdateDate", "UpdateUser" },
                values: new object[,]
                {
                    { new Guid("01df900c-1de8-43a6-b270-a72c1bebf1da"), new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7176), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0"), "User", new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7178), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0") },
                    { new Guid("0d83f64b-52eb-401a-baa7-eabc59bbabeb"), new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7128), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0"), "Admin", new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7142), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0") }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreateDate", "CreateUser", "Email", "Password", "UpdateDate", "UpdateUser", "Username" },
                values: new object[] { new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0"), new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7246), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0"), "murat@ulkubilgisayar.com", "Admin", new DateTime(2023, 9, 13, 11, 18, 47, 733, DateTimeKind.Local).AddTicks(7249), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0"), "Admin" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("0d83f64b-52eb-401a-baa7-eabc59bbabeb"), new Guid("29bcfcd7-b5e7-4b57-9a71-2d64ea6998a0") });

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
