namespace FinancialDucks.Models
{
    public class BankAccount : FinancialEntity
    {
        public BankAccount(int id, string name, decimal initialAmount)
            :base(id, name,initialAmount)
        {
        }
    }
}
