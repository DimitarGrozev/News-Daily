using Microsoft.Extensions.DependencyInjection;
using News_Daily.Services;
using News_Daily.Services.Contracts;

namespace News_Daily.Utilities
{
	public static class BusinessServiceRegistration
	{
		public static IServiceCollection AddServices(this IServiceCollection services)
		{
			services.AddScoped<INewsService, NewsService>();

			return services;
		}
	}
}
