string[] data = File.ReadAllLines("day4.txt");

int[] numbers = data[0].Split(',').Select(d => Convert.ToInt32(d)).ToArray();
List<int[,]> boards = new();
List<bool> bingo = new();

for (int c = 2; c < data.Length; c++)
{
    int [,] board = new int [5, 5];
    while (c < data.Length && data[c] == "")
    {
        c++;
    }
    for (int m = 0; m < 5; m++)
    {
        int[] row = data[c++].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(d => Convert.ToInt32(d)).ToArray();
        for (int p = 0; p < 5; p++)
        {
            board[m, p] = row[p];
        }
    }
    boards.Add(board);
    bingo.Add(false);
}

int lastWin = -1;
int firstWin = -1;
foreach (int number in numbers)
{
    for (int b = 0; b < boards.Count; b++)
    {
        if (bingo[b])
        {
            continue;
        }
        int[,] board = boards[b];
        bool win = false;
        int sum = 0;

        for (int r = 0; r < 5; r++)
        {
            bool winColumn = true;
            for (int c = 0; c < 5; c++)
            {
                if (board[r, c] < 0x80 && board[r, c] != number)
                {
                    winColumn = false;
                }
            }
            if (winColumn)
            {
                win = true;
            }
        }
        for (int c = 0; c < 5; c++)
        {
            bool winRow = true;
            for (int r = 0; r < 5; r++)
            {
                if (board[r, c] < 0x80)
                {
                    if (board[r, c] == number)
                    {
                        board[r, c] = board[r, c] | 0x80;
                    }
                    else
                    {
                        sum += board[r, c];
                        winRow = false;
                    }
                }
            }
            if (winRow)
            {
                win = true;
            }
        }
        if (win)
        {
            if (firstWin < 0)
            {
                firstWin = sum * number;
            }
            lastWin = sum * number;
            bingo[b] = true;
        }
    }
}

Console.WriteLine($"Puzzle 1 answer : {firstWin}");
Console.WriteLine($"Puzzle 2 answer : {lastWin}");