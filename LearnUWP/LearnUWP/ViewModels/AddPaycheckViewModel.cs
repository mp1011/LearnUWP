using FinancialDucks.Models;
using FinancialDucks.Services;
using FinancialDucks.Services.UserServices;
using System;
using System.ComponentModel;

namespace LearnUWP.ViewModels
{
    public class AddPaycheckViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private readonly IUserSessionManager _sessionManager;
        private readonly DateService _dateService;

        private string _companyName;
        public string CompanyName
        {
            get => _companyName;
            set
            {
                if (_companyName != value)
                {
                    _companyName = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CompanyName)));
                }
            }
        }

        private PayCycle _payCycle;
        public PayCycle PayCycle
        {
            get => _payCycle;
            set
            {
                if (_payCycle != value)
                {
                    _payCycle = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PayCycle)));
                }
            }
        }

        private DateTimeOffset _firstPayDate;
        public DateTimeOffset FirstPayDate
        {
            get => _firstPayDate;
            set
            {
                if (_firstPayDate != value)
                {
                    _firstPayDate = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FirstPayDate)));
                }
            }
        }

        private decimal _amount;
        public decimal Amount
        {
            get => _amount;
            set
            {
                if (_amount != value)
                {
                    _amount = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Amount)));
                }
            }
        }

        public AddPaycheckViewModel(IUserSessionManager sessionManager, DateService dateService)
        {
            _sessionManager = sessionManager;
            _dateService = dateService;
            FirstPayDate = DateTime.Now;
        }

        public void AddPaycheck()
        {
            var userFinances = _sessionManager.GetCurrentUserFinances();

            var startDate = FirstPayDate.DateTime;

            //todo, recurrence needs to be on another screen

            userFinances.AddEntity(new Paycheck(
                companyName: CompanyName,
                //recurrence: _dateService.CreateRecurrence(startDate, startDate.AddYears(100), PayCycle),
                initialAmount: Amount));
        }
    }
}
