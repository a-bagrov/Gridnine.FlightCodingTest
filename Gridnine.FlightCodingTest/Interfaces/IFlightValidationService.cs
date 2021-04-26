using System.Collections.Generic;

namespace Gridnine.FlightCodingTest.Interfaces
{
    /// <summary>
    /// Интерфейс, предназначенный для валидации набора полетов <see cref="Flight"/>.
    /// </summary>
    internal interface IFlightValidationService
    {
        /// <summary>
        /// Осуществляет валидацию набора полетов <paramref name="flights"/>.
        /// </summary>
        /// <param name="flights">Набор полетов для валидации.</param>
        /// <param name="options">Опции, определяющие порядок валидации.</param>
        /// <returns>Результат валидации - набор валидных полетов <see cref="Flight"/>.</returns>
        public IEnumerable<Flight> Validate(IEnumerable<Flight> flights, IFlightValidationServiceOptions options);
    }
}
