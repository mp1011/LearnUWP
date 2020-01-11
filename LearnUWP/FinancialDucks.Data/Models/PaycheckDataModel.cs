using Dapper.Contrib.Extensions;
using FinancialDucks.Data.Interfaces;

namespace FinancialDucks.Data.Models
{
    [Table("Paychecks")]
    public class PaycheckDataModel : IWithID
    {
        [Key]
        public int ID { get; set; }
        [Write(false)]
        public int? LocalID { get; set; }

        public string CompanyName { get; set; }
        public decimal InitialAmount { get; set; }
    }
}
