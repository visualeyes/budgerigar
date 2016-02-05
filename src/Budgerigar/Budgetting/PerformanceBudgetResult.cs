using Budgerigar.Timing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgerigar.Budgetting {
    public class PerformanceBudgetResult {
        private readonly decimal millisecondsOver;
        private readonly decimal millisecondsUnder;

        public PerformanceBudgetResult(string name, decimal budgetMs, decimal durationMs, Exception e) {
            if (budgetMs < 0) throw new ArgumentException("Budget must be positive", "budgetMs");
            if (durationMs < 0) throw new ArgumentException("Duration must be positive", "durationMs");


            this.millisecondsOver = durationMs - budgetMs;
            this.millisecondsUnder = Math.Abs(this.millisecondsOver);

            this.Name = name;
            this.BudgetMilliseconds = budgetMs;
            this.DurationMilliseconds = durationMs;
            this.Steps = Enumerable.Empty<PerformanceStepResult>();
            this.Exception = e;
        }

        public string Name { get; private set; }
        public decimal BudgetMilliseconds { get; private set; }
        public decimal DurationMilliseconds { get; private set; }

        public IEnumerable<PerformanceStepResult> Steps { get; set; }

        public Exception Exception { get; private set; }

        public bool IsOver {
            get { return millisecondsOver > 0; }
        }

        public decimal? OverBudgetPercentage {
            get { return IsOver ? (this.millisecondsOver / this.BudgetMilliseconds) * 100 : (decimal?)null; }
        }

        public decimal? UnderBudgetPercentage {
            get { return IsOver ? (decimal?)null : (this.millisecondsUnder / this.BudgetMilliseconds) * 100; }
        }

        public decimal? OverBudgetMilliseconds {
            get { return IsOver ? (decimal?)this.millisecondsOver : null; }
        }

        public decimal? UnderBudgetMilliseconds {
            get { return IsOver ? (decimal?)null : this.millisecondsUnder; }
        }

        public string GetResultMessage() {
            var sb = new StringBuilder();

            string name = String.IsNullOrWhiteSpace(this.Name) ? "Action" : "Budget for " + this.Name;

            string budgetMessageFormat = name + " was {0} performance budget by {1:0.000}% ({2:0.000}ms).";

            if (this.IsOver) {
                sb.AppendFormat(budgetMessageFormat, "over", this.OverBudgetPercentage.Value, this.OverBudgetMilliseconds.Value);
            } else {
                sb.AppendFormat(budgetMessageFormat, "under", this.UnderBudgetPercentage.Value, this.UnderBudgetMilliseconds.Value);
            }

            return sb.ToString();
        }

        public string GetDetailedOutput() {
            var sb = new StringBuilder();

            sb.AppendFormat("Budget was set to {0:0.000}ms", this.BudgetMilliseconds).AppendLine()
              .AppendFormat("Duration was {0:0.000}ms", this.DurationMilliseconds).AppendLine()
              .Append(this.GetResultMessage()).AppendLine();

            if(this.Steps != null) {
                var grouped = (from s in this.Steps
                               group s by s.Name into g
                               select g);


                foreach (var group in grouped) {
                    if(group.Count() == 1) {
                        GetSingleResult(group.Single(), sb);
                    } else {
                        GetAverageResult(group.Key, group.AsEnumerable(), sb);
                    }
                }
            }

            return sb.ToString();
        }

        private void GetSingleResult(PerformanceStepResult step, StringBuilder sb) {
            sb.AppendFormat("  Step: {0} ", step.Name);
            if (step.DurationMilliseconds.HasValue) {
                sb.AppendFormat("took {0:0.000}ms", step.DurationMilliseconds);
            } else {
                sb.Append("did not finish");
            }
            sb.AppendLine();
        }

        private void GetAverageResult(string name, IEnumerable<PerformanceStepResult> group, StringBuilder sb) {
            sb.AppendFormat(
                "  Step: {0} occured {1} times and took {3:0.000}ms on average. {2} did not finish.", 
                name, 
                group.Count(), 
                group.Count(c => !c.DurationMilliseconds.HasValue),
                group.Where(s => s.DurationMilliseconds.HasValue).Average(c => c.DurationMilliseconds.Value)
            );
                        
            sb.AppendLine();
        }
    }
}
