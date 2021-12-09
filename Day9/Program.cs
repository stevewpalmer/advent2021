string[] inputs = File.ReadAllLines("day9.txt");
int sumOfLowPoints = 0;

int[][] map = inputs.Select(r => r.ToCharArray().Select(c => c - '0').ToArray()).ToArray();

int PointValue(int x, int y) => x < 0 || x >= map[0].Length || y < 0 || y >= map.Length ? 9 : map[y][x];

int LowestPoint(int x, int y) => new int[] { PointValue(x - 1, y), PointValue(x + 1, y), PointValue(x, y - 1), PointValue(x, y + 1) }.Min();

int BasinSize(int x, int y)
{
    if (PointValue(x, y) == 9)
    {
        return 0;
    }
    map[y][x] = 9;
    return 1 + BasinSize(x - 1, y) + BasinSize(x + 1, y) + BasinSize(x, y - 1) + BasinSize(x, y + 1);
}

List<int> basinSizes = new();

for (int y = 0; y < map.Length; y++)
{
    for (int x = 0; x < map[y].Length; x++)
    {
        if (map[y][x] < LowestPoint(x, y))
        {
            sumOfLowPoints += map[y][x] + 1;
            basinSizes.Add(BasinSize(x, y));
        }
    }
}

int totalBasinSizes = basinSizes.OrderByDescending(b => b).Take(3).Aggregate((a, b) => a * b);

Console.WriteLine($"Puzzle 1 answer : {sumOfLowPoints}");
Console.WriteLine($"Puzzle 2 answer : {totalBasinSizes}");