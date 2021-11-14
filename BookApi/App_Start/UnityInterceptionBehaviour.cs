namespace BookApi.App_Start
{
    using System;
    using System.Collections.Generic;
    using Unity.Interception.InterceptionBehaviors;
    using Unity.Interception.PolicyInjection.Pipeline;

    public class UnityInterceptionBehaviour : IInterceptionBehavior
    {
        public bool WillExecute => true;

        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            IMethodReturn result = getNext()(input, getNext);

            return result;
        }
    }
}