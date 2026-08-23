using AdventOfCode2025;

// lees aangeleverde file in en converteer naar reeks rotaties.
IEnumerable<int> clicks = File
    .ReadAllText("safecombinations.txt")
    .Split()
    .Select(ParseClicks);

int solution = 0;

// De Dial class is verantwoordelijk voor bijhouden van de positie tijdens de rotaties.
// De rotaties kunnen meer dan één omwenteling naar links of rechts inhouden.
// Met TDD ontwikkeld. De tests staan in AdventOfCode2025.Tests.csproj
Dial dial = new(50);

// Voer alle ingelezen rotaties door in de juiste volgorde en houdt bij hoe vaak de Dial op positie op 0 land.
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
