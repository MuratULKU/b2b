using Entity;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

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
        public DbSet<OrdLine> OrdLine { get; set; }
        public DbSet<BankCard> BankCards { get; set; }
        public DbSet<VirtualPosParameter> VirtualPosParameters { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<CreditCardInstallment> CreditCardInstallments { get; set; }
        public DbSet<CreditCardPrefix> CreditCardPrefixes { get; set; }
        public DbSet<PaymentTransaction> PaymentTransactions { get; set; }
        public DbSet<VirtualPos> VirtualPos { get; set; }
        public DbSet<CardBrand> CardBrands { get; set; }
        public DbSet<ProductAmount> ProductAmounts { get; set; }
        public DbSet<FirmDoc> FirmDocs { get; set; }
        public DbSet<DocumentNo> DocumentNo { get; set; }
        public DbSet<OrdFiche> OrdFiches { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<CharSet> CharSets { get; set; }
         public DbSet<CharAsgn> CharAsgns { get; set; }
        public DbSet <CharCode> CharCodes { get; set; }
        public DbSet<CharVal> CharVals { get; set; }
        public DbSet<ShipAdress> ShipAdresses { get; set; }
        public DbSet<Client> Clients { get; set; }  
        public DbSet<Company> Companies { get; set; }
        public DbSet<ClFiche> ClFiches { get; set; }
        public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options)
        {
          
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           //optionsBuilder.UseSqlite("Data Source=./database/B2CData.db;Password=29YkM1975** ");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("NOCASE");
            var userId = Guid.NewGuid();
            var roleId = Guid.NewGuid();
            modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
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
                new { Id = Guid.NewGuid(), RoleName = "User", CreateUser = userId, UpdateUser = userId, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
                new { Id = Guid.NewGuid(), RoleName = "B2C", CreateUser = userId, UpdateUser = userId, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
                new { Id = Guid.NewGuid(), RoleName = "Managment", CreateUser = userId, UpdateUser = userId, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
                new { Id = Guid.NewGuid(), RoleName = "Payment", CreateUser = userId, UpdateUser = userId, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
                new { Id = Guid.NewGuid(), RoleName = "Dashboard", CreateUser = userId, UpdateUser = userId, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
                new { Id = Guid.NewGuid(), RoleName = "UserReport", CreateUser = userId, UpdateUser = userId, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
                new { Id = Guid.NewGuid(), RoleName = "Bank", CreateUser = userId, UpdateUser = userId, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
                new { Id = Guid.NewGuid(), RoleName = "Sales", CreateUser = userId, UpdateUser = userId, CreateDate = DateTime.Now, UpdateDate = DateTime.Now }
                );
            modelBuilder.Entity<User>()
                .HasData(
                new { Id = userId, Username = "Admin", Password = "Admin", Email = "murat@ulkubilgisayar.com", AccountCode = "",BankDetail = false, Dashboard =false, Product=false, Payment =false, Managment=false,CreateUser = userId, UpdateUser = userId, CreateDate = DateTime.Now, UpdateDate = DateTime.Now }
                );
            modelBuilder.Entity<UserRole>()
                 .HasData(
                new { Id = Guid.NewGuid(), UserId = userId, RoleId = roleId, CreateUser = userId, UpdateUser = userId, CreateDate = DateTime.Now, UpdateDate = DateTime.Now }
            );
            modelBuilder.Entity<Currency>()
                .HasData(
                new { Id = 1, CurrencySymbol = "TL", CurrencyName = "Türk Lirası" }
                );
            modelBuilder.Entity<FirmParam>().HasKey(no => no.Key);
            modelBuilder.Entity<DocumentNo>().HasKey(type => type.DocType);
            modelBuilder.Entity<DocumentNo>().HasData(new { DocType = 1,Prefix = "", DocNo=1 });
            modelBuilder.Entity<Product>().Property(x => x.Name).HasColumnType("TEXT COLLATE NOCASE");
            modelBuilder.Entity<Product>().Property(x => x.Code).HasColumnType("TEXT COLLATE NOCASE");

            modelBuilder.Entity<CreditCardPrefix>().HasIndex(x=> new {x.Prefix }).IsUnique();
            modelBuilder.Entity<User>().HasIndex(x=>x.Username).IsUnique();
            
            modelBuilder.Entity<User>().HasIndex(x=>x.Email).IsUnique();
            modelBuilder.Entity<BankCard>().HasIndex(x => x.BankCode).IsUnique();
            modelBuilder.Entity<CardBrand>().HasIndex(x=>x.Code).IsUnique();
            modelBuilder.Entity<Category>().HasIndex(x => x.Code).IsUnique();
            modelBuilder.Entity<CharAsgn>().HasIndex(x=>x.Code).IsUnique(); 
            modelBuilder.Entity<CharCode>().HasIndex(x=>x.Code).IsUnique();
            modelBuilder.Entity<CharSet>().HasIndex(x=>x.Code).IsUnique();
            modelBuilder.Entity<CharVal>().HasIndex(x=>x.Code).IsUnique();
            modelBuilder.Entity<FirmParam>().HasIndex(x=>x.No).IsUnique();
           // modelBuilder.Entity<OrdFiche>().HasIndex(x=> new {x.TrCode,x.FicheNo}).IsUnique();
            modelBuilder.Entity<OrdFiche>().HasOne(x =>x.Currency).WithMany().HasForeignKey(x=>x.CurrencyId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<OrdLine>().HasOne(x=>x.Currency).WithMany().HasForeignKey(x=>x.CurrencyId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<OrdLine>().HasOne(x => x.Product).WithMany().HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Product>().HasIndex(x => x.Code).IsUnique();
           
            modelBuilder.Entity<ShipAdress>().HasIndex(x => x.Code).IsUnique();

        }
    }
}
