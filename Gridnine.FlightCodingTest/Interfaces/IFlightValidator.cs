namespace Gridnine.FlightCodingTest.Interfaces
{
    /// <summary>
    /// Интерфейс, предназначенный для валидации полета <see cref="Flight"/>.
    /// </summary>
    internal interface IFlightValidator
    {
        /// <summary>
        /// Проводит процедуру валидации полета <paramref name="flight"/>.
        /// </summary>
        /// <param name="flight">Полет для валидации.</param>
        /// <returns>Результат валидации.</returns>
        public bool Validate(Flight flight);
    }
}
