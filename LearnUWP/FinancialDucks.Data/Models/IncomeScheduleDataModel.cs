﻿using Dapper.Contrib.Extensions;
using FinancialDucks.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinancialDucks.Data.Models
{
    [Table("IncomeSchedules")]
    public class IncomeScheduleDataModel : IWithID
    {
        public int ID { get; set; }

        public int PaycheckID { get; set; }

        public int DepositBankID { get; set; }

        public int PayCycleID { get; set; }

        public DateTime PaymentDate { get; set; }
    }
}
