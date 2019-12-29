using FinancialDucks.Models;
using FinancialDucks.Services;
using FinancialDucks.Services.UserServices;
using System.ComponentModel;

namespace LearnUWP.ViewModels
{
    public abstract class FinancialEntityCreateOrUpdateViewModel<T> : INotifyPropertyChanged
        where T : FinancialEntity
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected IUserSessionManager SessionManager { get; }
        protected StorageService StorageService { get; }

        private T _originalModel;

        public string SaveActionName => _originalModel.ID > 0 ? "Save" : "Create";
     
        protected void InvokePropertyChange(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public FinancialEntityCreateOrUpdateViewModel(IUserSessionManager sessionManager, StorageService storageService)
        {
            SessionManager = sessionManager;
            StorageService = storageService;
        }

        public void Initialize(T model)
        {
            model = model ?? StorageService.CreateNew<T>();
            _originalModel = model;
            SetDataModels(model);
        }

        protected abstract void SetDataModels(T model);

        protected abstract T SaveModel();

        public void SaveModelAndUpdateUserFinances()
        {
            var savedModel = SaveModel();
            SessionManager.CurrentUserFinances.AddEntity(savedModel);
        }
       
    }
}
