using LearnUWP.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using Windows.UI.Xaml;

namespace LearnUWP.Services
{
    public class UIValidationService : INotifyPropertyChanged
    {
        private readonly Style _errorStyle;
         
        private FinancialEntityCreateOrUpdateViewModel _viewModel;

        public event PropertyChangedEventHandler PropertyChanged;

        public Dictionary<string, Style> StyleFor { get; }
        public Dictionary<string, string> ErrorTextFor { get; }

        public bool HasNoErrors { get; private set; }

        public UIValidationService(FinancialEntityCreateOrUpdateViewModel viewModel)
        {
            StyleFor = new Dictionary<string, Style>();
            ErrorTextFor = new Dictionary<string, string>();

            _errorStyle = Application.Current.Resources["ErrorStyle"] as Style;
            _viewModel = viewModel;
            _viewModel.PropertyChanged += _viewModel_PropertyChanged;

            SetStyles();
        }

        private void _viewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            SetStyles(); // todo, inefficient
        }

        public void SetStyles()
        {
            bool hasErrors = false;

            foreach(var validation in _viewModel.Validate())
            {
                if (validation.IsValid)
                {
                    StyleFor[validation.PropertyName] = null;
                    ErrorTextFor[validation.PropertyName] = string.Empty;
                }
                else
                {
                    StyleFor[validation.PropertyName] = _errorStyle;
                    ErrorTextFor[validation.PropertyName] = validation.Message;
                    hasErrors = true;
                }
            }

            HasNoErrors = !hasErrors;

            //todo, should raise only when something is different
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(StyleFor)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ErrorTextFor)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HasNoErrors)));

        }
    }
}
