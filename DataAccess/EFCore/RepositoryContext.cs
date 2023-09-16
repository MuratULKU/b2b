using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EFCore
{
    public class RepositoryContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Product> Products { get; set; }
        public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
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
                new { Id = Guid.NewGuid(), RoleName = "User", CreateUser = userId, UpdateUser = userId, CreateDate=DateTime.Now,UpdateDate=DateTime.Now }
                );
            modelBuilder.Entity<User>()
                .HasData(
                new { Id = userId, Username = "Admin", Password = "Admin", Email = "murat@ulkubilgisayar.com", CreateUser = userId, UpdateUser = userId, CreateDate = DateTime.Now, UpdateDate = DateTime.Now }
                );
            modelBuilder.Entity<UserRole>()
                 .HasData(
                new {Id=Guid.NewGuid(), UserId = userId, RoleId = roleId, CreateUser = userId, UpdateUser = userId, CreateDate = DateTime.Now, UpdateDate = DateTime.Now }
            );
            modelBuilder.Entity<Product>()
                .HasData(
                new { Id = Guid.NewGuid(), CreateUser = userId, UpdateUser = userId, CreateDate = DateTime.Now, UpdateDate = DateTime.Now, CODE = "st1", NAME = "1 nolu urun", UNIT = "adet" },
                new { Id = Guid.NewGuid(), CreateUser = userId, UpdateUser = userId, CreateDate = DateTime.Now, UpdateDate = DateTime.Now, CODE = "st2", NAME = "2 nolu urun", UNIT = "adet" },
                new { Id = Guid.NewGuid(), CreateUser = userId, UpdateUser = userId, CreateDate = DateTime.Now, UpdateDate = DateTime.Now, CODE = "st3", NAME = "3 nolu urun", UNIT = "adet" },
                new { Id = Guid.NewGuid(), CreateUser = userId, UpdateUser = userId, CreateDate = DateTime.Now, UpdateDate = DateTime.Now, CODE = "st4", NAME = "4 nolu urun", UNIT = "adet" },
                new { Id = Guid.NewGuid(), CreateUser = userId, UpdateUser = userId, CreateDate = DateTime.Now, UpdateDate = DateTime.Now, CODE = "st5", NAME = "5 nolu urun", UNIT = "adet" },
                new { Id = Guid.NewGuid(), CreateUser = userId, UpdateUser = userId, CreateDate = DateTime.Now, UpdateDate = DateTime.Now, CODE = "st6", NAME = "6 nolu urun", UNIT = "adet" },
                new { Id = Guid.NewGuid(), CreateUser = userId, UpdateUser = userId, CreateDate = DateTime.Now, UpdateDate = DateTime.Now, CODE = "st7", NAME = "7 nolu urun", UNIT = "adet" },
                new { Id = Guid.NewGuid(), CreateUser = userId, UpdateUser = userId, CreateDate = DateTime.Now, UpdateDate = DateTime.Now, CODE = "st8", NAME = "8 nolu urun", UNIT = "adet" },
                new { Id = Guid.NewGuid(), CreateUser = userId, UpdateUser = userId, CreateDate = DateTime.Now, UpdateDate = DateTime.Now, CODE = "st9", NAME = "9 nolu urun", UNIT = "adet" },
                new { Id = Guid.NewGuid(), CreateUser = userId, UpdateUser = userId, CreateDate = DateTime.Now, UpdateDate = DateTime.Now, CODE = "st10", NAME = "10 nolu urun", UNIT = "adet" },
                new { Id = Guid.NewGuid(), CreateUser = userId, UpdateUser = userId, CreateDate = DateTime.Now, UpdateDate = DateTime.Now, CODE = "st11", NAME = "11 nolu urun", UNIT = "adet" },
                new { Id = Guid.NewGuid(), CreateUser = userId, UpdateUser = userId, CreateDate = DateTime.Now, UpdateDate = DateTime.Now, CODE = "st12", NAME = "12 nolu urun", UNIT = "adet" },
                new { Id = Guid.NewGuid(), CreateUser = userId, UpdateUser = userId, CreateDate = DateTime.Now, UpdateDate = DateTime.Now, CODE = "st13", NAME = "13 nolu urun", UNIT = "adet" },
                new { Id = Guid.NewGuid(), CreateUser = userId, UpdateUser = userId, CreateDate = DateTime.Now, UpdateDate = DateTime.Now, CODE = "st14", NAME = "14 nolu urun", UNIT = "adet" },
                new { Id = Guid.NewGuid(), CreateUser = userId, UpdateUser = userId, CreateDate = DateTime.Now, UpdateDate = DateTime.Now, CODE = "st15", NAME = "15 nolu urun", UNIT = "adet" },
                new { Id = Guid.NewGuid(), CreateUser = userId, UpdateUser = userId, CreateDate = DateTime.Now, UpdateDate = DateTime.Now, CODE = "st16", NAME = "16 nolu urun", UNIT = "adet" },
                new { Id = Guid.NewGuid(), CreateUser = userId, UpdateUser = userId, CreateDate = DateTime.Now, UpdateDate = DateTime.Now, CODE = "st17", NAME = "17 nolu urun", UNIT = "adet" },
                new { Id = Guid.NewGuid(), CreateUser = userId, UpdateUser = userId, CreateDate = DateTime.Now, UpdateDate = DateTime.Now, CODE = "st18", NAME = "18 nolu urun", UNIT = "adet" },
                new { Id = Guid.NewGuid(), CreateUser = userId, UpdateUser = userId, CreateDate = DateTime.Now, UpdateDate = DateTime.Now, CODE = "st19", NAME = "19 nolu urun", UNIT = "adet" },
                new { Id = Guid.NewGuid(), CreateUser = userId, UpdateUser = userId, CreateDate = DateTime.Now, UpdateDate = DateTime.Now, CODE = "st20", NAME = "20 nolu urun", UNIT = "adet" },
                new { Id = Guid.NewGuid(), CreateUser = userId, UpdateUser = userId, CreateDate = DateTime.Now, UpdateDate = DateTime.Now, CODE = "st21", NAME = "21 nolu urun", UNIT = "adet" },
                new { Id = Guid.NewGuid(), CreateUser = userId, UpdateUser = userId, CreateDate = DateTime.Now, UpdateDate = DateTime.Now, CODE = "st22", NAME = "22 nolu urun", UNIT = "adet" },
                new { Id = Guid.NewGuid(), CreateUser = userId, UpdateUser = userId, CreateDate = DateTime.Now, UpdateDate = DateTime.Now, CODE = "st23", NAME = "23 nolu urun", UNIT = "adet" },
                new { Id = Guid.NewGuid(), CreateUser = userId, UpdateUser = userId, CreateDate = DateTime.Now, UpdateDate = DateTime.Now, CODE = "st24", NAME = "24 nolu urun", UNIT = "adet" },
                new { Id = Guid.NewGuid(), CreateUser = userId, UpdateUser = userId, CreateDate = DateTime.Now, UpdateDate = DateTime.Now, CODE = "st25", NAME = "25 nolu urun", UNIT = "adet" },
                new { Id = Guid.NewGuid(), CreateUser = userId, UpdateUser = userId, CreateDate = DateTime.Now, UpdateDate = DateTime.Now, CODE = "st26", NAME = "26 nolu urun", UNIT = "adet" },
                new { Id = Guid.NewGuid(), CreateUser = userId, UpdateUser = userId, CreateDate = DateTime.Now, UpdateDate = DateTime.Now, CODE = "st27", NAME = "27 nolu urun", UNIT = "adet" },
                new { Id = Guid.NewGuid(), CreateUser = userId, UpdateUser = userId, CreateDate = DateTime.Now, UpdateDate = DateTime.Now, CODE = "st28", NAME = "28 nolu urun", UNIT = "adet" },
                new { Id = Guid.NewGuid(), CreateUser = userId, UpdateUser = userId, CreateDate = DateTime.Now, UpdateDate = DateTime.Now, CODE = "st29", NAME = "29 nolu urun", UNIT = "adet" },
                new { Id = Guid.NewGuid(), CreateUser = userId, UpdateUser = userId, CreateDate = DateTime.Now, UpdateDate = DateTime.Now, CODE = "st30", NAME = "30 nolu urun", UNIT = "adet" },
                new { Id = Guid.NewGuid(), CreateUser = userId, UpdateUser = userId, CreateDate = DateTime.Now, UpdateDate = DateTime.Now, CODE = "st31", NAME = "31 nolu urun", UNIT = "adet" },
                new { Id = Guid.NewGuid(), CreateUser = userId, UpdateUser = userId, CreateDate = DateTime.Now, UpdateDate = DateTime.Now, CODE = "st32", NAME = "32 nolu urun", UNIT = "adet" },
                new { Id = Guid.NewGuid(), CreateUser = userId, UpdateUser = userId, CreateDate = DateTime.Now, UpdateDate = DateTime.Now, CODE = "st33", NAME = "33 nolu urun", UNIT = "adet" },
                new { Id = Guid.NewGuid(), CreateUser = userId, UpdateUser = userId, CreateDate = DateTime.Now, UpdateDate = DateTime.Now, CODE = "st34", NAME = "34 nolu urun", UNIT = "adet" },
                new { Id = Guid.NewGuid(), CreateUser = userId, UpdateUser = userId, CreateDate = DateTime.Now, UpdateDate = DateTime.Now, CODE = "st35", NAME = "35 nolu urun", UNIT = "adet" },
                new { Id = Guid.NewGuid(), CreateUser = userId, UpdateUser = userId, CreateDate = DateTime.Now, UpdateDate = DateTime.Now, CODE = "st36", NAME = "36 nolu urun", UNIT = "adet" }


                );
        }
    }
}
