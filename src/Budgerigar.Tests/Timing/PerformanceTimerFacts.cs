using Budgerigar.Timing;
using Budgerigar.Timing.StopWatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Budgerigar.Tests.Timing {
    public class PerformanceTimerFacts {

        [Fact]
        public void Does_Not_Throw_If_Null() {
            var timer = new PerformanceTimerProviderFactory(null);
            var provider = timer.GetProvider();
            Assert.Null(provider);
        }

        [Fact]
        public void Returns_Provider() {
            var stopwatchProvider = new StopwatchTimerProvider();
            var timer = new PerformanceTimerProviderFactory(stopwatchProvider);
            var provider = timer.GetProvider();

            Assert.NotNull(provider);
            Assert.Same(stopwatchProvider, provider);
        }

    }
}
