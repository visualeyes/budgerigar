using Budgerigar.Budgetting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Budgerigar.XUnit {
    public static class AssertExtensions {
        public static void AssertBudget(this PerformanceBudgetResult result, decimal minDurationMsToThrowOver = 0.01M, decimal minBudgetMsToThrowUnder = 1.0M, ITestOutputHelper outputHelper = null) {
            if (outputHelper != null) {
                outputHelper.WriteLine(result.GetDetailedOutput());
            }

            if (result.IsOver && result.OverBudgetPercentage.Value > 25 && result.DurationMilliseconds > minDurationMsToThrowOver) {
                Assert.False(true, result.GetResultMessage());
            } else if (!result.IsOver && result.UnderBudgetPercentage.Value > 50 && result.BudgetMilliseconds > minBudgetMsToThrowUnder) {
                Assert.False(true, result.GetResultMessage());
            }
        }
    }
}
