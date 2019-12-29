using FinancialDucks.Data.Services;
using FinancialDucks.Services;
using FinancialDucks.Services.ModelStorageServices;
using FinancialDucks.Services.UserServices;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace FinancialDucks.IOC
{
    public static class DIRegistrar
    {
        public static void RegisterTypes(Action<ServiceCollection> registerTypes=null)
        {
            if (IOCContainer.IsInitialized)
                return;

            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton(typeof(IUserSessionManager),typeof(SingleUserInMemoryUserSessionManager));
            serviceCollection.AddSingleton(typeof(RecurrenceFactory));
            serviceCollection.AddSingleton(typeof(TransactionService));
            serviceCollection.AddSingleton(typeof(IConnectionProvider), typeof(ConnectionFromAppSettingsProvider));
            serviceCollection.AddSingleton(typeof(DAO));

            serviceCollection.AddImplementationsOf<IModelStorageService>();
            serviceCollection.AddSingleton(typeof(StorageService));

            registerTypes?.Invoke(serviceCollection);

            var container = serviceCollection.BuildServiceProvider();
            IOCContainer.SetContainer(container);
        }

        public static IServiceCollection AddImplementationsOf<T>(this IServiceCollection serviceCollection)
        {
            var assembly = typeof(T).Assembly;
            foreach (var type in assembly.GetTypes())
            {
                if (typeof(T).IsAssignableFrom(type) && type.IsClass && !type.IsAbstract)
                {
                    serviceCollection.AddSingleton(typeof(T), type);
                    serviceCollection.AddSingleton(type);
                }
            }

            return serviceCollection.AddSingleton<T[]>(sp => sp.GetServices<T>().ToArray());
        }
    }
}
