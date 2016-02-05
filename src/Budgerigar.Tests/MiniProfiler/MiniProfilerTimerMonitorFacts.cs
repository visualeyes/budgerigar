using Budgerigar.MiniProfilerProvider.Timing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Budgerigar.Tests.MiniProfiler {
    public class MiniProfilerTimerMonitorFacts {

        [Fact]
        public void Create_Monitor() {
            string name = "test";
            var timer = new MiniProfilerTimerMonitor(name);
            Assert.Equal(name, timer.Name);
        }

        [Fact]
        public void Step_Returns() {
            string name = "test";
            var timer = new MiniProfilerTimerMonitor(name);

            var step = timer.Step("test");

            Assert.NotNull(step);
        }

        [Fact]
        public void Stop_Returns_Result() {
            string name = "test";
            var timer = new MiniProfilerTimerMonitor(name);

            var result = timer.Stop();

            Assert.NotNull(result);
            Assert.NotNull(result.Steps);
            Assert.Equal(1, result.Steps.Count());
        }

        [Fact]
        public void Stop_With_Steps_Returns_Result() {
            string name = "test";
            var timer = new MiniProfilerTimerMonitor(name);

            using (timer.Step("test")) {
                // do nothing
            }

            var result = timer.Stop();

            Assert.NotNull(result);
            Assert.NotNull(result.Steps);
            Assert.Equal(2, result.Steps.Count());
        }
    }
}
