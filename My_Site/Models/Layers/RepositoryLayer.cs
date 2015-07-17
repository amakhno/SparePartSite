using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using My_Site.Models.Repository;
using My_Site.Models.Intefaces;

namespace My_Site.Models.Layers
{
    public class RepositoryLayer : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SparePartRepository>().As<ISparePartRepository>().InstancePerLifetimeScope();
            builder.RegisterType<OrderRepository>().As<IOrderRepository>().InstancePerDependency();
        }
    }
}