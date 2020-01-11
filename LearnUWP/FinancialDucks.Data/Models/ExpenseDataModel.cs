using Dapper.Contrib.Extensions;
using FinancialDucks.Data.Interfaces;

namespace FinancialDucks.Data.Models
{
    [Table("Expenses")]
    public class ExpensesDataModel : IWithID
    {
        [Key]
        public int ID { get; set; }
        [Write(false)]
        public int? LocalID { get; set; }
        public string Description { get; set; }
        public decimal InitialAmount { get; set; }
    }
}
