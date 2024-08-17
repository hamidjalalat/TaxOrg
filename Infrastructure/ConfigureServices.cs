using Application.Common.Interfaces;
using Application.Common.Interfaces.Repository.Anemic.Base;
using Application.Common.Interfaces.Repository.Anemic.Dapper;
using Application.Common.Interfaces.Repository.Anemic.EF;
using Application.Common.Interfaces.Repository.Anemic.Oracle;
using Application.Common.Models;
using Application.Common.Security.PhoneTotp;
using Domain.Anemic.Entities;
using Infrastructure.Common;
using Infrastructure.Common.Anemic;
using Infrastructure.Identity;
using Infrastructure.Repository.Anemic.Base;
using Infrastructure.Repository.Anemic.Dapper;
using Infrastructure.Repository.Anemic.EF;
using Infrastructure.Repository.Anemic.Oracle;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Persistence.Configurations.Anemic.Contexts;
using Thinktecture;

namespace Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<EFContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")
                , conf =>
                {
                    conf.AddRowNumberSupport();
                    conf.UseHierarchyId();
                    conf.CommandTimeout((int)TimeSpan.FromMinutes(30).TotalSeconds);
                }
                 //,o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
                 //builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
                 ));

            services.AddDbContext<OracleContext>(options => options.UseOracle(configuration.GetConnectionString("orclConnection")));
            //services.AddDbContext<Persistence.Configurations.Rich.DatabaseContext>(options =>
            //   options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
            //   o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
            //    //builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
            //    );

            services.AddSingleton<DapperContext>();
            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.User.RequireUniqueEmail = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;

            })
                .AddEntityFrameworkStores<EFContext>()
                .AddDefaultTokenProviders()
                .AddErrorDescriber<PersianIdentityErrorDescriber>()
                .AddClaimsPrincipalFactory<UserClaimsPrincipalFactory>();


            services.Configure<SecurityStampValidatorOptions>(option =>
            {
                // option.ValidationInterval = TimeSpan.FromSeconds(15);
            });



            services.AddScoped<IdentityUser, User>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //services.AddScoped<Application.Common.Interfaces.Repository.Rich.IUnitOfWork, Repository.Rich.UnitOfWork>();


            services.AddScoped<IActionMethodRepository, ActionMethodRepository>();
            services.AddScoped<IControllerRepository, ControllerRepository>();
            services.AddScoped<IMenuControllerActionRepository, MenuControllerActionRepository>();
            services.AddScoped<IMenuControllerRepository, MenuControllerRepository>();
            //services.AddScoped<IMenuDapperRepository, MenuDapperRepository>();
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddSingleton<IAuthenticatedUserService, AuthenticatedUserService>();
            services.AddScoped<IMessageSender, MessageSender>();
            services.AddScoped<IPhoneTotpProvider, PhoneTotpProvider>();
            services.AddScoped<IHistoryRepository, HistoryRepository>();
            services.AddScoped<ILoginHistoryRepository, LoginHistoryRepository>();

            services.AddScoped<IRegulationGroupRepository, RegulationGroupRepository>();
            services.AddScoped<ITspagentRepository, TspagentRepository>();
            services.AddScoped<INazm_tspagentRepository, Nazm_tspagentRepository>();
            services.AddScoped<IBranchRepository, BranchRepository>();
            services.AddScoped<INetsaleRepository, NetsaleRepository>();
            services.AddScoped<IDimProductRepository, DimProductRepository>();
            services.AddScoped<IInvoiceModelRepository, InvoiceModelRepository>();

            services.AddCustomOptions(configuration);
            services.AddCustomCors(configuration);
            services.AddCustomJwtBearer(configuration);

            return services;
        }

        public static void AddCustomAntiforgery(this IServiceCollection services)
        {
            services.AddAntiforgery(x => x.HeaderName = "X-XSRF-TOKEN");
            services.AddMvc(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });
        }

        public static void AddCustomCors(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .WithOrigins(configuration["ApiSettings:ClientUrl"].Split(',')) //Note:  The URL must be specified without a trailing slash (/).
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .SetIsOriginAllowed((host) => true)
                        );
            });
        }

        public static void AddCustomJwtBearer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(
                options =>
                {
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                }
                ).AddJwtBearer(options =>
                {

                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        ValidAudience = configuration["JWT:Audience"],
                        ValidateIssuer = false,
                        ValidIssuer = configuration["JWT:Issuer"],
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ClockSkew = TimeSpan.Zero,
                        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration["JWT:Key"])) // NOTE: THIS SHOULD BE A SECRET KEY NOT TO BE SHARED
                    };

                });
        }

        public static void AddCustomOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<PhoneTotpOptions>(options =>
            {
                options.StepInSeconds = 60;
            });
            services.Configure<BearerTokensOptions>(configuration.GetSection("JWT"));
            services.Configure<ApiSettings>(configuration.GetSection("ApiSettings"));
            services.Configure<EmailConfig>(configuration.GetSection("EmailConfig"));
            services.Configure<SmsConfig>(configuration.GetSection("SmsConfig"));
        }
    }
}