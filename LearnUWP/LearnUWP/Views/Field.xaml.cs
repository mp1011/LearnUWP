using LearnUWP.Services;
using LearnUWP.ViewModels;
using System.ComponentModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace LearnUWP.Views
{
    public sealed partial class Field : UserControl, INotifyPropertyChanged
    {
        public UIValidationService UIValidation { get; private set; }

        public Style FieldStyle => DataFieldName == null ? null : UIValidation.StyleFor[DataFieldName];

        public string ErrorText => DataFieldName == null ? null : UIValidation.ErrorTextFor[DataFieldName];

        public Control FieldContent { get; set; }

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

        public event PropertyChangedEventHandler PropertyChanged;

        public Field()
        {
            InitializeComponent();
            DataContextChanged += Field_DataContextChanged;
        }

        private void Field_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            if (UIValidation == null && DataContext is FinancialEntityCreateOrUpdateViewModel viewModel)
            {
                UIValidation = viewModel.UIValidations;
                UIValidation.PropertyChanged += UIValidation_PropertyChanged;
            }
        }

        private void UIValidation_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //inefficient
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FieldStyle)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ErrorText)));

        }
    }
}
