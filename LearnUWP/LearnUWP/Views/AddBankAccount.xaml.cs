using FinancialDucks.IOC;
using FinancialDucks.Models;
using FinancialDucks.Services.UserServices;
using LearnUWP.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LearnUWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddBankAccount : Page
    {
        public AddBankAccountViewModel ViewModel => DataContext as AddBankAccountViewModel;

        public AddBankAccount()
        {
            this.InitializeComponent();
            CreateButton.Click += CreateButton_Click;
            CancelButton.Click += CancelButton_Click;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel.Initialize(e.Parameter as BankAccount);
        }
        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.AddBankAccount();
            Frame.Navigate(typeof(MainPage));
        }
    }
}
