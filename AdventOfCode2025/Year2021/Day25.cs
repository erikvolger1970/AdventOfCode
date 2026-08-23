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

    private readonly ICellChecker _horizontalCellChecker = new HorizontalCellChecker();
    private readonly ICellChecker _verticalCellChecker = new VerticalCellChecker();
    private bool _somethingMoved = false;

    public Board(char[][] cells)
    {
        _cells = cells;
    }
    
    public bool Step()
    {
        _somethingMoved = false;
        CheckCells(_horizontalCellChecker);
        CheckCells(_verticalCellChecker);
        return _somethingMoved;
    }

    private void CheckCells(ICellChecker cellChecker)
    {
        for (int row = 0; row < Rows; row++)
        {
            for (int col = 0; col < Columns; col++)
            {
                if (_cells[row][col] == cellChecker.CreatureType)
                {
                    int adjacent = cellChecker.AdjacentCell(row, col);

                    // move creature to adjacent cell
                    if (_cells[row][adjacent] == Empty) // must be _cells[adjacent][col] for Vertical!!!
                    {
                        _cells[row][col] = Empty;
                        _cells[row][adjacent] = cellChecker.CreatureType;
                        _somethingMoved = true;
                    }
                }
            }
        }
    }

    private interface ICellChecker
    {
        char CreatureType { get; }
        int AdjacentCell(int row, int col);
    }

    private class HorizontalCellChecker : ICellChecker
    {
        public char CreatureType => Horizontal;
        public int AdjacentCell(int row, int col) => col < Columns - 1 ? col + 1 : 0;
        //public bool IsAdjacentCellEmpty() =>
    }

    private class VerticalCellChecker : ICellChecker
    {
        public char CreatureType => Vertical;
        public int AdjacentCell(int row, int col) => row < Rows - 1 ? row + 1 : 0;
    }
}