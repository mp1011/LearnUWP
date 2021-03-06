﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace LearnUWP.ViewModels
{
    public class EnumPickerViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<object> Choices { get; } = new ObservableCollection<object>();

        private object _selectedValue = string.Empty;
        public object SelectedValue
        {
            get => _selectedValue;
            set
            {
                if(_selectedValue != value)
                {
                    _selectedValue = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedValue)));
                }
            }
        }

        public void SetChoices(object choice)
        {
            if (!Choices.Any())
            {
                foreach (var enumValue in Enum.GetValues(choice.GetType()))
                    Choices.Add(enumValue);  
            }
        }
    }
}
