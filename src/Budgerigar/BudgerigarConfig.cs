using Budgerigar.Budgetting;
using Budgerigar.Timing;
using Budgerigar.Timing.StopWatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Budgerigar {
    public class BudgerigarConfig {
        private static readonly ReaderWriterLockSlim lck = new ReaderWriterLockSlim();
        private static BudgerigarConfig configuration = new BudgerigarConfig();


        public BudgerigarConfig() {
            var stopwatchProvider = new StopwatchTimerProvider();
            this.TimerProviderFactory = new PerformanceTimerProviderFactory(stopwatchProvider);
        }

        public IPerformanceTimerProviderFactory TimerProviderFactory { get; set; }

        public static BudgerigarConfig Config {
            get {
                try {
                    lck.EnterReadLock();
                    return configuration;
                } finally {
                    lck.ExitReadLock();
                }
            }
        }

        public static void SetConfig(BudgerigarConfig config) {
            try {
                lck.EnterWriteLock();
                configuration = config;
            } finally {
                lck.ExitWriteLock();
            }
        }
    }
}
