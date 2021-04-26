using Xunit;
using System;
using Gridnine.FlightCodingTest.Tests;

namespace Gridnine.FlightCodingTest.Implementations.Validators.Tests
{
    public class GroundTimeValidatorTests
    {
        [Fact]
        public void GroundTimeMoreThanValidator_Should_Throw_If_Less_Than_Two_Segments_Passed()
        {
            var validator = new GroundTimeMoreThanValidator(TimeSpan.FromDays(1));
            var flight = Common.CreateFlight(DateTime.Now, TimeSpan.FromTicks(0), TimeSpan.FromHours(5));

            Assert.Throws<ArgumentException>("flight", () => validator.Validate(flight));
        }

        [Fact]
        public void GroundTimeMoreThanValidator_Should_Return_False_If_Ground_Time_Less_Than_Passed()
        {
            var validator = new GroundTimeMoreThanValidator(TimeSpan.FromDays(1));
            var flight = Common.CreateFlight(DateTime.Now, TimeSpan.FromTicks(0), TimeSpan.FromHours(5), TimeSpan.FromHours(6), TimeSpan.FromHours(10));

            Assert.False(validator.Validate(flight));
        }

        [Fact]
        public void GroundTimeMoreThanValidator_Should_Return_True_If_Ground_Time_More_Than_Passed()
        {
            var validator = new GroundTimeMoreThanValidator(TimeSpan.FromMinutes(1));
            var flight = Common.CreateFlight(DateTime.Now, TimeSpan.FromTicks(0), TimeSpan.FromHours(5), TimeSpan.FromHours(6), TimeSpan.FromHours(10));

            Assert.True(validator.Validate(flight));
        }

        [Fact]
        public void GroundTimeMoreThanValidator_Should_Return_False_If_Ground_Time_Equals_To_Passed()
        {
            var validator = new GroundTimeMoreThanValidator(TimeSpan.FromHours(1));
            var flight = Common.CreateFlight(DateTime.Now, TimeSpan.FromTicks(0), TimeSpan.FromHours(5), TimeSpan.FromHours(6), TimeSpan.FromHours(10));

            Assert.False(validator.Validate(flight));
        }
    }
}