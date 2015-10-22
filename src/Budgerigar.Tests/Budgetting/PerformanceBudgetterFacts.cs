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
    public class PerformanceBudgetterFacts {

        [Fact]
        public void Does_Not_Throw_If_Timer_Null() {
            new PerformanceBudgetter(null);
        }

        [Fact]
        public void Returns_Budget_If_Timer_Null() {
            var budgetter = new PerformanceBudgetter(null);
            var budget = budgetter.RunWithBudget("test", 1.0M, null);

            Assert.NotNull(budget);
        }

        [Fact]
        public void Returns_Budget() {
            var timer = new PerformanceTimer(new StopwatchTimerProvider());
            var budgetter = new PerformanceBudgetter(timer);

            var budget = budgetter.RunWithBudget("test", 1.0M, null);

            Assert.NotNull(budget);
        }
    }
}
