using Budgerigar.Timing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgerigar.MiniProfilerProvider.Timing {
    public class MiniProfilerTimerProvider : IPerformanceTimerProvider {
        public IPerformanceTimerMonitor Start(string name) {
            return new MiniProfilerTimerMonitor(name);
        }
    }
}
