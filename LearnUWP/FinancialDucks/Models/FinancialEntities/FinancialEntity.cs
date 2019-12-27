using FinancialDucks.Data.Interfaces;
using FinancialDucks.Data.Models;
using FinancialDucks.Interfaces;
using System;

namespace FinancialDucks.Models
{
    public abstract class FinancialEntity
    {
        public int ID { get; protected set; }
        public string Name { get; protected set; }

        public decimal InitialAmount { get; protected set; }
    }

    public abstract class FinancialEntity<TDataModel> : FinancialEntity, IStoreable<TDataModel>
        where TDataModel:IWithID
    {
        public Type DataModelType => typeof(TDataModel);

        public abstract void SetFrom(TDataModel dataModel);

        public abstract TDataModel ToDataModel();
    }


}
