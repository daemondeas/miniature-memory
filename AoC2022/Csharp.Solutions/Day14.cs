namespace Csharp.Solutions;

public static class Day14
{
    public static long FirstPuzzle(List<List<(int x, int y)>> paths)
    {
        var map = DrawPaths(paths);
        var highestY = GetHighestY(paths);
        return LetSandFall(map, (500, 0), highestY);
    }

    public static long SecondPuzzle(List<List<(int x, int y)>> paths)
    {
        var map = DrawPaths(paths);
        var floor = GetHighestY(paths) + 2;
        return LetSandFallWithFloor(map, (500, 0), floor);
    }

    private static long LetSandFall(Dictionary<(int, int), TileType> map, (int x, int y) sandSource, int maxY)
    {
        var amountOfSandUnits = 0;
        var currentUnit = sandSource with { y = sandSource.y + 1 };
        while (currentUnit.y < maxY)
        {
            if (GetTile(currentUnit with{y = currentUnit.y + 1}, map) == TileType.Air)
            {
                currentUnit = currentUnit with { y = currentUnit.y + 1 };
            }
            else if (GetTile((currentUnit.x - 1, currentUnit.y + 1), map) == TileType.Air)
            {
                currentUnit = (currentUnit.x - 1, currentUnit.y + 1);
            }
            else if (GetTile((currentUnit.x + 1, currentUnit.y + 1), map) == TileType.Air)
            {
                currentUnit = (currentUnit.x + 1, currentUnit.y + 1);
            }
            else
            {
                map[currentUnit] = TileType.Sand;
                currentUnit = sandSource with { y = sandSource.y + 1 };
                amountOfSandUnits++;
            }
        }

        return amountOfSandUnits;
    }
    
    private static long LetSandFallWithFloor(Dictionary<(int, int), TileType> map, (int x, int y) sandSource, int floor)
    {
        var amountOfSandUnits = 0;
        var currentUnit = sandSource;
        while (!map.TryGetValue(sandSource, out _))
        {
            if (GetTileWithFloor(currentUnit with{y = currentUnit.y + 1}, map, floor) == TileType.Air)
            {
                currentUnit = currentUnit with { y = currentUnit.y + 1 };
            }
            else if (GetTileWithFloor((currentUnit.x - 1, currentUnit.y + 1), map, floor) == TileType.Air)
            {
                currentUnit = (currentUnit.x - 1, currentUnit.y + 1);
            }
            else if (GetTileWithFloor((currentUnit.x + 1, currentUnit.y + 1), map, floor) == TileType.Air)
            {
                currentUnit = (currentUnit.x + 1, currentUnit.y + 1);
            }
            else
            {
                map[currentUnit] = TileType.Sand;
                currentUnit = sandSource;
                amountOfSandUnits++;
            }
        }

        return amountOfSandUnits;
    }

    private static Dictionary<(int, int), TileType> DrawPaths(List<List<(int x, int y)>> paths)
    {
        var result = new Dictionary<(int, int), TileType>();
        foreach (var path in paths)
        {
            var current = path.First();
            foreach (var step in path.Skip(1))
            {
                if (current.x == step.x)
                {
                    var smallest = SmallestY(current, step);
                    var largest = LargestY(current, step);
                    for (var i = smallest.y; i <= largest.y; i++)
                    {
                        result[(current.x, i)] = TileType.Rock;
                    }
                }
                else
                {
                    var smallest = SmallestX(current, step);
                    var largest = LargestX(current, step);
                    for (var i = smallest.x; i <= largest.x; i++)
                    {
                        result[(i, current.y)] = TileType.Rock;
                    }
                }

                current = step;
            }
        }

        return result;
    }

    private static TileType GetTile((int, int) position, Dictionary<(int, int), TileType> map) =>
        map.TryGetValue(position, out var tile)
            ? tile
            : TileType.Air;

    private static TileType GetTileWithFloor((int, int) position, Dictionary<(int, int), TileType> map, int floor) =>
        position.Item2 == floor
            ? TileType.Rock
            : map.TryGetValue(position, out var tile)
                ? tile
                : TileType.Air;

    private static (int x, int y) SmallestY((int x, int y) a, (int x, int y) b) =>
        int.Min(a.y, b.y) == a.y
            ? a
            : b;
    
    private static (int x, int y) LargestY((int x, int y) a, (int x, int y) b) =>
        int.Max(a.y, b.y) == a.y
            ? a
            : b;
    
    private static (int x, int y) SmallestX((int x, int y) a, (int x, int y) b) =>
        int.Min(a.x, b.x) == a.x
            ? a
            : b;
    
    private static (int x, int y) LargestX((int x, int y) a, (int x, int y) b) =>
        int.Max(a.x, b.x) == a.x
            ? a
            : b;

    private static int GetHighestY(IEnumerable<List<(int x, int y)>> paths) => paths.SelectMany(p => p).Max(p => p.y);
}

public enum TileType
{
    Air,
    Rock,
    Sand
}