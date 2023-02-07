using Ciber.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.Cookies;
using Ciber.Services.Interfaces;
using Ciber.Services;

namespace Ciber
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
            services.AddMvc()
            .AddSessionStateTempDataProvider();

            services.AddSession();

            services.AddControllers();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
               .AddCookie(option =>
               {
                   option.Cookie.Name = "AccessCookie";
                   option.LoginPath = "/Access/Login";
                   option.ExpireTimeSpan = TimeSpan.FromMinutes(10);
               });

            services.AddDbContext<CiberDbContext>(option => {
                option.UseSqlServer(Configuration.GetConnectionString("DBConnection"));
            });

            //services.AddSingleton<IOrderServices, OrderServices>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // add authentication
            app.UseAuthentication();

            app.UseAuthorization();
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Access}/{action=Login}/{id?}");
            });

            loggerFactory.AddFile("Logs/app-{Date}.txt");
        }
    }
}
