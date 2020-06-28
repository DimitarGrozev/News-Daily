using Microsoft.AspNetCore.Mvc.Rendering;
using NewsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News_Daily.Models
{
	public class NewsQuery
	{
		public string SearchString { get; set; }

        public List<SelectListItem> SortDropdown { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "Popularity", Text = "Popularity" },
            new SelectListItem { Value = "PublishedAt", Text = "Release date" },
            new SelectListItem { Value = "Relevancy", Text = "Relevancy" },

        };
        public List<SelectListItem> LanguageDropdown { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "en", Text = "English" },
            new SelectListItem { Value = "de", Text = "German" },
            new SelectListItem { Value = "ru", Text = "Russian" },
            new SelectListItem { Value = "fr", Text = "French" },

        };
        public string SortString { get; set; }
        public string LanguageString { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public ICollection<Article> Articles { get; set; }

        public bool IsAjax { get; set; }
    }
}
