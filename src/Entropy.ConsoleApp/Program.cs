namespace Entropy.ConsoleApp;

internal sealed class Program
{
    public static void Main()
    {
        var maxCount = 1000;
        var amt = 100.0m;

        var res = new decimal[maxCount];

        for (int i = 0; i < maxCount; i++)
        {
            var sign = GetRandomSign();
            var delta = sign > 0 ? 1.01m : 0.99m;
            res[i] = amt * delta;
            amt = res[i];

            Console.WriteLine($"step={i} amt={res[i]}");
        }
    }

    private static int GetRandomSign()
        => Random.Shared.Next(0, 2) == 0 ? -1 : 1;
}
