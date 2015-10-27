using Budgerigar.Timing;
using Budgerigar.Timing.StopWatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Budgerigar.Tests {
    public class BudgerigarConfigFacts {

        [Fact]
        public void Config_Is_Set_ByDefault() {
            Assert.NotNull(BudgerigarConfig.Config);
            Assert.NotNull(BudgerigarConfig.Config.TimerProviderFactory);
        }

        [Fact]
        public void Default_Config_Is_Stopwatch() {
            var timerProviderFactory = BudgerigarConfig.Config.TimerProviderFactory as PerformanceTimerProviderFactory;
            Assert.NotNull(timerProviderFactory);

            var provider = timerProviderFactory.GetProvider() as StopwatchTimerProvider;
            Assert.NotNull(provider);
        }

        [Fact]
        public void Set_Config() {
            var config = new BudgerigarConfig();

            BudgerigarConfig.SetConfig(config);

            Assert.Same(config, BudgerigarConfig.Config);
        }
    }
}
