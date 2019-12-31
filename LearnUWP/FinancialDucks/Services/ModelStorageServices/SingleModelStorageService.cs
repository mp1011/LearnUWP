using FinancialDucks.Data.Interfaces;
using FinancialDucks.Data.Services;
using System;
using System.Linq;

namespace FinancialDucks.Services.ModelStorageServices
{
    /// <summary>
    /// Handles loading and saving of models that have a 1:1 mapping with a database table
    /// </summary>
    public abstract class SingleModelStorageService<T, TDataModel> : IModelStorageService<T>
        where TDataModel : class, IWithID
    {
        public abstract TDataModel ToDataModel(StorageService storageService, T model);

        public abstract T FromDataModel(StorageService storageService, TDataModel dataModel);

        public T[] LoadAllForUser(StorageService storageService, int userID)
        {
            if (userID > 0)
                throw new NotImplementedException("Filtering by user not implemented yet");

            var models = storageService.DAO.Read<TDataModel>();
            return models
                .Select(m=> FromDataModel(storageService,m))
                .ToArray();
        }

        public T Store(StorageService storageService, T model)
        {
            var dataModel = ToDataModel(storageService, model);
            storageService.DAO.Upsert(dataModel);

            return storageService.LoadModel<T>(dataModel.ID);
        }

        public T Load(StorageService storageService, int id)
        {
            var model = storageService.DAO.Read<TDataModel>("ID=@id", new { id })
                .FirstOrDefault();
            if (model == null)
                throw new NullReferenceException($"There is no {typeof(T).Name} with ID {id}");

            return FromDataModel(storageService, model);
        }

        public abstract T CreateNew();

        public abstract void DeleteModelAndDependencies(StorageService storageService, T model);
    }
}
