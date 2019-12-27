using Dapper.Contrib.Extensions;
using FinancialDucks.Data.Interfaces;

namespace FinancialDucks.Data.Models
{
    [Table("BankAccounts")]
    public class BankAccountDataModel : IWithID
    {
        [Key]
        public int ID { get; set; }

        public string Name { get; set; }
        public decimal InitialAmount { get; set; }
    }
}
