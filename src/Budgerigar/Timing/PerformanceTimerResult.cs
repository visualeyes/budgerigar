using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgerigar.Timing {
    public class PerformanceTimerResult : IPerformanceTimerResult {
        public PerformanceTimerResult(decimal duration, IEnumerable<PerformanceStepResult> steps) {
            this.DurationMilliseconds = duration;
            this.Steps = steps ?? Enumerable.Empty<PerformanceStepResult>();
        }

        public decimal DurationMilliseconds { get; private set; }
        public IEnumerable<PerformanceStepResult> Steps { get; private set; }
    }
}
