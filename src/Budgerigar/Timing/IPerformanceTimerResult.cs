using System.Collections.Generic;

namespace Budgerigar.Timing {
    public interface IPerformanceTimerResult {
        decimal DurationMilliseconds { get; }

        IEnumerable<PerformanceStepResult> Steps { get; }
    }
}