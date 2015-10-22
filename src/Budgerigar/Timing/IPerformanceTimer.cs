using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgerigar.Timing {
    public interface IPerformanceTimer {
        IPerformanceTimerProvider GetProvider();
    }
}
