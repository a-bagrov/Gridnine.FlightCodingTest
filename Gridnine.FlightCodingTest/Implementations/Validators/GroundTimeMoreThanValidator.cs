using System;
using Gridnine.FlightCodingTest.Interfaces;

namespace Gridnine.FlightCodingTest.Implementations.Validators
{
    /// <summary>
    /// Валидатор, возвращающий <i>false</i> в случае, если общее время на земле меньше указанного; иначе <i>true</i>.
    /// </summary>
    internal class GroundTimeMoreThanValidator : IFlightValidator
    {
        private readonly TimeSpan _targetDurationOnGround;

        public GroundTimeMoreThanValidator(TimeSpan targetDurationOnGround)
        {
            _targetDurationOnGround = targetDurationOnGround;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="flight"><inheritdoc/></param>
        /// <exception cref="ArgumentException">В случае если полет <paramref name="flight"/> содержит менее двух <see cref="Segment"/>.</exception>
        /// <returns><inheritdoc/></returns>
        public bool Validate(Flight flight)
        {
            if (flight.Segments.Count < 2)
                throw new ArgumentException("To validate ground time, flight must have at least two segments.", nameof(flight));

            var currDuration = TimeSpan.Zero;

            for (var i = 0; i < flight.Segments.Count - 1; i++)
            {
                currDuration = currDuration.Add(flight.Segments[i + 1].DepartureDate - flight.Segments[i].ArrivalDate);
                if (currDuration > _targetDurationOnGround)
                    return true;
            }

            return false;
        }
    }
}
