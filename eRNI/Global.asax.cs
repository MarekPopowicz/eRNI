using eRNI.App_Start;

using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace eRNI
{
    
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ModelBinders.Binders.Add(typeof(decimal), new ModelBinder());
            ModelBinders.Binders.Add(typeof(decimal?), new ModelBinder());
        }
    }

   
}
