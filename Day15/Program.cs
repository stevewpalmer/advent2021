string [] lines = File.ReadAllLines("day15.txt");

int width = lines[0].Length;
int height = lines.Length;

int[,] smallmap = new int[height, width];
for (int y = 0; y < height; y++)
{
    for (int x = 0; x < width; x++)
    {
        smallmap[y, x] = lines[y][x] - '0';
    }
}

int[,] bigmap = new int[height * 5, width * 5];
for (int y = 0; y < height * 5; y++)
{
    for (int x = 0; x < width * 5; x++)
    {
        bigmap[y, x] = 1 + ((smallmap[y % height, x % width] + (y / height) + (x / width) - 1) % 9);
    }
}

int CalculatePath(int [,] theMap, int width, int height)
{
    PriorityQueue<(int, int), int> queue = new ();
    Dictionary<(int, int), int> risks = new();

    int Risk(int x, int y) => risks.ContainsKey((x, y)) ? risks[(x, y)] : int.MaxValue;

    risks[(0, 0)] = 0;
    queue.Enqueue((0, 0), 0);

    while (queue.Count > 0)
    {
        int x, y;
        (x, y) = queue.Dequeue();

        int riskAtNode = Risk(x, y);

        foreach ((int xn, int yn) in new List<(int, int)>() { (x - 1, y), (x + 1, y), (x, y - 1), (x, y + 1) })
        {
            if (xn >= 0 && xn < width && yn >= 0 && yn < height)
            {
                int tn = riskAtNode + theMap[yn, xn];
                if (tn < Risk(xn, yn))
                {
                    risks[(xn, yn)] = tn;
                    queue.Enqueue((xn, yn), tn);
                }
            }
        }
    }
    return Risk(width - 1, height - 1);
}

Console.WriteLine($"Puzzle 1 answer : {CalculatePath(smallmap, width, height)}");
Console.WriteLine($"Puzzle 1 answer : {CalculatePath(bigmap, 5 * width, 5 * height)}");