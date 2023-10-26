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
            migrationBuilder.CreateTable(
                name: "BankCards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    SystemName = table.Column<string>(type: "TEXT", nullable: true),
                    BankCode = table.Column<int>(type: "INTEGER", nullable: false),
                    LogoPath = table.Column<string>(type: "TEXT", nullable: true),
                    UseCommonPaymentPage = table.Column<bool>(type: "INTEGER", nullable: false),
                    Active = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreateUser = table.Column<Guid>(type: "TEXT", nullable: false),
                    UpdateUser = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankCards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Baskets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserGuid = table.Column<Guid>(type: "TEXT", nullable: false),
                    Date_ = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LineNUmber = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductGuid = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProductCode = table.Column<string>(type: "TEXT", nullable: false),
                    ProductName = table.Column<string>(type: "TEXT", nullable: false),
                    ProductDescription = table.Column<string>(type: "TEXT", nullable: true),
                    Amount = table.Column<double>(type: "REAL", nullable: false),
                    Price = table.Column<double>(type: "REAL", nullable: false),
                    VatRate = table.Column<double>(type: "REAL", nullable: false),
                    VatPrice = table.Column<double>(type: "REAL", nullable: false),
                    DiscountRate = table.Column<double>(type: "REAL", nullable: false),
                    DiscountPrice = table.Column<double>(type: "REAL", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreateUser = table.Column<Guid>(type: "TEXT", nullable: false),
                    UpdateUser = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Baskets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CardBrands",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Code = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreateUser = table.Column<Guid>(type: "TEXT", nullable: false),
                    UpdateUser = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardBrands", x => x.Id);
                });

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
                name: "PriceLists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Cardref = table.Column<int>(type: "INTEGER", nullable: false),
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
                name: "BankParameters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    BankCardId = table.Column<int>(type: "INTEGER", nullable: false),
                    Key = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: false),
                    BankId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreateUser = table.Column<Guid>(type: "TEXT", nullable: false),
                    UpdateUser = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankParameters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankParameters_BankCards_BankId",
                        column: x => x.BankId,
                        principalTable: "BankCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTransactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    OrderNumber = table.Column<Guid>(type: "TEXT", nullable: false),
                    TransactionNumber = table.Column<string>(type: "TEXT", nullable: true),
                    ReferenceNumber = table.Column<string>(type: "TEXT", nullable: true),
                    UserIpAddress = table.Column<string>(type: "TEXT", nullable: true),
                    UserAgent = table.Column<string>(type: "TEXT", nullable: true),
                    BankCardId = table.Column<int>(type: "INTEGER", nullable: false),
                    CardPrefix = table.Column<string>(type: "TEXT", nullable: true),
                    CardHolderName = table.Column<string>(type: "TEXT", nullable: true),
                    Installment = table.Column<int>(type: "INTEGER", nullable: false),
                    ExtraInstallment = table.Column<int>(type: "INTEGER", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    BankErrorMessage = table.Column<string>(type: "TEXT", nullable: true),
                    PaidDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ClientCode = table.Column<string>(type: "TEXT", nullable: true),
                    StatusId = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    BankRequest = table.Column<string>(type: "TEXT", nullable: true),
                    BankResponse = table.Column<string>(type: "TEXT", nullable: true),
                    MaskedCardNumber = table.Column<string>(type: "TEXT", nullable: true),
                    BankId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreateUser = table.Column<Guid>(type: "TEXT", nullable: false),
                    UpdateUser = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentTransactions_BankCards_BankId",
                        column: x => x.BankId,
                        principalTable: "BankCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VirtualPos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    BankCardId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    AccountCode = table.Column<string>(type: "TEXT", nullable: false),
                    CardBrands = table.Column<int>(type: "INTEGER", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreateUser = table.Column<Guid>(type: "TEXT", nullable: false),
                    UpdateUser = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VirtualPos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VirtualPos_BankCards_BankCardId",
                        column: x => x.BankCardId,
                        principalTable: "BankCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CreditCards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    BankCardId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CardBrandId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Active = table.Column<bool>(type: "INTEGER", nullable: false),
                    ManufacturerCard = table.Column<bool>(type: "INTEGER", nullable: false),
                    CampaignCard = table.Column<bool>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    BrandCode = table.Column<int>(type: "INTEGER", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreateUser = table.Column<Guid>(type: "TEXT", nullable: false),
                    UpdateUser = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CreditCards_BankCards_BankCardId",
                        column: x => x.BankCardId,
                        principalTable: "BankCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CreditCards_CardBrands_CardBrandId",
                        column: x => x.CardBrandId,
                        principalTable: "CardBrands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    RoleId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreateUser = table.Column<Guid>(type: "TEXT", nullable: false),
                    UpdateUser = table.Column<Guid>(type: "TEXT", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "CreditCardInstallments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreditCardId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Installment = table.Column<int>(type: "INTEGER", nullable: false),
                    InstallmentRate = table.Column<decimal>(type: "TEXT", nullable: false),
                    Active = table.Column<bool>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    BankCardId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CardBrandId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Business = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreateUser = table.Column<Guid>(type: "TEXT", nullable: false),
                    UpdateUser = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditCardInstallments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CreditCardInstallments_BankCards_BankCardId",
                        column: x => x.BankCardId,
                        principalTable: "BankCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CreditCardInstallments_CardBrands_CardBrandId",
                        column: x => x.CardBrandId,
                        principalTable: "CardBrands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CreditCardInstallments_CreditCards_CreditCardId",
                        column: x => x.CreditCardId,
                        principalTable: "CreditCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CreditCardPrefixes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreditCardId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Prefix = table.Column<string>(type: "TEXT", nullable: false),
                    BankCode = table.Column<int>(type: "INTEGER", nullable: false),
                    BrandCode = table.Column<int>(type: "INTEGER", nullable: false),
                    Business = table.Column<bool>(type: "INTEGER", nullable: false),
                    Active = table.Column<bool>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    isInstallment = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreateUser = table.Column<Guid>(type: "TEXT", nullable: false),
                    UpdateUser = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditCardPrefixes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CreditCardPrefixes_CreditCards_CreditCardId",
                        column: x => x.CreditCardId,
                        principalTable: "CreditCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_CreditCardInstallments_BankCardId",
                table: "CreditCardInstallments",
                column: "BankCardId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditCardInstallments_CardBrandId",
                table: "CreditCardInstallments",
                column: "CardBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditCardInstallments_CreditCardId",
                table: "CreditCardInstallments",
                column: "CreditCardId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditCardPrefixes_CreditCardId",
                table: "CreditCardPrefixes",
                column: "CreditCardId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditCards_BankCardId",
                table: "CreditCards",
                column: "BankCardId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditCards_CardBrandId",
                table: "CreditCards",
                column: "CardBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransactions_BankId",
                table: "PaymentTransactions",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_VirtualPos_BankCardId",
                table: "VirtualPos",
                column: "BankCardId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankParameters");

            migrationBuilder.DropTable(
                name: "Baskets");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "CreditCardInstallments");

            migrationBuilder.DropTable(
                name: "CreditCardPrefixes");

            migrationBuilder.DropTable(
                name: "FirmParams");

            migrationBuilder.DropTable(
                name: "PaymentTransactions");

            migrationBuilder.DropTable(
                name: "PriceLists");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "VirtualPos");

            migrationBuilder.DropTable(
                name: "CreditCards");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "BankCards");

            migrationBuilder.DropTable(
                name: "CardBrands");
        }
    }
}
