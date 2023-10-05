using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace B2B.Migrations
{
    /// <inheritdoc />
    public partial class init5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FirmParams",
                keyColumn: "Id",
                keyValue: new Guid("faf2a9c3-1c10-4619-9bf7-08cf8cb6489c"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d4ab248-bfc8-4f4c-a0c9-9a69a793daab"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("95d1f468-39c0-47d4-bae2-50ae68dfcce4"), new Guid("2e0c5d07-4fca-40a9-85ea-5cec59c458a7") });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("95d1f468-39c0-47d4-bae2-50ae68dfcce4"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2e0c5d07-4fca-40a9-85ea-5cec59c458a7"));

            migrationBuilder.CreateTable(
                name: "PriceLists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Code = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Price = table.Column<double>(type: "REAL", nullable: false),
                    Currency = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductCode = table.Column<string>(type: "TEXT", nullable: false),
                    Priorty = table.Column<int>(type: "INTEGER", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreateUser = table.Column<Guid>(type: "TEXT", nullable: false),
                    UpdateUser = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceLists", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "FirmParams",
                columns: new[] { "Id", "Address1", "Address2", "City", "Country", "CreateDate", "CreateUser", "FirmId", "FirmName", "LastSync", "MailAddress", "Phone1", "Phone2", "SyncMinute", "Town", "UpdateDate", "UpdateUser" },
                values: new object[] { new Guid("792b4cd3-d51b-436f-a63f-a9f3f93810da"), null, null, null, null, new DateTime(2023, 9, 23, 12, 11, 38, 661, DateTimeKind.Local).AddTicks(8797), new Guid("8fc135e8-eebb-4204-8a9f-6af16eaa375b"), 1, null, null, null, null, null, 60, null, new DateTime(2023, 9, 23, 12, 11, 38, 661, DateTimeKind.Local).AddTicks(8798), new Guid("8fc135e8-eebb-4204-8a9f-6af16eaa375b") });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "CreateUser", "RoleName", "UpdateDate", "UpdateUser" },
                values: new object[,]
                {
                    { new Guid("4b429cc1-c7ff-4cca-ab43-97a44b7f8dcb"), new DateTime(2023, 9, 23, 12, 11, 38, 661, DateTimeKind.Local).AddTicks(8680), new Guid("8fc135e8-eebb-4204-8a9f-6af16eaa375b"), "User", new DateTime(2023, 9, 23, 12, 11, 38, 661, DateTimeKind.Local).AddTicks(8680), new Guid("8fc135e8-eebb-4204-8a9f-6af16eaa375b") },
                    { new Guid("842bfcd2-1a73-458b-aebe-2423e891dc23"), new DateTime(2023, 9, 23, 12, 11, 38, 661, DateTimeKind.Local).AddTicks(8619), new Guid("8fc135e8-eebb-4204-8a9f-6af16eaa375b"), "Admin", new DateTime(2023, 9, 23, 12, 11, 38, 661, DateTimeKind.Local).AddTicks(8631), new Guid("8fc135e8-eebb-4204-8a9f-6af16eaa375b") }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreateDate", "CreateUser", "Email", "Password", "UpdateDate", "UpdateUser", "Username" },
                values: new object[] { new Guid("8fc135e8-eebb-4204-8a9f-6af16eaa375b"), new DateTime(2023, 9, 23, 12, 11, 38, 661, DateTimeKind.Local).AddTicks(8735), new Guid("8fc135e8-eebb-4204-8a9f-6af16eaa375b"), "murat@ulkubilgisayar.com", "Admin", new DateTime(2023, 9, 23, 12, 11, 38, 661, DateTimeKind.Local).AddTicks(8736), new Guid("8fc135e8-eebb-4204-8a9f-6af16eaa375b"), "Admin" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("842bfcd2-1a73-458b-aebe-2423e891dc23"), new Guid("8fc135e8-eebb-4204-8a9f-6af16eaa375b") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PriceLists");

            migrationBuilder.DeleteData(
                table: "FirmParams",
                keyColumn: "Id",
                keyValue: new Guid("792b4cd3-d51b-436f-a63f-a9f3f93810da"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4b429cc1-c7ff-4cca-ab43-97a44b7f8dcb"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("842bfcd2-1a73-458b-aebe-2423e891dc23"), new Guid("8fc135e8-eebb-4204-8a9f-6af16eaa375b") });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("842bfcd2-1a73-458b-aebe-2423e891dc23"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8fc135e8-eebb-4204-8a9f-6af16eaa375b"));

            migrationBuilder.InsertData(
                table: "FirmParams",
                columns: new[] { "Id", "Address1", "Address2", "City", "Country", "CreateDate", "CreateUser", "FirmId", "FirmName", "LastSync", "MailAddress", "Phone1", "Phone2", "SyncMinute", "Town", "UpdateDate", "UpdateUser" },
                values: new object[] { new Guid("faf2a9c3-1c10-4619-9bf7-08cf8cb6489c"), null, null, null, null, new DateTime(2023, 9, 22, 10, 46, 46, 879, DateTimeKind.Local).AddTicks(684), new Guid("2e0c5d07-4fca-40a9-85ea-5cec59c458a7"), 1, null, null, null, null, null, 60, null, new DateTime(2023, 9, 22, 10, 46, 46, 879, DateTimeKind.Local).AddTicks(684), new Guid("2e0c5d07-4fca-40a9-85ea-5cec59c458a7") });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "CreateUser", "RoleName", "UpdateDate", "UpdateUser" },
                values: new object[,]
                {
                    { new Guid("8d4ab248-bfc8-4f4c-a0c9-9a69a793daab"), new DateTime(2023, 9, 22, 10, 46, 46, 879, DateTimeKind.Local).AddTicks(573), new Guid("2e0c5d07-4fca-40a9-85ea-5cec59c458a7"), "User", new DateTime(2023, 9, 22, 10, 46, 46, 879, DateTimeKind.Local).AddTicks(575), new Guid("2e0c5d07-4fca-40a9-85ea-5cec59c458a7") },
                    { new Guid("95d1f468-39c0-47d4-bae2-50ae68dfcce4"), new DateTime(2023, 9, 22, 10, 46, 46, 879, DateTimeKind.Local).AddTicks(517), new Guid("2e0c5d07-4fca-40a9-85ea-5cec59c458a7"), "Admin", new DateTime(2023, 9, 22, 10, 46, 46, 879, DateTimeKind.Local).AddTicks(530), new Guid("2e0c5d07-4fca-40a9-85ea-5cec59c458a7") }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreateDate", "CreateUser", "Email", "Password", "UpdateDate", "UpdateUser", "Username" },
                values: new object[] { new Guid("2e0c5d07-4fca-40a9-85ea-5cec59c458a7"), new DateTime(2023, 9, 22, 10, 46, 46, 879, DateTimeKind.Local).AddTicks(629), new Guid("2e0c5d07-4fca-40a9-85ea-5cec59c458a7"), "murat@ulkubilgisayar.com", "Admin", new DateTime(2023, 9, 22, 10, 46, 46, 879, DateTimeKind.Local).AddTicks(630), new Guid("2e0c5d07-4fca-40a9-85ea-5cec59c458a7"), "Admin" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("95d1f468-39c0-47d4-bae2-50ae68dfcce4"), new Guid("2e0c5d07-4fca-40a9-85ea-5cec59c458a7") });
        }
    }
}
