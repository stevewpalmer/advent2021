using System.Text.RegularExpressions;

string [] input = File.ReadAllLines("day17.txt");
Match matches = Regex.Match(input[0], @"target area: x=(-?\d+)..(-?\d+), y=(-?\d+)..(-?\d+)");
int x1, y1, x2, y2;

(x1, x2, y1, y2) = (Convert.ToInt32(matches.Groups[1].Value), Convert.ToInt32(matches.Groups[2].Value), Convert.ToInt32(matches.Groups[3].Value), Convert.ToInt32(matches.Groups[4].Value));

int highestYPosition = (y1 * (y1 + 1)) / 2;

int targetVelocities = 0;
for (int x = 0; x <= x2; x++)
{
    for (int y = y1; y < -y1; y++)
    {
        int xVelocity = x;
        int yVelocity = y;

        int tryX = 0;
        int tryY = 0;

        while (tryX <= x2 && tryY >= y1)
        {
            tryX += xVelocity;
            tryY += yVelocity;
            yVelocity--;
            if (xVelocity > 0)
            {
                --xVelocity;
            }
            if (tryX >= x1 && tryX <= x2 && tryY >= y1 && tryY <= y2)
            {
                ++targetVelocities;
                break;
            }
        }
    }
}

Console.WriteLine($"Puzzle 1 answer : {highestYPosition}");
Console.WriteLine($"Puzzle 2 answer : {targetVelocities}");
