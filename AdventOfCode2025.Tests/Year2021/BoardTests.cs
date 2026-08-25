using AdventOfCode.Year2021;

namespace AdventOfCode.Tests.Year2021;

public class BoardTests
{
    [Fact]
    public void OneHorizontalCreatureMovesRight()
    {
        char[][] cells = [['>', '.', '.', '.']];
        Board board = new(cells);
        bool moved =board.Step();
        Assert.True(moved);
        Assert.Equal(".>..", board.ToString());
    }

    [Fact]
    public void OneHorizontalCreatureOnEdgeWraps()
    {
        char[][] cells = [['.', '.', '.', '>']];
        Board board = new(cells);
        bool moved = board.Step();
        Assert.True(moved);
        Assert.Equal(">...", board.ToString());
    }

    [Fact]
    public void FacingHorizontalCreaturesMoveRightWhenAvailable()
    {
        char[][] cells = [['>', '>', '.', '.']];
        Board board = new(cells);
        bool moved = board.Step();
        Assert.True(moved);
        Assert.Equal(">.>.", board.ToString());
    }

    [Fact]
    public void OneVerticalCreatureMovesDown()
    {
        char[][] cells =
        [
            ['v', '.'],
            ['.', '.']  
         ];
        Board board = new(cells);

        bool moved = board.Step();

        Assert.True(moved);
        string expected = """
            ..
            v.
            """;
        Assert.Equal(expected, board.ToString());
    }

    [Fact]
    public void OneVerticalCreatureOnEdgeWraps()
    {
        char[][] cells =
        [
            ['.', '.'],
            ['v', '.']
        ];
        Board board = new(cells);

        bool moved = board.Step();

        string expected = """
            v.
            ..
            """;
        Assert.Equal(expected, board.ToString());
    }

    [Fact]
    public void FacingVerticalCreaturesMoveDownWhenAvailable()
    {
        char[][] cells =            
        [
            ['v', '.'],
            ['v', '.'],
            ['.', '.']
        ];
        Board board = new(cells);

        bool moved = board.Step();

        string expected = """
            v.
            ..
            v.
            """;
        Assert.Equal(expected, board.ToString());
    }

    [Fact]
    public void MixedCreaturesMoveWhenAvailable()
    {
        char[][] cells =
        [
            ['v', '.'],
            ['v', '.'],
            ['>', '.']
        ];
        Board board = new(cells);
        bool moved = board.Step();

        string expected = """
            v.
            ..
            v>
            """;
        Assert.Equal(expected, board.ToString());
    }
}
