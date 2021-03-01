using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace RoleApiClientIdentity.EF
{
    public class RoleUserDbContext : IdentityDbContext<APPLICATION_USER>
    {
        public RoleUserDbContext() : base("RoleUserConnection")
        {
            this.Database.CreateIfNotExists();
        }

        public static RoleUserDbContext Create()
        {
            return new RoleUserDbContext();
        }

        public DbSet<APPLICATION_GROUP> APPLICATION_GROUPS { get; set; }
        public DbSet<APPLICATION_MODULE> APPLICATION_MODULES { get; set; }
        public DbSet<APPLICATION_ROLE_GROUP> APPLICATION_ROLE_GROUPS { get; set; }
        public DbSet<APPLICATION_USER_GROUP> APPLICATION_USER_GROUPS { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserRole>().HasKey(i => new { i.UserId, i.RoleId }).ToTable("APPLICATION_USER_ROLES");
            modelBuilder.Entity<IdentityUserLogin>().HasKey(i => i.UserId).ToTable("APPLICATION_USER_LOGINS");
            modelBuilder.Entity<IdentityRole>().ToTable("APPLICATION_ROLES");
            modelBuilder.Entity<IdentityUserClaim>().HasKey(i => i.UserId).ToTable("APPLICATION_USER_CLAIMS");
        }

    }
}