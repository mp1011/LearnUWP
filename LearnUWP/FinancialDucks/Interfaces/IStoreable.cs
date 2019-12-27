using FinancialDucks.Data.Interfaces;
using FinancialDucks.Models;
using System;

namespace FinancialDucks.Interfaces
{
    public interface IStoreable
    {
        Type DataModelType { get; }
    }

    public interface IStoreable<TDataModel> : IStoreable
        where TDataModel:IWithID
    {
        void SetFrom(TDataModel dataModel);

        TDataModel ToDataModel();
    }
}
