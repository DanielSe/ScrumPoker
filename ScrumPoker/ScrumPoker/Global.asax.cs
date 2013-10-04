using Ninject;
using Ninject.Parameters;
using ScrumPoker.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ScrumPoker
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }

    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private static readonly IKernel Kernel;

        static NinjectControllerFactory()
        {
            Kernel = new StandardKernel(new ScrumPokerModule());
        }

        public override IController CreateController(RequestContext requestContext, string controllerName)
        {
            try
            {
                var t = Type.GetType("ScrumPoker.Controllers." + controllerName + "Controller");
                var controller = Kernel.Get(t);

                return (IController)controller;
            }
            catch (Exception)
            {
                return base.CreateController(requestContext, controllerName);
            }
        }
    }
}