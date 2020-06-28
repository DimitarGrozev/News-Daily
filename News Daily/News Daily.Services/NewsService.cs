using ClosedXML.Excel;
using Microsoft.Extensions.Configuration;
using News_Daily.Services.Contracts;
using NewsAPI;
using NewsAPI.Constants;
using NewsAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace News_Daily.Services
{
	public class NewsService : INewsService
	{
		private readonly NewsApiClient client;

		public NewsService(IConfiguration configuration)
		{
			this.client = new NewsApiClient(configuration["api-key"]) ?? throw new ArgumentNullException();
		}
		public async Task<ICollection<Article>> GetArticlesForPageAsync(string searchString, string sortString, int currentPage,string language)
		{

			var response = await ArticleQuery(searchString, sortString, currentPage, language);
			

			if (response.Status.ToString() == Statuses.Ok.ToString())
			{
				
				return response.Articles;
			}

			throw new ArgumentException();
		}
		public async Task<ICollection<Article>> GetArticlesForExportAsync(string searchString, string sortString, int currentPage, string language)
		{

			var response =await ArticleQuery(searchString, sortString, null, language);


			if (response.Status.ToString() == Statuses.Ok.ToString())
			{
				return response.Articles;
			}

			throw new ArgumentException();
		}
		public async Task<ArticlesResult> ArticleQuery(string searchString, string sortString, int? currentPage, string language)
		{
			var response = await this.client.GetEverythingAsync(new EverythingRequest
			{
				Q = searchString ?? "Apple",
				Page = currentPage ?? 1,
				Language = (Languages)Enum.Parse(typeof(Languages), language ?? "en", true),
				SortBy = (SortBys)Enum.Parse(typeof(SortBys), sortString ?? "Popularity", true),

			});

			return response;
		}
		public async Task<byte[]> DownloadToExcelAsync(string searchString,string sortString, int currentPage,string language)
		{
			searchString = searchString == string.Empty ? null : searchString;
			sortString = sortString == string.Empty ? null : sortString;
			language = language == string.Empty ? null : language;
			var result =await GetArticlesForExportAsync(searchString, sortString, currentPage, language);
			var articles = result;
			using (var workbook = new XLWorkbook())
			{
				var worksheet = workbook.Worksheets.Add("Articles");
				var currentRow = 2;
				worksheet.Columns().Width = 25;
				worksheet.Cell(currentRow, 2).Value = "Title";
				worksheet.Cell(currentRow, 3).Value = "Author";
				worksheet.Cell(currentRow, 4).Value = "Description";
				worksheet.Cell(currentRow, 5).Value = "Published At";
				worksheet.Cell(currentRow, 6).Value = "Source";
				worksheet.Cell(currentRow, 7).Value = "URL";
				worksheet.Range(currentRow, 7, currentRow, 11).Merge();

				foreach (var article in articles)
				{
					currentRow++;
					worksheet.Cell(currentRow, 2).Value = article.Title;
					worksheet.Cell(currentRow, 3).Value = article.Author;
					worksheet.Cell(currentRow, 4).Value = article.Description;
					worksheet.Cell(currentRow, 5).Value = article.PublishedAt;
					worksheet.Cell(currentRow, 6).Value = article.Source;
					worksheet.Cell(currentRow, 7).Value = article.Url;
					worksheet.Range(currentRow, 7, currentRow, 11).Merge();
				}
				var rngTable = worksheet.Range("B2:K22");
				rngTable.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
				rngTable.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
				rngTable.Style.Border.RightBorder = XLBorderStyleValues.Thin;
				var rngHeader = worksheet.Range("B2:K2");
				rngHeader.Style.Font.Bold = true;
				rngHeader.Style.Fill.BackgroundColor = XLColor.BlueGray;
				rngHeader.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
				using (var stream = new MemoryStream())
				{
					workbook.SaveAs(stream);
					var content = stream.ToArray();

					return
						content;
						
				}
			}
		}
	}
}
