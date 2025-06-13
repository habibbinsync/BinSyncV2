using BinSync.Core.Services.DomainServices;
using BinSync.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
namespace BinSync.Blazor;
public static class DomainServicesExtensions
{
	public static IServiceCollection AddDomainServices(this IServiceCollection services)
	{
		services.AddScoped<IUploadFileService, UploadFileService>();
		services.AddSingleton<HttpClient>(sp =>
		{
			return new HttpClient()
			{
				Timeout = TimeSpan.FromSeconds(100)
			};
		});
		return services;
	}
}
