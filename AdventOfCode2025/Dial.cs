namespace AdventOfCode2025;

public class Dial
{
    public const int Minimum = 0;
    public const int Maximum = 99;

    public Dial(int position = Minimum)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(position);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(position, Maximum);

        Position = position;
    }

    public int Position { get; private set; }

    public void Rotate(int clicks)
    {
        if (clicks > 0)
            Position += clicks % (Maximum + 1);                 // rotate right
        else
            Rotate((Maximum + 1) + (clicks % (Maximum + 1)));   // rotate left = rotate right (100 - clicks)
    }
}