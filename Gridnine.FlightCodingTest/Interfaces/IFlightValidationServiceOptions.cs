using System.Collections.Generic;

namespace Gridnine.FlightCodingTest.Interfaces
{
    /// <summary>
    /// Интерфейс, предназначенный для определения порядка валидации <see cref="IFlightValidationService"/>.
    /// </summary>
    internal interface IFlightValidationServiceOptions
    {
        /// <summary>
        /// Правила валидации, определяющие ее порядок.
        /// </summary>
        public IEnumerable<IFlightValidator> FlightValidators { get; }
    }
}
