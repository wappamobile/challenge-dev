using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebFC.Wappa.Teste.DI.Unity;

namespace WebFC.Wappa.Teste.APIWeb.App_Start
{
    public class CustomDependencyResolver : System.Web.Mvc.IDependencyResolver, System.Web.Http.Dependencies.IDependencyResolver
    {
        protected readonly System.Web.Mvc.IDependencyResolver _dr;
        protected readonly Resolver _customResolver;

        public CustomDependencyResolver()
        {
            _dr = System.Web.Mvc.DependencyResolver.Current;
            _customResolver = new Resolver();

        }

        public CustomDependencyResolver(System.Web.Mvc.IDependencyResolver dr, Resolver customResolver)
        {
            _dr = dr;
            _customResolver = customResolver;
        }

        private CustomDependencyResolver(Resolver childResolver)
        {
            _customResolver = childResolver;
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return _customResolver.Resolve(serviceType);

            }
            catch (Exception ex)
            {
                try
                {
                    return _dr.GetService(serviceType);
                }
                catch
                {
                    return null;
                }
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return _customResolver.ResolveAll(serviceType);
            }
            catch
            {
                try
                {
                    return _dr.GetServices(serviceType);
                }
                catch
                {
                    return null;
                }
            }
        }

        public System.Web.Http.Dependencies.IDependencyScope BeginScope()
        {
            Resolver child = _customResolver.CreateChild();
            return new CustomDependencyResolver(child);
        }

        public void Dispose()
        {
            _customResolver.Dispose();
        }
    }
}