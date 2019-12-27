using FinancialDucks.Data.Interfaces;

namespace FinancialDucks.Interfaces
{
    public interface IStoreable
    {
    }

    public interface IStoreable<TDataModel> : IStoreable
        where TDataModel:IWithID
    {
        void SetFrom(TDataModel dataModel);

        TDataModel ToDataModel();
    }
}
