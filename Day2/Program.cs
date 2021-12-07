string[] instructions = File.ReadAllLines("day2.txt");

int depth1 = 0;
int horizontal1 = 0;

foreach (string instruction in instructions)
{
    string[] tokens = instruction.Split(' ');
    int value = Convert.ToInt32(tokens[1]);

    switch (tokens[0])
    {
        case "forward":
            horizontal1 += value;
            break;

        case "up":
            depth1 -= value;
            break;

        case "down":
            depth1 += value;
            break;
    }
}

int aim = 0;
int depth2 = 0;
int horizontal2 = 0;

foreach (string instruction in instructions)
{
    string[] tokens = instruction.Split(' ');
    int value = Convert.ToInt32(tokens[1]);

    switch (tokens[0])
    {
        case "forward":
            horizontal2 += value;
            depth2 += aim * value;
            break;

        case "up":
            aim -= value;
            break;

        case "down":
            aim += value;
            break;
    }
}

Console.WriteLine($"Puzzle 1 answer : {depth1 * horizontal1}");
Console.WriteLine($"Puzzle 2 answer : {depth2 * horizontal2}");
