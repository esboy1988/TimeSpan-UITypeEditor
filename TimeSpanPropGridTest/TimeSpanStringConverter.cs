using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace TimeSpanPropGridTest
{
    /// <summary>
    /// Utility to convert between <see cref="string"/> and <see cref="TimeSpan"/>.
    /// Examples of valid strings: "1d, 5h, 20m, 50s, 300ms", "300ms", "50s", "2m"
    /// </summary>
    public class TimeSpanStringConverter : StringConverter
    {
        /// <summary>True if the source type is a string</summary>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return (sourceType == typeof(string));
        }


        /// <summary>True if the destination type is a string</summary>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return (destinationType == typeof(string));
        }


        /// <summary>Convert from a string to a TimeSpan.</summary>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            // Got a string ??
            if ((value as string) == null)
            {
                throw new ArgumentException("value must be a string");
            }


            // Get the comma-seperated parts
            var parts = (value as string).Split(',');


            // Helper using a regular expression to find the integer value from one
            // of the comma-seperated parts for a particular time unit type
            Func<string, int> extractPart = (unit) =>
            {
                int result = 0;

                var regexPattern = $"^([\\d]+)({unit})$";

                foreach (var part in parts)
                {
                    var trimmedPart = part.Trim();
                    var groups = Regex.Match(input: trimmedPart, pattern: regexPattern);
                    if (groups.Groups.Count == 3)
                    {
                        var numberAsText = groups.Groups[1].Value;
                        int.TryParse(numberAsText, out result);
                    }
                }

                return result;
            };


            var days = extractPart("d");
            var hours = extractPart("h");
            var minutes = extractPart("m");
            var seconds = extractPart("s");
            var milliseconds = extractPart("ms");
            var daysString = (parts.Length == 5) ? parts[0] : "0";

            var newTimeSpan = new TimeSpan(days, hours, minutes, seconds, milliseconds);
            return newTimeSpan;
        }


        /// <summary>
        /// Converts from a TimeSpan to a string
        /// </summary>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            TimeSpan timeSpan = (TimeSpan)value;
            StringBuilder sb = new StringBuilder();

            if (timeSpan == TimeSpan.Zero)
            {
                sb.Append("0");
            }
            else
            {
                bool addValues = false;

                Action<int, string> addTimeValue = (timeValue, description) =>
                {
                    if (addValues || (timeValue > 0))
                    {
                        if (addValues)
                        {
                            sb.Append(", ");
                        }

                        sb.Append(timeValue);
                        sb.Append(description);
                        addValues = true;
                    }
                };

                addTimeValue(timeSpan.Days, "d");
                addTimeValue(timeSpan.Hours, "h");
                addTimeValue(timeSpan.Minutes, "m");
                addTimeValue(timeSpan.Seconds, "s");
                addTimeValue(timeSpan.Milliseconds, "ms");
            }

            return sb.ToString();
        }
    }
}
