using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgerigar.Timing.StopWatch {
    public class StopwatchPerformanceMonitorStep : IDisposable {
        private readonly string name;
        private readonly Stopwatch stopwatch;

        private decimal? startMs = null;
        private decimal? endMs = null;

        public StopwatchPerformanceMonitorStep(string name, Stopwatch stopwatch) {
            if (!stopwatch.IsRunning) {
                throw new ArgumentException("Stopwatch is not started");
            }

            this.name = name;
            this.stopwatch = stopwatch;
        }

        public string Name { get { return this.name; } }

        public decimal? GetDurationMilliseconds() {
            if(!this.startMs.HasValue) {
                return null;
            }

            if(!this.endMs.HasValue) {
                return null;
            }

            return this.endMs.Value - this.startMs.Value;
        }


        internal void StartStep() {
            this.startMs = stopwatch.ElapsedMilliseconds;
        }

        public void Dispose() {
            this.endMs = stopwatch.ElapsedMilliseconds;
        }
    }
}
