using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorForum.Pages.Components.Head
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHeadBuilder(this IServiceCollection services)
        {
            return services.AddScoped<HeadBuilderService>();
        }
    }
}
