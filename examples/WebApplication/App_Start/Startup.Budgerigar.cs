using Budgerigar;
using Budgerigar.MiniProfilerProvider.Timing;
using Budgerigar.Mvc;
using Budgerigar.Timing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication {
    public partial class Startup {
        public static void ConfigurePerfBudget() {
            var config = new BudgerigarConfig() {
                TimerProviderFactory = new PerformanceTimerProviderFactory(new MiniProfilerTimerProvider())
            };

            BudgerigarConfig.SetConfig(config);
        }

        public static void RegisterPerformanceFilters(GlobalFilterCollection filters) {

            var perfBudgetLogger = log4net.LogManager.GetLogger("PerformanceBudget");

            var perfBudgetAttribute = new PerformanceBudgetAttribute(100, (result) => {
                if(result.IsOver) {
                    perfBudgetLogger.ErrorFormat("{0} ({1}): ", result.Name, result.OverBudgetPercentage.Value, result.GetDetailedOutput());
                }
            });

            filters.Add(perfBudgetAttribute);
        }
    }
}