﻿using Budgerigar.Timing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgerigar.Budgetting {
    public class PerformanceBudgetter : IPerformanceBudgetter {

        private readonly IPerformanceTimerProviderFactory timer;
        
        public PerformanceBudgetter() 
            : this(BudgerigarConfig.Config.TimerProviderFactory) {
        }

        public PerformanceBudgetter(IPerformanceTimerProviderFactory timer) {
            this.timer = timer;
        }

        public PerformanceBudget RunWithBudget(string name, decimal milliseconds, Action<PerformanceBudgetResult> onCompletion) {
            var monitor = timer.Start(name);

            return new PerformanceBudget(monitor, milliseconds, onCompletion);
        }
    }
}
