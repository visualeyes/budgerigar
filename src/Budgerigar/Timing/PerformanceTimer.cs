using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgerigar.Timing {
    public class PerformanceTimer : IPerformanceTimer {
        private IPerformanceTimerProvider provider;

        public PerformanceTimer(IPerformanceTimerProvider provider) {
            this.provider = provider;
        }

        public IPerformanceTimerProvider GetProvider() {
            return provider;
        }
    }
}
