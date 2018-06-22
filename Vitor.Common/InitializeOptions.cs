using Autofac.Builder;
using Autofac.Core;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Vitor.Common
{
    public class InitializeOptions
    {
        private static InitializeOptions _default = new InitializeOptions(new[] { typeof(InitializeOptions).GetTypeInfo().Assembly }, ContainerBuildOptions.None);

        public static InitializeOptions Default
        {
            get { return _default; }
        }
        public Assembly[] AssembliesToScan { get; private set; }
        public IModule[] Modules { get; private set; }
        public ContainerBuildOptions BuildOptions { get; private set; }

        public InitializeOptions(Assembly[] collection, ContainerBuildOptions buildOptions) : this(collection, new IModule[0], buildOptions)
        {
            this.AssembliesToScan = collection;
            this.BuildOptions = buildOptions;
        }

        public InitializeOptions(Assembly[] collection, IModule[] modules, ContainerBuildOptions buildOptions)
        {
            this.AssembliesToScan = collection;
            this.Modules = modules;
            this.BuildOptions = buildOptions;
        }
    }
}
