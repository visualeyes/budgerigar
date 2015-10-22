using Budgerigar.Timing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Budgerigar.Tests.Timing {
    public class PerformanceStepResultFacts {

        [Theory]
        [InlineData(null, null)]
        [InlineData(" ", null)]
        [InlineData("test", 1.0)]
        public void Sets_Properties(string name, double? durationDouble) {
            var duration = (decimal?)durationDouble;

            var result = new PerformanceStepResult(name, duration);

            Assert.Equal(name, result.Name);
            Assert.Equal(duration, result.DurationMilliseconds);
        }
    }
}
