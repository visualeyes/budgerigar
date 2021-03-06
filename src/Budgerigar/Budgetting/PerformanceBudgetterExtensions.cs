﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgerigar.Budgetting {
    public static class PerformanceBudgetterExtensions {
        public static void RunWithBudget(this IPerformanceBudgetter budgetter, string name, decimal budgetMs, Action<PerformanceBudget> work, Action<PerformanceBudgetResult> onCompletion) {
            if (budgetter != null) {
                using (var budget = budgetter.RunWithBudget(name, budgetMs, onCompletion)) {
                    budget.RunAsync(work);
                }
            } else {
                work(null);
            }
        }

        public static T RunWithBudget<T>(this IPerformanceBudgetter budgetter, string name, decimal budgetMs, Func<PerformanceBudget, T> work, Action<PerformanceBudgetResult> onCompletion) {
            if (budgetter != null) {
                using (var budget = budgetter.RunWithBudget(name, budgetMs, onCompletion)) {
                    return budget.RunAsync(work);
                }
            } else {
                return work(null);
            }
        }

        public static async Task RunWithBudgetAsync(this IPerformanceBudgetter budgetter, string name, decimal budgetMs, Func<PerformanceBudget, Task> work, Action<PerformanceBudgetResult> onCompletion) {
            if (budgetter != null) {
                using (var budget = budgetter.RunWithBudget(name, budgetMs, onCompletion)) {
                    await budget.RunAsync(work);
                }
            } else {
                await work(null);
            }
        }

        public static async Task<T> RunWithBudgetAsync<T>(this IPerformanceBudgetter budgetter, string name, decimal budgetMs, Func<PerformanceBudget, Task<T>> work, Action<PerformanceBudgetResult> onCompletion) {
            if (budgetter != null) {
                using (var budget = budgetter.RunWithBudget(name, budgetMs, onCompletion)) {
                    return await budget.RunAsync(work);
                }
            } else {
                return await work(null);
            }
        }
    }
}
