using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using IdealistaTest.Infrastructure;

namespace IdealistaTest
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            var adJsonFilename =
                System.Web.Hosting.HostingEnvironment.MapPath(@"~/Infrastructure/jsonPopulateFiles/ads.json");
            var pictureJsonFilename =
                System.Web.Hosting.HostingEnvironment.MapPath(@"~/Infrastructure/jsonPopulateFiles/pictures.json");
            FakeDatabase.Instance().InitializeDatabase(adJsonFilename, pictureJsonFilename);
        }
    }
}
