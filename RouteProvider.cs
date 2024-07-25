using CommerceBuilder.Web.Routing;
using System.Web.Mvc;
using System.Web.Routing;
namespace TPIPlugin
{
    public class RouteProvider : IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            if (routes["Plugins_General_TPIPlugin_Admin"] == null)
            {
                var route = routes.MapRoute(
                    "Plugins_General_TPIPlugin_Admin",
                    "Admin/TPI/{action}",
                    new { controller = "TPIAdmin", action = "Index" },
                    new[] { "TPIPlugin.Controllers" }
                );

                route.DataTokens["area"] = "Admin";
            }

            if (routes["Plugins_General_TPIPlugin_Retail"] == null)
            {
                var route = routes.MapRoute(
                    "Plugins_General_TPIPlugin_Retail",
                    "TPIRetail/{action}",
                    new { controller = "TPIRetail" },
                    new[] { "TPIPlugin.Controllers" }
                );
            }
        }
    }
}
