using Book.Abstract.Interfaces;
using Book.DataLayer;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace BookApi
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            container.RegisterType<IBookRepository, BookEFRepository>();
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}