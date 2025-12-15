using ErrorLibrary.Data;
using ErrorLibrary.Entities;
using ErrorLibrary.Extensions;
using ErrorLibrary.Services;
using ErrorLibrary.Services.IServices;
using ErrorLibrary.SignalR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProductCategoryLibrary.Services.IServices;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContextFactory<AppDbContext>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("STConnect"),
        new MySqlServerVersion(new Version(8, 0, 36)));
});

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("ApiSettings:JwtOptions"));

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IUserRoleService, UserRoleService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddScoped<IErrorService, ErrorService>();
builder.Services.AddScoped<IErrorGroupService, ErrorGroupService>();
builder.Services.AddScoped<IErrorCategoryService, ErrorCategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>();
builder.Services.AddScoped<ISolutionService, SolutionService>();
builder.Services.AddScoped<ISharedService, SharedService>();
builder.Services.AddScoped<IUnitService, UnitService>();
builder.Services.AddScoped<IEnterpriseService, EnterpriseService>();
builder.Services.AddScoped<IFactoryService, FactoryService>();
builder.Services.AddScoped<ILineService, LineService>();
builder.Services.AddScoped<IErrorDetailService, ErrorDetailService>();
builder.Services.AddScoped<IErrorDetailAttachmentService, ErrorDetailAttachmentService>();
builder.Services.AddScoped<IUserOrganizationService, UserOrganizationService>();
builder.Services.AddScoped<IOrganizationService, OrganizationService>();
builder.Services.AddScoped<IInLineService, InLineService>();
builder.Services.AddScoped<IInLineDetailService, InLineDetailService>();
builder.Services.AddScoped<IEndLineService, EndLineService>();
builder.Services.AddScoped<IEndLineDetailService, EndLineDetailService>();
builder.Services.AddScoped<ITimeFrameService, TimeFrameService>();
builder.Services.AddScoped<ITimeFrameColorService, TimeFrameColorService>();

var assemblies = AppDomain.CurrentDomain.GetAssemblies()
    .Where(a => !a.FullName.StartsWith("Microsoft.Data.SqlClient"))
    .ToArray();

builder.Services.AddAutoMapper(cfg => cfg.AddMaps(assemblies));
builder.AddAppAuthetication();
builder.Services.AddSignalR().AddMessagePackProtocol();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}")
    .WithStaticAssets();
app.MapHub<ErrorHub>("/errorHub");

await AddlyMigration();
app.Run();

async Task AddlyMigration()
{
    using (var scope = app.Services.CreateScope())
    {
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

        await DbInitializer.SeedAsync(userManager, roleManager);
        var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        if (_db.Database.GetPendingMigrations().Count() > 0)
        {
            _db.Database.Migrate();
        }
    }
}
