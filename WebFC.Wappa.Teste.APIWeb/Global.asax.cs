using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebFC.Wappa.Teste.APIWeb.App_Start;

namespace WebFC.Wappa.Teste.APIWeb
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            DependencyResolver.SetResolver(new CustomDependencyResolver());
            GlobalConfiguration.Configuration.DependencyResolver = new CustomDependencyResolver();
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings
     .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            GlobalConfiguration.Configuration.Formatters
                .Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);

        }

        protected void Application_BeginRequest()
        {
            var currentCulture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            currentCulture.NumberFormat.NumberDecimalSeparator = ",";
            currentCulture.NumberFormat.NumberGroupSeparator = ".";
            currentCulture.NumberFormat.CurrencyDecimalSeparator = ",";

            Thread.CurrentThread.CurrentCulture = currentCulture;
            //Thread.CurrentThread.CurrentUICulture = currentCulture;
        }
    }
}
