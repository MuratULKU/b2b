﻿// <auto-generated />
using System;
using DataAccess.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace B2B.Migrations
{
    [DbContext(typeof(RepositoryContext))]
    partial class RepositoryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.11");

            modelBuilder.Entity("Entity.BankCard", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Active")
                        .HasColumnType("INTEGER");

                    b.Property<int>("BankCode")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CreateUser")
                        .HasColumnType("TEXT");

                    b.Property<string>("LogoPath")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("SystemName")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UpdateUser")
                        .HasColumnType("TEXT");

                    b.Property<bool>("UseCommonPaymentPage")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("BankCards");
                });

            modelBuilder.Entity("Entity.BankParameter", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("BankCardId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CreateUser")
                        .HasColumnType("TEXT");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UpdateUser")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("BankCardId");

                    b.ToTable("BankParameters");
                });

            modelBuilder.Entity("Entity.Basket", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<double>("Amount")
                        .HasColumnType("REAL");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CreateUser")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Date_")
                        .HasColumnType("TEXT");

                    b.Property<double>("DiscountPrice")
                        .HasColumnType("REAL");

                    b.Property<double>("DiscountRate")
                        .HasColumnType("REAL");

                    b.Property<int>("LineNUmber")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Price")
                        .HasColumnType("REAL");

                    b.Property<string>("ProductCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ProductDescription")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ProductGuid")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UpdateUser")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserGuid")
                        .HasColumnType("TEXT");

                    b.Property<double>("VatPrice")
                        .HasColumnType("REAL");

                    b.Property<double>("VatRate")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("Baskets");
                });

            modelBuilder.Entity("Entity.CardBrand", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("Code")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CreateUser")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UpdateUser")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("CardBrands");
                });

            modelBuilder.Entity("Entity.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CreateUser")
                        .HasColumnType("TEXT");

                    b.Property<int>("LogicalRef")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Parent")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UpdateUser")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Entity.CreditCard", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Active")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("BankCardId")
                        .HasColumnType("TEXT");

                    b.Property<int>("BrandCode")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("CampaignCard")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("CardBrandId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CreateUser")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Deleted")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("ManufacturerCard")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UpdateUser")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("BankCardId");

                    b.HasIndex("CardBrandId");

                    b.ToTable("CreditCards");
                });

            modelBuilder.Entity("Entity.CreditCardInstallment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Active")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("BankCardId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Business")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("CardBrandId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CreateUser")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CreditCardId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Deleted")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Installment")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("InstallmentRate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UpdateUser")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("BankCardId");

                    b.HasIndex("CardBrandId");

                    b.HasIndex("CreditCardId");

                    b.ToTable("CreditCardInstallments");
                });

            modelBuilder.Entity("Entity.CreditCardPrefix", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Active")
                        .HasColumnType("INTEGER");

                    b.Property<int>("BankCode")
                        .HasColumnType("INTEGER");

                    b.Property<int>("BrandCode")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Business")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CreateUser")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CreditCardId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Deleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Prefix")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UpdateUser")
                        .HasColumnType("TEXT");

                    b.Property<bool>("isInstallment")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CreditCardId");

                    b.ToTable("CreditCardPrefixes");
                });

            modelBuilder.Entity("Entity.FirmParam", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Address1")
                        .HasColumnType("TEXT");

                    b.Property<string>("Address2")
                        .HasColumnType("TEXT");

                    b.Property<string>("City")
                        .HasColumnType("TEXT");

                    b.Property<string>("Country")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CreateUser")
                        .HasColumnType("TEXT");

                    b.Property<int>("FirmId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirmName")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("LastSync")
                        .HasColumnType("TEXT");

                    b.Property<string>("MailAddress")
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone1")
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone2")
                        .HasColumnType("TEXT");

                    b.Property<int>("SyncMinute")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Town")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UpdateUser")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("FirmParams");

                    b.HasData(
                        new
                        {
                            Id = new Guid("9f16798e-503e-4e1f-a30b-d5c3157758f7"),
                            CreateDate = new DateTime(2023, 10, 26, 10, 22, 25, 372, DateTimeKind.Local).AddTicks(470),
                            CreateUser = new Guid("1b570d99-052a-43b4-bbb9-bdac1037de08"),
                            FirmId = 1,
                            SyncMinute = 60,
                            UpdateDate = new DateTime(2023, 10, 26, 10, 22, 25, 372, DateTimeKind.Local).AddTicks(471),
                            UpdateUser = new Guid("1b570d99-052a-43b4-bbb9-bdac1037de08")
                        });
                });

            modelBuilder.Entity("Entity.PaymentTransaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("BankCardId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("BankErrorMessage")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("BankId")
                        .HasColumnType("TEXT");

                    b.Property<string>("BankRequest")
                        .HasColumnType("TEXT");

                    b.Property<string>("BankResponse")
                        .HasColumnType("TEXT");

                    b.Property<string>("CardHolderName")
                        .HasColumnType("TEXT");

                    b.Property<string>("CardPrefix")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClientCode")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CreateUser")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Deleted")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ExtraInstallment")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Installment")
                        .HasColumnType("INTEGER");

                    b.Property<string>("MaskedCardNumber")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("OrderNumber")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("PaidDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("ReferenceNumber")
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<int>("StatusId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("TEXT");

                    b.Property<string>("TransactionNumber")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UpdateUser")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserAgent")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserIpAddress")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("BankId");

                    b.ToTable("PaymentTransactions");
                });

            modelBuilder.Entity("Entity.PriceList", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("Cardref")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Code")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CreateUser")
                        .HasColumnType("TEXT");

                    b.Property<int>("Currency")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<double>("Price")
                        .HasColumnType("REAL");

                    b.Property<int>("Priorty")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ProductCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UpdateUser")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("PriceLists");
                });

            modelBuilder.Entity("Entity.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CreateUser")
                        .HasColumnType("TEXT");

                    b.Property<int>("LogicalRef")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("ParentRef")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ProducerCode")
                        .HasColumnType("TEXT");

                    b.Property<double?>("SellVat")
                        .HasColumnType("REAL");

                    b.Property<string>("SpeCode")
                        .HasColumnType("TEXT");

                    b.Property<string>("SpeCode2")
                        .HasColumnType("TEXT");

                    b.Property<string>("SpeCode3")
                        .HasColumnType("TEXT");

                    b.Property<string>("SpeCode4")
                        .HasColumnType("TEXT");

                    b.Property<string>("SpeCode5")
                        .HasColumnType("TEXT");

                    b.Property<string>("StGrupCode")
                        .HasColumnType("TEXT");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Unit1")
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("Unit1rate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Unit2")
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("Unit2rate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Unit3")
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("Unit3rate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UpdateUser")
                        .HasColumnType("TEXT");

                    b.Property<double?>("Vat")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Entity.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CreateUser")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UpdateUser")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = new Guid("94fc3abd-f2c1-4f93-99f0-5fb423ebcd70"),
                            CreateDate = new DateTime(2023, 10, 26, 10, 22, 25, 372, DateTimeKind.Local).AddTicks(353),
                            CreateUser = new Guid("1b570d99-052a-43b4-bbb9-bdac1037de08"),
                            RoleName = "Admin",
                            UpdateDate = new DateTime(2023, 10, 26, 10, 22, 25, 372, DateTimeKind.Local).AddTicks(366),
                            UpdateUser = new Guid("1b570d99-052a-43b4-bbb9-bdac1037de08")
                        },
                        new
                        {
                            Id = new Guid("5c1ee23c-bc95-4df9-9216-267f68ab1a7a"),
                            CreateDate = new DateTime(2023, 10, 26, 10, 22, 25, 372, DateTimeKind.Local).AddTicks(392),
                            CreateUser = new Guid("1b570d99-052a-43b4-bbb9-bdac1037de08"),
                            RoleName = "User",
                            UpdateDate = new DateTime(2023, 10, 26, 10, 22, 25, 372, DateTimeKind.Local).AddTicks(392),
                            UpdateUser = new Guid("1b570d99-052a-43b4-bbb9-bdac1037de08")
                        });
                });

            modelBuilder.Entity("Entity.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CreateUser")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UpdateUser")
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("1b570d99-052a-43b4-bbb9-bdac1037de08"),
                            CreateDate = new DateTime(2023, 10, 26, 10, 22, 25, 372, DateTimeKind.Local).AddTicks(434),
                            CreateUser = new Guid("1b570d99-052a-43b4-bbb9-bdac1037de08"),
                            Email = "murat@ulkubilgisayar.com",
                            Password = "Admin",
                            UpdateDate = new DateTime(2023, 10, 26, 10, 22, 25, 372, DateTimeKind.Local).AddTicks(435),
                            UpdateUser = new Guid("1b570d99-052a-43b4-bbb9-bdac1037de08"),
                            Username = "Admin"
                        });
                });

            modelBuilder.Entity("Entity.UserRole", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CreateUser")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UpdateUser")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("1b570d99-052a-43b4-bbb9-bdac1037de08"),
                            RoleId = new Guid("94fc3abd-f2c1-4f93-99f0-5fb423ebcd70"),
                            CreateDate = new DateTime(2023, 10, 26, 10, 22, 25, 372, DateTimeKind.Local).AddTicks(450),
                            CreateUser = new Guid("1b570d99-052a-43b4-bbb9-bdac1037de08"),
                            Id = new Guid("41667a37-db9e-48f4-b37a-a7ff3c9ea74b"),
                            UpdateDate = new DateTime(2023, 10, 26, 10, 22, 25, 372, DateTimeKind.Local).AddTicks(450),
                            UpdateUser = new Guid("1b570d99-052a-43b4-bbb9-bdac1037de08")
                        });
                });

            modelBuilder.Entity("Entity.VirtualPos", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("AccountCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("BankCardId")
                        .HasColumnType("TEXT");

                    b.Property<int?>("CardBrands")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CreateUser")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UpdateUser")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("BankCardId");

                    b.ToTable("VirtualPos");
                });

            modelBuilder.Entity("Entity.BankParameter", b =>
                {
                    b.HasOne("Entity.BankCard", "Bank")
                        .WithMany("Parameters")
                        .HasForeignKey("BankCardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bank");
                });

            modelBuilder.Entity("Entity.CreditCard", b =>
                {
                    b.HasOne("Entity.BankCard", "Bank")
                        .WithMany("CreditCards")
                        .HasForeignKey("BankCardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entity.CardBrand", "CardBrand")
                        .WithMany()
                        .HasForeignKey("CardBrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bank");

                    b.Navigation("CardBrand");
                });

            modelBuilder.Entity("Entity.CreditCardInstallment", b =>
                {
                    b.HasOne("Entity.BankCard", "BankCard")
                        .WithMany("Installments")
                        .HasForeignKey("BankCardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entity.CardBrand", "CardBrand")
                        .WithMany()
                        .HasForeignKey("CardBrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entity.CreditCard", "CreditCard")
                        .WithMany("Installments")
                        .HasForeignKey("CreditCardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BankCard");

                    b.Navigation("CardBrand");

                    b.Navigation("CreditCard");
                });

            modelBuilder.Entity("Entity.CreditCardPrefix", b =>
                {
                    b.HasOne("Entity.CreditCard", null)
                        .WithMany("Prefixes")
                        .HasForeignKey("CreditCardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Entity.PaymentTransaction", b =>
                {
                    b.HasOne("Entity.BankCard", "Bank")
                        .WithMany()
                        .HasForeignKey("BankId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bank");
                });

            modelBuilder.Entity("Entity.UserRole", b =>
                {
                    b.HasOne("Entity.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entity.User", "User")
                        .WithMany("UsersRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Entity.VirtualPos", b =>
                {
                    b.HasOne("Entity.BankCard", "BankCard")
                        .WithMany("VirtualPos")
                        .HasForeignKey("BankCardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BankCard");
                });

            modelBuilder.Entity("Entity.BankCard", b =>
                {
                    b.Navigation("CreditCards");

                    b.Navigation("Installments");

                    b.Navigation("Parameters");

                    b.Navigation("VirtualPos");
                });

            modelBuilder.Entity("Entity.CreditCard", b =>
                {
                    b.Navigation("Installments");

                    b.Navigation("Prefixes");
                });

            modelBuilder.Entity("Entity.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Entity.User", b =>
                {
                    b.Navigation("UsersRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
