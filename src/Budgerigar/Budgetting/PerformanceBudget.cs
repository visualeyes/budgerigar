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

        private Exception exception;

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
                    onCompletion(new PerformanceBudgetResult(monitor.Name, budgetMs, result.DurationMilliseconds, this.exception) {
                        Steps = result.Steps
                    });
                }
            }
        }

        internal T RunAsync<T>(Func<PerformanceBudget, T> work) {
            try {
                return work(this);
            } catch (Exception e) {
                this.exception = e;
                throw;
            }
        }

        internal void RunAsync(Action<PerformanceBudget> work) {
            try {
                work(this);
            } catch (Exception e) {
                this.exception = e;
                throw;
            }
        }

        internal async Task<T> RunAsync<T>(Func<PerformanceBudget, Task<T>> work) {
            try {
                return await work(this);
            } catch (Exception e) {
                this.exception = e;
                throw;
            }
        }

        internal async Task RunAsync(Func<PerformanceBudget, Task> work) {
            try {
                await work(this);
            } catch (Exception e) {
                this.exception = e;
                throw;
            }
        }
    }
}
