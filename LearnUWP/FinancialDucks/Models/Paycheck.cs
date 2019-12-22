namespace FinancialDucks.Models
{
    public class Paycheck : RecurringFinancialEntity
    {
        public Paycheck(string companyName, Recurrence recurrence, decimal initialAmount) 
            :base($"{companyName} paycheck", recurrence, initialAmount)
        {

        }
    }
}
