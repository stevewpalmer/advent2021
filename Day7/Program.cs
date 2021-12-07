int[] positions = File.ReadAllText("day7.txt").Split(',').Select(p => Convert.ToInt32(p)).ToArray();
int smallestDelta1 = int.MaxValue;
int smallestDelta2 = int.MaxValue;

// Brute force and O(n^2) runtime at worse but it works.

int start = positions.Min();
int end = positions.Max();
for (int p = start; p <= end; p++)
{
    int delta1Sum = 0;
    int delta2Sum = 0;
    for (int m = 0; m < positions.Length; m++)
    {
        int range = Math.Abs(positions[m] - p);
        delta1Sum += range;
        delta2Sum += (int)(((float)range / 2) * (range + 1));
    }
    if (delta1Sum < smallestDelta1)
    {
        smallestDelta1 = delta1Sum;
    }
    if (delta2Sum < smallestDelta2)
    {
        smallestDelta2 = delta2Sum;
    }
}
Console.WriteLine($"Puzzle 1 answer : Least fuel is {smallestDelta1}");
Console.WriteLine($"Puzzle 2 answer : Least fuel is {smallestDelta2}");
