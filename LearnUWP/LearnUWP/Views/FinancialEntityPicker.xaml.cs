using FinancialDucks.IOC;
using FinancialDucks.Models;
using LearnUWP.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace LearnUWP.Views
{
    public sealed partial class FinancialEntityPicker : UserControl
    {
        public FinancialEntityPickerViewModel ViewModel { get; } 

        public FinancialEntityPicker()
        {
            ViewModel = IOCContainer.Resolve<FinancialEntityPickerViewModel>(); //constructor injection would be nice...
            this.InitializeComponent();
            this.DataContextChanged += EnumPicker_DataContextChanged;
            ViewModel.PropertyChanged += ViewModel_PropertyChanged;
            Picker.SelectionChanged += Picker_SelectionChanged;
        }

        private void Picker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.SelectedValue = (FinancialEntity)Picker.SelectedValue;
        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ViewModel.SelectedValue))
                DataContext = ViewModel.SelectedValue;
        }

        private void EnumPicker_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            if(DataContext is FinancialEntity entity)
            {
                ViewModel.SelectedValue = entity;
                ViewModel.SetChoicesIfNeeded(entity.GetType());
            }
        }
    }
}
