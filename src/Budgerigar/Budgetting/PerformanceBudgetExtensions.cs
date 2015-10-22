using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgerigar.Budgetting {
    public static class PerformanceBudgetExtensions {
        
        public static IDisposable Step(this PerformanceBudget budget, string name) {
            if (budget == null) return new NoOpDisposable();

            return budget.CreateStep(name);
        }

        public static T Step<T>(this PerformanceBudget budget, string name, Func<T> work) {
            if (budget != null) {
                using (budget.CreateStep(name)) {
                    return work();
                }
            } else {
                return work();
            }
        }

        public static async Task<T> StepAsync<T>(this PerformanceBudget budget, string name, Func<Task<T>> work) {
            if (budget != null) {
                using (budget.CreateStep(name)) {
                    return await work();
                }
            } else {
                return await work();
            }
        }

        public static void Step(this PerformanceBudget budget, string name, Action work) {
            if (budget != null) {
                using (budget.CreateStep(name)) {
                    work();
                }
            } else {
                work();
            }
        }

        public static async Task StepAsync(this PerformanceBudget budget, string name, Func<Task> work) {
            if (budget != null) {
                using (budget.CreateStep(name)) {
                    await work();
                }
            } else {
                await work();
            }
        }
    }
}
