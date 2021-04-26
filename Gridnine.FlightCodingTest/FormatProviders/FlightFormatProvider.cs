using System;
using System.Text;

namespace Gridnine.FlightCodingTest.FormatProviders
{
    internal class FlightFormatProvider : IFormatProvider, ICustomFormatter
    {
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if (arg == null)
                throw new ArgumentNullException(nameof(arg), "Arg to format must not be null.");

            if (arg is not Flight flight)
                throw new NotImplementedException($"Currently supporting only {nameof(Flight)} objects.");

            var sb = new StringBuilder();
            sb.Append("Flight №");
            sb.Append(flight.GetHashCode());
            sb.AppendLine();

            var segFormatter = new SegmentFormatProvider();
            for (var i = 0; i < flight.Segments.Count; i++)
            {
                var seg = flight.Segments[i];
                sb.Append(i + 1);
                sb.AppendLine(string.Format(segFormatter, " segment: {0}", seg));
            }

            return sb.ToString();
        }

        public object GetFormat(Type formatType)
        {
            return formatType == typeof(ICustomFormatter) ? this : null;
        }
    }
}
