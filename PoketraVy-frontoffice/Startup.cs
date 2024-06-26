using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PoketraVy_frontoffice.Data;

namespace PoketraVy_frontoffice
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
            services.AddControllersWithViews();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/Authentication/Login";
                options.LogoutPath = "/Authentication/Logout";
            });

            services.AddSingleton(new UtilisateurRepository(Configuration.GetConnectionString("PoketraVy_frontofficeContext")));
            services.AddSingleton(new BudgetRepository(Configuration.GetConnectionString("PoketraVy_frontofficeContext")));
            services.AddSingleton(new UtilisateurBudgetRepository(Configuration.GetConnectionString("PoketraVy_frontofficeContext")));
            services.AddSingleton(new CategorieUtilisateurBudgetRepository(Configuration.GetConnectionString("PoketraVy_frontofficeContext")));
            services.AddSingleton(new MouvementRepository(Configuration.GetConnectionString("PoketraVy_frontofficeContext")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Authentication}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                   name: "api",
                   pattern: "api/{controller}/{action}/{id?}");
            });
        }
    }
}
