namespace BookApi
{
    using Book.Abstract.Interfaces;
    using Book.Configuration;
    using Book.Configuration.ApiIntegration;
    using Book.DataLayer;
    using System.Web.Http;
    using Unity;
    using Unity.Interception;
    using Unity.Interception.ContainerIntegration;
    using Unity.Interception.Interceptors.InstanceInterceptors.InterfaceInterception;
    using Unity.Interception.PolicyInjection;
    using Unity.Interception.PolicyInjection.MatchingRules;
    using Unity.Interception.PolicyInjection.Pipeline;
    using Unity.WebApi;

    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = UnityApi.container;

            container.AddNewExtension<Interception>();
            container.RegisterType<ICallHandlerExtension, ServiceExceptionCallHandler>();

            container.Configure<Interception>()
                     .AddPolicy("ExceptionPolicy")
                     .AddMatchingRule(new CustomAttributeMatchingRule(typeof(ServiceException), false))
                     .AddCallHandler<ServiceExceptionCallHandler>();

            container.RegisterType<IBookRepository, BookEFRepository>(
                new Interceptor<InterfaceInterceptor>(), 
                new InterceptionBehavior<PolicyInjectionBehavior>());
            container.RegisterType<IReviewRepository, ReviewRepository>(
                new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<PolicyInjectionBehavior>());
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}