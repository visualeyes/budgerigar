using Budgerigar.Timing;
using StackExchange.Profiling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgerigar.MiniProfilerProvider.Timing {
    public class MiniProfilerTimerMonitor : IPerformanceTimerMonitor {
        private readonly string name;
        private readonly MiniProfiler profiler;

        public MiniProfilerTimerMonitor(string name) {
            this.name = name;

            profiler = new MiniProfiler(name);
        }

        public string Name { get { return this.name; } }

        public IDisposable Step(string name) {
            return this.profiler.Step(name);
        }

        public PerformanceTimerResult Stop() {
            profiler.Root.Stop();

            decimal? duration = profiler.Root.DurationMilliseconds;

            if(!duration.HasValue) {
                return null;
            }

            var stepResults = profiler.GetTimingHierarchy()
                            .Select(t => new PerformanceStepResult(t.Name, t.DurationMilliseconds));

            return new PerformanceTimerResult(duration.Value, stepResults);
        }
    }
}
