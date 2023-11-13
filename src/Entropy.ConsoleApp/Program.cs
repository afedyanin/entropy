namespace Entropy.ConsoleApp;

internal sealed class Program
{
    public static void Main()
    {
        // var amt = 100m;
        var negCount = 0;
        var posCount = 0;
        var iterations = 10000000;

        for (int i = 0; i < iterations; i++)
        {
            var sign = GetRandomSign();
            if (sign > 0)
            {
                posCount++;
            }
            else
            {
                negCount++;
            }
        }

        var negMed = negCount * 1.0m / iterations;
        var posMed = posCount * 1.0m / iterations;

        Console.WriteLine($"negMed={negMed} posMed={posMed}");
    }

    private static int GetRandomSign()
        => Random.Shared.Next(0, 2) == 0 ? -1 : 1;
}
