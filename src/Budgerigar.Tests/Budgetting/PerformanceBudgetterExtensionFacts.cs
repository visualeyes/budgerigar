using Budgerigar.Budgetting;
using Budgerigar.Timing;
using Budgerigar.Timing.StopWatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Budgerigar.Tests.Budgetting {
    public class PerformanceBudgetterExtensionFacts {

        public IPerformanceBudgetter GetBudgetter() {            
            var timer = new PerformanceTimerProviderFactory(new StopwatchTimerProvider());
            return new PerformanceBudgetter(timer);
        }

        [Fact]
        public void RunWithBudget_Action_Does_Work() {
            bool workDone = false;
            PerformanceBudget budget = null;
            IPerformanceBudgetter budgetter = GetBudgetter();

            budgetter.RunWithBudget("test", 1.0M, (b) => { workDone = true; budget = b; }, null);

            Assert.True(workDone);
            Assert.NotNull(budget);
        }

        [Fact]
        public void RunWithBudget_Action_Does_Work_If_Budgetter_Null() {
            bool workDone = false;
            PerformanceBudget budget = null;
            IPerformanceBudgetter budgetter = null;

            budgetter.RunWithBudget("test", 1.0M, (b) => { workDone = true; budget = b; }, null);

            Assert.True(workDone);
            Assert.Null(budget);
        }

        [Fact]
        public void RunWithBudget_Func_Does_Work() {
            PerformanceBudget budget = null;
            IPerformanceBudgetter budgetter = GetBudgetter();

            bool workDone = budgetter.RunWithBudget("test", 1.0M, (b) => { budget = b; return true; }, null);

            Assert.True(workDone);
            Assert.NotNull(budget);
        }

        [Fact]
        public void RunWithBudget_Func_Does_Work_If_Budgetter_Null() {
            PerformanceBudget budget = null;
            IPerformanceBudgetter budgetter = null;

            bool workDone = budgetter.RunWithBudget("test", 1.0M, (b) => { budget = b; return true; }, null);

            Assert.True(workDone);
            Assert.Null(budget);
        }

        [Fact]
        public async Task RunWithBudgetAsync_Action_Does_Work() {
            bool workDone = false;
            PerformanceBudget budget = null;
            IPerformanceBudgetter budgetter = GetBudgetter();

            await budgetter.RunWithBudgetAsync("test", 1.0M, (b) => { workDone = true; budget = b; return Task.FromResult(0); }, null);

            Assert.True(workDone);
            Assert.NotNull(budget);
        }

        [Fact]
        public async Task RunWithBudgetAsync_Action_Does_Work_If_Budgetter_Null() {
            bool workDone = false;
            PerformanceBudget budget = null;
            IPerformanceBudgetter budgetter = null;

            await budgetter.RunWithBudgetAsync("test", 1.0M, (b) => { workDone = true; budget = b; return Task.FromResult(0); }, null);

            Assert.True(workDone);
            Assert.Null(budget);
        }

        [Fact]
        public async Task RunWithBudgetAsync_Func_Does_Work() {
            PerformanceBudget budget = null;
            IPerformanceBudgetter budgetter = GetBudgetter();

            bool workDone = await budgetter.RunWithBudgetAsync("test", 1.0M, (b) => { budget = b; return Task.FromResult(true); }, null);

            Assert.True(workDone);
            Assert.NotNull(budget);
        }

        [Fact]
        public async Task RunWithBudgetAsync_Func_Does_Work_If_Budgetter_Null() {
            PerformanceBudget budget = null;
            IPerformanceBudgetter budgetter = null;

            bool workDone = await budgetter.RunWithBudgetAsync("test", 1.0M, (b) => { budget = b; return Task.FromResult(true); }, null);

            Assert.True(workDone);
            Assert.Null(budget);
        }
    }
}
