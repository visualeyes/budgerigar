using Budgerigar.Budgetting;
using Budgerigar.Timing;
using Budgerigar.Timing.StopWatch;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Budgerigar.Tests.Budgetting {
    
    public class PerformanceBudgetFacts {
        
        [Fact]
        public void Does_Not_Throws_If_Monitor_IsNull() {
            new PerformanceBudget(null, 1.0M, null);
        }

        [Fact]
        public void Throws_If_Monitor_Is_Not_Positive() {
            var timer = GetMonitor(true);

            Assert.Throws<ArgumentException>(() => {
                new PerformanceBudget(timer, -1.0M, null);
            });
        }

        [Fact]
        public void Does_Not_Throw_If_OnCompletion_Null() {
            var timer = GetMonitor(true);
            new PerformanceBudget(timer, 1.0M, null);
        }
        
        [Fact]
        public void Dispose_Does_Not_Throw_If_Monitor_Is_Null() {
            var budget = new PerformanceBudget(null, 1.0M, (r) => { });
            budget.Dispose();
        }

        [Fact]
        public void Dispose_Does_Not_Throw_If_OnCompletion_Null() {
            var timer = GetMonitor(true);
            var budget = new PerformanceBudget(timer, 1.0M, null);
            budget.Dispose();
        }

        [Fact]
        public void OnCompletion_Not_Triggered_If_No_Result() {
            var timer = GetMonitor(false);

            PerformanceBudgetResult result = null;

            var budget = new PerformanceBudget(timer, 1.0M, (r) => result = r);
            budget.Dispose();

            Assert.Null(result);
        }

        [Fact]
        public void OnCompletion_Triggered_If_Has_Result() {
            var timer = GetMonitor(true);

            PerformanceBudgetResult result = null;

            var budget = new PerformanceBudget(timer, 1.0M, (r) => result = r);
            budget.Dispose();

            Assert.NotNull(result);
        }

        private static IPerformanceTimerMonitor GetMonitor(bool willProduceResult) {
            var stopwatch = new Stopwatch();

            if(willProduceResult) {
                stopwatch.Start();
            }

            return new StopwatchPerformanceTimerMonitor("test", stopwatch);
        }
    }
}
