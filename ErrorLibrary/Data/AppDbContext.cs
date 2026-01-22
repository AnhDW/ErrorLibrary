using ErrorLibrary.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ErrorLibrary.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<ApplicationRole> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Solution> Solutions { get; set; }
        public DbSet<Error> Errors { get; set; }
        public DbSet<ErrorGroup> ErrorGroups { get; set; }
        public DbSet<ErrorCategory> ErrorCategories { get; set; }

        public DbSet<Line> Lines { get; set; }
        public DbSet<Enterprise> Enterprises { get; set; }
        public DbSet<Factory> Factories { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<ErrorDetail> ErrorDetails { get; set; }
        public DbSet<ErrorDetailAttachment> ErrorDetailAttachments { get; set; }
        public DbSet<UserOrganization> UserOrganizations { get; set; }

        public DbSet<TimeFrame> TimeFrames { get; set; }
        public DbSet<TimeFrameColor> TimeFrameColors { get; set; }
        public DbSet<InLine> InLines { get; set; }
        public DbSet<InLineDetail> InLineDetails { get; set; }

        public DbSet<EndLine> EndLines { get; set; }
        public DbSet<EndLineDetail> EndLineDetails { get; set; }

        public DbSet<Customer> Customers { get; set; }// khách hàng
        public DbSet<Style> Styles { get; set; } // mã hàng
        public DbSet<Defect> Defects { get; set; }// lỗi

        //Report Final Factory
        public DbSet<ReportFinalFactory> ReportFinalFactories { get; set; }
        public DbSet<ReportFinalFactoryDetail> ReportFinalFactoryDetails { get; set; }
        public DbSet<Inspection> Inspections { get; set; }
        public DbSet<InspectionRound> InspectionRounds { get; set; }
        public DbSet<InspectionDefect> InspectionDefects { get; set; }

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

            builder.Entity<Error>()
                .HasOne(x=>x.ErrorCategory)
                .WithMany(x=>x.Errors)
                .HasForeignKey(x=>x.ErrorCategoryId);

            builder.Entity<Error>()
                .HasOne(x => x.ProductCategory)
                .WithMany(x => x.Errors)
                .HasForeignKey(x => x.ProductCategoryId);

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

            builder.Entity<ErrorDetail>()
                .HasKey(x => new { x.LineId, x.ProductId, x.ErrorId, x.UserId });

            builder.Entity<ErrorDetail>()
                .HasOne(x=>x.Line)
                .WithMany(x=>x.ErrorDetails)
                .HasForeignKey(x=>x.LineId);

            builder.Entity<ErrorDetail>()
                .HasOne(x => x.Product)
                .WithMany(x => x.ErrorDetails)
                .HasForeignKey(x => x.ProductId);

            builder.Entity<ErrorDetail>()
                .HasOne(x => x.Error)
                .WithMany(x => x.ErrorDetails)
                .HasForeignKey(x => x.ErrorId);

            builder.Entity<ErrorDetail>()
                .HasOne(x => x.User)
                .WithMany(x => x.ErrorDetails)
                .HasForeignKey(x => x.UserId);

            builder.Entity<ErrorDetailAttachment>()
                .HasOne(x => x.ErrorDetail)
                .WithMany(x => x.ErrorDetailAttachments)
                .HasForeignKey(x => new { x.LineId, x.ProductId, x.ErrorId, x.UserId });

            builder.Entity<UserOrganization>()
                .HasKey(x => new { x.UserId, x.OrganizationType, x.OrganizationId });

            builder.Entity<UserOrganization>()
                .HasOne(x => x.User)
                .WithMany(x => x.UserOrganizations)
                .HasForeignKey(x => x.UserId);

            builder.Entity<TimeFrameColor>()
                .HasOne(x => x.TimeFrame)
                .WithMany(x => x.TimeFrameColors)
                .HasForeignKey(x => x.TimeFrameId);

            builder.Entity<InLine>()
                .HasOne(x => x.Line)
                .WithMany(x => x.InLines)
                .HasForeignKey(x => x.LineId);

            builder.Entity<InLine>()
                .HasOne(x => x.Product)
                .WithMany(x => x.InLines)
                .HasForeignKey(x => x.ProductId);

            builder.Entity<InLine>()
                .HasOne(x => x.User)
                .WithMany(x => x.InLines)
                .HasForeignKey(x => x.UserId);

            builder.Entity<InLineDetail>()
                .HasOne(x=>x.TimeFrame)
                .WithMany(x=>x.InLineDetails)
                .HasForeignKey(x=>x.TimeFrameId);

            builder.Entity<InLineDetail>()
                .HasOne(x => x.InLine)
                .WithMany(x => x.InLineDetails)
                .HasForeignKey(x => x.InLineId);

            builder.Entity<InLineDetail>()
                .HasOne(x => x.Error)
                .WithMany(x => x.InLineDetails)
                .HasForeignKey(x => x.ErrorId);

            builder.Entity<EndLine>()
                .HasOne(x => x.Line)
                .WithMany(x => x.EndLines)
                .HasForeignKey(x => x.LineId);

            builder.Entity<EndLine>()
                .HasOne(x => x.Product)
                .WithMany(x => x.EndLines)
                .HasForeignKey(x => x.ProductId);

            builder.Entity<EndLineDetail>()
                .HasOne(x => x.EndLine)
                .WithMany(x => x.EndLineDetails)
                .HasForeignKey(x => x.EndLineId);

            builder.Entity<EndLineDetail>()
                .HasOne(x => x.Error)
                .WithMany(x => x.EndLineDetails)
                .HasForeignKey(x => x.ErrorId);

            builder.Entity<EndLineDetail>()
                .HasOne(x => x.User)
                .WithMany(x => x.EndLineDetails)
                .HasForeignKey(x => x.UserId);

            builder.Entity<RolePermission>()
                .HasKey(x => new { x.RoleId, x.PermissionId });

            builder.Entity<RolePermission>()
                .HasOne(x => x.Role)
                .WithMany(x => x.RolePermissions)
                .HasForeignKey(x => x.RoleId);

            builder.Entity<RolePermission>()
                .HasOne(x => x.Permission)
                .WithMany(x => x.RolePermissions)
                .HasForeignKey(x => x.PermissionId);
            

            builder.Entity<ReportFinalFactory>()
                .HasOne(x => x.Factory)
                .WithMany(x => x.ReportFinalFactories)
                .HasForeignKey(x => x.FactoryId);

            builder.Entity<ReportFinalFactoryDetail>()
                .HasOne(x=>x.Customer)
                .WithMany(x=>x.ReportFinalFactoryDetails)
                .HasForeignKey(x => x.CustomerId);

            builder.Entity<ReportFinalFactoryDetail>()
                .HasOne(x => x.Style)
                .WithMany(x => x.ReportFinalFactoryDetails)
                .HasForeignKey(x => x.StyleId);

            builder.Entity<ReportFinalFactoryDetail>()
                .HasOne(x => x.ReportFinalFactory)
                .WithMany(x => x.ReportFinalFactoryDetails)
                .HasForeignKey(x => x.ReportFinalFactoryId);

            builder.Entity<Inspection>()
                .HasOne(x => x.ReportFinalFactoryDetail)
                .WithMany(x => x.Inspections)
                .HasForeignKey(x => x.ReportFinalFactoryDetailId);

            builder.Entity<InspectionDefect>()
                .HasOne(x => x.ReportFinalFactoryDetail)
                .WithMany(x => x.InspectionDefects)
                .HasForeignKey(x => x.ReportFinalFactoryDetailId);
            
            builder.Entity<InspectionDefect>()
                .HasOne(x => x.Defect)
                .WithMany(x => x.InspectionDefects)
                .HasForeignKey(x => x.DefectId);

            builder.Entity<InspectionDefect>()
                .HasKey(x => new { x.ReportFinalFactoryDetailId, x.DefectId });

            builder.Entity<InspectionRound>()
                .HasOne(x => x.Inspection)
                .WithMany(x => x.InspectionRounds)
                .HasForeignKey(x => x.InspectionId);

            //unique index
            builder.Entity<InLine>()
                .HasIndex(x => new { x.LineId, x.ProductId, x.UserId, x.Date })
                .IsUnique();

            builder.Entity<InLineDetail>()
                .HasIndex(x => new { x.InLineId, x.TimeFrameId, x.ErrorId })
                .IsUnique();

            builder.Entity<EndLine>()
                .HasIndex(x => new { x.LineId, x.ProductId, x.Date })
                .IsUnique();

            builder.Entity<EndLineDetail>()
                .HasIndex(x => new { x.EndLineId, x.ErrorId, x.UserId, x.CreatedAt })
                .IsUnique();

            builder.Entity<Unit>()
                .HasIndex(x => x.Name)
                .IsUnique();

            builder.Entity<Factory>()
                .HasIndex(x => new { x.Name, x.UnitId })
                .IsUnique();

            builder.Entity<Enterprise>()
                .HasIndex(x => new { x.Name, x.FactoryId })
                .IsUnique();

            builder.Entity<Line>()
                .HasIndex(x => new { x.Name, x.EnterpriseId })
                .IsUnique();

            builder.Entity<Error>()
                .HasIndex(x => x.Code)
                .IsUnique();

            builder.Entity<Error>()
                .HasIndex(x => new { x.ErrorGroupId, x.ErrorCategoryId, x.ProductCategoryId, x.Name })
                .IsUnique();

            builder.Entity<ErrorGroup>()
                .HasIndex(x => x.Name)
                .IsUnique();

            builder.Entity<ErrorCategory>()
                .HasIndex(x => x.Name)
                .IsUnique();

            builder.Entity<Product>()
                .HasIndex(x => x.Code)
                .IsUnique();

            builder.Entity<ProductCategory>()
                .HasIndex(x => x.Name)
                .IsUnique();

            builder.Entity<TimeFrame>()
                .HasIndex(x => x.Name)
                .IsUnique();

            builder.Entity<TimeFrame>()
                .HasIndex(x => new { x.StartTime, x.EndTime })
                .IsUnique();

            builder.Entity<TimeFrameColor>()
                .HasIndex(x => new { x.TimeFrameId, x.HexCode })
                .IsUnique();

        }

    }
}
