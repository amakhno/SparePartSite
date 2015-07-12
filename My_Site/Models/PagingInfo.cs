using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace My_Site.Models
{
    public class PagingInfo
    {
        //Всего запчастей
        public int TotalSpare { get; set; }

        //Число на одной странице
        public int ItemsPerPage { get; set; }

        // Номер текущей страницы
        public int CurrentPage { get; set; }

        // Общее кол-во страниц
        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)TotalSpare / ItemsPerPage); }
        } 
    }

    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfo paginginfo, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            for(int i = 1; i<=paginginfo.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();
                if (i == paginginfo.CurrentPage)
                {
                    tag.AddCssClass("selcted");
                    tag.AddCssClass("btn-primary");
                }
                tag.AddCssClass("btn btn-dafeult");
                result.Append(tag.ToString());
            }
            return MvcHtmlString.Create(result.ToString());
        }
    }
}