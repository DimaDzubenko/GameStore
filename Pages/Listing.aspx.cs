using System;
using System.Collections.Generic;
using GameStore.Models;
using GameStore.Models.Repository;
using System.Linq;
using GameStore.Pages.Helpers;
using System.Web.Routing;

namespace GameStore.Pages
{    
    public partial class Listing : System.Web.UI.Page
    {
        /// <summary>
        /// repository
        /// </summary>
        private Repository repository = new Repository();
        /// <summary>
        /// Количество товаров на одной странице.
        /// </summary>
        private int pageSize = 4;

        /// <summary>
        /// Свойсвто
        /// </summary>
        protected int CurrentPage
        {
            get
            {
                int page;
                page = GetPageFromRequest();
                return page > MaxPage ? MaxPage : page;
            }
        }

        /// <summary>
        /// Свойство, возвращающее наибольший номер допустимой страницы.
        /// </summary>
        protected int MaxPage
        {
            get
            {
                int prodCount = FilterGames().Count();
                return (int)Math.Ceiling((decimal)prodCount / pageSize);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private int GetPageFromRequest()
        {
            int page;
            string reqValue = (string)RouteData.Values["page"] ??
                Request.QueryString["page"];
            return reqValue != null && int.TryParse(reqValue, out page) ? page : 1;
        }

        /// <summary>
        /// Метод для использования класса Repository
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Game> GetGames()
        {
            return FilterGames()
                .OrderBy(g => g.GameId)
                .Skip((CurrentPage - 1) * pageSize)
                .Take(pageSize);
        }

        /// <summary>
        /// Метод для фильтрации игр по категориям.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<Game> FilterGames()
        {
            IEnumerable<Game> games = repository.Games;
            string currentCategory = (string)RouteData.Values["category"] ??
                Request.QueryString["category"];
            return currentCategory == null ? games :
                games.Where(p => p.Category == currentCategory);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                int selectedGameId;
                if (int.TryParse(Request.Form["add"], out selectedGameId))
                {
                    Game selectedGame = repository.Games
                        .Where(g => g.GameId == selectedGameId).FirstOrDefault();

                    if (selectedGame != null)
                    {
                        SessionHelper.GetCart(Session).AddItem(selectedGame, 1);
                        SessionHelper.Set(Session, SessionKey.RETURN_URL,
                            Request.RawUrl);

                        Response.Redirect(RouteTable.Routes
                            .GetVirtualPath(null, "cart", null).VirtualPath);
                    }
                }
            }
        }
    }
}