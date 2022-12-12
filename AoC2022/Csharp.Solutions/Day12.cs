namespace Csharp.Solutions;

public static class Day12
{
    private static readonly Position NotSetPosition = new() { X = -1, Y = -1 };

    private static readonly List<List<Position>> PossiblePaths = new();
    private static readonly Dictionary<Position, int> VisitedLength = new();

    public static long FirstPuzzle(char[][] map)
    {
        var (start, end) = GetStartingAndEndingPosition(map);
        FindPaths(start, end, map, new List<Position>());
        return PossiblePaths.Min(p => p.Count) - 1;
    }
    
    public static long SecondPuzzle(char[][] map)
    {
        var (_, end) = GetStartingAndEndingPosition(map);
        var starts = GetPossibleStarts(map);
        foreach (var start in starts)
        {
            VisitedLength.Add(start, 0);
        }
        
        foreach (var start in starts)
        {
            VisitedLength.Remove(start);
            FindPaths(start, end, map, new List<Position>());
        }
        
        return PossiblePaths.Min(p => p.Count) - 1;
    }

    
    private static (Position start, Position end) GetStartingAndEndingPosition(char[][] map)
    {
        var start = NotSetPosition;
        var end = NotSetPosition;

        for (var i = 0; i < map.Length; i++)
        {
            for (var j = 0; j < map[0].Length; j++)
            {
                switch (map[i][j])
                {
                    case 'S':
                        start = new Position { X = j, Y = i };
                        break;
                    case 'E':
                        end = new Position { X = j, Y = i };
                        break;
                }
            }
        }

        return (start, end);
    }

    private static List<Position> GetPossibleStarts(char[][] map)
    {
        var result = new List<Position>();
        for (var i = 0; i < map.Length; i++)
        {
            for (var j = 0; j < map[0].Length; j++)
            {
                if (map[i][j] == 'a')
                {
                    result.Add(new Position { X = j, Y = i });
                }
            }
        }

        return result;
    }

    private static void FindPaths(Position start, Position end, char[][] map, List<Position> path)
    {
        if (start == end)
        {
            path.Add(start);
            PossiblePaths.Add(path);
            return;
        }

        if (path.Contains(start))
        {
            return;
        }

        if (VisitedLength.TryGetValue(start, out var length))
        {
            if (length <= path.Count)
            {
                return;
            }

            VisitedLength[start] = path.Count;
        }
        else
        {
            VisitedLength.Add(start, path.Count);
        }

        var nextPositions = GetPossibleNextPositions(start, map);
        if (nextPositions.Count == 0)
        {
            return;
        }
        
        path.Add(start);
        foreach (var nextPosition in nextPositions)
        {
            FindPaths(nextPosition, end, map, new List<Position>(path));
        }
    }

    private static List<Position> GetPossibleNextPositions(Position current, char[][] map) =>
        new List<Position>
            {
                current with { X = current.X - 1 },
                current with { X = current.X + 1 },
                current with { Y = current.Y - 1 },
                current with { Y = current.Y + 1 },
            }.Where(p => IsValidNextStep(current, p, map))
            .ToList();

    private static bool IsValidNextStep(Position current, Position next, char[][] map) =>
        next.X >= 0
        && next.X < map[0].Length
        && next.Y >= 0
        && next.Y < map.Length
        && map[next.Y][next.X].GetHeight() - map[current.Y][current.X].GetHeight() < 2;

    private static char GetHeight(this char position) =>
        position switch
        {
            'S' => 'a',
            'E' => 'z',
            _   => position
        };
}

public record struct Position
{
    public int X;
    public int Y;
}