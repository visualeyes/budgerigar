using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgerigar.Budgetting {
    public interface IPerformanceBudgetter {
        PerformanceBudget RunWithBudget(string name, decimal milliseconds, Action<PerformanceBudgetResult> onCompletion);
    }
}
