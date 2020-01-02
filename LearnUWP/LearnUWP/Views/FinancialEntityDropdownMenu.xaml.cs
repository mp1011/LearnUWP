using LearnUWP.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace LearnUWP.Views
{
    public sealed partial class FinancialEntityDropdownMenu : UserControl
    {
        public FinancialEntityDropdownMenuViewModel ViewModel { get; private set; }

        public string Title
        {
            get => GetValue(TitleProperty) as string;
            set => SetValue(TitleProperty, value);
        }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
              nameof(Title),
              typeof(string),
              typeof(FinancialEntityDropdownMenu),
              new PropertyMetadata(null)
            );

        public FinancialEntityDropdownMenu()
        {
            InitializeComponent();
            DataContextChanged += FinancialEntityDropdownMenu_DataContextChanged;
            MenuButton.Click += MenuButton_Click;
        }

        private void FinancialEntityDropdownMenu_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            if (ViewModel == null)
                ViewModel = FinancialEntityDropdownMenuViewModel.TryCreate(DataContext);
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout(MenuButton);
        }
    }
}
