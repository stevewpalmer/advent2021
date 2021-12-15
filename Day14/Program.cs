string[] lines = File.ReadAllLines("day14.txt");

string template = lines[0];

Dictionary<string, char> rules = new();
for (int index = 2; index < lines.Length; index++)
{
    string [] ruleParts = lines[index].Split("->", StringSplitOptions.TrimEntries);
    rules[ruleParts[0]] = ruleParts[1][0];
}

Dictionary<string, long> counts = new();
long[] letterArray = new long[26];

long CountLetterArray() =>
    letterArray.OrderByDescending(c => c).First() -
    letterArray.OrderBy(c => c).SkipWhile(c => c <= 0).First();

for (int c = 0; c < template.Length - 1; c++)
{
    string pair = template.Substring(c, 2);
    if (!counts.ContainsKey(pair))
    {
        counts[pair] = 0;
    }
    ++counts[pair];
    ++letterArray[template[c] - 'A'];
}
++letterArray[template[^1] - 'A'];

for (int steps = 1; steps <= 40; steps++)
{
    Dictionary<string, long> newCounts = new(counts);

    foreach (string pair in newCounts.Keys)
    {
        long total = newCounts[pair];

        counts[pair] -= total;
        string newPairLeft = pair[0] + rules[pair].ToString();
        if (!counts.ContainsKey(newPairLeft))
        {
            counts[newPairLeft] = 0;
        }
        counts[newPairLeft] += total;
        string newPairRight = rules[pair].ToString() + pair[1];
        if (!counts.ContainsKey(newPairRight))
        {
            counts[newPairRight] = 0;
        }
        counts[newPairRight] += total;

        letterArray[rules[pair] - 'A'] += total;
    }

    if (steps == 10)
    {
        Console.WriteLine($"Puzzle 1 answer : {CountLetterArray()}");
    }

    if (steps == 40)
    {
        Console.WriteLine($"Puzzle 2 answer : {CountLetterArray()}");
    }
}
