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

        public DbSet<Line> Lines { get; set; }
        public DbSet<Enterprise> Enterprises { get; set; }
        public DbSet<Factory> Factories { get; set; }
        public DbSet<Unit> Units { get; set; }

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

            builder.Entity<Line>()
                .HasOne(x=>x.Enterprise)
                .WithMany(x=>x.Lines)
                .HasForeignKey(x=>x.EnterpriseId);

            builder.Entity<Enterprise>()
                .HasOne(x=>x.Factory)
                .WithMany(x=>x.Enterprises)
                .HasForeignKey(x=>x.FactoryId);

            builder.Entity<Factory>()
                .HasOne(x=>x.Unit)
                .WithMany(x=>x.Factories)
                .HasForeignKey(x=>x.UnitId);
        }

    }
}
