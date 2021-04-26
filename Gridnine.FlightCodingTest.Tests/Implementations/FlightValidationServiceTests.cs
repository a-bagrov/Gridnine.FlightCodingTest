using Xunit;
using System;
using System.Collections.Generic;
using Gridnine.FlightCodingTest.Tests;
using Gridnine.FlightCodingTest.Implementations.Validators;
using System.Linq;

namespace Gridnine.FlightCodingTest.Implementations.Tests
{
    public class FlightValidationServiceTests
    {
        [Fact]
        public void No_Flights_With_Zero_Ground_Time_Shoul_Be_Valid()
        {
            var now = DateTime.Now;
            var fvs = new FlightValidationService();
            var f = CreateFlights(5,
                i => Common.CreateFlight(now, TimeSpan.FromHours(i * 2), TimeSpan.FromHours((i + 1) * 2),
                    TimeSpan.FromHours((i + 1) * 2), TimeSpan.FromHours((i + 2) * 2)));

            Assert.Empty(fvs.Validate(f,
                new FlightValidationServiceOptions(new GroundTimeMoreThanValidator(TimeSpan.FromSeconds(1)))));
        }

        [Fact]
        public void No_Flights_With_Zero_Ground_Time_Shoul_Be_Valid_And_Departure_Time_Earlier_Than_Passed()
        {
            var now = DateTime.Now;
            var fvs = new FlightValidationService();
            //ground time 0
            var f = CreateFlights(5,
                i => Common.CreateFlight(now, TimeSpan.FromHours(i), TimeSpan.FromHours(i), TimeSpan.FromHours(i),
                    TimeSpan.FromHours(i + 1)));

            var options = new FlightValidationServiceOptions(new GroundTimeMoreThanValidator(TimeSpan.FromHours(1)),
                new AnyDepartureTimeEarlierThanValidator(now));
            Assert.Empty(fvs.Validate(f, options));

            //ground time 5
            f = CreateFlights(5,
                i => Common.CreateFlight(now, TimeSpan.FromHours(i), TimeSpan.FromHours(i), TimeSpan.FromHours(i + 5),
                    TimeSpan.FromHours(i + 6)));
            options = new FlightValidationServiceOptions(new GroundTimeMoreThanValidator(TimeSpan.FromHours(1)),
                new AnyDepartureTimeEarlierThanValidator(now.AddHours(2)));
            Assert.Equal(2, fvs.Validate(f, options).Count());
        }

        [Fact]
        public void FlightValidationService_Should_Throw()
        {
            var now = DateTime.Now;
            var fvs = new FlightValidationService();
            var correctOptions =
                new FlightValidationServiceOptions(new GroundTimeMoreThanValidator(TimeSpan.FromSeconds(1)));

            Assert.Throws<ArgumentNullException>("flights", () => fvs.Validate(null, correctOptions).FirstOrDefault());

            var f = CreateFlights(5,
                i => Common.CreateFlight(now, TimeSpan.FromHours(i), TimeSpan.FromHours(i), TimeSpan.FromHours(i + 5),
                    TimeSpan.FromHours(i + 6)));
            Assert.Throws<ArgumentNullException>("options", () => fvs.Validate(f, null).FirstOrDefault());

            Assert.Throws<ArgumentNullException>("options",
                () => fvs.Validate(f, new FlightValidationServiceOptions(null)).FirstOrDefault());

            Assert.Throws<ArgumentNullException>("flights",
                () => fvs.Validate(f.Prepend(null), correctOptions).FirstOrDefault());

            Assert.Throws<ArgumentNullException>("flights",
                () => fvs.Validate(f.Prepend(new Flight {Segments = null}), correctOptions).FirstOrDefault());

            Assert.Throws<ArgumentNullException>("flights",
                () => fvs.Validate(f.Prepend(new Flight {Segments = new List<Segment> {new Segment(), null}}),
                    correctOptions).FirstOrDefault());
        }

        private static IEnumerable<Flight> CreateFlights(int count, Func<int, Flight> flightCreator)
        {
            for (var i = 0; i < count; i++)
                yield return flightCreator.Invoke(i);
        }
    }
}