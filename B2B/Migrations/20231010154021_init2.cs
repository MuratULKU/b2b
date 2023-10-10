using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace B2B.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CreditCardPrefixes_CreditCards_CreditCardId1",
                table: "CreditCardPrefixes");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditCards_BankCards_BankId",
                table: "CreditCards");

            migrationBuilder.DropIndex(
                name: "IX_CreditCards_BankId",
                table: "CreditCards");

            migrationBuilder.DropIndex(
                name: "IX_CreditCardPrefixes_CreditCardId1",
                table: "CreditCardPrefixes");

            migrationBuilder.DeleteData(
                table: "FirmParams",
                keyColumn: "Id",
                keyValue: new Guid("141e24ac-fe65-410d-8096-ba2d095676bb"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("d87f6657-2979-4e8c-9f66-bee8e985696a"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("d90f5099-a426-4433-a19b-4c214d6fdd78"), new Guid("56f5576b-f209-4a40-b305-0b44580a6f3e") });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("d90f5099-a426-4433-a19b-4c214d6fdd78"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("56f5576b-f209-4a40-b305-0b44580a6f3e"));

            migrationBuilder.DropColumn(
                name: "BankId",
                table: "CreditCards");

            migrationBuilder.DropColumn(
                name: "CreditCardId1",
                table: "CreditCardPrefixes");

            migrationBuilder.AlterColumn<Guid>(
                name: "BankCardId",
                table: "CreditCards",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<Guid>(
                name: "CreditCardId",
                table: "CreditCardPrefixes",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.InsertData(
                table: "FirmParams",
                columns: new[] { "Id", "Address1", "Address2", "City", "Country", "CreateDate", "CreateUser", "FirmId", "FirmName", "LastSync", "MailAddress", "Phone1", "Phone2", "SyncMinute", "Town", "UpdateDate", "UpdateUser" },
                values: new object[] { new Guid("75b6a30a-d653-49e2-acb4-d66885ffa221"), null, null, null, null, new DateTime(2023, 10, 10, 18, 40, 21, 21, DateTimeKind.Local).AddTicks(1752), new Guid("a5b54d1f-516f-488f-b6e4-50ae90ead0d0"), 1, null, null, null, null, null, 60, null, new DateTime(2023, 10, 10, 18, 40, 21, 21, DateTimeKind.Local).AddTicks(1753), new Guid("a5b54d1f-516f-488f-b6e4-50ae90ead0d0") });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "CreateUser", "RoleName", "UpdateDate", "UpdateUser" },
                values: new object[,]
                {
                    { new Guid("32bd2994-955f-4595-a3ce-0a4219c8c41a"), new DateTime(2023, 10, 10, 18, 40, 21, 21, DateTimeKind.Local).AddTicks(1646), new Guid("a5b54d1f-516f-488f-b6e4-50ae90ead0d0"), "User", new DateTime(2023, 10, 10, 18, 40, 21, 21, DateTimeKind.Local).AddTicks(1647), new Guid("a5b54d1f-516f-488f-b6e4-50ae90ead0d0") },
                    { new Guid("f0373dc4-cf0c-4935-9088-e43223267c7b"), new DateTime(2023, 10, 10, 18, 40, 21, 21, DateTimeKind.Local).AddTicks(1611), new Guid("a5b54d1f-516f-488f-b6e4-50ae90ead0d0"), "Admin", new DateTime(2023, 10, 10, 18, 40, 21, 21, DateTimeKind.Local).AddTicks(1625), new Guid("a5b54d1f-516f-488f-b6e4-50ae90ead0d0") }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreateDate", "CreateUser", "Email", "Password", "UpdateDate", "UpdateUser", "Username" },
                values: new object[] { new Guid("a5b54d1f-516f-488f-b6e4-50ae90ead0d0"), new DateTime(2023, 10, 10, 18, 40, 21, 21, DateTimeKind.Local).AddTicks(1697), new Guid("a5b54d1f-516f-488f-b6e4-50ae90ead0d0"), "murat@ulkubilgisayar.com", "Admin", new DateTime(2023, 10, 10, 18, 40, 21, 21, DateTimeKind.Local).AddTicks(1698), new Guid("a5b54d1f-516f-488f-b6e4-50ae90ead0d0"), "Admin" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("f0373dc4-cf0c-4935-9088-e43223267c7b"), new Guid("a5b54d1f-516f-488f-b6e4-50ae90ead0d0") });

            migrationBuilder.CreateIndex(
                name: "IX_CreditCards_BankCardId",
                table: "CreditCards",
                column: "BankCardId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditCardPrefixes_CreditCardId",
                table: "CreditCardPrefixes",
                column: "CreditCardId");

            migrationBuilder.AddForeignKey(
                name: "FK_CreditCardPrefixes_CreditCards_CreditCardId",
                table: "CreditCardPrefixes",
                column: "CreditCardId",
                principalTable: "CreditCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CreditCards_BankCards_BankCardId",
                table: "CreditCards",
                column: "BankCardId",
                principalTable: "BankCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CreditCardPrefixes_CreditCards_CreditCardId",
                table: "CreditCardPrefixes");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditCards_BankCards_BankCardId",
                table: "CreditCards");

            migrationBuilder.DropIndex(
                name: "IX_CreditCards_BankCardId",
                table: "CreditCards");

            migrationBuilder.DropIndex(
                name: "IX_CreditCardPrefixes_CreditCardId",
                table: "CreditCardPrefixes");

            migrationBuilder.DeleteData(
                table: "FirmParams",
                keyColumn: "Id",
                keyValue: new Guid("75b6a30a-d653-49e2-acb4-d66885ffa221"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("32bd2994-955f-4595-a3ce-0a4219c8c41a"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("f0373dc4-cf0c-4935-9088-e43223267c7b"), new Guid("a5b54d1f-516f-488f-b6e4-50ae90ead0d0") });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("f0373dc4-cf0c-4935-9088-e43223267c7b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a5b54d1f-516f-488f-b6e4-50ae90ead0d0"));

            migrationBuilder.AlterColumn<int>(
                name: "BankCardId",
                table: "CreditCards",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddColumn<Guid>(
                name: "BankId",
                table: "CreditCards",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<int>(
                name: "CreditCardId",
                table: "CreditCardPrefixes",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddColumn<Guid>(
                name: "CreditCardId1",
                table: "CreditCardPrefixes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.InsertData(
                table: "FirmParams",
                columns: new[] { "Id", "Address1", "Address2", "City", "Country", "CreateDate", "CreateUser", "FirmId", "FirmName", "LastSync", "MailAddress", "Phone1", "Phone2", "SyncMinute", "Town", "UpdateDate", "UpdateUser" },
                values: new object[] { new Guid("141e24ac-fe65-410d-8096-ba2d095676bb"), null, null, null, null, new DateTime(2023, 10, 10, 18, 17, 59, 921, DateTimeKind.Local).AddTicks(5337), new Guid("56f5576b-f209-4a40-b305-0b44580a6f3e"), 1, null, null, null, null, null, 60, null, new DateTime(2023, 10, 10, 18, 17, 59, 921, DateTimeKind.Local).AddTicks(5337), new Guid("56f5576b-f209-4a40-b305-0b44580a6f3e") });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "CreateUser", "RoleName", "UpdateDate", "UpdateUser" },
                values: new object[,]
                {
                    { new Guid("d87f6657-2979-4e8c-9f66-bee8e985696a"), new DateTime(2023, 10, 10, 18, 17, 59, 921, DateTimeKind.Local).AddTicks(5271), new Guid("56f5576b-f209-4a40-b305-0b44580a6f3e"), "User", new DateTime(2023, 10, 10, 18, 17, 59, 921, DateTimeKind.Local).AddTicks(5272), new Guid("56f5576b-f209-4a40-b305-0b44580a6f3e") },
                    { new Guid("d90f5099-a426-4433-a19b-4c214d6fdd78"), new DateTime(2023, 10, 10, 18, 17, 59, 921, DateTimeKind.Local).AddTicks(5247), new Guid("56f5576b-f209-4a40-b305-0b44580a6f3e"), "Admin", new DateTime(2023, 10, 10, 18, 17, 59, 921, DateTimeKind.Local).AddTicks(5255), new Guid("56f5576b-f209-4a40-b305-0b44580a6f3e") }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreateDate", "CreateUser", "Email", "Password", "UpdateDate", "UpdateUser", "Username" },
                values: new object[] { new Guid("56f5576b-f209-4a40-b305-0b44580a6f3e"), new DateTime(2023, 10, 10, 18, 17, 59, 921, DateTimeKind.Local).AddTicks(5303), new Guid("56f5576b-f209-4a40-b305-0b44580a6f3e"), "murat@ulkubilgisayar.com", "Admin", new DateTime(2023, 10, 10, 18, 17, 59, 921, DateTimeKind.Local).AddTicks(5304), new Guid("56f5576b-f209-4a40-b305-0b44580a6f3e"), "Admin" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("d90f5099-a426-4433-a19b-4c214d6fdd78"), new Guid("56f5576b-f209-4a40-b305-0b44580a6f3e") });

            migrationBuilder.CreateIndex(
                name: "IX_CreditCards_BankId",
                table: "CreditCards",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditCardPrefixes_CreditCardId1",
                table: "CreditCardPrefixes",
                column: "CreditCardId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CreditCardPrefixes_CreditCards_CreditCardId1",
                table: "CreditCardPrefixes",
                column: "CreditCardId1",
                principalTable: "CreditCards",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CreditCards_BankCards_BankId",
                table: "CreditCards",
                column: "BankId",
                principalTable: "BankCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
