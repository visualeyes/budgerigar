using Budgerigar.Timing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgerigar.Budgetting {
    public class PerformanceBudget : IDisposable {
        private readonly IPerformanceTimerMonitor monitor;
        private readonly decimal budgetMs;
        private readonly Action<PerformanceBudgetResult> onCompletion;

        public PerformanceBudget(IPerformanceTimerMonitor monitor, decimal budgetMilliseconds, Action<PerformanceBudgetResult> onCompletion) {
            if (budgetMilliseconds < 0) throw new ArgumentException("Budget must be positive", "budgetMilliseconds");

            this.monitor = monitor;
            this.budgetMs = budgetMilliseconds;
            this.onCompletion = onCompletion;
        }

        internal IDisposable CreateStep(string name) {
            if (this.monitor == null) return new NoOpDisposable();

            return this.monitor.Step(name);
        }

        public void Dispose() {
            if (this.monitor != null) {
                var result = this.monitor.Stop();

                if (result != null && onCompletion != null) {
                    onCompletion(new PerformanceBudgetResult(monitor.Name, budgetMs, result.DurationMilliseconds) {
                        Steps = result.Steps
                    });
                }
            }
        }
    }
}
