namespace AdventOfCode.Year2021;

internal class Day25
{
    public static void Run()
    {
        // read board from file in a matrix
        Board board = new(ReadInputFile("Year2021/day25input.txt"));

        int numberOfSteps = 0;
        while (true)
        {
            numberOfSteps++;

            // calculate the new positions
            // if nothing moved break out
            if (!board.Step())
                break;
        }

        Console.WriteLine($"2021 Day 25 Solution = {numberOfSteps}");
    }

    private static char[][] ReadInputFile(string filename)
    {
        List<char[]> rows = [];
        foreach (string line in File.ReadAllText(filename).Split())
        {
            rows.Add([.. line]);
        }
        
        return [.. rows];
    }
}

public class Board
{
    private const int Rows = 137;
    private const int Columns = 140;
    private const char Empty = '.';
    private const char Horizontal = '>';
    private const char Vertical = 'v';

    // I use a simple 2 * 2 array because I need to go to rows and columns
    private readonly char[][] _cells = [];

    public Board(char[][] cells)
    {
        _cells = cells;
    }
    
    // There is some duplication here but fixing it doesn't make it clearer
    // Or I do not see the abstraction yet...
    public bool Step()
    {
        bool somethingMoved = false;

        // horizontal movement
        for (int row = 0; row < Rows; row++)
        {
            for (int col = 0; col < Columns; col++)
            {
                if (_cells[row][col] == Horizontal) // check for creature type
                {
                    int adjacent = col < Columns - 1 ? col + 1 : 0; // calc adjacent cell
                    
                    // move creature to adjacent cell
                    if (_cells[row][adjacent] == Empty)
                    {
                        _cells[row][col] = Empty;
                        _cells[row][adjacent] = Horizontal;
                        somethingMoved = true;
                    }
                }
            }
        }

        // vertical movement
        for (int row = 0; row < Rows; row++)
        { 
            for (int col = 0; col < Columns; col++)
            {           
                if (_cells[row][col] == Vertical) // check for creature type
                {
                    int adjacent = row < Rows - 1 ? row + 1 : 0; // calc adjacent cell

                    // move creature to adjacent cell
                    if (_cells[adjacent][col] == Empty)
                    {
                        _cells[row][col] = Empty;
                        _cells[adjacent][col] = Horizontal;
                        somethingMoved = true;
                    }
                }
            }
        }

        return somethingMoved;
    }
}