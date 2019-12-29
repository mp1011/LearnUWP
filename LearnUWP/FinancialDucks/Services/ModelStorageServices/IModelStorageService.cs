using FinancialDucks.Data.Services;

namespace FinancialDucks.Services.ModelStorageServices
{
    public interface IModelStorageService
    {
    }

    public interface IModelStorageService<T> : IModelStorageService
    {
        void Store(StorageService storageService, T model);
        T[] LoadAllForUser(StorageService storageService, int userID);
        T Load(StorageService storageService, int id);
    }

}
