using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Services.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApp.Utility.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddCookieAuthentication(this IServiceCollection services,
             IConfiguration configuration)
        {
            services.AddHttpContextAccessor();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<ICookieProvider, CookieProvider>();
            services.AddScoped(sp =>
            {
                var httpContext = sp.GetRequiredService<IHttpContextAccessor>().HttpContext;
                if (httpContext != null && httpContext.User != null)
                {
                    return httpContext.User.Identity;
                }
                return ClaimsPrincipal.Current?.Identity;
            });

            services.Configure<CookieConfiguration>(options => configuration.GetSection(nameof(CookieConfiguration)).Bind(options));

            services
                .AddAuthentication(options =>
                {
                    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })
                //.AddGoogle(options =>
                //{
                //    options.ClientId = "";
                //    options.ClientSecret = "";
                //})
                //.AddFacebook(options =>
                //{
                //    options.ClientId = "";
                //    options.ClientSecret = "";
                //})
                .AddCookie(options =>
                {
                    services.BuildServiceProvider().RunScopedService<IOptionsSnapshot<CookieConfiguration>>(cookieConfiguration =>
                    {
                        options.SlidingExpiration = false;
                        options.LoginPath = cookieConfiguration.Value.LoginPath; //"/Admin/Main/Login";
                        options.LogoutPath = cookieConfiguration.Value.LogoutPath;//"/Admin/Main/Logout";
                        options.AccessDeniedPath = new PathString(cookieConfiguration.Value.LoginPath);//"/Errors/Error403/");
                        options.Cookie.Name = ".my.app.cookie";
                        options.Cookie.HttpOnly = true;
                        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                        options.Cookie.SameSite = SameSiteMode.Lax;
                        options.Events = new CookieAuthenticationEvents
                        {
                            OnValidatePrincipal = async context =>
                            {
                                context.HttpContext.User = context.Principal;
                            },
                        };
                    });
                });
            return services;
        }

    }

}
