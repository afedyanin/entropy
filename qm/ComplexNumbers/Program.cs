using System.Numerics;

namespace ComplexNumbers;

internal sealed class Program
{
    public static void Main()
    {
        PowerOfIm(8);
    }

    // https://learn.microsoft.com/en-us/dotnet/api/system.numerics.complex?view=net-7.0
    internal static void SimpleOperations()
    {
        var c1 = new Complex(12, 6);
        Console.WriteLine(c1);

        var c4 = Complex.Pow(Complex.One, -1);
        Console.WriteLine(c4);

        var c5 = Complex.One + Complex.One;
        Console.WriteLine(c5);

        var c6 = Complex.FromPolarCoordinates(10, .524);
        Console.WriteLine(c6);

        var c7 = Complex.Conjugate(Complex.ImaginaryOne);
        Console.WriteLine(c7);
    }

    internal static void PowerOfIm(int count)
    {
        for (int i = 0; i <= count; i++)
        {
            var value = Complex.Pow(Complex.ImaginaryOne, i);
            //var formatted = string.Format(new ComplexFormatter(), "{0:I0}", value);
            Console.WriteLine($"powOf i = {i:N0} => {value:N0}");
        }
    }
}
