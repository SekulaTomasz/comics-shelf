using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace comics_shelf_api.core.IoC
{
	public class ContainerModule : Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterModule<RepositoryModules>();
			builder.RegisterModule<ServiceModules>();

		}
	}
}
