using System.Web.Http.Dependencies;
using Microsoft.AspNet.SignalR;
using Ninject;
using Ninject.Activation.Blocks;
using Ninject.Parameters;
using ScrumPoker.Controllers;
using ScrumPoker.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using IDependencyResolver = System.Web.Mvc.IDependencyResolver;
using Ninject.Syntax;

namespace ScrumPoker
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Use Ninject to create SignalR Hubs
            GlobalHost.DependencyResolver.Register(typeof(RoomHub), () => ScrumPokerKernel.Instance.Get<RoomHub>());
            RouteTable.Routes.MapHubs();

            // Use Ninject to create MVC4 Controllers
            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());

            // Use Ninject to create WebAPI Controllers
            GlobalConfiguration.Configuration.DependencyResolver = new NinjectApiResolver(ScrumPokerKernel.Instance);

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
            Kernel = ScrumPokerKernel.Instance;
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


    public class NinjectApiScope : System.Web.Http.Dependencies.IDependencyScope
    {
        protected IResolutionRoot ResolutionRoot;

        public NinjectApiScope(IResolutionRoot resolutionRoot)
        {
            ResolutionRoot = resolutionRoot;
        }

        public void Dispose()
        {
            var svc = (IDisposable) ResolutionRoot;
            if (svc != null) svc.Dispose();
            ResolutionRoot = null;
        }

        public object GetService(Type serviceType)
        {
            var r = ResolutionRoot.CreateRequest(serviceType, null, new Parameter[0], true, true);
            return ResolutionRoot.Resolve(r).SingleOrDefault();
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            var r = ResolutionRoot.CreateRequest(serviceType, null, new Parameter[0], true, true);
            return ResolutionRoot.Resolve(r).ToList();
        }
    }

    public class NinjectApiResolver : NinjectApiScope, System.Web.Http.Dependencies.IDependencyResolver
    {
        private readonly IKernel _kernel;

        public NinjectApiResolver(IKernel kernel) : base(kernel)
        {
            _kernel = kernel;
        }

        public IDependencyScope BeginScope()
        {
            return new NinjectApiScope(_kernel.BeginBlock());
        }
    }
}