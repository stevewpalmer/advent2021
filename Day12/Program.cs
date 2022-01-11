string[] lines = File.ReadAllLines("day12.txt");

Dictionary<string, List<string>> graph = new();
List<string> listOfVisits = new();

bool SmallCave(string s) => s == s.ToLower();
bool StartCave(string s) => s == "start";
bool EndCave(string s) => s == "end";

foreach (string line in lines)
{
    string[] parts = line.Split('-');
    if (parts[0] != "end")
    {
        if (!graph.ContainsKey(parts[0]))
        {
            graph.Add(parts[0], new List<string>() { parts[1] });
        }
        else
        {
            graph[parts[0]].Add(parts[1]);
        }
    }
    if (parts[1] != "end")
    {
        if (!graph.ContainsKey(parts[1]))
        {
            graph.Add(parts[1], new List<string>() { parts[0] });
        }
        else
        {
            graph[parts[1]].Add(parts[0]);
        }
    }
}

VisitCaves("start", new List<string>(), false);
int smallCavesVisitedOnce = listOfVisits.Distinct().ToList().Count;

VisitCaves("start", new List<string>(), true);
int smallCavesVisitedTwice = listOfVisits.Distinct().ToList().Count;

Console.WriteLine($"Puzzle 1 answer : {smallCavesVisitedOnce}");
Console.WriteLine($"Puzzle 2 answer : {smallCavesVisitedTwice}");

void VisitCaves(string name, List<string> cavesVisited, bool visitTwice)
{
    if (EndCave(name))
    {
        listOfVisits.Add(string.Join(",", cavesVisited));
        return;
    }

    cavesVisited.Add(name);

    List<string> nextCaves;
    if ((cavesVisited.Any(x => SmallCave(x) && cavesVisited.Count(y => y == x) == 2)) || !visitTwice)
    {
        nextCaves = graph[name].Where(cave => !SmallCave(cave) || EndCave(cave) || !cavesVisited.Contains(cave)).ToList();
    }
    else
    {
        nextCaves = graph[name].Where(cave => !StartCave(cave)).ToList();
    }
    foreach (string nextCave in nextCaves)
    {
        VisitCaves(nextCave, cavesVisited.ToList(), visitTwice);
    }
}
