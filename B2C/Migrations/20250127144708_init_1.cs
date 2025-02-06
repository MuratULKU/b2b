using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace B2C.Migrations
{
    /// <inheritdoc />
    public partial class init_1 : Migration
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
                name: "CardBrands",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Code = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
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
                name: "CharCodes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    LogiclRef = table.Column<int>(type: "INTEGER", nullable: false),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    CSetCode = table.Column<string>(type: "TEXT", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreateUser = table.Column<Guid>(type: "TEXT", nullable: false),
                    UpdateUser = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharCodes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CharSets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    LogicalRef = table.Column<int>(type: "INTEGER", nullable: false),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreateUser = table.Column<Guid>(type: "TEXT", nullable: false),
                    UpdateUser = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharSets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    VatOffice = table.Column<string>(type: "TEXT", nullable: true),
                    VKN = table.Column<string>(type: "TEXT", nullable: true),
                    MailAdress = table.Column<string>(type: "TEXT", nullable: true),
                    Phone1 = table.Column<string>(type: "TEXT", nullable: true),
                    Address1 = table.Column<string>(type: "TEXT", nullable: true),
                    Address2 = table.Column<string>(type: "TEXT", nullable: true),
                    Town = table.Column<string>(type: "TEXT", nullable: true),
                    City = table.Column<string>(type: "TEXT", nullable: true),
                    Country = table.Column<string>(type: "TEXT", nullable: true),
                    FirmExecutiveName = table.Column<string>(type: "TEXT", nullable: true),
                    FirmExecutiveSurName = table.Column<string>(type: "TEXT", nullable: true),
                    Phone2 = table.Column<string>(type: "TEXT", nullable: true),
                    MailAdress2 = table.Column<string>(type: "TEXT", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreateUser = table.Column<Guid>(type: "TEXT", nullable: false),
                    UpdateUser = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Address1 = table.Column<string>(type: "TEXT", nullable: false),
                    Address2 = table.Column<string>(type: "TEXT", nullable: false),
                    City = table.Column<string>(type: "TEXT", nullable: false),
                    Town = table.Column<string>(type: "TEXT", nullable: false),
                    TelNo1 = table.Column<string>(type: "TEXT", nullable: false),
                    TelNo2 = table.Column<string>(type: "TEXT", nullable: false),
                    Mail = table.Column<string>(type: "TEXT", nullable: false),
                    ProgramCode = table.Column<string>(type: "TEXT", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreateUser = table.Column<Guid>(type: "TEXT", nullable: false),
                    UpdateUser = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CreditCardPrefixes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreditCardId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Prefix = table.Column<string>(type: "TEXT", nullable: true),
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
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CurrencySymbol = table.Column<string>(type: "TEXT", nullable: false),
                    CurrencyName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentNo",
                columns: table => new
                {
                    DocType = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Prefix = table.Column<string>(type: "TEXT", nullable: true),
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
                    Value = table.Column<byte[]>(type: "BLOB", nullable: true),
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
                name: "VirtualPos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    BankCardId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CardBrandId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    AccountCode = table.Column<string>(type: "TEXT", nullable: true),
                    SinglePayment = table.Column<bool>(type: "INTEGER", nullable: false),
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
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Active = table.Column<bool>(type: "INTEGER", nullable: false),
                    ManufacturerCard = table.Column<bool>(type: "INTEGER", nullable: false),
                    CampaignCard = table.Column<bool>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    isBusiness = table.Column<bool>(type: "INTEGER", nullable: false),
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
                name: "CharVals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    LogicalRef = table.Column<int>(type: "INTEGER", nullable: false),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    CharCodeId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreateUser = table.Column<Guid>(type: "TEXT", nullable: false),
                    UpdateUser = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharVals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CharVals_CharCodes_CharCodeId",
                        column: x => x.CharCodeId,
                        principalTable: "CharCodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    LogicalRef = table.Column<int>(type: "INTEGER", nullable: false),
                    Code = table.Column<string>(type: "TEXT COLLATE NOCASE", nullable: true),
                    Name = table.Column<string>(type: "TEXT COLLATE NOCASE", nullable: true),
                    Name2 = table.Column<string>(type: "TEXT", nullable: true),
                    Name3 = table.Column<string>(type: "TEXT", nullable: true),
                    Name4 = table.Column<string>(type: "TEXT", nullable: true),
                    ParentRef = table.Column<int>(type: "INTEGER", nullable: true),
                    Vat = table.Column<double>(type: "REAL", nullable: false),
                    SellVat = table.Column<double>(type: "REAL", nullable: true),
                    StGrupCode = table.Column<string>(type: "TEXT", nullable: true),
                    ProducerCode = table.Column<string>(type: "TEXT", nullable: true),
                    SpeCode = table.Column<string>(type: "TEXT", nullable: true),
                    SpeCode2 = table.Column<string>(type: "TEXT", nullable: true),
                    SpeCode3 = table.Column<string>(type: "TEXT", nullable: true),
                    SpeCode4 = table.Column<string>(type: "TEXT", nullable: true),
                    SpeCode5 = table.Column<string>(type: "TEXT", nullable: true),
                    Keyword1 = table.Column<string>(type: "TEXT", nullable: true),
                    Keyword2 = table.Column<string>(type: "TEXT", nullable: true),
                    Keyword3 = table.Column<string>(type: "TEXT", nullable: true),
                    Keyword4 = table.Column<string>(type: "TEXT", nullable: true),
                    Keyword5 = table.Column<string>(type: "TEXT", nullable: true),
                    Unit = table.Column<string>(type: "TEXT", nullable: true),
                    Unit1 = table.Column<string>(type: "TEXT", nullable: true),
                    Unit1rate = table.Column<decimal>(type: "TEXT", nullable: true),
                    Unit2 = table.Column<string>(type: "TEXT", nullable: true),
                    Unit2rate = table.Column<decimal>(type: "TEXT", nullable: true),
                    Unit3 = table.Column<string>(type: "TEXT", nullable: true),
                    Unit3rate = table.Column<decimal>(type: "TEXT", nullable: true),
                    UsRef = table.Column<int>(type: "INTEGER", nullable: false),
                    UomRef = table.Column<int>(type: "INTEGER", nullable: false),
                    CharSetId = table.Column<Guid>(type: "TEXT", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreateUser = table.Column<Guid>(type: "TEXT", nullable: false),
                    UpdateUser = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_CharSets_CharSetId",
                        column: x => x.CharSetId,
                        principalTable: "CharSets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CompanyId = table.Column<Guid>(type: "TEXT", nullable: true),
                    Username = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    Password = table.Column<string>(type: "TEXT", nullable: true),
                    Discount = table.Column<double>(type: "REAL", nullable: true),
                    AccountCode = table.Column<string>(type: "TEXT", nullable: true),
                    AccountName = table.Column<string>(type: "TEXT", nullable: true),
                    Active = table.Column<bool>(type: "INTEGER", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreateUser = table.Column<Guid>(type: "TEXT", nullable: false),
                    UpdateUser = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "VirtualPosParameters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    VirtualPosId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Key = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreateUser = table.Column<Guid>(type: "TEXT", nullable: false),
                    UpdateUser = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VirtualPosParameters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VirtualPosParameters_VirtualPos_VirtualPosId",
                        column: x => x.VirtualPosId,
                        principalTable: "VirtualPos",
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
                        name: "FK_CreditCardInstallments_CreditCards_CreditCardId",
                        column: x => x.CreditCardId,
                        principalTable: "CreditCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharAsgns",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    LogicalRef = table.Column<int>(type: "INTEGER", nullable: false),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    CharCodeName = table.Column<string>(type: "TEXT", nullable: false),
                    CharValName = table.Column<string>(type: "TEXT", nullable: false),
                    CharCodeCode = table.Column<string>(type: "TEXT", nullable: false),
                    CharValCode = table.Column<string>(type: "TEXT", nullable: false),
                    ProductId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CharCodeId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CharValId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreateUser = table.Column<Guid>(type: "TEXT", nullable: false),
                    UpdateUser = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharAsgns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CharAsgns_CharCodes_CharCodeId",
                        column: x => x.CharCodeId,
                        principalTable: "CharCodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharAsgns_CharVals_CharValId",
                        column: x => x.CharValId,
                        principalTable: "CharVals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharAsgns_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
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
                    ProtuctId = table.Column<Guid>(type: "TEXT", nullable: false),
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
                    Logicalref = table.Column<int>(type: "INTEGER", nullable: false),
                    Cardref = table.Column<int>(type: "INTEGER", nullable: false),
                    Code = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Price = table.Column<double>(type: "REAL", nullable: false),
                    CurrencyId = table.Column<int>(type: "INTEGER", nullable: false),
                    IncVat = table.Column<short>(type: "INTEGER", nullable: false),
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
                        name: "FK_PriceLists_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    Code = table.Column<string>(type: "TEXT", nullable: false),
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
                name: "OrdFiches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    LogicalRef = table.Column<int>(type: "INTEGER", nullable: false),
                    ClientCode = table.Column<string>(type: "TEXT", nullable: true),
                    TrCode = table.Column<short>(type: "INTEGER", nullable: false),
                    FicheNo = table.Column<string>(type: "TEXT", nullable: false),
                    Total = table.Column<double>(type: "REAL", nullable: false),
                    TotalDiscounted = table.Column<double>(type: "REAL", nullable: false),
                    GrossTotal = table.Column<double>(type: "REAL", nullable: false),
                    TotalVat = table.Column<double>(type: "REAL", nullable: false),
                    Date_ = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Docode = table.Column<string>(type: "TEXT", nullable: true),
                    Send = table.Column<byte>(type: "INTEGER", nullable: false),
                    ApprovingUser = table.Column<Guid>(type: "TEXT", nullable: true),
                    Active = table.Column<bool>(type: "INTEGER", nullable: false),
                    Explanation = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    CurrencyId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CompanyId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreateUser = table.Column<Guid>(type: "TEXT", nullable: false),
                    UpdateUser = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdFiches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrdFiches_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdFiches_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrdFiches_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
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
                    VirtualPosId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CardPrefix = table.Column<string>(type: "TEXT", nullable: true),
                    CardHolderName = table.Column<string>(type: "TEXT", nullable: true),
                    Installment = table.Column<int>(type: "INTEGER", nullable: false),
                    ExtraInstallment = table.Column<int>(type: "INTEGER", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    BankErrorMessage = table.Column<string>(type: "TEXT", nullable: true),
                    PaidDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    StatusId = table.Column<int>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    BankRequest = table.Column<string>(type: "TEXT", nullable: true),
                    BankResponse = table.Column<string>(type: "TEXT", nullable: true),
                    MaskedCardNumber = table.Column<string>(type: "TEXT", nullable: true),
                    Explanation = table.Column<string>(type: "TEXT", nullable: true),
                    CompanyId = table.Column<Guid>(type: "TEXT", nullable: false),
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
                        name: "FK_PaymentTransactions_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaymentTransactions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaymentTransactions_VirtualPos_VirtualPosId",
                        column: x => x.VirtualPosId,
                        principalTable: "VirtualPos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShipAdresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    SpeCode = table.Column<string>(type: "TEXT", nullable: false),
                    Address1 = table.Column<string>(type: "TEXT", nullable: false),
                    Address2 = table.Column<string>(type: "TEXT", nullable: false),
                    City = table.Column<string>(type: "TEXT", nullable: false),
                    Country = table.Column<string>(type: "TEXT", nullable: false),
                    Phone = table.Column<string>(type: "TEXT", nullable: false),
                    PostalCode = table.Column<string>(type: "TEXT", nullable: false),
                    Town = table.Column<string>(type: "TEXT", nullable: false),
                    District = table.Column<string>(type: "TEXT", nullable: false),
                    Inchange = table.Column<string>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreateUser = table.Column<Guid>(type: "TEXT", nullable: false),
                    UpdateUser = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipAdresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShipAdresses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
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
                name: "OrdLine",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Logicalref = table.Column<int>(type: "INTEGER", nullable: false),
                    StockRef = table.Column<int>(type: "INTEGER", nullable: false),
                    ClientRef = table.Column<int>(type: "INTEGER", nullable: false),
                    LineType = table.Column<short>(type: "INTEGER", nullable: false),
                    LineNo = table.Column<short>(type: "INTEGER", nullable: false),
                    TrCode = table.Column<short>(type: "INTEGER", nullable: false),
                    Date_ = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Amount = table.Column<double>(type: "REAL", nullable: false),
                    Price = table.Column<double>(type: "REAL", nullable: false),
                    Total = table.Column<double>(type: "REAL", nullable: false),
                    Discper = table.Column<double>(type: "REAL", nullable: false),
                    Distdisc = table.Column<double>(type: "REAL", nullable: false),
                    Vat = table.Column<double>(type: "REAL", nullable: false),
                    VatAmnt = table.Column<double>(type: "REAL", nullable: false),
                    VatMatrah = table.Column<double>(type: "REAL", nullable: false),
                    UomRef = table.Column<double>(type: "REAL", nullable: false),
                    UsRef = table.Column<double>(type: "REAL", nullable: false),
                    Unit = table.Column<string>(type: "TEXT", nullable: true),
                    AvailableStock = table.Column<double>(type: "REAL", nullable: false),
                    ProductId = table.Column<Guid>(type: "TEXT", nullable: false),
                    OrdFicheId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CurrencyId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreateUser = table.Column<Guid>(type: "TEXT", nullable: false),
                    UpdateUser = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdLine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrdLine_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrdLine_OrdFiches_OrdFicheId",
                        column: x => x.OrdFicheId,
                        principalTable: "OrdFiches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdLine_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "CurrencyName", "CurrencySymbol" },
                values: new object[] { 1, "Türk Lirası", "TL" });

            migrationBuilder.InsertData(
                table: "DocumentNo",
                columns: new[] { "DocType", "DocNo", "Prefix" },
                values: new object[] { 1, 1, "" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "CreateUser", "RoleName", "UpdateDate", "UpdateUser" },
                values: new object[,]
                {
                    { new Guid("361080bc-cdc5-411c-bab2-e55983a0c952"), new DateTime(2025, 1, 27, 17, 47, 8, 178, DateTimeKind.Local).AddTicks(7777), new Guid("b6958f05-df58-4c21-a698-fe5aff764df3"), "Sales", new DateTime(2025, 1, 27, 17, 47, 8, 178, DateTimeKind.Local).AddTicks(7778), new Guid("b6958f05-df58-4c21-a698-fe5aff764df3") },
                    { new Guid("3ff11bb0-e84b-4ad0-97d7-564f6da064a1"), new DateTime(2025, 1, 27, 17, 47, 8, 178, DateTimeKind.Local).AddTicks(7777), new Guid("b6958f05-df58-4c21-a698-fe5aff764df3"), "Bank", new DateTime(2025, 1, 27, 17, 47, 8, 178, DateTimeKind.Local).AddTicks(7777), new Guid("b6958f05-df58-4c21-a698-fe5aff764df3") },
                    { new Guid("5214148c-b160-49ec-89de-4e0fe37c8299"), new DateTime(2025, 1, 27, 17, 47, 8, 178, DateTimeKind.Local).AddTicks(7772), new Guid("b6958f05-df58-4c21-a698-fe5aff764df3"), "B2C", new DateTime(2025, 1, 27, 17, 47, 8, 178, DateTimeKind.Local).AddTicks(7772), new Guid("b6958f05-df58-4c21-a698-fe5aff764df3") },
                    { new Guid("8b49a3a5-f378-49ab-988d-c3695af90f72"), new DateTime(2025, 1, 27, 17, 47, 8, 178, DateTimeKind.Local).AddTicks(7774), new Guid("b6958f05-df58-4c21-a698-fe5aff764df3"), "Payment", new DateTime(2025, 1, 27, 17, 47, 8, 178, DateTimeKind.Local).AddTicks(7774), new Guid("b6958f05-df58-4c21-a698-fe5aff764df3") },
                    { new Guid("ac2527f1-5769-42d8-9710-9076ca347c5d"), new DateTime(2025, 1, 27, 17, 47, 8, 178, DateTimeKind.Local).AddTicks(7776), new Guid("b6958f05-df58-4c21-a698-fe5aff764df3"), "UserReport", new DateTime(2025, 1, 27, 17, 47, 8, 178, DateTimeKind.Local).AddTicks(7776), new Guid("b6958f05-df58-4c21-a698-fe5aff764df3") },
                    { new Guid("b86d9b61-1972-4c8f-84e4-e228c00d8e6c"), new DateTime(2025, 1, 27, 17, 47, 8, 178, DateTimeKind.Local).AddTicks(7773), new Guid("b6958f05-df58-4c21-a698-fe5aff764df3"), "Managment", new DateTime(2025, 1, 27, 17, 47, 8, 178, DateTimeKind.Local).AddTicks(7773), new Guid("b6958f05-df58-4c21-a698-fe5aff764df3") },
                    { new Guid("be9901da-f04f-44cc-8129-f39921ad6500"), new DateTime(2025, 1, 27, 17, 47, 8, 178, DateTimeKind.Local).AddTicks(7764), new Guid("b6958f05-df58-4c21-a698-fe5aff764df3"), "User", new DateTime(2025, 1, 27, 17, 47, 8, 178, DateTimeKind.Local).AddTicks(7764), new Guid("b6958f05-df58-4c21-a698-fe5aff764df3") },
                    { new Guid("d05e1bb3-3383-4cb8-bc53-644b7bee8c1f"), new DateTime(2025, 1, 27, 17, 47, 8, 178, DateTimeKind.Local).AddTicks(7751), new Guid("b6958f05-df58-4c21-a698-fe5aff764df3"), "Admin", new DateTime(2025, 1, 27, 17, 47, 8, 178, DateTimeKind.Local).AddTicks(7758), new Guid("b6958f05-df58-4c21-a698-fe5aff764df3") },
                    { new Guid("f52cbb25-2911-4274-8118-34a45a382cdd"), new DateTime(2025, 1, 27, 17, 47, 8, 178, DateTimeKind.Local).AddTicks(7775), new Guid("b6958f05-df58-4c21-a698-fe5aff764df3"), "Dashboard", new DateTime(2025, 1, 27, 17, 47, 8, 178, DateTimeKind.Local).AddTicks(7775), new Guid("b6958f05-df58-4c21-a698-fe5aff764df3") }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccountCode", "AccountName", "Active", "CompanyId", "CreateDate", "CreateUser", "Discount", "Email", "Password", "UpdateDate", "UpdateUser", "Username" },
                values: new object[] { new Guid("b6958f05-df58-4c21-a698-fe5aff764df3"), "", null, null, null, new DateTime(2025, 1, 27, 17, 47, 8, 178, DateTimeKind.Local).AddTicks(7793), new Guid("b6958f05-df58-4c21-a698-fe5aff764df3"), null, "murat@ulkubilgisayar.com", "Admin", new DateTime(2025, 1, 27, 17, 47, 8, 178, DateTimeKind.Local).AddTicks(7793), new Guid("b6958f05-df58-4c21-a698-fe5aff764df3"), "Admin" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId", "CreateDate", "CreateUser", "Id", "UpdateDate", "UpdateUser" },
                values: new object[] { new Guid("d05e1bb3-3383-4cb8-bc53-644b7bee8c1f"), new Guid("b6958f05-df58-4c21-a698-fe5aff764df3"), new DateTime(2025, 1, 27, 17, 47, 8, 178, DateTimeKind.Local).AddTicks(7799), new Guid("b6958f05-df58-4c21-a698-fe5aff764df3"), new Guid("c737c015-1146-469d-8fbe-ef83f7a3699a"), new DateTime(2025, 1, 27, 17, 47, 8, 178, DateTimeKind.Local).AddTicks(7800), new Guid("b6958f05-df58-4c21-a698-fe5aff764df3") });

            migrationBuilder.CreateIndex(
                name: "IX_BankCards_BankCode",
                table: "BankCards",
                column: "BankCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CardBrands_Code",
                table: "CardBrands",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Code",
                table: "Categories",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CharAsgns_CharCodeId",
                table: "CharAsgns",
                column: "CharCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_CharAsgns_CharValId",
                table: "CharAsgns",
                column: "CharValId");

            migrationBuilder.CreateIndex(
                name: "IX_CharAsgns_Code",
                table: "CharAsgns",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CharAsgns_ProductId",
                table: "CharAsgns",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CharCodes_Code",
                table: "CharCodes",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CharSets_Code",
                table: "CharSets",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CharVals_CharCodeId",
                table: "CharVals",
                column: "CharCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_CharVals_Code",
                table: "CharVals",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CreditCardInstallments_CreditCardId",
                table: "CreditCardInstallments",
                column: "CreditCardId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditCardPrefixes_Prefix",
                table: "CreditCardPrefixes",
                column: "Prefix",
                unique: true);

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
                name: "IX_FirmParams_No",
                table: "FirmParams",
                column: "No",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrdFiches_CompanyId",
                table: "OrdFiches",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdFiches_CurrencyId",
                table: "OrdFiches",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdFiches_UserId",
                table: "OrdFiches",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdLine_CurrencyId",
                table: "OrdLine",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdLine_OrdFicheId",
                table: "OrdLine",
                column: "OrdFicheId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdLine_ProductId",
                table: "OrdLine",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransactions_CompanyId",
                table: "PaymentTransactions",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransactions_UserId",
                table: "PaymentTransactions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransactions_VirtualPosId",
                table: "PaymentTransactions",
                column: "VirtualPosId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceLists_CurrencyId",
                table: "PriceLists",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceLists_ProductId",
                table: "PriceLists",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAmounts_ProductId",
                table: "ProductAmounts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CharSetId",
                table: "Products",
                column: "CharSetId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Code",
                table: "Products",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShipAdresses_Code",
                table: "ShipAdresses",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShipAdresses_UserId",
                table: "ShipAdresses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CompanyId",
                table: "Users",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VirtualPos_BankCardId",
                table: "VirtualPos",
                column: "BankCardId");

            migrationBuilder.CreateIndex(
                name: "IX_VirtualPosParameters_VirtualPosId",
                table: "VirtualPosParameters",
                column: "VirtualPosId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "CharAsgns");

            migrationBuilder.DropTable(
                name: "Clients");

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
                name: "OrdLine");

            migrationBuilder.DropTable(
                name: "PaymentTransactions");

            migrationBuilder.DropTable(
                name: "PriceLists");

            migrationBuilder.DropTable(
                name: "ProductAmounts");

            migrationBuilder.DropTable(
                name: "ShipAdresses");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "VirtualPosParameters");

            migrationBuilder.DropTable(
                name: "CharVals");

            migrationBuilder.DropTable(
                name: "CreditCards");

            migrationBuilder.DropTable(
                name: "OrdFiches");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "VirtualPos");

            migrationBuilder.DropTable(
                name: "CharCodes");

            migrationBuilder.DropTable(
                name: "CardBrands");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "CharSets");

            migrationBuilder.DropTable(
                name: "BankCards");

            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
