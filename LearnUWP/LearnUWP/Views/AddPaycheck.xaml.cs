using FinancialDucks.Models;
using LearnUWP.Services;
using LearnUWP.ViewModels;
using Windows.UI.Xaml.Controls;
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
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel.Initialize(e.Parameter as Paycheck);
        }
    }
}
