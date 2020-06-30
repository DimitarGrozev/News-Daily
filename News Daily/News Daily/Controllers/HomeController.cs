using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using News_Daily.Models;
using News_Daily.Services.Contracts;
using NewsAPI;
using NewsAPI.Constants;
using NewsAPI.Models;

namespace News_Daily.Controllers
{
	//api key : fe7096fa41e84cd2b410230482fea758
	public class HomeController : Controller
	{
		private readonly INewsService newsService;

		public HomeController(INewsService newsService)
		{
			this.newsService = newsService;
		}

		public async  Task<IActionResult> Index()
		{
			try {
				var articles = await this.newsService.GetTrendingNewsAsync();

				return View(articles);
			}
			catch
			{
				//TODO toaster
			}

			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
