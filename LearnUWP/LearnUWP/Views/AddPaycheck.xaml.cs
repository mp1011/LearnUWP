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
    public sealed partial class AddPaycheck : Page
    {

        public AddPaycheckViewModel ViewModel => DataContext as AddPaycheckViewModel;

        public AddPaycheck()
        {
            this.InitializeComponent();
            CreateButton.Click += CreateButton_Click;

            foreach (PayCycle payCycle in Enum.GetValues(typeof(PayCycle)))
                PayCyclePicker.Items.Add(payCycle);
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.AddPaycheck();
            Frame.Navigate(typeof(MainPage));
        }
    }
}
