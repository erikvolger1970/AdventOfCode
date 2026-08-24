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
    }

    private static char[][] ReadInputFile(string filename)
    {
        List<char[]> rows = [];
        foreach (string line in File.ReadAllText(filename).Split().Where(line => !string.IsNullOrEmpty(line)))
            rows.Add([.. line]);
        
        return [.. rows];
    }
}

public class Board
{
    // I use a simple 2 * 2 array because I need to traverse rows and columns
    private readonly char[][] _cells = [];
    private readonly int _rows;
    private readonly int _columns;

    private readonly ICellChecker _horizontalCellChecker;
    private readonly ICellChecker _verticalCellChecker;
    private bool _somethingMoved = false;

    public Board(char[][] cells)
    {
        _cells = cells;
        _rows = _cells.Length;
        _columns = _cells[0].Length;

        _horizontalCellChecker = new HorizontalCellChecker(_columns - 1);
        _verticalCellChecker = new VerticalCellChecker(_rows - 1);
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
        for (int row = 0; row < _rows; row++)
            for (int col = 0; col < _columns; col++)
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

    private bool IsEmpty(Cell cell) => _cells[cell.Row][cell.Col] == '.';

    private void Move(char creatureType, Cell from, Cell to)
    {
        _cells[from.Row][from.Col] = '.';
        _cells[to.Row][to.Col] = creatureType;
        _somethingMoved = true;
    }

    private record Cell(int Row, int Col);

    private interface ICellChecker // Todo: Maybe a better name?
    {
        char CreatureType { get; }
        Cell AdjacentCell(Cell cell);
    }

    private class HorizontalCellChecker(int maximumColumn) : ICellChecker
    {
        public char CreatureType => '>';

        public Cell AdjacentCell(Cell cell) => cell with { Col = cell.Col < maximumColumn ? cell.Col + 1 : 0 };
    }

    private class VerticalCellChecker(int maximumRow) : ICellChecker
    {
        public char CreatureType => 'v';

        public Cell AdjacentCell(Cell cell) => cell with { Row = cell.Row < maximumRow ? cell.Row + 1 : 0 };
    }
}