using System;
using System.Linq;
using Gridnine.FlightCodingTest.Interfaces;

namespace Gridnine.FlightCodingTest.Implementations.Validators
{
    /// <summary>
    /// Валидатор, проверяющий время отправления <see cref="Segment.DepartureDate"/> - если оно раньше указанного, возвращает <i>true</i>; иначе <i>false</i>.
    /// </summary>
    internal class AnyDepartureTimeEarlierThanValidator : IFlightValidator
    {
        private readonly DateTime _departureTimeToCheck;

        public AnyDepartureTimeEarlierThanValidator(DateTime departureTimeToCheck)
        {
            _departureTimeToCheck = departureTimeToCheck;
        }

        public bool Validate(Flight flight) =>
            flight.Segments.Any(c => c.DepartureDate < _departureTimeToCheck);
    }
}
