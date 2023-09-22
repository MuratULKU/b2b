using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace B2B.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    LogicalRef = table.Column<int>(type: "INTEGER", nullable: false),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Parent = table.Column<int>(type: "INTEGER", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreateUser = table.Column<Guid>(type: "TEXT", nullable: false),
                    UpdateUser = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FirmParams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FirmId = table.Column<int>(type: "INTEGER", nullable: false),
                    SyncMinute = table.Column<int>(type: "INTEGER", nullable: false),
                    LastSync = table.Column<DateTime>(type: "TEXT", nullable: true),
                    FirmName = table.Column<string>(type: "TEXT", nullable: true),
                    Address1 = table.Column<string>(type: "TEXT", nullable: true),
                    Address2 = table.Column<string>(type: "TEXT", nullable: true),
                    Town = table.Column<string>(type: "TEXT", nullable: true),
                    City = table.Column<string>(type: "TEXT", nullable: true),
                    Country = table.Column<string>(type: "TEXT", nullable: true),
                    Phone1 = table.Column<string>(type: "TEXT", nullable: true),
                    Phone2 = table.Column<string>(type: "TEXT", nullable: true),
                    MailAddress = table.Column<string>(type: "TEXT", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreateUser = table.Column<Guid>(type: "TEXT", nullable: false),
                    UpdateUser = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FirmParams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    LogicalRef = table.Column<int>(type: "INTEGER", nullable: false),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ParentRef = table.Column<int>(type: "INTEGER", nullable: true),
                    Vat = table.Column<double>(type: "REAL", nullable: true),
                    SellVat = table.Column<double>(type: "REAL", nullable: true),
                    StGrupCode = table.Column<string>(type: "TEXT", nullable: true),
                    ProducerCode = table.Column<string>(type: "TEXT", nullable: true),
                    SpeCode = table.Column<string>(type: "TEXT", nullable: true),
                    SpeCode2 = table.Column<string>(type: "TEXT", nullable: true),
                    SpeCode3 = table.Column<string>(type: "TEXT", nullable: true),
                    SpeCode4 = table.Column<string>(type: "TEXT", nullable: true),
                    SpeCode5 = table.Column<string>(type: "TEXT", nullable: true),
                    Unit = table.Column<string>(type: "TEXT", nullable: false),
                    Unit1 = table.Column<string>(type: "TEXT", nullable: true),
                    Unit1rate = table.Column<decimal>(type: "TEXT", nullable: true),
                    Unit2 = table.Column<string>(type: "TEXT", nullable: true),
                    Unit2rate = table.Column<decimal>(type: "TEXT", nullable: true),
                    Unit3 = table.Column<string>(type: "TEXT", nullable: true),
                    Unit3rate = table.Column<decimal>(type: "TEXT", nullable: true),
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

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "FirmParams");

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
