using Gridnine.FlightCodingTest.Interfaces;
using System.Collections.Generic;

namespace Gridnine.FlightCodingTest.Implementations
{
    internal class FlightValidationServiceOptions : IFlightValidationServiceOptions
    {
        public IEnumerable<IFlightValidator> FlightValidators { get; }

        /// <summary>
        /// Создает настройки валидации, определяющие ее порядок.
        /// </summary>
        /// <param name="flightValidators">Набор валидаторов.</param>
        public FlightValidationServiceOptions(params IFlightValidator[] flightValidators)
        {
            FlightValidators = flightValidators;
        }
    }
}
