using Inventra.Application;
using Inventra.Application.Interfaces;
using Inventra.Domain.Entities;
using Inventra.Infrastructure;
using Inventra.Infrastructure.Persistence;
using Inventra.Web.Hubs;
using Inventra.Web.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Inventra.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddSignalR();

            builder.Services.AddApplication();
            builder.Services.AddInfrastructure(builder.Configuration);

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedEmail = false;
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireUppercase = true;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            var googleClientId = builder.Configuration["Authentication:Google:ClientId"];
            var githubClientId = builder.Configuration["Authentication:GitHub:ClientId"];

            if (!string.IsNullOrEmpty(googleClientId))
            {
                builder.Services.AddAuthentication()
                    .AddGoogle(options =>
                    {
                        options.ClientId = googleClientId;
                        options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"]!;
                    });
            }

            if (!string.IsNullOrEmpty(githubClientId))
            {
                builder.Services.AddAuthentication()
                    .AddGitHub(options =>
                    {
                        options.ClientId = builder.Configuration["Authentication:GitHub:ClientId"]!;
                        options.ClientSecret = builder.Configuration["Authentication:GitHub:ClientSecret"]!;
                    });
            }

            builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[] { "en", "ru", "kk" };
                options.SetDefaultCulture("en")
                    .AddSupportedCultures(supportedCultures)
                    .AddSupportedUICultures(supportedCultures);
            });

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/Account/Login";
            });

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                db.Database.Migrate();

                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                if (!await roleManager.RoleExistsAsync("Admin"))
                    await roleManager.CreateAsync(new IdentityRole("Admin"));

                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var adminEmail = "admin@inventra.com";
                var adminUser = await userManager.FindByEmailAsync(adminEmail);
                if (adminUser == null)
                {
                    adminUser = new ApplicationUser
                    {
                        UserName = "admin",
                        Email = adminEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(adminUser, "Admin1234!");
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseRequestLocalization();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapHub<ChatHub>("/chatHub");

            app.Run();
        }
    }
}
