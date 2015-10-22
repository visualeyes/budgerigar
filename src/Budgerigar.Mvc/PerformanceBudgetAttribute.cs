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

        public PerformanceBudgetAttribute(decimal budgetMs) {
            this.budgetMs = budgetMs;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            base.OnActionExecuting(filterContext);

            //TODO: Performance Timer Setting
            //TODO: OnCompletion Factory

            string url = filterContext.HttpContext.Request.Url.ToString();
                        
            var monitor = BudgerigarWebConfig.Config.TimerProviderFactory.Start(url);

            var budget = new PerformanceBudget(monitor, budgetMs, BudgerigarWebConfig.Config.OnBudgetResult);
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
