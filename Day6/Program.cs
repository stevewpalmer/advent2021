Console.WriteLine($"Puzzle 1 answer : After 80 days there are {SumFishes(80)} fish");
Console.WriteLine($"Puzzle 2 answer : After 256 days there are {SumFishes(256)} fish");

static long SumFishes(int days)
{
    long[] fishes = new long[9];

    foreach (int fish in File.ReadAllText("day6.txt").Split(',').Select(p => Convert.ToInt32(p)).ToList())
    {
        ++fishes[fish];
    }

    int day = 0;

    while (++day <= days)
    {
        long x = fishes[0];
        for (int p = 1; p < 9; ++p)
        {
            fishes[p - 1] = fishes[p];
        }
        fishes[8] = x;
        fishes[6] += x;
    }
    return fishes.Sum();
}