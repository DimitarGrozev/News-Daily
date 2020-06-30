using Microsoft.Extensions.DependencyInjection;
using News_Daily.Services;
using News_Daily.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
