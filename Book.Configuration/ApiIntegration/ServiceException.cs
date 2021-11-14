namespace Book.Configuration.ApiIntegration
{
    using Book.Abstract.Interfaces;
    using Unity;
    using Unity.Interception.PolicyInjection.Pipeline;
    using Unity.Interception.PolicyInjection.Policies;

    public class ServiceException : HandlerAttribute
    {
        public ICallHandlerExtension _serviceExceptionCallHandler { get; set; }

        public ServiceException(string errorCodeValue)
        {
            this._serviceExceptionCallHandler = UnityApi.container.Resolve<ICallHandlerExtension>();
            this._serviceExceptionCallHandler.SetErrorCode(errorCodeValue);
        }

        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            return this._serviceExceptionCallHandler;
        }
    }
}