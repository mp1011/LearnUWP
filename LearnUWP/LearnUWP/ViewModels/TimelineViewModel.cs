using FinancialDucks.Models;
using FinancialDucks.Models.Timeline;
using FinancialDucks.Services;
using FinancialDucks.Services.UserServices;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LearnUWP.ViewModels
{
    public class TimelineViewModel : INotifyPropertyChanged
    {

        private readonly IUserSessionManager _userSessionManager;
        private readonly TimelineService _timelineService;
        private readonly TransactionService _transactionService;
        private readonly RecurrenceFactory _recurrenceFactory;
        public event PropertyChangedEventHandler PropertyChanged;



        #region Properties
        public ObservableCollection<FinancialSnapshotForDateRange> Timeline { get; } = new ObservableCollection<FinancialSnapshotForDateRange>();

        private DateTimeOffset _start;
        public DateTimeOffset RangeStart
        {
            get => _start;
            set
            {
                _start = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RangeStart)));
            }
        }

        private DateTimeOffset _end;
        public DateTimeOffset RangeEnd
        {
            get => _end;
            set
            {
                _end = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RangeEnd)));
            }
        }


        private TimelineInterval _interval = TimelineInterval.Day;
        public TimelineInterval TimelineInterval
        {
            get => _interval;
            set
            {
                _interval = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TimelineInterval)));
            }
        }

        private BankAccount _bankAccount;
        public BankAccount BankAccount
        {
            get => _bankAccount;
            set
            {
                if (_bankAccount != value)
                {
                    _bankAccount = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BankAccount)));
                }
            }
        }

        #endregion

        public TimelineViewModel(IUserSessionManager userSessionManager, TimelineService timelineService, 
            TransactionService transactionService, RecurrenceFactory recurrenceFactory)
        {
            _userSessionManager = userSessionManager;
            _timelineService = timelineService;
            _transactionService = transactionService;
            _recurrenceFactory = recurrenceFactory;

            RecalculateCommand = new RelayCommand(RecalculateTimeline, keepTargetAlive: true);

            RangeStart = DateTimeOffset.Now;
            RangeEnd = DateTimeOffset.Now.AddDays(7);
        }

        public ICommand RecalculateCommand { get; }
        
        private void RecalculateTimeline()
        {
            var history = _transactionService.CreateHistory(_userSessionManager.CurrentUserFinances.TransactionSchedules);

            var days = _timelineService
                        .CreateTimeline(
                            entity: BankAccount,
                            history: history,
                            range: new DateRange(RangeStart.Date, RangeEnd.Date));

            Timeline.Clear();

            var groupedTimeline =
                _timelineService.CreateGroupedTimeline(
                    days,
                    _recurrenceFactory.CreatePeriod(RangeStart.Date, TimelineInterval))
                .ToArray();

            foreach (var group in groupedTimeline)
                Timeline.Add(group);
        }
        
        public void Initialize()
        {
            BankAccount = BankAccount ?? _userSessionManager.CurrentUserFinances.BankAccounts.First();
            RecalculateTimeline();
        }
    }
}
