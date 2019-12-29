using FinancialDucks.Models;
using FinancialDucks.Services;
using FinancialDucks.Services.UserServices;
using System.ComponentModel;

namespace LearnUWP.ViewModels
{
    public abstract class CreateOrEditViewModel<T> : INotifyPropertyChanged
        where T : FinancialEntity
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly IUserSessionManager _sessionManager;
        private readonly StorageService _storageService;

        public string SaveActionName => "fix me"; // DataModel.ID > 0 ? "Save" : "Create";

     
        protected void InvokePropertyChange(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public CreateOrEditViewModel(IUserSessionManager sessionManager, StorageService storageService)
        {
            _sessionManager = sessionManager;
            _storageService = storageService;
        }

        public void Initialize(T model)
        {
            throw new System.NotImplementedException();
        //    if (model != null)
        //        DataModel = model.ToDataModel();
        //    else
        //        DataModel = new T().ToDataModel();

        //    Initialize(_sessionManager);
        }

        protected virtual void Initialize(IUserSessionManager sessionManager)
        {

        }

        public void CreateOrUpdate()
        {
            throw new System.NotImplementedException();
            //var userFinances = _sessionManager.GetCurrentUserFinances();

            //var model = userFinances.TryGetEntity<T>(DataModel.ID);
            //if (model == null)
            //{
            //    model = new T();
            //    userFinances.AddEntity(model);
            //}

            //model.SetFrom(DataModel);
            //_storageService.StoreModel(model);

            //AfterModelSaved(_storageService, model);
        }

        protected virtual void AfterModelSaved(StorageService storage, T savedModel)
        {

        }
    }
}
