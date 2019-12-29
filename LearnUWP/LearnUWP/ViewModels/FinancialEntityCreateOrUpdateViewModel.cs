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

        private readonly IUserSessionManager _sessionManager;
        private readonly StorageService _storageService;

        private T _originalModel;

        public string SaveActionName => _originalModel.ID > 0 ? "Save" : "Create";
     
        protected void InvokePropertyChange(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public FinancialEntityCreateOrUpdateViewModel(IUserSessionManager sessionManager, StorageService storageService)
        {
            _sessionManager = sessionManager;
            _storageService = storageService;
        }

        public void Initialize(T model)
        {
            model = model ?? _storageService.CreateNew<T>();
            _originalModel = model;
            SetDataModels(_storageService, model);
        }

        protected abstract void SetDataModels(StorageService storageService, T model);

        protected abstract T CreateOrUpdate(StorageService storageService);

        public void CreateOrUpdate()
        {
            var savedModel = CreateOrUpdate(_storageService);
            _sessionManager.CurrentUserFinances.AddEntity(savedModel);
        }
       
    }
}
