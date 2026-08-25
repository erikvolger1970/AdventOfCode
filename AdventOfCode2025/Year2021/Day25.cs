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
    private readonly int _numberOfRows;
    private readonly int _numberOfColumns;

    private readonly ICellChecker _horizontalCellChecker;
    private readonly ICellChecker _verticalCellChecker;

    public Board(char[][] cells)
    {
        _cells = cells;
        _numberOfRows = _cells.Length;
        _numberOfColumns = _cells[0].Length;

        _horizontalCellChecker = new HorizontalCellChecker(_numberOfColumns - 1);
        _verticalCellChecker = new VerticalCellChecker(_numberOfRows - 1);
    }
    
    public bool Step()
    {
        IEnumerable<Move> horizontalMoves = CalculateMoves(_horizontalCellChecker);
        ExecuteMoves(horizontalMoves);

        IEnumerable<Move> verticalMoves = CalculateMoves(_verticalCellChecker);
        ExecuteMoves(verticalMoves);

        return horizontalMoves.Any() || verticalMoves.Any();
    }

    private List<Move> CalculateMoves(ICellChecker cellChecker)    
    {
        List<Move> moves = [];

        // use the same order for both horizontal and vertical creatures
        // it makes no difference that vertical creatures are processed by row first, while they move by col...
        for (int row = 0; row < _numberOfRows; row++)
            for (int col = 0; col < _numberOfColumns; col++)
            {
                Cell currentCell = new(row, col);
                if (HasCreature(currentCell, cellChecker.CreatureType))
                {
                    Cell adjacentCell = cellChecker.AdjacentCell(currentCell);
                    if (IsEmpty(adjacentCell))
                        moves.Add(new Move(currentCell, adjacentCell));
                }
            }

        return moves;
    }

    private void ExecuteMoves(IEnumerable<Move> moves)
    {
        foreach (Move move in moves)
        {
            _cells[move.To.Row][move.To.Col] = _cells[move.From.Row][move.From.Col];
            _cells[move.From.Row][move.From.Col] = '.';
        }
    }

    private bool HasCreature(Cell cell, char creatureType) => _cells[cell.Row][cell.Col] == creatureType;

    private bool IsEmpty(Cell cell) => _cells[cell.Row][cell.Col] == '.';

    private record Cell(int Row, int Col);
    private record Move(Cell From, Cell To);

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