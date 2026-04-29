using FluentValidation;
using Inventra.Application.Common.Mappings;
using Inventra.Application.Interfaces;
using Inventra.Application.Inventories.Commands.CreateInventory;
using Inventra.Domain.Interfaces;
using Inventra.Infrastructure.Persistence;
using Inventra.Infrastructure.Repositories;
using Inventra.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CloudinaryDotNet;
using Inventra.Domain.Entities;

namespace Inventra.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedEmail = false;
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireUppercase = true;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            //builder.Services.AddAuthentication()
            //    .AddGoogle(options =>
            //    {
            //        options.ClientId = builder.Configuration["Authentication:Google:ClientId"]!;
            //        options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"]!;
            //    })
            //    .AddFacebook(options =>
            //    {
            //        options.AppId = builder.Configuration["Authentication:Facebook:AppId"]!;
            //        options.AppSecret = builder.Configuration["Authentication:Facebook:AppSecret"]!;
            //    });

            var googleClientId = builder.Configuration["Authentication:Google:ClientId"];
            var facebookAppId = builder.Configuration["Authentication:Facebook:AppId"];

            if (!string.IsNullOrEmpty(googleClientId))
            {
                builder.Services.AddAuthentication()
                    .AddGoogle(options =>
                    {
                        options.ClientId = googleClientId;
                        options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"]!;
                    });
            }

            if (!string.IsNullOrEmpty(facebookAppId))
            {
                builder.Services.AddAuthentication()
                    .AddFacebook(options =>
                    {
                        options.AppId = facebookAppId;
                        options.AppSecret = builder.Configuration["Authentication:Facebook:AppSecret"]!;
                    });
            }

            builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());

            builder.Services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(
                        typeof(CreateInventoryCommand).Assembly));

            builder.Services.AddValidatorsFromAssembly(
                typeof(CreateInventoryCommand).Assembly);

            builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
            builder.Services.AddScoped<IItemRepository, ItemRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            var cloudName = builder.Configuration["Cloudinary:CloudName"];
            if (!string.IsNullOrEmpty(cloudName))
            {
                var cloudinaryAccount = new Account(
                    cloudName,
                    builder.Configuration["Cloudinary:ApiKey"],
                    builder.Configuration["Cloudinary:ApiSecret"]);
                builder.Services.AddSingleton(new Cloudinary(cloudinaryAccount));
                builder.Services.AddScoped<CloudStorageService>();
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

            app.Run();
        }
    }
}
