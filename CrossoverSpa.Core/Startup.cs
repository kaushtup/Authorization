using System.Collections.Generic;
using CrossoverSpa.Core.Controllers;
using CrossoverSpa.Core.Services;
using CrossoverSpa.Database;
using CrossoverSpa.Helper;
using CrossoverSpa.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace CrossoverSpa.Core
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddTransient<IReadOnlyRepository<User>, Repository<User>>();

            services.AddTransient<IDbHelper, DbHelper>();
            services.AddScoped<IMvcControllerDiscovery, MvcControllerDiscovery>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();




            services.AddScoped<List<MvcControllerInfo>>();

           


            services.AddHttpContextAccessor();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);




            services.AddDbContext<SpaDbContext>

            (options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddAuthentication(options =>
            {
                  options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                  options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                  options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(options => { options.LoginPath = "/Account/Login"; });
            services.AddScoped<IFeatureDiscovery, FeatureDiscovery>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();
            //app.Use((context, next) =>
            //{
            //    context.Response.Headers.Add("X-Content-Type-Option", "nosniff");
            //    return next();
            //});
            app.UseMiddleware();
            app.UseMvc(
            routes =>
            {
                routes.MapRoute(
                   "areas",
                  "{area:exists}/{controller}/{action}/{id?}");

                routes.MapRoute(
                    "default",
                    "{Account}/{Login}/{id?}");
            });

            // app.MapWhen(_ => _.Request.Path.Value.Contains("/"), map=>map.UseMiddleware());
            //app.MapWhen(context => context.Request.Path.StartsWithSegments("/"), appBuilder =>
            //{
            //   appBuilder.UseMiddleware();
            //});
            //app.Use((context, next) =>
            //{
            //    if ((context as DefaultHttpContext).Request.Path.StartsWithSegments("/"))
            //    {
            //        app.UseMiddleware();
            //    }

            //    return next();
            //});


        }
    }
}
