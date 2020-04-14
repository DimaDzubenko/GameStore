using GameStore.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;
using System.Web.UI;

namespace GameStore.Controls
{
    /// <summary>
    /// Класс отделенного кода для пользовательского элемента управления.
    /// </summary>
    public partial class CategoryList : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// Метод для генерации с помощью LINQ списка названий категорий, который отсортирован в алфавитном порядке и не содержит дубликатов.
        /// </summary>
        /// <returns>Cписок категорий.</returns>
        protected IEnumerable<string> GetCategories()
        {
            return new Repository().Games
                .Select(p => p.Category)
                .Distinct()
                .OrderBy(x => x);
        }

        /// <summary>
        /// Метод который использует систему маршрутизации для генерирования URL.
        /// </summary>
        /// <returns>Ссылку</returns>
        protected string CreateHomeLinkHtml()
        {
            string path = RouteTable.Routes.GetVirtualPath(null, null).VirtualPath;
            return string.Format("<a href='{0}'>Главная</a>", path);
        }

        /// <summary>
        /// Метод для генерации HTML-разметки, представляющей каждую категорию.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        protected string CreateLinkHtml(string category)
        {
            string selectedCategory = (string)Page.RouteData.Values["category"]
                ?? Request.QueryString["category"];

            string path = RouteTable.Routes.GetVirtualPath(null, null,
                new RouteValueDictionary() { { "category", category },
                    {"page", "1"} }).VirtualPath;

            return string.Format("<a href='{0}' {1}>{2}</a>",
                path, category == selectedCategory ? "class='selected'" : "", category);
        }
    }
}