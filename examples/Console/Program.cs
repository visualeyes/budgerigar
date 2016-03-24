using Budgerigar.Budgetting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgerigarExample {
    class Program {
        static void Main(string[] args) {
            var budgetter = new PerformanceBudgetter();

            budgetter.RunWithBudget("over-budget", 1, LongRunningTask, LogResult);
            budgetter.RunWithBudget("under-budget", 100, LongRunningTask, LogResult);

            Console.ReadLine();
        }

        static void LongRunningTask(PerformanceBudget budget) {
            budget.Step("one", () => Thread.Sleep(10));
            budget.Step("two", () => Thread.Sleep(10));
        }

        static void LogResult(PerformanceBudgetResult result) {
            if(result.IsOver) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Over Budget!");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(result.GetDetailedOutput());
            } else {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Under Budget!");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(result.GetDetailedOutput());
            }
        }
    }
}
