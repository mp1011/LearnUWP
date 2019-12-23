using Microsoft.Extensions.DependencyInjection;
using System;

namespace FinancialDucks.IOC
{
    public class IOCContainer
    {
        private static ServiceProvider _container;

        public static bool IsInitialized => _container != null;


        public static void SetContainer(ServiceProvider container)
        {
            _container = container;
        }

        public static T Resolve<T>()
        {
            return _container.GetService<T>();
        }

        public static object Resolve(Type t)
        {
            return _container.GetService(t);
        }
    }


}
