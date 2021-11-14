namespace Book.Abstract.Interfaces
{
    using Unity.Interception.PolicyInjection.Pipeline;

    public interface ICallHandlerExtension : ICallHandler
    {
        void SetErrorCode(string errorCode);
    }
}