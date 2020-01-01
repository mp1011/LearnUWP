using FinancialDucks.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnUWP.ViewModels
{
    public class EnumPickerViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<PayCycle> Choices { get; } = new ObservableCollection<PayCycle>();

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
                {
                    Choices.Add((PayCycle)enumValue);
                }
            }
        }
    }
}
