using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Utility.Extensions
{
    public static class IServiceProviderExtensions
    {
        public static void RunScopedService<TService, TInnerService>(this IServiceProvider serviceProvider, Action<TService, TInnerService> callback)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<TService>();
                callback(context, serviceScope.ServiceProvider.GetRequiredService<TInnerService>());

                if (context is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }
        }

        public static void RunScopedService<TService>(this IServiceProvider serviceProvider, Action<TService> callback)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<TService>();
                callback(context);

                if (context is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }
        }

        public static TResult RunScopedService<TResult, TService>(this IServiceProvider serviceProvider, Func<TService, TResult> callback)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<TService>();
                return callback(context);
            }
        }
    }
}
