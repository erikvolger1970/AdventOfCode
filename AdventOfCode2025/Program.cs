using AdventOfCode2025;

var clicks = File
    .ReadAllText("safecombinations.txt")
    .Split()
    .Select(line => ParseClicks(line));

int solution = 0;
Dial dial = new(50);
foreach (var click in clicks)
{
    dial.Rotate(click);
    if (dial.Position == 0)
        solution++;
}

Console.WriteLine($"Solution = { solution}");

static int ParseClicks(string rotation)
{
    if (string.IsNullOrEmpty(rotation))
        return 0;

    string number = rotation[0] switch
    {
        'R' => rotation[1..],
        'L' => '-'+rotation[1..],
        _ => "0"
    };

    return int.TryParse(number, out int result) ? result : 0;
}
