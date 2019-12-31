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
        internal DAO DAO { get; }

        public StorageService(
            DAO dao,
            IModelStorageService[] modelStorageServices)
        {
            DAO = dao;
            _modelStorageServices = modelStorageServices;
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

        public T CreateNew<T>()
        {
            return GetStorageService<T>()
               .CreateNew();
        }

        public T StoreModel<T>(T model)
        {
            return GetStorageService<T>()
                .Store(this, model);
        }

        public T LoadModel<T>(int id)
        {
            //todo, we could avoid hitting the database for financial entity models by reading from the session first
            return GetStorageService<T>()
                .Load(this, id);
        }

        public T[] LoadModelsForUser<T>(int userID)
        {
            return GetStorageService<T>()
                .LoadAllForUser(this, userID);
        }

        public void DeleteModelAndDependencies<T>(T model)
        {
            GetStorageService<T>()
                .DeleteModelAndDependencies(this, model);
        }
    }
}
