using Budgerigar.Budgetting;
using Budgerigar.Timing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Budgerigar.Tests.Budgetting {
    public class PerformanceBudgetResultFacts {

        [Fact]
        public void Does_Not_Throw_On_Null_Name() {
            new PerformanceBudgetResult(null, 1.0M, 1.0M, null);
        }

        [Fact]
        public void Does_Not_Throw_On_Whitespace_Name() {
            new PerformanceBudgetResult("  ", 1.0M, 1.0M, null);
        }

        [Fact]
        public void Throws_If_Budget_Negative() {
            Assert.Throws<ArgumentException>(() => new PerformanceBudgetResult(null, -1.0M, 1.0M, null));
        }

        [Fact]
        public void Throws_If_Duration_Negative() {
            Assert.Throws<ArgumentException>(() => new PerformanceBudgetResult(null, 1.0M, -1.0M, null));
        }

        [Fact]
        public void Steps_initialised() {
            var result = new PerformanceBudgetResult("test", 1.0M, 1.0M, null);
            Assert.NotNull(result.Steps);
            Assert.Empty(result.Steps);
        }


        [Theory]
        [InlineData(1.0, 2.0)]
        [InlineData(2.0, 1.0)]
        public void Calculates_Ms_Diff(decimal budgetMs, decimal durationMs) {
            bool isOver = budgetMs < durationMs;
            decimal msDiff = Math.Abs(durationMs - budgetMs);
            decimal diffPercentage = (msDiff / budgetMs) * 100;

            var result = new PerformanceBudgetResult("test", budgetMs, durationMs, null);

            Assert.Equal(isOver, result.IsOver);
            Assert.Equal(isOver, result.OverBudgetPercentage.HasValue);
            Assert.Equal(isOver, result.OverBudgetMilliseconds.HasValue);
            Assert.Equal(!isOver, result.UnderBudgetPercentage.HasValue);
            Assert.Equal(!isOver, result.UnderBudgetMilliseconds.HasValue);

            if (isOver) {
                Assert.Equal(diffPercentage, result.OverBudgetPercentage.Value);
                Assert.Equal(msDiff, result.OverBudgetMilliseconds.Value);
            } else {
                Assert.Equal(diffPercentage, result.UnderBudgetPercentage.Value);
                Assert.Equal(msDiff, result.UnderBudgetMilliseconds.Value);
            }
        }

        [Theory]
        [InlineData("test", 1.0, 2.0, "Budget for test was over performance budget by 100.000% (1.000ms).")]
        [InlineData(null, 1.0, 2.0, "Action was over performance budget by 100.000% (1.000ms).")]
        [InlineData("test", 2.0, 1.0, "Budget for test was under performance budget by 50.000% (1.000ms).")]
        [InlineData(null, 2.0, 1.0, "Action was under performance budget by 50.000% (1.000ms).")]
        public void Get_Result_Message(string name, decimal budgetMs, decimal durationMs, string expected) {
            var result = new PerformanceBudgetResult(name, budgetMs, durationMs, null);
            string actual = result.GetResultMessage();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("test", 1.0, 2.0, "Budget was set to 1.000ms\r\nDuration was 2.000ms\r\nBudget for test was over performance budget by 100.000% (1.000ms).\r\n")]
        [InlineData(null, 1.0, 2.0, "Budget was set to 1.000ms\r\nDuration was 2.000ms\r\nAction was over performance budget by 100.000% (1.000ms).\r\n")]
        [InlineData("test", 2.0, 1.0, "Budget was set to 2.000ms\r\nDuration was 1.000ms\r\nBudget for test was under performance budget by 50.000% (1.000ms).\r\n")]
        [InlineData(null, 2.0, 1.0, "Budget was set to 2.000ms\r\nDuration was 1.000ms\r\nAction was under performance budget by 50.000% (1.000ms).\r\n")]
        public void Get_Detailed_Output_Without_Steps(string name, decimal budgetMs, decimal durationMs, string expected) {
            var result = new PerformanceBudgetResult(name, budgetMs, durationMs, null);
            string actual = result.GetDetailedOutput();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Get_Detailed_Output_With_Steps() {
            string expected = "Budget was set to 2.000ms\r\nDuration was 1.000ms\r\nAction was under performance budget by 50.000% (1.000ms).\r\n  Step: one took 1.000ms\r\n";

            var steps = new List<PerformanceStepResult>() {
                new PerformanceStepResult("one", 1.0M)
            };

            var result = new PerformanceBudgetResult(null, 2.0M, 1.0M, null) { Steps = steps };

            string actual = result.GetDetailedOutput();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Get_Detailed_Output_With_Duplicate_Steps() {
            string expected = "Budget was set to 2.000ms\r\nDuration was 1.000ms\r\nAction was under performance budget by 50.000% (1.000ms).\r\n  Step: duplicate occured 3 times and took 1.000ms on average. 1 did not finish.\r\n";

            var steps = new List<PerformanceStepResult>() {
                new PerformanceStepResult("duplicate", 1.0M),
                new PerformanceStepResult("duplicate", 1.0M),
                new PerformanceStepResult("duplicate", null),
            };

            var result = new PerformanceBudgetResult(null, 2.0M, 1.0M, null) { Steps = steps };

            string actual = result.GetDetailedOutput();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Get_Detailed_Output_With_Unfinished_Step() {
            string expected = "Budget was set to 2.000ms\r\nDuration was 1.000ms\r\nAction was under performance budget by 50.000% (1.000ms).\r\n  Step: notfinished did not finish\r\n";

            var steps = new List<PerformanceStepResult>() {
                new PerformanceStepResult("notfinished", null),
            };

            var result = new PerformanceBudgetResult(null, 2.0M, 1.0M, null) { Steps = steps };

            string actual = result.GetDetailedOutput();

            Assert.Equal(expected, actual);
        }
    }
}
