using CrossoverSpa.Core.Services;
using CrossoverSpa.Database;
using CrossoverSpa.Helper;
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


            services.AddScoped<IMvcControllerDiscovery, MvcControllerDiscovery>();

            services.AddScoped<IDbHelper, DbHelper>();

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
            services.AddMvc().AddRazorPagesOptions(options =>
          {
              options.Conventions.AuthorizeFolder("/");
              options.Conventions.AllowAnonymousToPage("/Account/Login");
          });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admins", policy => policy.RequireRole("Admins"));
                options.AddPolicy("Employees", policy => policy.RequireRole("Employees"));
 
            });
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

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                  name: "areas",
                  template: "{area:exists}/{controller}/{action}/{id?}");


                routes.MapRoute(
                    name: "default",
                    template: "{controller=Account}/{action=Login}/{id?}");
            });

            app.UseMiddleware();
        }
    }
}
