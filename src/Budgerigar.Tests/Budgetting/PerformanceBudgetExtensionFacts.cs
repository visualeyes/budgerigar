using Budgerigar.Budgetting;
using Budgerigar.Timing.StopWatch;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Budgerigar.Tests.Budgetting {
    public class PerformanceBudgetExtensionFacts {

        [Fact]
        public void Step_Returns_Value_If_Budget_Null() {
            PerformanceBudget budget = null;
            var step = budget.Step("step");
            Assert.NotNull(step);
        }

        [Fact]
        public void Step_Func_Performs_Work_Value_If_Budget_Null() {
            PerformanceBudget budget = null;
            bool workDone = budget.Step("step", () => true);            
            Assert.True(workDone);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Step_Func_Creates_Step(bool hasMonitor) {
            string stepName = "step";
            PerformanceBudgetResult result = null;

            var budget = GetBudget(hasMonitor, (r) => result = r);
            bool workDone = budget.Step(stepName, () => true);
            budget.Dispose();

            Assert.True(workDone);
            AssertResult(hasMonitor, stepName, result);
        }

        [Fact]
        public async Task StepAsync_Func_Performs_Work_Value_If_Budget_Null() {
            PerformanceBudget budget = null;
            bool workDone = await budget.StepAsync("step", () => Task.FromResult(true));
            Assert.True(workDone);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task StepAsync_Func_Creates_Step(bool hasMonitor) {
            string stepName = "step";
            PerformanceBudgetResult result = null;

            var budget = GetBudget(hasMonitor, (r) => result = r);
            bool workDone = await budget.StepAsync(stepName, () => Task.FromResult(true));
            budget.Dispose();

            Assert.True(workDone);
            AssertResult(hasMonitor, stepName, result);
        }

        [Fact]
        public void Step_Action_Performs_Work_Value_If_Budget_Null() {
            PerformanceBudget budget = null;
            bool workDone = false;
            budget.Step("step", () => { workDone = true; });
            Assert.True(workDone);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Step_Action_Creates_Step(bool hasMonitor) {
            string stepName = "step";
            PerformanceBudgetResult result = null;

            var budget = GetBudget(hasMonitor, (r) => result = r);
            bool workDone = false;
            budget.Step(stepName, () => { workDone = true; });
            budget.Dispose();

            Assert.True(workDone);
            AssertResult(hasMonitor, stepName, result);
        }

        [Fact]
        public async Task StepAsync_Action_Performs_Work_Value_If_Budget_Null() {
            PerformanceBudget budget = null;
            bool workDone = false;
            await budget.StepAsync("step", () => {
                workDone = true;
                return (Task)Task.FromResult(0);
            });
            Assert.True(workDone);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task StepAsync_Action_Creates_Step(bool hasMonitor) {
            string stepName = "step";
            PerformanceBudgetResult result = null;

            var budget = GetBudget(hasMonitor, (r) => result = r);
            bool workDone = false;
            await budget.StepAsync(stepName, () => {
                workDone = true;
                return (Task)Task.FromResult(0);
            });
            budget.Dispose();

            Assert.True(workDone);
            AssertResult(hasMonitor, stepName, result);
        }

        private static void AssertResult(bool hasMonitor, string stepName, PerformanceBudgetResult result) {
            if (hasMonitor) {
                Assert.NotNull(result);
                Assert.Equal(1, result.Steps.Count());
                Assert.Equal(stepName, result.Steps.Single().Name);
            } else {
                Assert.Null(result);
            }
        }

        private static PerformanceBudget GetBudget(bool hasMonitor, Action<PerformanceBudgetResult> onCompletion) {
            StopwatchPerformanceTimerMonitor monitor = null;

            if (hasMonitor) {
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                monitor = new StopwatchPerformanceTimerMonitor("test", stopwatch);
            }

            return new PerformanceBudget(monitor, 10M, onCompletion);
        }
    }
}
