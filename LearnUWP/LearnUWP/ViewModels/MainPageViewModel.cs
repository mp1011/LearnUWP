﻿using FinancialDucks.Models;
using FinancialDucks.Models.FinancialEntities;
using FinancialDucks.Models.Transactions;
using FinancialDucks.Services;
using FinancialDucks.Services.UserServices;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace LearnUWP.ViewModels
{
    public class MainPageViewModel 
    {
        private readonly IUserSessionManager _sessionManager;
        private readonly TransactionService _transactionService;

        public ObservableCollection<BankAccount> BankAccounts { get; private set; }
        public ObservableCollection<Paycheck> Paychecks { get; private set; }
        public ObservableCollection<GoodOrService> Expenses { get; private set; }

    
        public MainPageViewModel(IUserSessionManager sessionManager, TransactionService transactionService)
        {
            _sessionManager = sessionManager;
            _transactionService = transactionService;
            Initialize();
        }

        public void Initialize()
        {
            var userFinances = _sessionManager.CurrentUserFinances;
            BankAccounts = new ObservableCollection<BankAccount>(userFinances.BankAccounts);
            Paychecks = new ObservableCollection<Paycheck>(userFinances.Paychecks);
            Expenses = new ObservableCollection<GoodOrService>(userFinances.Expenses);
        }
    }
}
