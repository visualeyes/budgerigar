using Budgerigar.Budgetting;
using Budgerigar.Timing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Budgerigar.Mvc {

    public class PerformanceBudgetAttribute : ActionFilterAttribute {
        private const string BudgetKey = "BudgerigarPerfBudgetKey";

        private readonly decimal budgetMs;
        private readonly Action<PerformanceBudgetResult> onCompletion;

        public PerformanceBudgetAttribute(decimal budgetMs, Action<PerformanceBudgetResult> onCompletion) {
            this.budgetMs = budgetMs;
            this.onCompletion = onCompletion;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            base.OnActionExecuting(filterContext);

            string url = filterContext.HttpContext.Request.Url.ToString();
                        
            var budgetter = new PerformanceBudgetter();

            var budget = budgetter.RunWithBudget(url, budgetMs, onCompletion);

            SetPerformaceBudgetterFromContext(budget, filterContext);
        }
        
        public override void OnActionExecuted(ActionExecutedContext filterContext) {
            base.OnActionExecuted(filterContext);

            var budget = GetPerformaceBudgetterFromContext(filterContext);
            budget.Dispose();
        }

        private PerformanceBudget GetPerformaceBudgetterFromContext(ControllerContext context) {
            return context.HttpContext.Items[BudgetKey] as PerformanceBudget;
        }

        private void SetPerformaceBudgetterFromContext(PerformanceBudget budget, ControllerContext context) {
            context.HttpContext.Items[BudgetKey] = budget;
        }
    }
}
