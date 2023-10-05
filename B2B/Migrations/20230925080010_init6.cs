using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace B2B.Migrations
{
    /// <inheritdoc />
    public partial class init6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                values: new object[] { new Guid("1d4a04c5-0b5e-40c3-8b71-fc5408b04348"), null, null, null, null, new DateTime(2023, 9, 25, 11, 0, 10, 499, DateTimeKind.Local).AddTicks(6623), new Guid("59557f0e-65af-435f-9147-7d715f084130"), 1, null, null, null, null, null, 60, null, new DateTime(2023, 9, 25, 11, 0, 10, 499, DateTimeKind.Local).AddTicks(6624), new Guid("59557f0e-65af-435f-9147-7d715f084130") });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "CreateUser", "RoleName", "UpdateDate", "UpdateUser" },
                values: new object[,]
                {
                    { new Guid("ca44eca5-01d8-4747-84cb-e2feae90a4a3"), new DateTime(2023, 9, 25, 11, 0, 10, 499, DateTimeKind.Local).AddTicks(6553), new Guid("59557f0e-65af-435f-9147-7d715f084130"), "User", new DateTime(2023, 9, 25, 11, 0, 10, 499, DateTimeKind.Local).AddTicks(6553), new Guid("59557f0e-65af-435f-9147-7d715f084130") },
                    { new Guid("e08559ae-27ab-4cc4-974d-1afb14c86229"), new DateTime(2023, 9, 25, 11, 0, 10, 499, DateTimeKind.Local).AddTicks(6518), new Guid("59557f0e-65af-435f-9147-7d715f084130"), "Admin", new DateTime(2023, 9, 25, 11, 0, 10, 499, DateTimeKind.Local).AddTicks(6529), new Guid("59557f0e-65af-435f-9147-7d715f084130") }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreateDate", "CreateUser", "Email", "Password", "UpdateDate", "UpdateUser", "Username" },
                values: new object[] { new Guid("59557f0e-65af-435f-9147-7d715f084130"), new DateTime(2023, 9, 25, 11, 0, 10, 499, DateTimeKind.Local).AddTicks(6588), new Guid("59557f0e-65af-435f-9147-7d715f084130"), "murat@ulkubilgisayar.com", "Admin", new DateTime(2023, 9, 25, 11, 0, 10, 499, DateTimeKind.Local).AddTicks(6589), new Guid("59557f0e-65af-435f-9147-7d715f084130"), "Admin" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("e08559ae-27ab-4cc4-974d-1afb14c86229"), new Guid("59557f0e-65af-435f-9147-7d715f084130") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FirmParams",
                keyColumn: "Id",
                keyValue: new Guid("1d4a04c5-0b5e-40c3-8b71-fc5408b04348"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("ca44eca5-01d8-4747-84cb-e2feae90a4a3"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("e08559ae-27ab-4cc4-974d-1afb14c86229"), new Guid("59557f0e-65af-435f-9147-7d715f084130") });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("e08559ae-27ab-4cc4-974d-1afb14c86229"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("59557f0e-65af-435f-9147-7d715f084130"));

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
    }
}
