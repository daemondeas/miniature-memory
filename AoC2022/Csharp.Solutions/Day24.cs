namespace Csharp.Solutions;

public static class Day24
{
    private static int? _bestSoFar;
    private static HashSet<(int, int, int)> _visited = new();

    public static int FirstPuzzle(
        (IEnumerable<Blizzard> blizzards, IReadOnlySet<(int, int)> walls, (int, int) goal) input)
    {
        FindShortestPath((1, 0), input.blizzards, input.walls, input.goal, 0, input.goal.Item2);
        return _bestSoFar.Value;
    }

    public static int SecondPuzzle(
        (IEnumerable<Blizzard> blizzards, IReadOnlySet<(int, int)> walls, (int, int) goal) input)
    {
        FindShortestPath(input.goal, MoveBlizzards(input.blizzards, 228), input.walls, (1, 0), 0, input.goal.Item2);
        var back = _bestSoFar.Value;
        Console.WriteLine($"Back at {back}");
        _bestSoFar = null;
        _visited.Clear();
        FindShortestPath((1, 0), MoveBlizzards(input.blizzards, 228 + back), input.walls, input.goal, 0, input.goal.Item2);
        return _bestSoFar.Value + back + 228;
    }

    private static void FindShortestPath((int x, int y) position, IEnumerable<Blizzard> blizzards, IReadOnlySet<(int, int)> walls, (int, int) goal, int time, int bottom)
    {
        if (time > 400)
        {
            return;
        }
        
        if (_visited.Contains((position.x, position.y, time)))
        {
            return;
        }

        _visited.Add((position.x, position.y, time));
        
        if (_bestSoFar.HasValue && _bestSoFar.Value < time)
        {
            return;
        }
        
        if (position == goal)
        {
            _bestSoFar = time;
            return;
        }

        var newBlizzards = blizzards.Select(b => b.Move()).ToList();
        var positions = newBlizzards.Select(b => b.Position).ToHashSet();
        foreach (var adjacentPosition in GetAdjacent(position).Where(p => p.Item2 >= 0 && p.Item2 <= bottom && !walls.Contains(p) && !positions.Contains(p)))
        {
            FindShortestPath(adjacentPosition, newBlizzards, walls, goal, time + 1, bottom);
        }

        if (!positions.Contains(position))
        {
            FindShortestPath(position, newBlizzards, walls, goal, time + 1, bottom);
        }
    }

    private static IEnumerable<Blizzard> MoveBlizzards(IEnumerable<Blizzard> blizzards, int times) =>
        times switch
        {
            0 => blizzards,
            _ => MoveBlizzards(blizzards.Select(b => b.Move()), times - 1)
        };

    private static IEnumerable<(int, int)> GetAdjacent((int x, int y) position) =>
        new[]
        {
            position with { x = position.x - 1 },
            position with { x = position.x + 1 },
            position with { y = position.y - 1 },
            position with { y = position.y + 1 }
        };
}

public class Blizzard
{
    private readonly int _min;
    private readonly int _max;
    public (int x, int y) Position { get; }
    private Direction Direction { get; }

    public Blizzard((int, int) position, Direction direction, int min, int max)
    {
        _min = min;
        _max = max;
        Position = position;
        Direction = direction;
    }

    public Blizzard Move()
    {
        switch (Direction)
        {
            case Direction.North:
                var newY = Position.y == _min ? _max : Position.y - 1;
                return new Blizzard((Position.x, newY), Direction, _min, _max);
            case Direction.South:
                newY = Position.y == _max ? _min : Position.y + 1;
                return new Blizzard((Position.x, newY), Direction, _min, _max);
            case Direction.West:
                var newX = Position.x == _min ? _max : Position.x - 1;
                return new Blizzard((newX, Position.y), Direction, _min, _max);
            case Direction.East:
                newX = Position.x == _max ? _min : Position.x + 1;
                return new Blizzard((newX, Position.y), Direction, _min, _max);
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}