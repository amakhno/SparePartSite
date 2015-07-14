using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Data.Entity;
using My_Site.Models;
using My_Site.Infrastructure.Binders;
using My_Site.Models.Layers;
using Autofac;
using Autofac.Integration.Mvc;

namespace My_Site
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new RepositoryLayer());

            IContainer container = builder.Build();

            // Setup DI as default MVC controller factory
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));



            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ModelBinders.Binders.Add(typeof(Cart), new CartModelBinder());
        }
    }
}
