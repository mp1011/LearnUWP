using Dapper.Contrib.Extensions;

namespace FinancialDucks.Data.Models
{
    public abstract class EnumTable
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    [Table("PayCycles")]
    public class PayCycleDataModel : EnumTable { }

    [Table("RecurrenceTypes")]
    public class RecurrenceTypeDataModel : EnumTable { }


    [Table("AmountTypes")]
    public class AmountTypeDataModel : EnumTable { }

}
