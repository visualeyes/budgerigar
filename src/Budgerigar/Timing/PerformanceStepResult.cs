using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgerigar.Timing {
    public class PerformanceStepResult {
        public PerformanceStepResult(string name, decimal? durationMs) {
            this.Name = name;
            this.DurationMilliseconds = durationMs;
        }

        public string Name { get; private set; }
        public decimal? DurationMilliseconds { get; private set; }
    }
}
