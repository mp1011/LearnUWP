using FinancialDucks.Models;
using GalaSoft.MvvmLight.Command;
using LearnUWP.Services;
using System;
using System.Windows.Input;

namespace LearnUWP.ViewModels
{
    public class FinancialEntityDropdownItem
    {
        public FinancialEntity Entity { get; }

        public string Text => Entity?.Name ?? "Add New";

        public ICommand CreateOrEdit { get; }

        public FinancialEntityDropdownItem(FinancialEntity entity, NavigationService navigationService)
        {
            Entity = entity;
            CreateOrEdit = new RelayCommand(() =>
            {
                navigationService.NavigateToEditPageFor(entity);
            });
        }

        public FinancialEntityDropdownItem(Type entityType, NavigationService navigationService)
        {
            CreateOrEdit = new RelayCommand(() =>
            {
                navigationService.NavigateToCreatePageFor(entityType);
            });
        }
    }
}
