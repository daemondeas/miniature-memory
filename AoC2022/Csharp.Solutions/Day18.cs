namespace Csharp.Solutions;

public static class Day18
{
    public static int FirstPuzzle(List<(int, int, int)> cubes) => GetCubesDictionary(cubes).Values.Sum();

    public static int SecondPuzzle(List<(int, int, int)> cubes)
    {
        var cubeField = GetCubesDictionary(cubes);
        var xs = cubes.Select(c => c.Item1).ToList();
        var ys = cubes.Select(c => c.Item2).ToList();
        var zs = cubes.Select(c => c.Item3).ToList();

        var minX = xs.Min();
        var maxX = xs.Max();

        var minY = ys.Min();
        var maxY = ys.Max();

        var minZ = zs.Min();
        var maxZ = zs.Max();
        
        var emptyConnectingToOutside = new HashSet<(int, int, int)>();
        var emptyAndTrapped = new HashSet<(int, int, int)>();

        var fieldAsSet = cubeField.Keys.ToHashSet();

        for (var x = minX; x <= maxX; x++)
        {
            for (var y = minY; y <= maxY; y++)
            {
                for (var z = minZ; z <= maxZ; z++)
                {
                    if (!cubeField.ContainsKey((x, y, z)) && GetAdjacent((x, y, z)).All(c => cubeField.ContainsKey(c))
                        && !emptyConnectingToOutside.Contains((x, y, x)) && !emptyAndTrapped.Contains((x, y, x)))
                    {
                        var pocket = GetPocket((x, y, z), new HashSet<(int, int, int)>(), minX, maxX, minY, maxY, minZ, maxZ, fieldAsSet);
                        if (pocket.connectsToOutside)
                        {
                            foreach (var innerCube in pocket.entails)
                            {
                                emptyConnectingToOutside.Add(innerCube);
                            }
                        }
                        else
                        {
                            foreach (var innerCube in pocket.entails)
                            {
                                emptyAndTrapped.Add(innerCube);
                            }
                        }
                    }
                }
            }
        }

        var trappedAir = emptyAndTrapped.Select(GetAdjacent).Select(sides => sides.Count(side => cubeField.ContainsKey(side))).Sum();

        return cubeField.Values.Sum() - trappedAir;
    }

    private static Dictionary<(int, int, int), int> GetCubesDictionary(List<(int, int, int)> cubes)
    {
        var cubesDictionary = new Dictionary<(int, int, int), int>(cubes.Count);
        foreach (var cube in cubes)
        {
            var freeSides = 6;
            foreach (var adjacentPosition in GetAdjacent(cube))
            {
                if (cubesDictionary.TryGetValue(adjacentPosition, out var adjacentFree))
                {
                    freeSides--;
                    cubesDictionary[adjacentPosition] = adjacentFree - 1;
                }
            }

            cubesDictionary[cube] = freeSides;
        }

        return cubesDictionary;
    }

    private static List<(int, int, int)> GetAdjacent((int x, int y, int z) cube) =>
        new()
        {
            cube with { x = cube.x - 1 },
            cube with { x = cube.x + 1 },
            cube with { y = cube.y - 1 },
            cube with { y = cube.y + 1 },
            cube with { z = cube.z - 1 },
            cube with { z = cube.z + 1 }
        };

    private static (HashSet<(int, int, int)> entails, bool connectsToOutside) GetPocket((int x, int y, int z) cube, HashSet<(int, int, int)> coordinatesSoFar, int minX, int maxX, int minY, int maxY, int minZ, int maxZ, HashSet<(int, int, int)> field)
    {
        if (coordinatesSoFar.Contains(cube) || field.Contains(cube))
        {
            return (coordinatesSoFar, false);
        }

        if (cube.x > maxX || cube.x < minX || cube.y > maxY || cube.y < minY || cube.z > maxZ || cube.z < minZ)
        {
            return (coordinatesSoFar, true);
        }

        coordinatesSoFar.Add(cube);
        var connects = false;
        foreach (var pocketContinuation in GetAdjacent(cube).Select(side => GetPocket(side, coordinatesSoFar, minX, maxX, minY, maxY, minZ, maxZ, field)))
        {
            connects |= pocketContinuation.connectsToOutside;
            coordinatesSoFar = pocketContinuation.entails;
        }

        return (coordinatesSoFar, connects);
    }
}