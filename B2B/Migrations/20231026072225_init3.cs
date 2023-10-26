using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace B2B.Migrations
{
    /// <inheritdoc />
    public partial class init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankParameters_BankCards_BankId",
                table: "BankParameters");

            migrationBuilder.DropIndex(
                name: "IX_BankParameters_BankId",
                table: "BankParameters");

            migrationBuilder.DeleteData(
                table: "FirmParams",
                keyColumn: "Id",
                keyValue: new Guid("b3cceac2-8743-45be-a9af-2fea0151101f"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("6093673e-7b0d-4e6a-802e-8e3b99e54f09"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("b47d4a53-b042-47ff-9af8-82046f5950e0"), new Guid("085d5805-7b6d-40cc-9575-1e677580fd4b") });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("b47d4a53-b042-47ff-9af8-82046f5950e0"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("085d5805-7b6d-40cc-9575-1e677580fd4b"));

            migrationBuilder.DropColumn(
                name: "BankId",
                table: "BankParameters");

            migrationBuilder.AlterColumn<Guid>(
                name: "BankCardId",
                table: "BankParameters",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.InsertData(
                table: "FirmParams",
                columns: new[] { "Id", "Address1", "Address2", "City", "Country", "CreateDate", "CreateUser", "FirmId", "FirmName", "LastSync", "MailAddress", "Phone1", "Phone2", "SyncMinute", "Town", "UpdateDate", "UpdateUser" },
                values: new object[] { new Guid("9f16798e-503e-4e1f-a30b-d5c3157758f7"), null, null, null, null, new DateTime(2023, 10, 26, 10, 22, 25, 372, DateTimeKind.Local).AddTicks(470), new Guid("1b570d99-052a-43b4-bbb9-bdac1037de08"), 1, null, null, null, null, null, 60, null, new DateTime(2023, 10, 26, 10, 22, 25, 372, DateTimeKind.Local).AddTicks(471), new Guid("1b570d99-052a-43b4-bbb9-bdac1037de08") });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "CreateUser", "RoleName", "UpdateDate", "UpdateUser" },
                values: new object[,]
                {
                    { new Guid("5c1ee23c-bc95-4df9-9216-267f68ab1a7a"), new DateTime(2023, 10, 26, 10, 22, 25, 372, DateTimeKind.Local).AddTicks(392), new Guid("1b570d99-052a-43b4-bbb9-bdac1037de08"), "User", new DateTime(2023, 10, 26, 10, 22, 25, 372, DateTimeKind.Local).AddTicks(392), new Guid("1b570d99-052a-43b4-bbb9-bdac1037de08") },
                    { new Guid("94fc3abd-f2c1-4f93-99f0-5fb423ebcd70"), new DateTime(2023, 10, 26, 10, 22, 25, 372, DateTimeKind.Local).AddTicks(353), new Guid("1b570d99-052a-43b4-bbb9-bdac1037de08"), "Admin", new DateTime(2023, 10, 26, 10, 22, 25, 372, DateTimeKind.Local).AddTicks(366), new Guid("1b570d99-052a-43b4-bbb9-bdac1037de08") }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreateDate", "CreateUser", "Email", "Password", "UpdateDate", "UpdateUser", "Username" },
                values: new object[] { new Guid("1b570d99-052a-43b4-bbb9-bdac1037de08"), new DateTime(2023, 10, 26, 10, 22, 25, 372, DateTimeKind.Local).AddTicks(434), new Guid("1b570d99-052a-43b4-bbb9-bdac1037de08"), "murat@ulkubilgisayar.com", "Admin", new DateTime(2023, 10, 26, 10, 22, 25, 372, DateTimeKind.Local).AddTicks(435), new Guid("1b570d99-052a-43b4-bbb9-bdac1037de08"), "Admin" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId", "CreateDate", "CreateUser", "Id", "UpdateDate", "UpdateUser" },
                values: new object[] { new Guid("94fc3abd-f2c1-4f93-99f0-5fb423ebcd70"), new Guid("1b570d99-052a-43b4-bbb9-bdac1037de08"), new DateTime(2023, 10, 26, 10, 22, 25, 372, DateTimeKind.Local).AddTicks(450), new Guid("1b570d99-052a-43b4-bbb9-bdac1037de08"), new Guid("41667a37-db9e-48f4-b37a-a7ff3c9ea74b"), new DateTime(2023, 10, 26, 10, 22, 25, 372, DateTimeKind.Local).AddTicks(450), new Guid("1b570d99-052a-43b4-bbb9-bdac1037de08") });

            migrationBuilder.CreateIndex(
                name: "IX_BankParameters_BankCardId",
                table: "BankParameters",
                column: "BankCardId");

            migrationBuilder.AddForeignKey(
                name: "FK_BankParameters_BankCards_BankCardId",
                table: "BankParameters",
                column: "BankCardId",
                principalTable: "BankCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankParameters_BankCards_BankCardId",
                table: "BankParameters");

            migrationBuilder.DropIndex(
                name: "IX_BankParameters_BankCardId",
                table: "BankParameters");

            migrationBuilder.DeleteData(
                table: "FirmParams",
                keyColumn: "Id",
                keyValue: new Guid("9f16798e-503e-4e1f-a30b-d5c3157758f7"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("5c1ee23c-bc95-4df9-9216-267f68ab1a7a"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("94fc3abd-f2c1-4f93-99f0-5fb423ebcd70"), new Guid("1b570d99-052a-43b4-bbb9-bdac1037de08") });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("94fc3abd-f2c1-4f93-99f0-5fb423ebcd70"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("1b570d99-052a-43b4-bbb9-bdac1037de08"));

            migrationBuilder.AlterColumn<int>(
                name: "BankCardId",
                table: "BankParameters",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddColumn<Guid>(
                name: "BankId",
                table: "BankParameters",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "FirmParams",
                columns: new[] { "Id", "Address1", "Address2", "City", "Country", "CreateDate", "CreateUser", "FirmId", "FirmName", "LastSync", "MailAddress", "Phone1", "Phone2", "SyncMinute", "Town", "UpdateDate", "UpdateUser" },
                values: new object[] { new Guid("b3cceac2-8743-45be-a9af-2fea0151101f"), null, null, null, null, new DateTime(2023, 10, 16, 19, 31, 22, 83, DateTimeKind.Local).AddTicks(6709), new Guid("085d5805-7b6d-40cc-9575-1e677580fd4b"), 1, null, null, null, null, null, 60, null, new DateTime(2023, 10, 16, 19, 31, 22, 83, DateTimeKind.Local).AddTicks(6709), new Guid("085d5805-7b6d-40cc-9575-1e677580fd4b") });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "CreateUser", "RoleName", "UpdateDate", "UpdateUser" },
                values: new object[,]
                {
                    { new Guid("6093673e-7b0d-4e6a-802e-8e3b99e54f09"), new DateTime(2023, 10, 16, 19, 31, 22, 83, DateTimeKind.Local).AddTicks(6645), new Guid("085d5805-7b6d-40cc-9575-1e677580fd4b"), "User", new DateTime(2023, 10, 16, 19, 31, 22, 83, DateTimeKind.Local).AddTicks(6645), new Guid("085d5805-7b6d-40cc-9575-1e677580fd4b") },
                    { new Guid("b47d4a53-b042-47ff-9af8-82046f5950e0"), new DateTime(2023, 10, 16, 19, 31, 22, 83, DateTimeKind.Local).AddTicks(6617), new Guid("085d5805-7b6d-40cc-9575-1e677580fd4b"), "Admin", new DateTime(2023, 10, 16, 19, 31, 22, 83, DateTimeKind.Local).AddTicks(6628), new Guid("085d5805-7b6d-40cc-9575-1e677580fd4b") }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreateDate", "CreateUser", "Email", "Password", "UpdateDate", "UpdateUser", "Username" },
                values: new object[] { new Guid("085d5805-7b6d-40cc-9575-1e677580fd4b"), new DateTime(2023, 10, 16, 19, 31, 22, 83, DateTimeKind.Local).AddTicks(6675), new Guid("085d5805-7b6d-40cc-9575-1e677580fd4b"), "murat@ulkubilgisayar.com", "Admin", new DateTime(2023, 10, 16, 19, 31, 22, 83, DateTimeKind.Local).AddTicks(6676), new Guid("085d5805-7b6d-40cc-9575-1e677580fd4b"), "Admin" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId", "CreateDate", "CreateUser", "Id", "UpdateDate", "UpdateUser" },
                values: new object[] { new Guid("b47d4a53-b042-47ff-9af8-82046f5950e0"), new Guid("085d5805-7b6d-40cc-9575-1e677580fd4b"), new DateTime(2023, 10, 16, 19, 31, 22, 83, DateTimeKind.Local).AddTicks(6690), new Guid("085d5805-7b6d-40cc-9575-1e677580fd4b"), new Guid("366290ae-3504-471c-9d58-7884f5deb0f0"), new DateTime(2023, 10, 16, 19, 31, 22, 83, DateTimeKind.Local).AddTicks(6691), new Guid("085d5805-7b6d-40cc-9575-1e677580fd4b") });

            migrationBuilder.CreateIndex(
                name: "IX_BankParameters_BankId",
                table: "BankParameters",
                column: "BankId");

            migrationBuilder.AddForeignKey(
                name: "FK_BankParameters_BankCards_BankId",
                table: "BankParameters",
                column: "BankId",
                principalTable: "BankCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
