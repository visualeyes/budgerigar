using Budgerigar.Budgetting;
using Budgerigar.Timing;
using Budgerigar.Timing.StopWatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Budgerigar.Mvc {
    public class BudgerigarWebConfig {
        private static readonly ReaderWriterLockSlim lck = new ReaderWriterLockSlim();
        private static BudgerigarWebConfig configuration = new BudgerigarWebConfig();


        public BudgerigarWebConfig() {
            var stopwatchProvider = new StopwatchTimerProvider();
            this.TimerProviderFactory = new PerformanceTimerProviderFactory(stopwatchProvider);
        }

        public IPerformanceTimerProviderFactory TimerProviderFactory { get; set; }
        public Action<PerformanceBudgetResult> OnBudgetResult { get; set; }

        public static BudgerigarWebConfig Config {
            get { return configuration; }
        }

        public static void SetConfig(BudgerigarWebConfig config) {
            try {
                lck.EnterWriteLock();
                configuration = config;
            } finally {
                lck.ExitWriteLock();
            }
        }
    }
}
