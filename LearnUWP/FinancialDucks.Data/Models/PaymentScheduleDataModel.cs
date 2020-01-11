using Dapper.Contrib.Extensions;
using FinancialDucks.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinancialDucks.Data.Models
{
    [Table("PaymentSchedules")]
    public class PaymentScheduleDataModel : IWithID
    {
        [Key]
        public int ID { get; set; }
        [Write(false)]
        public int? LocalID { get; set; }
        public int BankAccountID { get; set; }
        public int ExpenseID { get; set; }
        public int RecurrenceTypeID { get; set; }
        public DateTime FirstPaymentDate { get; set; }
        public DateTime LastPaymentDate { get; set; }
        public decimal Amount { get; set; }
        public int AmountTypeID { get; set; }
        public string Description { get; set; }
    }
}
