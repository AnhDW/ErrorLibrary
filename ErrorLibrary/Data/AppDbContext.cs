using ErrorLibrary.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ErrorLibrary.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<ApplicationRole> Roles { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Solution> Solutions { get; set; }
        public DbSet<Error> Errors { get; set; }
        public DbSet<ErrorGroup> ErrorGroups { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Product>()
                .HasOne(x => x.ProductCategory)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.ProductCategoryId);

            builder.Entity<Solution>()
                .HasOne(x=>x.Error)
                .WithMany(x=>x.Solutions)
                .HasForeignKey(x=>x.ErrorId);

            builder.Entity<Error>()
                .HasOne(x=>x.ErrorGroup)
                .WithMany(x=>x.Errors)
                .HasForeignKey(x=>x.ErrorGroupId);
        }

    }
}
