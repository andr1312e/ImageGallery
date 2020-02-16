using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImageGallary.Data;
using ImageGalleryServises;
using ImageGalleryUsers.CustomIdentityApp.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ImageGalleryUsers.Models;


namespace ImageGallery
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
            var config2 = Configuration.GetConnectionString("ImageGallery");
            services.AddMvc(options => options.EnableEndpointRouting = false);
            services.AddScoped<IImage, Service>();

            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<UserDbContext>().AddDefaultTokenProviders();

            services.AddDbContext<ImageGalleryDbContext>(options => options
            .UseSqlServer(Configuration.GetConnectionString("ImageGallery")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            app.UseStatusCodePages();
            app.UseDeveloperExceptionPage();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseRouting();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=ImageGallery}/{action=Index}/{id?}");
            });
            //logger.LogInformation("Processing request {0}");
            //logger.LogDebug($"Handled");
        }
    }
}
