﻿using Entity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EFCore
{
    public class RepositoryContext : DbContext
    {
        
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<PriceList> PriceLists { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<FirmParam> FirmParams { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BankCard> BankCards { get; set; }
        public DbSet<BankParameter> BankParameters { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<CreditCardInstallment> CreditCardInstallments { get; set; }
        public DbSet<CreditCardPrefix> CreditCardPrefixes { get; set; }
        public DbSet<PaymentTransaction> PaymentTransactions { get; set; }
        public DbSet<VirtualPos> VirtualPos { get; set; }
        public DbSet<CardBrand> CardBrands { get; set; }
        public DbSet<ProductAmount> ProductAmounts { get; set; }
        public DbSet<FirmDoc> FirmDocs { get; set; }
        public DbSet<DocumentNo> DocumentNo { get; set; }
        public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options)
        {

        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("NOCASE");
            var userId = Guid.NewGuid();
            var roleId = Guid.NewGuid();
            modelBuilder.Entity<UserRole>().HasKey(
               ur => new { ur.UserId, ur.RoleId }
                );
            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UsersRoles)
                .HasForeignKey(ur => ur.UserId);
            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.RoleId);
            modelBuilder.Entity<Role>()
                .HasData(
                new { Id = roleId, RoleName = "Admin", CreateUser = userId, UpdateUser = userId, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
                new { Id = Guid.NewGuid(), RoleName = "User", CreateUser = userId, UpdateUser = userId, CreateDate = DateTime.Now, UpdateDate = DateTime.Now }
                );
            modelBuilder.Entity<User>()
                .HasData(
                new { Id = userId, Username = "Admin", Password = "Admin", Email = "murat@ulkubilgisayar.com", AccountCode = "" ,CreateUser = userId, UpdateUser = userId, CreateDate = DateTime.Now, UpdateDate = DateTime.Now }
                );
            modelBuilder.Entity<UserRole>()
                 .HasData(
                new { Id = Guid.NewGuid(), UserId = userId, RoleId = roleId, CreateUser = userId, UpdateUser = userId, CreateDate = DateTime.Now, UpdateDate = DateTime.Now }
            );
            modelBuilder.Entity<FirmParam>().HasKey(no=>no.Key);
            modelBuilder.Entity<DocumentNo>().HasKey(type =>type.DocType);
            modelBuilder.Entity<Product>().Property(x => x.Name).HasColumnType("TEXT COLLATE NOCASE");
            modelBuilder.Entity<Product>().Property(x => x.Code).HasColumnType("TEXT COLLATE NOCASE");
        }
    }
}
