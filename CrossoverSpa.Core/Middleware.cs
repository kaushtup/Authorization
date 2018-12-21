using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CrossoverSpa.Core.Services;
using CrossoverSpa.Helper;
using CrossoverSpa.ViewModel;
using CrossoverSpa.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace CrossoverSpa.Core
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class Middleware
    {
        private readonly RequestDelegate _next;

        private static IDbHelper _dbHelper;
        //private static MvcControllerDiscovery _mvcControllerDiscovery;

        //readonly Disposable _disposable;

        public Middleware(RequestDelegate next)
        {
            _next = next;
        }

        public  Task Invoke(HttpContext httpContext, IDbHelper dbHelper)
        {
            _dbHelper = dbHelper;
            bool Count=false;
            //mvcControllerDiscovery = _mvcControllerDiscovery;

            var users =   _dbHelper.GetUsersAsync().Result;

            var email = httpContext.User.Identity.Name;

            if (email != null)
            {
                var role = new RoleViewModel();

                foreach (var item in users)
                {
                    if (email.ToLower() == item.Email.ToLower())
                    {
                        role.Id = item.RoleId;
                    }
                }

                var roleFeatures =   _dbHelper.GetRoleFeaturesAsync().Result;

                List<int> list = new List<int>();

                foreach (var item in roleFeatures)
                {
                    if (item.RoleId == role.Id)
                    {
                        list.Add(item.FeatureId);
                    }
                }
                var featureList = new List<FeatureViewModel>();
                foreach (var item in list)
                {
                    var feature =  _dbHelper.GetFeatureByIdAsync(item).Result;
                    featureList.Add(feature);
                }

                var path = httpContext.Request.Path.ToString();
               
                    var addressLink = new Uri(path);

                    var absPath = addressLink.AbsolutePath;
                    foreach (var item in featureList)
                    {
                        var concatPath = "/" + item.RouteUrl;

                        if ((path.ToLower() == (concatPath).ToLower()) || path == "/Error/UnAuthorized" || path== "/")
                         { 
                             Count = true;
                         }
                    }
                    //if(path == "/Error/UnAuthorized") { Count = true; }
               
                    if (Count == true)
                    {
                        return _next(httpContext);
                    }
                    else
                    {
                        httpContext.Response.Redirect("/Error/UnAuthorized");
                        return _next(httpContext);
                    }
                
                

               
            }
            return _next(httpContext);
        }
    }

   

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<Middleware>();
        }

        public static T RegisterForDispose<T>(this T disposable, HttpContext context) where T : IDisposable
        {
            context.Response.RegisterForDispose(disposable);
            return disposable;
        }
    }
}
