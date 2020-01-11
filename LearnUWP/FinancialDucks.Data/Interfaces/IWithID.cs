namespace FinancialDucks.Data.Interfaces
{
    public interface IWithID
    {
        int ID { get; set; }
        int? LocalID { get; set; }
    }
}
