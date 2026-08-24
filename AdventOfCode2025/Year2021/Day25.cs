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
            rows.Add([.. line]);
        
        return [.. rows];
    }
}

public class Board
{
    private const int Rows = 137;
    private const int Columns = 139;
    private const char Empty = '.';
    private const char Horizontal = '>';
    private const char Vertical = 'v';

    // I use a simple 2 * 2 array because I need to traverse rows and columns
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
        // use the same order for both horizontal and vertical creatures
        // it makes no difference that vertical creatures are processed by row first, while they move by col...
        for (int row = 0; row < Rows; row++)
            for (int col = 0; col < Columns; col++)
                CheckCell(cellChecker, new Cell(row, col));
    }

    private void CheckCell(ICellChecker cellChecker, Cell current)
    {
        if (HasCreature(current, cellChecker.CreatureType))
        {
            Cell cell = cellChecker.AdjacentCell(current);
            if (IsEmpty(cell))
                Move(cellChecker.CreatureType, current, cell);
        }
    }

    private bool HasCreature(Cell cell, char creatureType) => _cells[cell.Row][cell.Col] == creatureType;

    private bool IsEmpty(Cell cell) => _cells[cell.Row][cell.Col] == Empty;

    private void Move(char creatureType, Cell from, Cell to)
    {
        _cells[from.Row][from.Col] = Empty;
        _cells[to.Row][to.Col] = creatureType;
        _somethingMoved = true;
    }

    private record Cell(int Row, int Col);

    private interface ICellChecker // Todo: Maybe a better name?
    {
        char CreatureType { get; }
        Cell AdjacentCell(Cell cell);
    }

    private class HorizontalCellChecker : ICellChecker
    {
        public char CreatureType => Horizontal;
        public Cell AdjacentCell(Cell cell) => cell with { Col = cell.Col < Columns - 1 ? cell.Col + 1 : 0 };
    }

    private class VerticalCellChecker : ICellChecker
    {
        public char CreatureType => Vertical;
        public Cell AdjacentCell(Cell cell) => cell with { Row = cell.Row < Rows - 1 ? cell.Row + 1 : 0 };
    }
}