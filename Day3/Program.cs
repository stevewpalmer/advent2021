const int maxBits = 12;

List<int> codes = File.ReadAllLines("day3.txt").Select(p => Convert.ToInt32(p, 2)).ToList();

int[] bitCount = CountBits(codes);

int gamma = 0;
int epsilon = 0;
int mask = 1;
for (int p = maxBits - 1; p >= 0; p--)
{
    if (bitCount[p] >= 0)
    {
        gamma |= mask;
    }
    epsilon |= mask;
    mask <<= 1;
}
epsilon -= gamma;

List<int> oxygenCodes = new (codes);
List<int> co2scrubberCodes = new (codes);

for (int p = 0; p < maxBits; p++)
{
    mask >>= 1;

    int[] oxygenBitCount = CountBits(oxygenCodes);
    int[] co2BitCount = CountBits(co2scrubberCodes);

    for (int m = oxygenCodes.Count - 1; m >= 0 && oxygenCodes.Count > 1; m--)
    {
        int testBit = oxygenBitCount[p] >= 0 ? mask : 0;
        if ((oxygenCodes[m] & mask) != testBit)
        {
            oxygenCodes.RemoveAt(m);
        }
    }

    for (int m = co2scrubberCodes.Count - 1; m >= 0 && co2scrubberCodes.Count > 1; m--)
    {
        int testBit = co2BitCount[p] >= 0 ? 0 : mask;
        if ((co2scrubberCodes[m] & mask) != testBit)
        {
            co2scrubberCodes.RemoveAt(m);
        }
    }
}

int oxygen = oxygenCodes[0];
int co2scrubber = co2scrubberCodes[0];

Console.WriteLine($"Puzzle 1 answer : {gamma * epsilon}");
Console.WriteLine($"Puzzle 2 answer : {oxygen * co2scrubber}");

static int[] CountBits(List<int> codes)
{
    int[] bitCount = new int[maxBits];

    foreach (int code in codes)
    {
        int mask = 1;
        for (int p = maxBits - 1; p >= 0; p--)
        {
            bitCount[p] += (code & mask) == mask ? 1 : -1;
            mask <<= 1;
        }
    }
    return bitCount;
}
