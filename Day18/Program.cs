List<List<int>> lines = File.ReadAllLines("day18.txt").Select(l => ParseLine(l)).ToList();

const int PAIR_OPEN = -1;
const int PAIR_CLOSE = -2;
const int PAIR_SEPARATOR = -3;

const int PAIR_SIZE = 5;

static bool IsNumber(int c) => c >= 0;

Console.WriteLine($"Puzzle 1 answer : {TotalMagnitude(lines)}");
Console.WriteLine($"Puzzle 2 answer : {LargestMagnitude(lines)}");

static void AddAndReduce(List<int> leftPart, List<int> rightPart)
{
    if (leftPart.Count == 0)
    {
        leftPart.AddRange(rightPart);
    }
    else
    {
        leftPart.Insert(0, PAIR_OPEN);
        leftPart.Add(PAIR_SEPARATOR);
        leftPart.AddRange(rightPart);
        leftPart.Add(PAIR_CLOSE);
    }

    bool wasReduced = false;
    while (!wasReduced)
    {
        if (Explode(leftPart))
        {
            continue;
        }
        if (Split(leftPart))
        {
            continue;
        }
        wasReduced = true;
    }
}

static int TotalMagnitude(List<List<int>> listOfNumbers)
{
    List<int> accumulator = new();

    listOfNumbers.ForEach(l => AddAndReduce(accumulator, l));
    return Magnitude(accumulator);
}

static int LargestMagnitude(List<List<int>> listOfNumbers)
{
    int totalNumbers = listOfNumbers.Count;
    int largestMagnitude = 0;

    for (int m = 0; m < totalNumbers; m++)
    {
        for (int n = 0; n < totalNumbers; n++)
        {
            if (m != n)
            {
                List<int> numbers = new(listOfNumbers[m]);
                AddAndReduce(numbers, listOfNumbers[n]);
                int thisMagnitude = Magnitude(numbers);
                if (thisMagnitude > largestMagnitude)
                {
                    largestMagnitude = thisMagnitude;
                }
            }
        }
    }
    return largestMagnitude;
}

static int Magnitude(List <int> accumulator)
{
    List<int> magnitudeList = new(accumulator);
    while (magnitudeList.Count > 1)
    {
        for (int c = 0; c < magnitudeList.Count; c++)
        {
            if (magnitudeList[c] == PAIR_OPEN && magnitudeList[c + 4] == PAIR_CLOSE)
            {
                int mag = magnitudeList[c + 1] * 3 + magnitudeList[c + 3] * 2;
                magnitudeList.RemoveRange(c, PAIR_SIZE);
                magnitudeList.Insert(c, mag);
            }
        }
    }
    return magnitudeList[0];
}

static bool Split(List<int> accumulator)
{
    for (int c = 0; c < accumulator.Count; c++)
    {
        if (accumulator[c] > 9)
        {
            float value = accumulator[c];
            int floor = (int)Math.Floor(value / 2);
            int ceil = (int)Math.Ceiling(value / 2);

            accumulator.RemoveAt(c);
            accumulator.InsertRange(c, new [] { PAIR_OPEN, floor, PAIR_SEPARATOR, ceil, PAIR_CLOSE });
            return true;
        }
    }
    return false;
}

static bool Explode(List<int> accumulator)
{
    int leftNumber = -1;
    int rightNumber = -1;
    int depth = 0;

    for (int c = 0; c < accumulator.Count; c++)
    {
        switch (accumulator[c])
        {
            case PAIR_OPEN when depth == 4:
                for (int d = c + PAIR_SIZE; d < accumulator.Count; d++)
                {
                    if (IsNumber(accumulator[d]))
                    {
                        rightNumber = d;
                        break;
                    }
                }
                if (leftNumber >= 0)
                {
                    accumulator[leftNumber] += accumulator[c + 1];
                }
                if (rightNumber >= 0)
                {
                    accumulator[rightNumber] += accumulator[c + 3];
                }
                accumulator.RemoveRange(c, PAIR_SIZE);
                accumulator.Insert(c, 0);
                return true;

            case PAIR_OPEN:
                ++depth;
                break;

            case PAIR_CLOSE:
                --depth;
                break;

            default:
                if (IsNumber(accumulator[c]))
                {
                    leftNumber = c;
                }
                break;
        }
    }
    return false;
}

static List<int> ParseLine(string line)
{
    return line.ToCharArray().Select(d => d switch
    {
        char c when c == '[' => PAIR_OPEN,
        char c when c == ']' => PAIR_CLOSE,
        char c when c >= '0' && c <= '9' => c - '0',
        _ => PAIR_SEPARATOR
    }).ToList();
}