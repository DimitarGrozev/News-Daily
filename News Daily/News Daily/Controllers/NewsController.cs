﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using News_Daily.Models;
using News_Daily.Services.Contracts;
using NewsAPI;
using NewsAPI.Constants;
using NewsAPI.Models;
using Newtonsoft.Json;

namespace News_Daily.Controllers
{
	public class NewsController : Controller
	{
		private readonly INewsService newsService;
		private List<Article> cachedArticles;

		public NewsController(INewsService newsService)
		{
			this.newsService = newsService;
			this.cachedArticles = new List<Article>();
		}
		public async Task<IActionResult> Index(NewsQuery newsQuery)
		{

			if (ModelState.IsValid)
			{
				try
				{
					if (newsQuery.CurrentPage == 0)
						newsQuery.CurrentPage = 1;

					var articles = await this.newsService.GetArticlesForPageAsync(newsQuery.SearchString, newsQuery.SortString, newsQuery.CurrentPage, newsQuery.LanguageString);
					this.cachedArticles = articles.Item1.ToList();
					newsQuery.Articles = articles.Item1.ToList();
					newsQuery.Topic = articles.Item2;
					if (newsQuery.IsAjax)
						return View("_Articles", newsQuery);
					else
						return View(newsQuery);
				}
				catch
				{
					//TODO toaster
					return RedirectToAction("Index", "Home");
				}
			}

			return RedirectToAction("Index", "Home");
		}



		public async Task<IActionResult> Download(string articles)
		{
			var query = JsonConvert.DeserializeObject<NewsQuery>(articles);

			var file =await this.newsService.DownloadToExcelAsync(query.SearchString, query.SortString, query.CurrentPage, query.LanguageString,query.Topic);

			return File(file,
			"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
						"articles.xlsx");
		}
	}
}