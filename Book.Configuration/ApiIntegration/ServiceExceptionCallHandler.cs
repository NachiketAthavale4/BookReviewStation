namespace Book.Configuration.ApiIntegration
{
    using Book.Abstract.Interfaces;
    using System;
    using System.Reflection;
    using Unity.Interception.PolicyInjection.Pipeline;

    public class ServiceExceptionCallHandler : ICallHandlerExtension
    {
        private static string ErrorCode = string.Empty;

        public int Order { get; set; }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            IMethodReturn result = getNext()(input, getNext);

            if (result.Exception != null)
            {
                Type type = ((MethodInfo)input.MethodBase).ReturnType;
                result.ReturnValue = Activator.CreateInstance(type, args: new object[] { result.Exception.Message, ErrorCode });
                result.Exception = null;
            }

            return result;
        }

        public void SetErrorCode(string errorCode)
        {
            ErrorCode = errorCode;
        }
    }
}
