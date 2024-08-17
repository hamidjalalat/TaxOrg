using Blazored.LocalStorage;
using Client.Services.Tokens;
using Infrastructure;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using Radzen;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Toolbelt.Blazor.Extensions.DependencyInjection;

namespace Client
{
    public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebAssemblyHostBuilder.CreateDefault(args);

			builder.RootComponents.Add<App>("#app");
			builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddHttpClientInterceptor();
            builder.Services.AddSingleton<HttpInterceptorService>();

            builder.Services.AddSingleton(sp => new HttpClient { 
				BaseAddress = new Uri(builder.HostEnvironment.BaseAddress), 
				Timeout = Utility.getTimeoutHttpClient(builder.Configuration), 
			}.EnableIntercept(sp));

            //builder.Services.AddBlazoredSessionStorage();
            //builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddBlazoredLocalStorageAsSingleton();

            builder.Services.AddScoped<Utility>();
            builder.Services.AddScoped<NotificationSettings>();

			builder.Services.AddAuthorizationCore();
            builder.Services.AddSingleton<ITokensService, TokensService>();
			builder.Services.AddSingleton<NazmAuthenticationStateProvider>();
			builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<NazmAuthenticationStateProvider>());

			builder.Services.AddSingleton<Services.LogsService>();

			builder.Services.AddSingleton<Services.UserMockService>();
			builder.Services.AddSingleton<Services.UserAuthenticationsService>();
			builder.Services.AddSingleton<Services.UserManagesService>();

			builder.Services.AddSingleton<Services.MenusService>();
			builder.Services.AddSingleton<Services.HistoriesService>();
			builder.Services.AddSingleton<Services.RoleManagesService>();
			builder.Services.AddSingleton<Services.RoleMenusService>();
			builder.Services.AddSingleton<Services.UserRolesService>();
			builder.Services.AddSingleton<Services.LoginHistoriesService>();
			
			builder.Services.AddSingleton<Services.RegulationGroupsService>();

			builder.Services.AddScoped<DialogService>();

			builder.Services.AddMudServices();

			//System.Globalization.CultureInfo.DefaultThreadCurrentCulture = new System.Globalization.CultureInfo("fa-IR");
			//System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = new System.Globalization.CultureInfo("fa-IR");

			await builder.Build().RunAsync();
		}
	}
}
