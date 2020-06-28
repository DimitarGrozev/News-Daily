using NewsAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace News_Daily.Services.Contracts
{
	public interface INewsService
	{
		Task<ICollection<Article>> GetArticlesForPageAsync(string searchString, string sortString, int currentPage,string languageString);
		Task<byte[]> DownloadToExcelAsync(string searchString,string sortString,int currentPage, string language);
	}
}