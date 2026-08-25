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
        Assert.Equal('>', board.GetCellForTests(0, 1));
    }

    [Fact]
    public void OneHorizontalCreatureOnEdgeWraps()
    {
        char[][] cells = [['.', '.', '.', '>']];
        Board board = new(cells);
        bool moved = board.Step();
        Assert.True(moved);
        Assert.Equal('>', board.GetCellForTests(0, 0));
    }

    [Fact]
    public void FacingHorizontalCreaturesMoveRightWhenAvailable()
    {
        char[][] cells = [['>', '>', '.', '.']];
        Board board = new(cells);
        bool moved = board.Step();
        Assert.True(moved);
        Assert.Equal('>', board.GetCellForTests(0, 0));
        Assert.Equal('>', board.GetCellForTests(0, 2));
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
        Assert.Equal('v', board.GetCellForTests(1, 0));
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
        Assert.True(moved);
        Assert.Equal('v', board.GetCellForTests(0, 0));
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
        Assert.True(moved);
        Assert.Equal('v', board.GetCellForTests(0, 0));
        Assert.Equal('v', board.GetCellForTests(2, 0));
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
        Assert.True(moved);
        Assert.Equal('v', board.GetCellForTests(0, 0));
        Assert.Equal('v', board.GetCellForTests(2, 0));
        Assert.Equal('>', board.GetCellForTests(2, 1));
    }
}
