int previousMeasurement = int.MaxValue;

int numberOfIncreases1 = 0;
int numberOfIncreases2 = 0;

int[] values = File.ReadAllLines("day1.txt").Select(line => Convert.ToInt32(line)).ToArray();

// Calculate number of times each measurement increases.
foreach (int measurement in values)
{
    if (measurement > previousMeasurement)
    {
        ++numberOfIncreases1;
    }
    previousMeasurement = measurement;
}

// Sliding window approach
int index = 0;
int value1 = values[index++];
int value2 = values[index++];

previousMeasurement = int.MaxValue;

while (index < values.Length)
{
    int value3 = values[index++];
    int measurement = value1 + value2 + value3;
    if (measurement > previousMeasurement)
    {
        ++numberOfIncreases2;
    }
    previousMeasurement = measurement;
    value1 = value2;
    value2 = value3;
}

Console.WriteLine($"Puzzle 1 answer : Number of increases = {numberOfIncreases1}");
Console.WriteLine($"Puzzle 2 answer : Number of increases using sliding window = {numberOfIncreases2}");
