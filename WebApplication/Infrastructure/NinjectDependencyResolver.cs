using Ninject;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {

        public NinjectDependencyResolver(IKernel kernel)
        {
            _kernel = kernel;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }


        private void AddBindings()
        {
            _kernel.Bind<IHttpContextFactory>().To<HttpContextFactory>().InRequestScope();
        }

        private readonly IKernel _kernel;
    }
}