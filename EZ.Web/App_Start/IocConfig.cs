using Ninject;
using System.Web.Http;
using EZ.Data;

namespace EZ.Web
{
    public class IocConfig
    {
        public static void RegisterIoc(HttpConfiguration config)
        {
            var kernel = new StandardKernel();  //Ninject IoC

            //These registrations are "per instance request".
            // See http://blog.bobcravens.com/2010/03/ninject-life-cycle-management-or-scoping/

            kernel.Bind<RepositoryFactories>().To<RepositoryFactories>().InSingletonScope();
            kernel.Bind<IRepositoryProvider>().To<RepositoryProvider>();
            kernel.Bind<IEzUow>().To<EzUow>();

            //Tell WebApi how to use our Ninject IoX
            config.DependencyResolver = new NinjectDependencyResolver(kernel);
        }
    }
}