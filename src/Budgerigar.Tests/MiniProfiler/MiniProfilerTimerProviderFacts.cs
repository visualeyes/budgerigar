using Budgerigar.MiniProfilerProvider.Timing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Budgerigar.Tests.MiniProfiler {
    public class MiniProfilerTimerProviderFacts {

        [Fact]
        public void Creates_Timer() {
            string name = "test";

            var provider = new MiniProfilerTimerProvider();
            var timer = provider.Start(name) as MiniProfilerTimerMonitor;

            Assert.NotNull(timer);
            Assert.Equal(name, timer.Name);
        }
    }
}
