using FinancialDucks.Models;
using FinancialDucks.Models.FinancialEntities;
using LearnUWP.Views;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace LearnUWP.Services
{
    public class NavigationService
    {
        private static Frame AppFrame => Window.Current.Content as Frame;

        public void NavigateToMainPage()
        {
            AppFrame.Navigate(typeof(MainPage));
        }

        public void NavigateToCreatePageFor(Type entityType)
        {
            AppFrame.Navigate(GetEditViewModelType(entityType));
        }

        public void NavigateToEditPageFor(FinancialEntity entity)
        {
            AppFrame.Navigate(GetEditViewModelType(entity.GetType()), entity);
        }

        private Type GetEditViewModelType(Type modelType)
        {
            //todo, can we make automatic?
            if (modelType == typeof(BankAccount))
                return typeof(AddBankAccount);

            if (modelType == typeof(Paycheck))
                return typeof(AddPaycheck);

            if (modelType == typeof(GoodOrService))
                return typeof(AddExpense);

            throw new Exception($"There is no edit form defined for type {modelType}");
        }
    }
}
