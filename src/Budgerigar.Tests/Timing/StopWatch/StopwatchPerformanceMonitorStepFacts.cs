using Budgerigar.Timing.StopWatch;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Budgerigar.Tests.Timing.StopWatch {
    public class StopwatchPerformanceMonitorStepFacts {

        [Fact]
        public void Throws_If_Stopwatch_Not_Running() {
            var stopwatch = new Stopwatch();

            Assert.Throws<ArgumentException>(() => new StopwatchPerformanceMonitorStep("test", stopwatch));
        }


    }
}
