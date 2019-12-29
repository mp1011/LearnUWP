using FinancialDucks.Data.Services;
using FinancialDucks.Services.ModelStorageServices;
using FinancialDucks.Services.UserServices;
using System;
using System.Linq;

namespace FinancialDucks.Services
{
    public class StorageService
    {
        private readonly IModelStorageService[] _modelStorageServices;
        private readonly IUserSessionManager _userSessionManager;

        internal DAO DAO { get; }

        public StorageService(
            DAO dao,
            IModelStorageService[] modelStorageServices, 
            IUserSessionManager userSessionManager)
        {
            DAO = dao;
            _modelStorageServices = modelStorageServices;
            _userSessionManager = userSessionManager;
        }

        private IModelStorageService<T> GetStorageService<T>()
        {
            var result = _modelStorageServices
                .OfType<IModelStorageService<T>>()
                .FirstOrDefault();

            if (result == null)
                throw new NullReferenceException($"There is no IModelStorageService implementation for type {typeof(T).Name}");

            return result;
        }

        public void StoreModel<T>(T model)
        {
            GetStorageService<T>()
                .Store(this, model);
        }

        public T LoadModel<T>(int id)
        {
            return GetStorageService<T>()
                .Load(this, id);
        }

        public T[] LoadModels<T>()
        {
            return GetStorageService<T>()
                .LoadAllForUser(this, _userSessionManager.CurrentUserID);
        }

        
    }
}
