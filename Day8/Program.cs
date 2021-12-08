string [] inputs = File.ReadAllLines("day8.txt");

int countOfUniqueSegments = 0;
int totalSum = 0;

static string SortString(string s1) => new(s1.ToCharArray().OrderBy(p => p).ToArray());

static string CommonString(string s1, string s2) => new(s1.ToCharArray().Intersect(s2.ToCharArray()).ToArray());

static string DiffString(string s1, string s2) => new(s1.ToCharArray().Except(s2.ToCharArray()).ToArray());

static bool ContainsString(string s1, string s2) => CommonString(s1, s2) == s2;

foreach (string input in inputs)
{
    string [] parts = input.Split('|');
    IEnumerable<string> signals = parts[0].Trim().Split(' ').Select(p => SortString(p));
    IEnumerable<string> outputs = parts[1].Trim().Split(' ').Select(p => SortString(p));

    countOfUniqueSegments += outputs.Where(s => s.Length == 2 || s.Length == 4 || s.Length == 3 || s.Length == 7).Count();

    string[] segments = new string[10];

    segments[1] = signals.First(s => s.Length == 2);
    segments[4] = signals.First(s => s.Length == 4);
    segments[7] = signals.First(s => s.Length == 3);
    segments[8] = signals.First(s => s.Length == 7);

    segments[3] = signals.First(s => s.Length == 5 && ContainsString(s, segments[7]));
    segments[9] = signals.First(s => s.Length == 6 && ContainsString(s, segments[4]));
    segments[0] = signals.First(s => s.Length == 6 && ContainsString(s, segments[1]) && s != segments[9]);

    string inter1 = DiffString(segments[4], segments[1]);

    segments[5] = signals.First(s => s.Length == 5 && ContainsString(s, inter1));
    segments[6] = signals.First(s => s.Length == 6 && ContainsString(s, inter1) && s != segments[9]);

    segments[2] = signals.First(s => s.Length == 5 && !segments.Contains(s));

    int outputSum = 0;
    foreach (string output in outputs)
    {
        outputSum = outputSum * 10 + Array.IndexOf(segments, output);
    }
    totalSum += outputSum;
}
Console.WriteLine($"Puzzle 1 answer : {countOfUniqueSegments}");
Console.WriteLine($"Puzzle 2 answer : {totalSum}");
