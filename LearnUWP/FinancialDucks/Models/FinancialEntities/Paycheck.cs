namespace FinancialDucks.Models
{
    public class Paycheck : FinancialEntity
    {

        public string CompanyName
        {
            get
            {
                return Name.Replace(" paycheck", "");
            }
        }

        public Paycheck(int id, string companyName, decimal initialAmount) 
            : base(id, $"{companyName} paycheck",initialAmount)
        {
        }
    }
}
