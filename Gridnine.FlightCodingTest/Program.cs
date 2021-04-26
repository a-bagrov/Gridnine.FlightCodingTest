using Gridnine.FlightCodingTest.FormatProviders;
using Gridnine.FlightCodingTest.Implementations;
using Gridnine.FlightCodingTest.Implementations.Validators;
using System;
using System.Linq;

namespace Gridnine.FlightCodingTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine($"Текущее время: {DateTime.Now}");
            var flights = new FlightBuilder().GetFlights();
            var flightFormatter = new FlightFormatProvider();
            var validatorService = new FlightValidationService();

            WriteLineWithColors("Всего полетов:");
            foreach (var f in flights)
                Console.WriteLine(string.Format(flightFormatter, "{0}", f));

            WriteLineWithColors("Вылет до текущего момента времени:");
            foreach (var f in validatorService.Validate(flights, new FlightValidationServiceOptions(new AnyDepartureTimeEarlierThanValidator(DateTime.Now))))
                Console.WriteLine(string.Format(flightFormatter, "{0}", f));

            WriteLineWithColors("Имеются сегменты с датой прилёта раньше даты вылета:");
            foreach (var f in validatorService.Validate(flights, new FlightValidationServiceOptions(new AnyArrivalTimeEarlierThanDepartureValidator())))
                Console.WriteLine(string.Format(flightFormatter, "{0}", f));

            WriteLineWithColors("Общее время, проведённое на земле превышает два часа:");
            try
            {
                foreach (var f in validatorService.Validate(flights,
                    new FlightValidationServiceOptions(new GroundTimeMoreThanValidator(TimeSpan.FromHours(2)))))
                {
                    Console.WriteLine(string.Format(flightFormatter, "{0}", f));
                }
            }
            catch (ArgumentException ex)
            {
                WriteLineWithColors($"{ex.Message}", ConsoleColor.Red);
            }
            finally
            {
                WriteLineWithColors("Валидируем все полеты, имеющие не менее двух сегментов. Результат:");
                foreach (var f in validatorService.Validate(flights.Where(c => c.Segments.Count > 1),
                    new FlightValidationServiceOptions(new GroundTimeMoreThanValidator(TimeSpan.FromHours(2)))))
                {
                    Console.WriteLine(string.Format(flightFormatter, "{0}", f));
                }
            }
        }

        private static void WriteLineWithColors(string line, ConsoleColor foreColor = ConsoleColor.Green)
        {
            var tempForeColor = Console.ForegroundColor;

            Console.ForegroundColor = foreColor;
            Console.WriteLine(line);

            Console.ForegroundColor = tempForeColor;
        }
    }
}
