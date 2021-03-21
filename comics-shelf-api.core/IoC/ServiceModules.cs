using Autofac;
using comics_shelf_api.core.ExternalProviders;
using comics_shelf_api.core.Services;
using comics_shelf_api.core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace comics_shelf_api.core.IoC
{
    public class ServiceModules : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<PurchaseComicsService>().As<IPurchaseComicsService>();
            builder.RegisterType<ComicsService>().As<IComicsService>();
            builder.RegisterType<PasswordHasher>().As<IPasswordHasher>();
        }
    }
}
