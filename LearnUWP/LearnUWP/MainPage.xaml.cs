using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using LearnUWP.ViewModels;
using LearnUWP.Views;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace LearnUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPageViewModel ViewModel => DataContext as MainPageViewModel;

        public MainPage()
        {
            this.InitializeComponent();
            GotoAddBankAccount.Click += GotoAddBankAccount_Click;
            GotoAddPaycheck.Click += GotoAddPaycheck_Click;
            GotoAddExpense.Click += GotoAddExpense_Click;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ViewModel.Initialize();
        }

        private void GotoAddBankAccount_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddBankAccount));
        }

        private void GotoAddPaycheck_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddPaycheck));
        }

        private void GotoAddExpense_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddExpense));
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            var dataContext = (e.OriginalSource as FrameworkElement).DataContext;
            Frame.Navigate(typeof(AddBankAccount), dataContext);
        }
    }
}
