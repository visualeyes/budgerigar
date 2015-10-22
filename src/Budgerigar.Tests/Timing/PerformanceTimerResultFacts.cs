using Budgerigar.Timing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Budgerigar.Tests.Timing {
    public class PerformanceTimerResultFacts {

        [Theory]
        [InlineData(1.0, null)]
        [InlineData(2.0, 0)]
        [InlineData(3.0, 10)]
        public void Sets_Properties(decimal duration, int? numSteps) {
            IEnumerable<PerformanceStepResult> steps = null;

            if(numSteps.HasValue) {
                steps = Enumerable.Range(0, numSteps.Value).Select(i => new PerformanceStepResult("test", null));
            }

            var result = new PerformanceTimerResult(duration, steps);
            
            Assert.Equal(duration, result.DurationMilliseconds);
            Assert.NotNull(result.Steps);

            int expectedStepsCount = numSteps.HasValue ? numSteps.Value : 0;

            Assert.Equal(expectedStepsCount, result.Steps.Count());
        }
    }
}
