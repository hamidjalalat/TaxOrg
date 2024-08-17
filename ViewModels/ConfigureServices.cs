using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using System.Reflection;


namespace ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public static class ConfigureServices
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddViewModelServices(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}