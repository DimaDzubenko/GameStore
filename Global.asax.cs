using GameStore.App_Start;
using System;
using System.Web.Optimization;
using System.Web.Routing;

namespace GameStore
{
    public class Global : System.Web.HttpApplication
    {
        /// <summary>
        /// Метод конфигурирующий маршрутизацию.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_Start(object sender, EventArgs e)
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}