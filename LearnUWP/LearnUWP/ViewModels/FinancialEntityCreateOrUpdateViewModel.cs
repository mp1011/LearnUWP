using FinancialDucks.Models;
using FinancialDucks.Services;
using FinancialDucks.Services.UserServices;
using GalaSoft.MvvmLight.Command;
using LearnUWP.Services;
using System.ComponentModel;
using System.Windows.Input;

namespace LearnUWP.ViewModels
{
    public abstract class FinancialEntityCreateOrUpdateViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public abstract ValidationResult[] Validate();

        protected void InvokePropertyChange(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public abstract class FinancialEntityCreateOrUpdateViewModel<T> : FinancialEntityCreateOrUpdateViewModel
        where T : FinancialEntity
    {
       
        protected IUserSessionManager SessionManager { get; }
        protected StorageService StorageService { get; }
        protected ValidationService ValidationService { get; }
        protected NavigationService NavigationService { get; }

        private T _originalModel;

        public string SaveActionName => _originalModel.ID > 0 ? "Save" : "Create";

        public bool IsSavedModel => _originalModel.ID > 0;

        public ICommand DeleteCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand SaveCommand { get; }

        public FinancialEntityCreateOrUpdateViewModel(IUserSessionManager sessionManager, 
            StorageService storageService, ValidationService validationService, NavigationService navigationService)
        {
            SessionManager = sessionManager;
            StorageService = storageService;
            ValidationService = validationService;
            NavigationService = navigationService;

            DeleteCommand = new RelayCommand(
                () =>
                {
                    StorageService.DeleteModelAndDependencies<T>(_originalModel);
                    SessionManager.CurrentUserFinances.ReloadRequired = true;
                    NavigationService.NavigateToMainPage();
                }, 
                keepTargetAlive: true);

            CancelCommand = new RelayCommand(
                () =>
                {
                    NavigationService.NavigateToMainPage();
                },
                keepTargetAlive: true);

            SaveCommand = new RelayCommand(
               () =>
               {
                   SaveModel();
                   NavigationService.NavigateToMainPage();
               },
               keepTargetAlive: true);
        }

        public void Initialize(T model)
        {
            model = model ?? StorageService.CreateNew<T>();
            _originalModel = model;
            SetDataModels(model);
        }

        protected abstract void SetDataModels(T model);

        public abstract T SaveModel();


    }
}
