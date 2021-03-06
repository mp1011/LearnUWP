﻿using FinancialDucks.Data.Services;

namespace FinancialDucks.Services.ModelStorageServices
{
    public interface IModelStorageService
    {
    }

    public interface IModelStorageService<T> : IModelStorageService
    {
        T CreateNew();
        T Store(StorageService storageService, T model);
        T[] LoadAllForUser(StorageService storageService, int userID);
        T Load(StorageService storageService, int id);
        void DeleteModelAndDependencies(StorageService storageService, T model);
    }

}
