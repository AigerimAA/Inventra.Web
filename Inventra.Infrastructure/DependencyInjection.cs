using Inventra.Application.Interfaces;
using Inventra.Domain.Interfaces;
using Inventra.Infrastructure.Persistence;
using Inventra.Infrastructure.Repositories;
using Inventra.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CloudinaryDotNet;

namespace Inventra.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IInventoryRepository, InventoryRepository>();
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ILikeRepository, LikeRepository>();
            services.AddScoped<ISearchRepository, SearchRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<ICustomIdGenerator, CustomIdGeneratorService>();
            services.AddScoped<ITagRepository, TagRepository>();

            var cloudName = configuration["Cloudinary:CloudName"];
            if (!string.IsNullOrEmpty(cloudName))
            {
                var cloudinaryAccount = new Account(
                    cloudName,
                    configuration["Cloudinary:ApiKey"],
                    configuration["Cloudinary:ApiSecret"]);
                services.AddSingleton(new Cloudinary(cloudinaryAccount));
                services.AddScoped<CloudStorageService>();
            }

            return services;
        }
    }
}
