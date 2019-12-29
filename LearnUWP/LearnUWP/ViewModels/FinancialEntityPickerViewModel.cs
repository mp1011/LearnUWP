﻿using FinancialDucks.Models;
using FinancialDucks.Services.UserServices;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace LearnUWP.ViewModels
{
    public class FinancialEntityPickerViewModel
    {
        private readonly IUserSessionManager _userSessionManager;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<FinancialEntity> Choices { get; } = new ObservableCollection<FinancialEntity>();

        private FinancialEntity _selectedValue;
        public FinancialEntity SelectedValue
        {
            get => _selectedValue;
            set
            {
                if (_selectedValue != value)
                {
                    _selectedValue = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedValue)));
                }
            }
        }

        public FinancialEntityPickerViewModel(IUserSessionManager userSessionManager)
        {
            _userSessionManager = userSessionManager;
        }

        public void SetChoicesIfNeeded(Type financialEntityType)
        {
            if (Choices.Count == 0)
            {
                foreach (var entity in _userSessionManager
                    .GetCurrentUserFinances()
                    .GetEntities(financialEntityType))
                {
                    Choices.Add(entity);
                }
            }
        }
    }
}
