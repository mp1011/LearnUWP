using FinancialDucks.Models;
using FinancialDucks.Services;
using FinancialDucks.Services.UserServices;
using GalaSoft.MvvmLight.Command;
using LearnUWP.Services;
using System.ComponentModel;
using System.Windows.Input;

namespace LearnUWP.ViewModels
{
    public abstract class FinancialEntityCreateOrUpdateViewModel<T> : INotifyPropertyChanged, IDataErrorInfo
        where T : FinancialEntity
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected IUserSessionManager SessionManager { get; }
        protected StorageService StorageService { get; }

        protected NavigationService NavigationService { get; }

        private T _originalModel;

        public string SaveActionName => _originalModel.ID > 0 ? "Save" : "Create";

        public bool IsSavedModel => _originalModel.ID > 0;

        protected void InvokePropertyChange(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand DeleteCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand SaveCommand { get; }

        public FinancialEntityCreateOrUpdateViewModel(IUserSessionManager sessionManager, 
            StorageService storageService, NavigationService navigationService)
        {
            SessionManager = sessionManager;
            StorageService = storageService;
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

        #region Validation 

        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                return "You done goofed";
            }
        }


        #endregion

    }
}
