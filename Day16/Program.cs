string input = File.ReadAllText("day16.txt").Trim();

string[] bits = { "0000", "0001", "0010", "0011", "0100", "0101", "0110", "0111", "1000", "1001", "1010", "1011", "1100", "1101", "1110", "1111" };

Queue<char> packet = new(string.Join("", input.ToCharArray().Select(c => bits[c > '9' ? c - 'A' + 10 : c - '0'])).ToCharArray());

int ReadValue(Queue<char> valuePacket, int count) => Convert.ToInt32(string.Join("", Enumerable.Range(0, count).Select(_ => valuePacket.Dequeue())), 2);

long ReadLiteral(Queue<char> literalPacket)
{
    long value = 0;
    long nybble = 0;
    do
    {
        nybble = ReadValue(literalPacket, 5);
        value = (value << 4) | (nybble & 0b1111);
    } while (nybble >= 0b10000);
    return value;
}

(int, long) ReadPacket(Queue<char> subPacket)
{
    int packetVersion = ReadValue(subPacket, 3);
    int packetType = ReadValue(subPacket, 3);
    long value = 0;

    if (packetType == 4)
    {
        value = ReadLiteral(subPacket);
    }
    else
    {
        int operatorMode = ReadValue(subPacket, 1);
        List<long> valueArray = new();
        if (operatorMode == 0)
        {
            int subPacketLength = ReadValue(subPacket, 15);
            Queue<char> operatorPacket = new(Enumerable.Range(0, subPacketLength).Select(_ => subPacket.Dequeue()));
            while (operatorPacket.Count > 0)
            {
                (int subVersion, long subValue) = ReadPacket(operatorPacket);
                packetVersion += subVersion;
                valueArray.Add(subValue);
            }
        }
        else
        {
            int subPacketCount = ReadValue(subPacket, 11);
            while (subPacketCount-- > 0)
            {
                (int subVersion, long subValue) = ReadPacket(subPacket);
                packetVersion += subVersion;
                valueArray.Add(subValue);
            }
        }
        if (packetType == 0)
        {
            value = valueArray.Sum();
        }
        if (packetType == 1)
        {
            value = valueArray.Aggregate((a, b) => a * b);
        }
        if (packetType == 2)
        {
            value = valueArray.Min();
        }
        if (packetType == 3)
        {
            value = valueArray.Max();
        }
        if (packetType == 5)
        {
            value = valueArray[0] > valueArray[1] ? 1 : 0;
        }
        if (packetType == 6)
        {
            value = valueArray[0] < valueArray[1] ? 1 : 0;
        }
        if (packetType == 7)
        {
            value = valueArray[0] == valueArray[1] ? 1 : 0;
        }
    }
    return (packetVersion, value);
}

(int versionTotal, long valueTotal) = ReadPacket(packet);

Console.WriteLine($"Puzzle 1 answer : {versionTotal}");
Console.WriteLine($"Puzzle 2 answer : {valueTotal}");