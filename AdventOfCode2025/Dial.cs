namespace AdventOfCode2025;

public class Dial
{
    public Dial(int position = 0)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(position);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(position, 99);

        Position = position;
    }

    public int Position { get; }
}
