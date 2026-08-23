namespace AdventOfCode.Year2025;

public class Dial
{
    public const int Minimum = 0;
    public const int Maximum = 99;

    public Dial(int initialPosition = Minimum)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(initialPosition);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(initialPosition, Maximum);

        Position = initialPosition;
    }

    public int Position { get; private set; }

    public void Rotate(int clicks)
    {
        // remove full rotations
        clicks %= (Maximum + 1);

        if (clicks > 0)
            Position += clicks;                 // rotate right
        else
            Position += (Maximum + 1) + clicks; // rotate left = rotate right (100 - clicks)

        // check overflow
        while (Position > Maximum)
            Position -= (Maximum + 1);
    }
}