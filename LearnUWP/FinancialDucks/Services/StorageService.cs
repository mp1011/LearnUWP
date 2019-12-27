using FinancialDucks.Data.Interfaces;
using FinancialDucks.Data.Services;
using FinancialDucks.Extensions;
using FinancialDucks.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinancialDucks.Services
{
    public class StorageService
    {
        private readonly DAO _dao;

        public StorageService(DAO dao)
        {
            _dao = dao;
        }

        public void StoreModel<TDataModel>(IStoreable<TDataModel> model)
            where TDataModel : class, IWithID
        {
            var dataModel = model.ToDataModel();
            _dao.Upsert(dataModel);
            model.SetFrom(dataModel);
        }

        //todo - need to filter by user or something
        public T[] LoadModels<T>()
            where T:IStoreable,new()
        {
            return this.DynamicDispatch<T[]>
            (
                methodName: nameof(this.LoadModels), 
                typeArguments: new Type[] { typeof(T), new T().DataModelType },
                args: null
            );
        }

        public T[] LoadModels<T,TDataModel>()
            where T:IStoreable<TDataModel>, new()
            where TDataModel:IWithID
        {
            var dataModels = _dao.Read<TDataModel>();

            return dataModels.Select(dataModel =>
            {
                var model = new T();
                model.SetFrom(dataModel);
                return model;
            }).ToArray();
        }
    }
}
