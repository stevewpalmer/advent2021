string [] input = File.ReadAllLines("day13.txt");

List<(int, int)> map = new();

bool HasDot(int y, int x) => map.Contains((y, x));

void SetDot(int y, int x) { if (!HasDot(y, x)) map.Add((y, x)); }

int index = 0;
int lastRow = 0;
int lastColumn = 0;

while (index < input.Length && !string.IsNullOrEmpty(input[index]))
{
    string[] coords = input[index].Split(',');
    int x = Convert.ToInt32(coords[0]);
    int y = Convert.ToInt32(coords[1]);

    map.Add((y, x));

    lastRow = Math.Max(y, lastRow);
    lastColumn = Math.Max(x, lastColumn);
    index++;
}

while (string.IsNullOrEmpty(input[index]))
{
    index++;
}

int countOfVisibleDots = -1;

while (index < input.Length)
{
    string[] inst = input[index].Split('=');
    if (inst[0] == "fold along y")
    {
        int foldLine = Convert.ToInt32(inst[1]);

        List<(int, int)> copyOfMap = new(map);
        foreach ((int y, int x) in copyOfMap)
        {
            if (y > foldLine)
            {
                SetDot(y - 2 * (y - foldLine), x);
            }
        }
        lastRow = foldLine - 1;
    }
    if (inst[0] == "fold along x")
    {
        int foldLine = Convert.ToInt32(inst[1]);
        List<(int, int)> copyOfMap = new(map);
        foreach ((int y, int x) in copyOfMap)
        {
            if (x > foldLine)
            {
                SetDot(y, x - 2 * (x - foldLine));
            }
        }
        lastColumn = foldLine - 1;
    }

    if (countOfVisibleDots < 0)
    {
        countOfVisibleDots = 0;
        foreach ((int y, int x) in map)
        {
            if (y <= lastRow && x <= lastColumn)
            {
                ++countOfVisibleDots;
            }
        }
    }
    index++;
}

Console.WriteLine($"Puzzle 1 answer : {countOfVisibleDots}");
Console.WriteLine("Puzzle 2 answer : below ...");

for (int y = 0; y <= lastRow; y++)
{
    for (int x = 0; x <= lastColumn; x++)
    {
        Console.Write(HasDot(y, x) ? "#" : ".");
    }
    Console.WriteLine();
}
