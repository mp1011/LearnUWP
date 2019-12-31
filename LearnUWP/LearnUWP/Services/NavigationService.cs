using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace LearnUWP.Services
{
    public class NavigationService
    {
        private static Frame AppFrame => Window.Current.Content as Frame;

        public void NavigateToMainPage()
        {
            AppFrame.Navigate(typeof(MainPage));
        }
    }
}
