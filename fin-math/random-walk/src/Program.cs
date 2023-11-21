using System.Diagnostics;
using Microsoft.Data.Analysis;
using ScottPlot;
using Spectre.Console;

namespace Entropy.RandomWalk;

/// <summary>
/// https://swharden.com/blog/2022-05-01-dotnet-dataframe/
/// https://zetcode.com/csharp/msa-dataframe/
/// </summary>
internal sealed class Program
{
    public static void Main()
    {
        var maxCount = 500;
        var initialAmt = 100m;
        var delta = .01m;

        var columns = new DataFrameColumn[]
        {
            new PrimitiveDataFrameColumn<int>("Trial", Enumerable.Range(0, maxCount)),
            new PrimitiveDataFrameColumn<decimal>("Set1", GetRandomWalkValues(initialAmt, delta, delta, maxCount)),
            new PrimitiveDataFrameColumn<decimal>("Set2", GetRandomWalkValues(initialAmt, delta, delta, maxCount)),
            new PrimitiveDataFrameColumn<decimal>("Set3", GetRandomWalkValues(initialAmt, delta, delta, maxCount)),
            new PrimitiveDataFrameColumn<decimal>("Set4", GetRandomWalkValues(initialAmt, delta, delta, maxCount)),
            new PrimitiveDataFrameColumn<decimal>("Set5", GetRandomWalkValues(initialAmt, delta, delta, maxCount)),
        };

        DataFrame df = new(columns);
        /*
        var rowValues = new List<KeyValuePair<string, object>>()
        {
            new KeyValuePair<string, object>("Trial", 1),
            new KeyValuePair<string, object>("Set1", 32.1)
        };
        df.Append(rowValues, true);
        */

        // Console.WriteLine(df.PrettyText());
        PlotDF(df);

        Process photoViewer = new Process();
        photoViewer.StartInfo.FileName = @"C:\Program Files (x86)\FastStone Image Viewer\FSViewer.exe";
        photoViewer.StartInfo.Arguments = @"DataFrame.png";
        photoViewer.Start();

        // ConsoleDF(df);
    }

    private static decimal[] GetRandomWalkValues(
        decimal initialAmt,
        decimal deltaPlus,
        decimal deltaMinus,
        int count)
    {
        var res = new decimal[count];

        for (int i = 0; i < count; i++)
        {
            var sign = Random.Shared.Next(0, 2) == 0 ? -1 : 1;
            var delta = sign > 0 ? (1 + deltaPlus) : (1 - deltaMinus);
            res[i] = initialAmt * delta;
            initialAmt = res[i];
        }

        return res;
    }

    /// <summary>
    /// https://scottplot.net/cookbook/4.1/category/quickstart/#signal-plot
    /// </summary>
    /// <param name="df"></param>
    public static void PlotDF(DataFrame df)
    {
        var plt = new Plot(600, 400);

        // sample data
        double[] xs = DataGen.Consecutive((int)df.Rows.Count);
        double[] ys1 = Enumerable.Range(0, (int)df.Rows.Count).Select(row => Convert.ToDouble(df["Set1"][row])).ToArray();
        double[] ys2 = Enumerable.Range(0, (int)df.Rows.Count).Select(row => Convert.ToDouble(df["Set2"][row])).ToArray();
        double[] ys3 = Enumerable.Range(0, (int)df.Rows.Count).Select(row => Convert.ToDouble(df["Set3"][row])).ToArray();
        double[] ys4 = Enumerable.Range(0, (int)df.Rows.Count).Select(row => Convert.ToDouble(df["Set4"][row])).ToArray();
        double[] ys5 = Enumerable.Range(0, (int)df.Rows.Count).Select(row => Convert.ToDouble(df["Set5"][row])).ToArray();

        // plot the data
        plt.AddScatter(xs, ys1);
        plt.AddScatter(xs, ys2);
        plt.AddScatter(xs, ys3);
        plt.AddScatter(xs, ys4);
        plt.AddScatter(xs, ys5);

        // customize the axis labels
        plt.Title("Sample Random Walk");
        plt.XLabel("Trials");
        plt.YLabel("Value");
        plt.SaveFig("DataFrame.png");
    }

    /// <summary>
    /// https://github.com/spectreconsole/spectre.console
    /// </summary>
    /// <param name="df"></param>
    public static void ConsoleDF(DataFrame df)
    {
        var table = new Table()
            .Border(TableBorder.Ascii)
            .BorderColor(Color.SteelBlue)
            .AddColumn(new TableColumn("Trial").LeftAligned())
            .AddColumn(new TableColumn("Set1").RightAligned())
            .AddColumn(new TableColumn("Set2").RightAligned())
            .AddColumn(new TableColumn("Set3").RightAligned())
            .AddColumn(new TableColumn("Set4").RightAligned())
            .AddColumn(new TableColumn("Set5").RightAligned());

        foreach (var e in df.Head(100).Rows)
        {
            string[] row = { $"{e[0]}", $"{e[1]:0.0000}", $"{e[2]:0.0000}", $"{e[3]:0.0000}", $"{e[4]:0.0000}", $"{e[5]:0.0000}" };
            table.AddRow(row);
        }

        AnsiConsole.Write(table);
    }
}
