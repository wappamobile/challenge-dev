using Autofac;
using Autofac.Core;
using System;
using Autofac.Builder;
using Autofac.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Text;
using Castle.Core.Internal;

namespace Vitor.Common
{
    public sealed class IoCWrapper
    {
        private static IContainer _container;
        private static object _padlock = new object();

        public static IContainer Container
        {
            get
            {
                if (_container == null)
                {
                    lock (_padlock)
                    {
                        if (_container == null)
                        {
                            InitializeContainer(InitializeOptions.Default);
                        }
                    }
                }
                return _container;
            }
        }

        public static IContainer InitializeContainer(InitializeOptions options)
        {
            var builder = new ContainerBuilder();
            registerModulesFromAssemblies(options, builder);
            registerIndividualModules(options, builder);
            return _container = builder.Build(options.BuildOptions);
        }

        public static IContainer InitializeContainer(InitializeOptions options, ContainerBuilder builder)
        {
            registerModulesFromAssemblies(options, builder);
            registerIndividualModules(options, builder);
            return _container = builder.Build(options.BuildOptions);
        }

        private static void registerModulesFromAssemblies(InitializeOptions options, ContainerBuilder builder)
        {
            if (!options.AssembliesToScan.IsNullOrEmpty())
                builder.RegisterAssemblyModules(options.AssembliesToScan);
        }

        private static void registerIndividualModules(InitializeOptions options, ContainerBuilder builder)
        {
            if (!options.Modules.IsNullOrEmpty())
                foreach (var module in options.Modules)
                    builder.RegisterModule(module);
        }
    }
}
