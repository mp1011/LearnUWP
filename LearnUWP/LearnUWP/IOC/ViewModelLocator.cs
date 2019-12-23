using FinancialDucks.IOC;
using System;

namespace LearnUWP.IOC
{
    public class ViewModelLocator
    {
        public object this[string viewModelName]
        {
            get
            {
                try
                {
                    var viewModelType = GetViewModelType(viewModelName);
                    var viewModel = IOCContainer.Resolve(viewModelType);
                    return viewModel ?? throw new NullReferenceException();
                }
                catch (Exception e)
                {
                    throw new Exception($"Unable to resolve view model {viewModelName}", e);
                }
            }
        }

        private Type GetViewModelType(string viewModelName)
        {
            var assembly = typeof(ViewModelLocator).Assembly;
            var fullName = $"LearnUWP.ViewModels.{viewModelName}";
            var type = assembly.GetType(fullName);
            return type;
        }
    }

}
