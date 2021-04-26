using System;

namespace Gridnine.FlightCodingTest.FormatProviders
{
    internal class SegmentFormatProvider : IFormatProvider, ICustomFormatter
    {
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if (arg == null)
                throw new ArgumentNullException(nameof(arg), "Arg to format must not be null.");

            if (arg is not Segment seg)
                throw new NotImplementedException($"Currently supporting only {nameof(Segment)} objects.");

            return $"Dep.date: {seg.DepartureDate} —> Arr.date: {seg.ArrivalDate}";
        }

        public object GetFormat(Type formatType)
        {
            return formatType == typeof(ICustomFormatter) ? this : null;
        }
    }
}
