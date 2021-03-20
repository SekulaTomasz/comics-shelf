using Autofac;
using comics_shelf_api.core.Repositories;
using comics_shelf_api.core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace comics_shelf_api.core.IoC
{
    public class RepositoryModules : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<UserRepository>().As<IUserRepository>();
            builder.RegisterType<PurchaseComicsRepository>().As<IPurchaseComicsRepository>();
        }
    }
}
