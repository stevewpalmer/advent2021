const int maxX = 1000;
const int maxY = 1000;

Console.WriteLine($"Puzzle 1 answer : {CountOverlaps(false)} overlapping lines");
Console.WriteLine($"Puzzle 2 answer : {CountOverlaps(true)} overlapping lines");

static int CountOverlaps(bool withDiagonals)
{
    int[,] map = new int[maxX, maxY];

    int overlapCount = 0;
    foreach (string coords in File.ReadAllLines("day5.txt"))
    {
        string[] pairs = coords.Split(" -> ");
        string[] v1 = pairs[0].Split(',');
        string[] v2 = pairs[1].Split(',');

        int x1, y1, x2, y2;

        (x1, y1) = (Convert.ToInt32(v1[0]), Convert.ToInt32(v1[1]));
        (x2, y2) = (Convert.ToInt32(v2[0]), Convert.ToInt32(v2[1]));

        if (!withDiagonals && x1 != x2 && y1 != y2)
        {
            continue;
        }

        if (x1 > x2 || y1 > y2)
        {
            (x2, x1) = (x1, x2);
            (y2, y1) = (y1, y2);
        }

        int length = x2 > x1 ? x2 - x1 : y2 - y1;
        int deltaX = (x2 - x1) / length;
        int deltaY = (y2 - y1) / length;
        do
        {
            ++map[x1, y1];
            if (map[x1, y1] == 2)
            {
                ++overlapCount;
            }
            x1 += deltaX;
            y1 += deltaY;
            length--;
        } while (length >= 0);
    }
    return overlapCount;
}
