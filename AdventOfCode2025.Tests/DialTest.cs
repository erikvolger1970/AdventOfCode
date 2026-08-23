using System.Diagnostics.CodeAnalysis;
using Xunit.Sdk;

namespace AdventOfCode2025.Tests;

// Tests
// A new Dial starts at the default position
// A new Dial with given position starts at that position
// A new Dial with position outside 0-99 throws an exception

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
        Assert.Throws<ArgumentOutOfRangeException>(() => new Dial(100));
    }
}
