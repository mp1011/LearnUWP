using FinancialDucks.Services.UserServices;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinancialDucks.IOC
{
    public class DIRegistrar
    {
        public static void RegisterTypes(Action<ServiceCollection> registerTypes)
        {
            if (IOCContainer.IsInitialized)
                return;

            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton(typeof(IUserSessionManager),typeof(SingleUserInMemoryUserSessionManager));

            registerTypes(serviceCollection);

            var container = serviceCollection.BuildServiceProvider();
            IOCContainer.SetContainer(container);
        }
    }
}
