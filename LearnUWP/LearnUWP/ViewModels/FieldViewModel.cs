using LearnUWP.Services;
using System.ComponentModel;
using Windows.UI.Xaml;

namespace LearnUWP.ViewModels
{
    public class FieldViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public UIValidations UIValidations { get; private set; }

        public string DataFieldName { get; set; }

        private Style _fieldStyle;
        public Style FieldStyle
        {
            get => _fieldStyle;
            set
            {
                if(_fieldStyle != value)
                {
                    _fieldStyle = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FieldStyle)));
                }
            }
        }

        private string _errorTest;
        public string ErrorText
        {
            get => _errorTest;
            set
            {
                if (_errorTest != value)
                {
                    _errorTest = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ErrorText)));
                }
            }
        }
       
        public FieldViewModel(FinancialEntityCreateOrUpdateViewModel editViewModel)
        {
            UIValidations = editViewModel.UIValidations;
            UIValidations.PropertyChanged += UIValidations_PropertyChanged;
        }

        private void UIValidations_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            FieldStyle = DataFieldName == null ? null : UIValidations.StyleFor[DataFieldName];
            ErrorText = DataFieldName == null ? null : UIValidations.ErrorTextFor[DataFieldName];
        }
    }
}

