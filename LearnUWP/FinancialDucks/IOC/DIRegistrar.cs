using FinancialDucks.Services;
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
            serviceCollection.AddSingleton(typeof(DateService));
            serviceCollection.AddSingleton(typeof(TransactionService));

            registerTypes(serviceCollection);

            var container = serviceCollection.BuildServiceProvider();
            IOCContainer.SetContainer(container);
        }
    }
}
