using FinancialDucks.Data.Interfaces;
using FinancialDucks.Data.Services;
using FinancialDucks.Interfaces;
using System;
using System.Collections.Generic;
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
    }
}
