using LearnUWP.Services;
using LearnUWP.ViewModels;
using System.ComponentModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace LearnUWP.Views
{
    public sealed partial class Field : UserControl
    {
        public Control FieldContent { get; set; }

        public FieldViewModel ViewModel { get; private set; }

        public string DataFieldName
        {
            get => GetValue(DataFieldNameProperty) as string;
            set => SetValue(DataFieldNameProperty, value);
        }

        public string Label
        {
            get => GetValue(LabelProperty) as string;
            set => SetValue(LabelProperty, value);
        }

        public static readonly DependencyProperty DataFieldNameProperty = DependencyProperty.Register(
              nameof(DataFieldName),
              typeof(string),
              typeof(Field),
              new PropertyMetadata(null)            
            );

        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register(
              nameof(Label),
              typeof(string),
              typeof(Field),
              new PropertyMetadata(null)
            );

        public Field()
        {
            InitializeComponent();
            DataContextChanged += Field_DataContextChanged;
        }

        private void Field_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            if (ViewModel == null && DataContext is FinancialEntityCreateOrUpdateViewModel viewModel)
            {
                ViewModel = new FieldViewModel(viewModel);
                ViewModel.DataFieldName = DataFieldName;
            }
        }
    }
}
