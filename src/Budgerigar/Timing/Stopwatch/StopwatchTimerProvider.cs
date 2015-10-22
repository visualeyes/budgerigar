using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgerigar.Timing.StopWatch {
    public class StopwatchTimerProvider : IPerformanceTimerProvider {
        public IPerformanceTimerMonitor Start(string name) {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            return new StopwatchPerformanceTimerMonitor(name, stopwatch);
        }
    }
}
