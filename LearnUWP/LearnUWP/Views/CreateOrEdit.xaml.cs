using LearnUWP.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace LearnUWP.Views
{
    public sealed partial class CreateOrEdit : UserControl
    {
        public FinancialEntityCreateOrUpdateViewModel ViewModel => DataContext as FinancialEntityCreateOrUpdateViewModel;

        public string FullTitle 
        { 
            get
            {
                if (ViewModel.IsSavedModel)
                    return $"Edit {Title}";
                else
                    return $"Create {Title}";
            } 
        }
        public string Title
        {
            get => GetValue(TitleProperty) as string;
            set => SetValue(TitleProperty, value);
        }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
              nameof(Title),
              typeof(string),
              typeof(CreateOrEdit),
              new PropertyMetadata(null)
            );

        public FrameworkElement FormContent { get; set; }

        public CreateOrEdit()
        {
            this.InitializeComponent();
        }

    }
}
