using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using TPshop.Model.Models;

namespace TPshop.Data
{
    public class TPshopDbContext : IdentityDbContext<ApplicationUser>
    {
        public TPshopDbContext() : base("TPshopDbContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Order> Orders { set; get; }
        public DbSet<OrderDetail> OrderDetails { set; get; }
        public DbSet<Product> Products { set; get; }
        public DbSet<VisitorStatistic> VisitorStatistics { set; get; }
        public DbSet<Category> Categories { set; get; }
        public DbSet<CategoryGroup> CategoryGroups { set; get; }
        public DbSet<Setting> Settings { set; get; }
        public DbSet<Supplier> Suppliers { set; get; }
        public DbSet<ApplicationGroup> ApplicationGroups { set; get; }
        public DbSet<ApplicationRole> ApplicationRoles { set; get; }
        public DbSet<ApplicationRoleGroup> ApplicationRoleGroups { set; get; }
        public DbSet<ApplicationUserGroup> ApplicationUserGroups { set; get; }

        public DbSet<Error> Errors { set; get; }
        public DbSet<ContactDetail> ContactDetails { set; get; }        
        public DbSet<Feedback> Feedbacks { set; get; }

        public static TPshopDbContext Create()
        {
            return new TPshopDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Entity<IdentityUserRole>().HasKey(i => new { i.UserId, i.RoleId }).ToTable("ApplicationUserRoles");
            builder.Entity<IdentityUserLogin>().HasKey(i => i.UserId).ToTable("ApplicationUserLogins");
            builder.Entity<IdentityRole>().ToTable("ApplicationRoles");
            builder.Entity<IdentityUserClaim>().HasKey(i => i.UserId).ToTable("ApplicationUserClaims");
        }
    }
}