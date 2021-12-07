const int maxBits = 12;

List<string> codes = File.ReadAllLines("day3.txt").ToList();

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

List<string> oxygenCodes = new (codes);
List<string> co2scrubberCodes = new (codes);

for (int p = 0; p < maxBits; p++)
{
    int[] oxygenBitCount = CountBits(oxygenCodes);
    int[] co2BitCount = CountBits(co2scrubberCodes);

    for (int m = oxygenCodes.Count - 1; m >= 0 && oxygenCodes.Count > 1; m--)
    {
        char[] bits = oxygenCodes[m].ToCharArray();
        char testBit = oxygenBitCount[p] >= 0 ? '1' : '0';
        if (bits[p] != testBit)
        {
            oxygenCodes.RemoveAt(m);
        }
    }

    for (int m = co2scrubberCodes.Count - 1; m >= 0 && co2scrubberCodes.Count > 1; m--)
    {
        char[] bits = co2scrubberCodes[m].ToCharArray();
        char testBit = co2BitCount[p] >= 0 ? '0' : '1';
        if (bits[p] != testBit)
        {
            co2scrubberCodes.RemoveAt(m);
        }
    }
}

int oxygen = Convert.ToInt32(oxygenCodes[0], 2);
int co2scrubber = Convert.ToInt32(co2scrubberCodes[0], 2);

Console.WriteLine($"Puzzle 1 answer : {gamma * epsilon}");
Console.WriteLine($"Puzzle 2 answer : {oxygen * co2scrubber}");

static int[] CountBits(List<string> codes)
{
    int[] bitCount = new int[maxBits];

    foreach (string code in codes)
    {
        char[] bits = code.ToCharArray();
        for (int p = 0; p < maxBits; p++)
        {
            bitCount[p] += bits[p] == '1' ? 1 : -1;
        }
    }
    return bitCount;
}
