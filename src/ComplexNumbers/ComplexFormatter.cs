using System.Numerics;

namespace ComplexNumbers;

// https://learn.microsoft.com/en-us/dotnet/api/system.numerics.complex?view=net-7.0
public class ComplexFormatter : IFormatProvider, ICustomFormatter
{
    public object? GetFormat(Type? formatType)
    {
        if (formatType == typeof(ICustomFormatter))
        {
            return this;
        }
        else
        {
            return null;
        }
    }

    public string Format(string? format, object? arg, IFormatProvider? provider)
    {
        if (format == null)
        {
            return string.Empty;
        }

        if (arg is Complex c1)
        {
            // Check if the format string has a precision specifier.
            int precision;
            string fmtString = string.Empty;

            if (format.Length > 1)
            {
                try
                {
                    precision = int.Parse(format[1..]);
                }
                catch (FormatException)
                {
                    precision = 0;
                }

                fmtString = "N" + precision.ToString();
            }

            if (format[..1].Equals("I", StringComparison.OrdinalIgnoreCase))
            {
                return c1.Real.ToString(fmtString) + " + " + c1.Imaginary.ToString(fmtString) + "i";
            }
            else if (format[..1].Equals("J", StringComparison.OrdinalIgnoreCase))
            {
                return c1.Real.ToString(fmtString) + " + " + c1.Imaginary.ToString(fmtString) + "j";
            }

            return c1.ToString(format, provider);
        }

        if (arg is IFormattable formattable)
        {
            return formattable.ToString(format, provider);
        }

        return arg?.ToString() ?? string.Empty;
    }
}
