using System;
using System.Collections.Generic;
using System.Linq;
using Gridnine.FlightCodingTest.Interfaces;
namespace Gridnine.FlightCodingTest.Implementations
{
    internal class FlightValidationService : IFlightValidationService
    {
        public IEnumerable<Flight> Validate(IEnumerable<Flight> flights, IFlightValidationServiceOptions options)
        {
            if (flights == null)
                throw new ArgumentNullException(nameof(flights), "Flights to validate must not be null.");

            if (options == null)
                throw new ArgumentNullException(nameof(options), "Options must not be null.");

            if (options.FlightValidators == null)
                throw new ArgumentNullException(nameof(options), "Options validators must not be null.");

            foreach (var flight in flights)
            {
                if (flight == null)
                    throw new ArgumentNullException(nameof(flights), "All flights must not be null.");

                if (flight.Segments == null)
                    throw new ArgumentNullException(nameof(flights), "Flight segment collection must not be null.");

                if (flight.Segments.Any(c => c == null))
                    throw new ArgumentNullException(nameof(flights), "All flight segment collection items must not be null.");

                if (options.FlightValidators.All(validator => validator.Validate(flight)))
                    yield return flight;
            }
        }
    }
}
