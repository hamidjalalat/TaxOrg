using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using System.Reflection;
using System.Text.Json.Serialization;
using FluentValidation.AspNetCore;
using Application.Common.Exceptions;

namespace Api
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
        public static IServiceCollection AddWebUIServices(this IServiceCollection services)
        {
			services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                })
                .AddJsonOptions(option =>
                {
                    option.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                    option.JsonSerializerOptions.PropertyNamingPolicy = null;
                });
            //services.Configure<MvcNewtonsoftJsonOptions>(options =>
            //{
            //    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            //});

			services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

            services.AddHttpContextAccessor();

			// Customise default API behaviour
			//services.Configure<ApiBehaviorOptions>(options =>
			//    options.SuppressModelStateInvalidFilter = true);

			services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Nazm_API",
                    Version = "v1.1"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddDistributedMemoryCache();

            services.AddHsts(services =>
            {
                services.MaxAge = TimeSpan.FromDays(365);
                services.IncludeSubDomains = true;
                services.Preload = true;
            });
            services.AddTransient<ExceptionHandlingMiddleware>();
            return services;
        }
    }
}