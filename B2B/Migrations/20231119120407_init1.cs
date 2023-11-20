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
                    DocNo = table.Column<string>(type: "TEXT", nullable: false),
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
                    Send = table.Column<bool>(type: "INTEGER", nullable: false),
                    Total = table.Column<double>(type: "REAL", nullable: false),
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
                name: "DocumentNo",
                columns: table => new
                {
                    DocType = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Prefix = table.Column<string>(type: "TEXT", nullable: false),
                    DocNo = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentNo", x => x.DocType);
                });

            migrationBuilder.CreateTable(
                name: "FirmParams",
                columns: table => new
                {
                    Key = table.Column<string>(type: "TEXT", nullable: false),
                    No = table.Column<int>(type: "INTEGER", nullable: false),
                    Value = table.Column<byte[]>(type: "BLOB", nullable: false),
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreateUser = table.Column<Guid>(type: "TEXT", nullable: false),
                    UpdateUser = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FirmParams", x => x.Key);
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
                    AccountCode = table.Column<string>(type: "TEXT", nullable: true),
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
                    BankCardId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Key = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreateUser = table.Column<Guid>(type: "TEXT", nullable: false),
                    UpdateUser = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankParameters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankParameters_BankCards_BankCardId",
                        column: x => x.BankCardId,
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
                    BankCardId = table.Column<Guid>(type: "TEXT", nullable: false),
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
                        name: "FK_PaymentTransactions_BankCards_BankCardId",
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
                name: "VirtualPos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    BankCardId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CardBrandId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    AccountCode = table.Column<string>(type: "TEXT", nullable: false),
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
                    table.ForeignKey(
                        name: "FK_VirtualPos_CardBrands_CardBrandId",
                        column: x => x.CardBrandId,
                        principalTable: "CardBrands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FirmDocs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    InfoType = table.Column<int>(type: "INTEGER", nullable: false),
                    LineNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    ProtuctId = table.Column<Guid>(type: "TEXT", nullable: true),
                    LData = table.Column<byte[]>(type: "BLOB", nullable: false),
                    ProductId = table.Column<Guid>(type: "TEXT", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreateUser = table.Column<Guid>(type: "TEXT", nullable: false),
                    UpdateUser = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FirmDocs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FirmDocs_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
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
                    ProductId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreateUser = table.Column<Guid>(type: "TEXT", nullable: false),
                    UpdateUser = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriceLists_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductAmounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    StockRef = table.Column<int>(type: "INTEGER", nullable: false),
                    OnHand = table.Column<double>(type: "REAL", nullable: false),
                    ProductId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreateUser = table.Column<Guid>(type: "TEXT", nullable: false),
                    UpdateUser = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAmounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductAmounts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
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
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "CreateUser", "RoleName", "UpdateDate", "UpdateUser" },
                values: new object[,]
                {
                    { new Guid("1e8aa23c-343f-48a8-aa6e-0a8cbf9232d6"), new DateTime(2023, 11, 19, 15, 4, 7, 5, DateTimeKind.Local).AddTicks(99), new Guid("064a3ac8-9505-4d43-8a4e-9c822cab7a39"), "User", new DateTime(2023, 11, 19, 15, 4, 7, 5, DateTimeKind.Local).AddTicks(100), new Guid("064a3ac8-9505-4d43-8a4e-9c822cab7a39") },
                    { new Guid("c226ba28-e656-46c9-b8b5-465edf0dffa3"), new DateTime(2023, 11, 19, 15, 4, 7, 5, DateTimeKind.Local).AddTicks(68), new Guid("064a3ac8-9505-4d43-8a4e-9c822cab7a39"), "Admin", new DateTime(2023, 11, 19, 15, 4, 7, 5, DateTimeKind.Local).AddTicks(81), new Guid("064a3ac8-9505-4d43-8a4e-9c822cab7a39") }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccountCode", "CreateDate", "CreateUser", "Email", "Password", "UpdateDate", "UpdateUser", "Username" },
                values: new object[] { new Guid("064a3ac8-9505-4d43-8a4e-9c822cab7a39"), "", new DateTime(2023, 11, 19, 15, 4, 7, 5, DateTimeKind.Local).AddTicks(133), new Guid("064a3ac8-9505-4d43-8a4e-9c822cab7a39"), "murat@ulkubilgisayar.com", "Admin", new DateTime(2023, 11, 19, 15, 4, 7, 5, DateTimeKind.Local).AddTicks(134), new Guid("064a3ac8-9505-4d43-8a4e-9c822cab7a39"), "Admin" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId", "CreateDate", "CreateUser", "Id", "UpdateDate", "UpdateUser" },
                values: new object[] { new Guid("c226ba28-e656-46c9-b8b5-465edf0dffa3"), new Guid("064a3ac8-9505-4d43-8a4e-9c822cab7a39"), new DateTime(2023, 11, 19, 15, 4, 7, 5, DateTimeKind.Local).AddTicks(151), new Guid("064a3ac8-9505-4d43-8a4e-9c822cab7a39"), new Guid("4666365f-014c-4fc7-87f4-4dfe96174263"), new DateTime(2023, 11, 19, 15, 4, 7, 5, DateTimeKind.Local).AddTicks(151), new Guid("064a3ac8-9505-4d43-8a4e-9c822cab7a39") });

            migrationBuilder.CreateIndex(
                name: "IX_BankParameters_BankCardId",
                table: "BankParameters",
                column: "BankCardId");

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
                name: "IX_FirmDocs_ProductId",
                table: "FirmDocs",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransactions_BankCardId",
                table: "PaymentTransactions",
                column: "BankCardId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceLists_ProductId",
                table: "PriceLists",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAmounts_ProductId",
                table: "ProductAmounts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_VirtualPos_BankCardId",
                table: "VirtualPos",
                column: "BankCardId");

            migrationBuilder.CreateIndex(
                name: "IX_VirtualPos_CardBrandId",
                table: "VirtualPos",
                column: "CardBrandId");
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
                name: "DocumentNo");

            migrationBuilder.DropTable(
                name: "FirmDocs");

            migrationBuilder.DropTable(
                name: "FirmParams");

            migrationBuilder.DropTable(
                name: "PaymentTransactions");

            migrationBuilder.DropTable(
                name: "PriceLists");

            migrationBuilder.DropTable(
                name: "ProductAmounts");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "VirtualPos");

            migrationBuilder.DropTable(
                name: "CreditCards");

            migrationBuilder.DropTable(
                name: "Products");

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
