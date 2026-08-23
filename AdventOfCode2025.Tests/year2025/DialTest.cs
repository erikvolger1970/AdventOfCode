using AdventOfCode.Year2025;

namespace AdventOfCode.Tests.year2025;

// Tests
// A new Dial starts at the default position
// A new Dial with given position starts at that position
// A new Dial with position outside 0-99 throws an exception
// A Dial can be rotated right n clicks and wraps around
// A Dial can be rotated left n clicks and wraps around

public class DialTest
{
    [Fact]
    public void NewDialHasPosition0()
    {
        Dial dial = new();
        Assert.Equal(0, dial.Position);
    }

    [Fact]
    public void NewDialWithPosition50HasPosition50()
    {
        Dial dial = new(50);
        Assert.Equal(50, dial.Position);
    }

    [Fact]
    public void NewDialWithPosition100Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Dial(Dial.Maximum + 1));
    }

    [Theory]
    [InlineData(   1,  1)] // one click right
    [InlineData(  99, 99)] // almost full turn right
    [InlineData( 100,  0)] // one full turn right
    [InlineData( 314, 14)] // three full turn right and a bit
    [InlineData(  -1, 99)] // one click left
    [InlineData( -99,  1)] // almost full turn left
    [InlineData(-100,  0)] // one full turn left
    [InlineData(-314, 86)] // three full turn left and a bit
    public void DialRotate(int clicks, int expectedPosition)
    {
        Dial dial = new();
        dial.Rotate(clicks);
        Assert.Equal(expectedPosition, dial.Position);
    }
}
