using FinancialDucks.Extensions;
using FinancialDucks.IOC;
using FinancialDucks.Models;
using GalaSoft.MvvmLight.Command;
using LearnUWP.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LearnUWP.ViewModels
{
    public class FinancialEntityDropdownMenuViewModel 
    {

        private readonly NavigationService _navigationService;

        public ObservableCollection<FinancialEntityDropdownItem> Items { get; } = new ObservableCollection<FinancialEntityDropdownItem>();

        public static FinancialEntityDropdownMenuViewModel TryCreate(object dataContext)
        {
            var genericArgType = dataContext
                .GetType()
                .GetGenericArguments()
                .FirstOrDefault();

            if (genericArgType != null)
            {
                var navigationService = IOCContainer.Resolve<NavigationService>();
                return new FinancialEntityDropdownMenuViewModel(navigationService, genericArgType, dataContext);
            }

            return null;
        }

        public FinancialEntityDropdownMenuViewModel(NavigationService navigationService, Type entityType, object entityCollection)
        {
            _navigationService = navigationService;
            this.DynamicDispatch(nameof(Initialize), new Type[] { entityType }, new object[] { navigationService, entityCollection });
        }

        public void Initialize<T>(NavigationService navigationService, ObservableCollection<T> collection)
            where T:FinancialEntity
        {
            //todo, refresh on change

            Items.Clear();
            foreach(var item in collection)
                Items.Add(new FinancialEntityDropdownItem(item, navigationService));

            Items.Add(new FinancialEntityDropdownItem(typeof(T), navigationService));
        }
    }
}
