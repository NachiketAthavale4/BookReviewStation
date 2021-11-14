namespace Book.Configuration
{
    using Unity;

    public class UnityApi
    {
        private static IUnityContainer _unityContainer = null;

        private UnityApi()
        {
            _unityContainer = new UnityContainer();
        }

        public static IUnityContainer container 
        { 
            get 
            {
                if (_unityContainer == null)
                {
                    new UnityApi();
                }

                return _unityContainer;
            }
        }
    }
}