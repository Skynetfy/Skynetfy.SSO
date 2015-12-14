using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Fiddler;

namespace SSO.HostWebSite
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //FiddlerApplication.Shutdown();
            //FiddlerApplication.BeforeRequest += FiddlerApplication_BeforeRequest;
            //FiddlerApplication.BeforeResponse += FiddlerApplication_BeforeResponse;
            //FiddlerApplication.Startup(9898, FiddlerCoreStartupFlags.Default);
        }

        protected void Application_End()
        {
            FiddlerApplication.Shutdown();
        }

        private void FiddlerApplication_BeforeRequest(Session oSession)
        {
            //想如何改写Response信息在这里随意发挥了
            Console.WriteLine("BeforeResponse: {0}", oSession.responseCode);
        }

        private void FiddlerApplication_BeforeResponse(Session oSession)
        {
            //想如何改写Request信息在这里随意发挥了
            Console.WriteLine("BeforeRequest: {0}, {1}", oSession.fullUrl, oSession.responseCode);
        }
    }
}
