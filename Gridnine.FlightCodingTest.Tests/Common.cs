using System;
using System.Linq;

namespace Gridnine.FlightCodingTest.Tests
{
    public static class Common
    {
        public static Flight CreateFlight(DateTime referenceDate, params TimeSpan[] timeShifts)
        {
            if (timeShifts.Length % 2 != 0) throw new ArgumentException("You must pass an even number of time shifts.", nameof(timeShifts));

            var departureShifts = timeShifts.Where((date, index) => index % 2 == 0);
            var arrivalShifts = timeShifts.Where((date, index) => index % 2 == 1);

            var segments = departureShifts.Zip(arrivalShifts,
                                              (departureDate, arrivalDate) =>
                                              new Segment { DepartureDate = referenceDate.Add(departureDate), ArrivalDate = referenceDate.Add(arrivalDate) }).ToList();

            return new Flight { Segments = segments };
        }
    }
}
