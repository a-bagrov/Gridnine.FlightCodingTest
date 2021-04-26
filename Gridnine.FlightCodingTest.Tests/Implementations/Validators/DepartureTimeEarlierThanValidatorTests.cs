using Xunit;
using System;
using Gridnine.FlightCodingTest.Tests;

namespace Gridnine.FlightCodingTest.Implementations.Validators.Tests
{
    public class DepartureTimeEarlierThanValidatorTests
    {
        [Fact]
        public void AnyDepartureTimeEarlierThanValidator_Should_Return_False_If_Passed_DateTime_MinValue()
        {
            var validator = new AnyDepartureTimeEarlierThanValidator(DateTime.MinValue);
            var flight = Common.CreateFlight(DateTime.Now, TimeSpan.FromTicks(0), TimeSpan.FromHours(5));

            Assert.False(validator.Validate(flight));
        }

        [Fact]
        public void AnyDepartureTimeEarlierThanValidator_Should_Return_True_If_DepDate_Earlier_Than_Passed()
        {
            var validator = new AnyDepartureTimeEarlierThanValidator(DateTime.Now.AddDays(1));
            var flight = Common.CreateFlight(DateTime.Now, TimeSpan.FromTicks(0), TimeSpan.FromHours(5));

            Assert.True(validator.Validate(flight));
        }

        [Fact]
        public void AnyDepartureTimeEarlierThanValidator_Should_Return_True_If_Any_DepDate_Earlier_Than_Passed()
        {
            var flight = Common.CreateFlight(DateTime.Now, TimeSpan.FromTicks(0), TimeSpan.FromHours(5), TimeSpan.FromHours(12), TimeSpan.FromHours(16));
            var validator = new AnyDepartureTimeEarlierThanValidator(DateTime.Now.AddHours(7));

            Assert.True(validator.Validate(flight));
        }
    }
}