int [][] map = File.ReadAllLines("day11.txt").Select(l => l.ToCharArray().Select(c => c - '0').ToArray()).ToArray();

int totalFlashes = 0;

void FlashOctopus(int x, int y) {
    if (x >= 0 && y >= 0 && y < map.Length && x < map[y].Length)
    {
        ++map[y][x];
        if (map[y][x] == 10)
        {
            FlashOctopus(x - 1, y - 1);
            FlashOctopus(x, y - 1);
            FlashOctopus(x + 1, y - 1);
            FlashOctopus(x - 1, y);
            FlashOctopus(x + 1, y);
            FlashOctopus(x - 1, y + 1);
            FlashOctopus(x, y + 1);
            FlashOctopus(x + 1, y + 1);

            totalFlashes++;
        }
    }
}

int firstSynchronizedFlashStep = -1;
for (int steps = 1; steps <= 100 || firstSynchronizedFlashStep < 0; steps++)
{
    for (int y = 0; y < map.Length; y++)
    {
        for (int x = 0; x < map[y].Length; x++)
        {
            FlashOctopus(x, y);
        }
    }

    int countOfFlashesInStep = 0;
    for (int y = 0; y < map.Length; y++)
    {
        for (int x = 0; x < map[y].Length; x++)
        {
            if (map[y][x] > 9)
            {
                ++countOfFlashesInStep;
                map[y][x] = 0;
            }
        }
    }
    if (countOfFlashesInStep == map.Length * map[0].Length && firstSynchronizedFlashStep < 0)
    {
        firstSynchronizedFlashStep = steps;
    }
}

Console.WriteLine($"Puzzle 1 answer : {totalFlashes}");
Console.WriteLine($"Puzzle 2 answer : {firstSynchronizedFlashStep}");