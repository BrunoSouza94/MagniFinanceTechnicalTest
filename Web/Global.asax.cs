using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Web.Migrations;

namespace Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var migrator = new System.Data.Entity.Migrations.DbMigrator(new Configuration());

            migrator.Update();
        }
    }
}
