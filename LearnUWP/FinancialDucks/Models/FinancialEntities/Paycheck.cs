namespace FinancialDucks.Models
{
    public class Paycheck : FinancialEntity
    {
        public Paycheck(string companyName, decimal initialAmount)
        {
            Name = $"{companyName} paycheck";
            InitialAmount = initialAmount;
        }
    }
}
