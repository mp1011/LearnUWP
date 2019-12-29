using Dapper.Contrib.Extensions;
using FinancialDucks.Data.Interfaces;

namespace FinancialDucks.Data.Models
{
    [Table("Paychecks")]
    public class PaycheckDataModel : IWithID
    {
        [Key]
        public int ID { get; set; }

        public string CompanyName { get; set; }
        public decimal InitialAmount { get; set; }
    }
}
