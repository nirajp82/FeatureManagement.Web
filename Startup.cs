using FeatureManagement.Web.Infrastructure;
using FeatureManagement.Web.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.FeatureFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeatureManagement.Web
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
            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(
                        Configuration.GetConnectionString("DefaultConnection")));

            services.AddAzureAppConfiguration();

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddHttpContextAccessor();

            services.AddDistributedMemoryCache(); // For demo purposes, not distributed across multiple web servers

            services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddTransient<ISessionManager, HttpContextFeatureSessionManager>();

            services.AddControllersWithViews(options =>
            {
                options.Filters.AddForFeature<TimeElapsedFilter>(nameof(FeatureFlag.TimeElapsed));
            });
            services.AddRazorPages();
            services.AddFeatureManagement()
                 .UseDisabledFeaturesHandler(new CustomDisabledFeatureHandler())
                 .AddFeatureFilter<TimeWindowFilter>()
                 .AddFeatureFilter<TargetingFilter>()
                 .AddFeatureFilter<PercentageFilter>()
                 .AddFeatureFilter<BetaCookieFilter>()
                 .AddFeatureFilter<ClaimsFeatureFilter>();

            services.AddSingleton<ITargetingContextAccessor, HttpTargetingContextAccessor>();
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
            }
            app.UseAzureAppConfiguration();
            //app.UseMiddlewareForFeature<LogURLMiddleware>(nameof(FeatureFlag.LogUrl));
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession()
                .UseMiddlewareForFeature<LogURLMiddleware>(nameof(FeatureFlag.LogUrl));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
