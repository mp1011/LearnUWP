using FinancialDucks.Models;
using LearnUWP.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LearnUWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddExpense : Page
    {
        public AddExpenseViewModel ViewModel => DataContext as AddExpenseViewModel;

        public AddExpense()
        {
            this.InitializeComponent();
            CreateButton.Click += CreateButton_Click;
            this.DataContextChanged += AddExpense_DataContextChanged;

        }

        private void AddExpense_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            if (ViewModel != null)
            {
                ViewModel.PropertyChanged += ViewModel_PropertyChanged;
            }
        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ViewModel.RecurrenceType))
            {
                if (ViewModel.RecurrenceType == RecurrenceType.OneTime)
                    PayDateLabel.Text = "Payment Date:";
                else
                    PayDateLabel.Text = "First Payment Date:";
            }
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.CreateOrUpdate();
            Frame.Navigate(typeof(MainPage));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel.Initialize();
        }

    }
}
