namespace FinancialDucks.Models
{
    public class Paycheck : FinancialEntity
    {
        public Paycheck(string companyName, decimal initialAmount) 
            :base($"{companyName} paycheck", initialAmount)
        {

        }
    }
}
