using System.Web.Optimization;

namespace GameStore.App_Start
{
    /// <summary>
    /// Клас настройки для управления файлами JavaScript и CSS.
    /// </summary>
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/validation").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery.validate.js",
                "~/Scripts/jquery.validate.unobtrusive.js"));
        }
    }
}