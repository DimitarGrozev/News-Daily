using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using News_Daily.Models;
using NewsAPI;
using NewsAPI.Constants;
using NewsAPI.Models;

namespace News_Daily.Controllers
{
	//api key : fe7096fa41e84cd2b410230482fea758
	public class HomeController : Controller
	{

		public HomeController()
		{
		}

		public  IActionResult Index()
		{
			
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
