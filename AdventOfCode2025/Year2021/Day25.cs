namespace AdventOfCode.Year2021;

internal class Day25
{
    public static void Run()
    {
        // read board from file in a matrix
        Board board = new(ReadInputFile("Year2021/day25input.txt"));

        int numberOfSteps = 0;
        while (true) // potential infinite loop depending on input...
        {
            // calculate the new positions
            // if nothing moved break out
            if (board.Step())
                numberOfSteps++;
            else
                break;
        }

        Console.WriteLine($"2021 Day 25 Solution = {numberOfSteps}");

        string s = board.ToString();
        Console.WriteLine(s);
    }

    private static char[][] ReadInputFile(string filename)
    {
        List<char[]> rows = [];
        foreach (string line in File.ReadAllText(filename).Split().Where(line => !string.IsNullOrEmpty(line)))
            rows.Add([.. line]);
        
        return [.. rows];
    }
}
