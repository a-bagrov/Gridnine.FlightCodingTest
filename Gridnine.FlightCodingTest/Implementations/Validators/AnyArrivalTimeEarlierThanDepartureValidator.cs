using System.Linq;
using Gridnine.FlightCodingTest.Interfaces;

namespace Gridnine.FlightCodingTest.Implementations.Validators
{
    /// <summary>
    /// Валидатор, возвращающий <i>true</i> в случае, если полет имеет хотя бы один сегмент с временем прибытия <see cref="Segment.ArrivalDate"/> раньше времени отправления <see cref="Segment.DepartureDate"/>; иначе <i>false</i>.
    /// </summary>
    internal class AnyArrivalTimeEarlierThanDepartureValidator : IFlightValidator
    {
        public bool Validate(Flight flight) => flight.Segments.Any(c => c.ArrivalDate < c.DepartureDate);
    }
}
