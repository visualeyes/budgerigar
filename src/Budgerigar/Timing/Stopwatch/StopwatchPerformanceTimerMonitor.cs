using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgerigar.Timing.StopWatch {
    public class StopwatchPerformanceTimerMonitor : IPerformanceTimerMonitor {
        private readonly ConcurrentQueue<StopwatchPerformanceMonitorStep> steps = new ConcurrentQueue<StopwatchPerformanceMonitorStep>();

        private readonly string name;
        private readonly Stopwatch stopwatch;

        public StopwatchPerformanceTimerMonitor(string name, Stopwatch stopwatch) {
            this.name = name;
            this.stopwatch = stopwatch;
        }

        public string Name { get { return this.name; } }

        public IDisposable Step(string name) {
            var step = new StopwatchPerformanceMonitorStep(name, this.stopwatch);
            steps.Enqueue(step);
            step.StartStep(); // start step after adding to bag to ensure it's not influenced by that operation

            return step;
        }

        public PerformanceTimerResult Stop() {
            if (!stopwatch.IsRunning) {
                return null;
            }

            stopwatch.Stop();
            
            var stepResults = steps.Select(s => new PerformanceStepResult(s.Name, s.GetDurationMilliseconds())).ToList();

            return new PerformanceTimerResult(this.stopwatch.ElapsedMilliseconds, stepResults);
        }
    }
}
