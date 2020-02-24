using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomIdentityApp.Models;
using ImageGallary.Data;
using ImageGalleryServises;
using ImageGalleryUsers.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ImageGallery.Infrastrucure;


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
            
            services.AddDbContext<ImageGalleryDbContext>(options => options
            .UseSqlServer(Configuration["Data:ImageGallery:ConnectionString"]));

            services.AddDbContext<UserDbContext>(options => options
            .UseSqlServer(Configuration["Data:IdentityConnection:ConnectionString"]));
            services.AddTransient<IPasswordValidator<AppUser>, CustomPasswordValidation>();
            services.AddTransient<IUserValidator<AppUser>, CustomUserValidation>();
            services.AddIdentity<AppUser, IdentityRole>(options=> 
            {
                options.User.AllowedUserNameCharacters = "qwertyuiopasdfghjklzxcvbnm";
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 3;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
            }).AddEntityFrameworkStores<UserDbContext>().AddDefaultTokenProviders();
            services.AddScoped<IImage, Service>();
            services.AddMvc(options => options.EnableEndpointRouting = false);
            services.AddMemoryCache();
            services.AddSession();
        }

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
            UserDbContext.CreateAdminAccount(app.ApplicationServices, Configuration).Wait();
        }
    }
}
