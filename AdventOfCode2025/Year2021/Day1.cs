namespace AdventOfCode.Year2021;

internal class Day1
{
    public static void Run()
    {
        // read file and convertt into list of integers
        IEnumerable<int> depths = File
            .ReadAllText("Year2021/day1input.txt")
            .Split()
            .Select(ParseInt);

        // count all the depth increases of a moveing window
        int previous = depths.First();
        var totalDepthIncreases = depths.Aggregate(0, (total, next) =>
        {
            int increase = next > previous ? 1 : 0;
            previous = next;
            return total + increase;
        });

        Console.WriteLine($"2021 Day 1 Solution = {totalDepthIncreases}"); // 1390
    }

    // convert a string to an int, use 0 for empty strings
    private static int ParseInt(string s) =>
        int.TryParse(s, out int result) ? result : 0;
}
