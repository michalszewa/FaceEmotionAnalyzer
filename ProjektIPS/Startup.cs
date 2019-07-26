using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using ProjektIPS.Models;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.EntityFrameworkCore;
using ProjektIPS.Persistance.Repositories;
using ProjektIPS.Domain.Repositories;
using ProjektIPS.Services;
using ProjektIPS.Domain.Services;
using ProjektIPS.Domain.SeedWork;

namespace ProjektIPS
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<FaceApiConfigHelper>(Configuration.GetSection("AzureFaceApiConfig"));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddMvc().AddJsonOptions(
                        options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                    );
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
                );

            services.AddScoped<IPhotoRepository, PhotoRepository>();

            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<IFaceApiService, FaceApiService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddCors();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var webRoot = env.WebRootPath;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseCors(builder => builder.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, @"PhotoResources")),
                RequestPath = new PathString("/PhotoResources")
            });

            app.UseMvc();
        }
    }
}
