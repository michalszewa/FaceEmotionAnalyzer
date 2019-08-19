using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjektIPS.Domain.Repositories;
using ProjektIPS.Domain.SeedWork;
using ProjektIPS.Domain.Services;
using ProjektIPS.Models;
using ProjektIPS.Persistance.Repositories;
using ProjektIPS.Services;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace ProjektIPS.Helpers
{
    public static class ServiceConfigHelper
    {
        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
              );

            return services;
        }

        public static IServiceCollection AddCustomMvc(this IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddMvc().AddJsonOptions(
                        options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                    );

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.WithOrigins("http://localhost:4200")
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            return services;
        }

        public static IServiceCollection AddCustomIntegrations(this IServiceCollection services)
        {
            services.AddScoped<IPhotoRepository, PhotoRepository>();

            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<IFaceApiService, FaceApiService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        public static IServiceCollection AddCustomConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<FaceApiConfigHelper>(configuration.GetSection("AzureFaceApiConfig"));
            services.AddAutoMapper();

            return services;
        }
    }
}
