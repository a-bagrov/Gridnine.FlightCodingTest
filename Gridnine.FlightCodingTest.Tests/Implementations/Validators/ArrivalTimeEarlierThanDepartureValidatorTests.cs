using Xunit;
using Gridnine.FlightCodingTest.Tests;
using System;

namespace Gridnine.FlightCodingTest.Implementations.Validators.Tests
{
    public class ArrivalTimeEarlierThanDepartureValidatorTests
    {
        [Fact]
        public void AnyArrivalTimeEarlierThanDepartureValidator_Should_Fail_If_Correct_Flight()
        {
            var validator = new AnyArrivalTimeEarlierThanDepartureValidator();
            var flight = Common.CreateFlight(DateTime.Now, TimeSpan.FromTicks(0), TimeSpan.FromHours(5));

            Assert.False(validator.Validate(flight));
        }

        [Fact]
        public void AnyArrivalTimeEarlierThanDepartureValidator_Should_Return_True_If_InCorrect_Flight()
        {
            var validator = new AnyArrivalTimeEarlierThanDepartureValidator();
            var flight = Common.CreateFlight(DateTime.Now, TimeSpan.FromTicks(0), TimeSpan.FromHours(-5));

            Assert.True(validator.Validate(flight));
        }

        [Fact]
        public void AnyArrivalTimeEarlierThanDepartureValidator_Should_Return_True_If_InCorrect_Flight_With_Multiple_Segments()
        {
            var validator = new AnyArrivalTimeEarlierThanDepartureValidator();
            var flight = Common.CreateFlight(DateTime.Now, TimeSpan.FromTicks(0), TimeSpan.FromHours(-5), TimeSpan.FromHours(5), TimeSpan.FromHours(15));

            Assert.True(validator.Validate(flight));
        }

    }
}