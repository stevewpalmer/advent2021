string[] lines = File.ReadAllLines("day10.txt");

int totalErrorScore = 0;
List<long> autocompleteScores = new();

char[] openingTag = { '(', '[', '{', '<' };
char[] closingTag = { ')', ']', '}', '>' };
int[] errorScore = { 3, 57, 1197, 25137 };

foreach (string line in lines)
{
    Stack<char> chunkStack = new();

    foreach (char ch in line.ToCharArray())
    {
        int indexOfTag = Array.IndexOf(openingTag, ch);
        if (indexOfTag >= 0)
        {
            chunkStack.Push(closingTag[indexOfTag]);
        }
        else
        {
            indexOfTag = Array.IndexOf(closingTag, ch);
            if (indexOfTag >= 0)
            {
                if (ch != chunkStack.Pop())
                {
                    totalErrorScore += errorScore[indexOfTag];
                    chunkStack.Clear();
                    break;
                }
            }
        }
    }

    if (chunkStack.Count > 0)
    {
        long autocompleteScore = 0;
        while (chunkStack.Count != 0)
        {
            autocompleteScore = autocompleteScore * 5 + (Array.IndexOf(closingTag, chunkStack.Pop()) + 1);
        }
        autocompleteScores.Add(autocompleteScore);
    }
}

long totalAutocompleteScore = autocompleteScores.OrderBy(a => a).ElementAt(autocompleteScores.Count / 2);

Console.WriteLine($"Puzzle 1 answer : {totalErrorScore}");
Console.WriteLine($"Puzzle 2 answer : {totalAutocompleteScore}");