using LearnUWP.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace LearnUWP.Views
{
    public sealed partial class Timeline : UserControl
    {
        public TimelineViewModel ViewModel => DataContext as TimelineViewModel;
        public Timeline()
        {
            this.InitializeComponent();
            this.Loaded += Timeline_Loaded;
            Loading += Timeline_Loading;
        }

        private void Timeline_Loading(FrameworkElement sender, object args)
        {
            ViewModel.Initialize();
        }

        private void Timeline_Loaded(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
