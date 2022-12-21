namespace Csharp.Solutions;

public static class Day16
{
    private static readonly Dictionary<(string, string), int> ShortestPaths = new();

    private static readonly List<Valve> ClosedValves = new();

    public static int FirstPuzzle(List<Valve> input)
    {
        ClosedValves.AddRange(input.Where(v => v.Pressure > 0));
        PopulatePaths(input);

        return GetBestPressure(30, input.First(v => v.Name == "AA"), 0, ClosedValves);
    }

    public static int SecondPuzzle(List<Valve> input)
    {
        ClosedValves.AddRange(input.Where(v => v.Pressure > 0));
        PopulatePaths(input);

        return GetBestPressureWithElephant(26, input.First(v => v.Name == "AA"), 0, ClosedValves, false);
    }

    private static int GetBestPressure(int remainingTime, Valve currentValve, int pressure, IReadOnlyCollection<Valve> remainingValves)
    {
        if (!remainingValves.Any() || remainingValves.All(v => ShortestPaths[(currentValve.Name, v.Name)] + 1 > remainingTime))
        {
            return pressure;
        }

        return remainingValves.Where(v => remainingTime - ShortestPaths[(currentValve.Name, v.Name)] >= 1)
            .Select(
                remainingValve => GetBestPressure(
                    remainingTime - ShortestPaths[(currentValve.Name, remainingValve.Name)] - 1,
                    remainingValve,
                    pressure + remainingValve.Pressure * (remainingTime - ShortestPaths[(currentValve.Name, remainingValve.Name)] - 1),
                    remainingValves.Where(v => v != remainingValve && remainingTime - ShortestPaths[(currentValve.Name, v.Name)] >= 1)
                        .ToList()))
            .Prepend(pressure)
            .Max();
    }
    
    private static int GetBestPressureWithElephant(int remainingTime, Valve currentValve, int pressure, IReadOnlyCollection<Valve> remainingValves, bool elephantChooses)
    {
        if (!remainingValves.Any() || remainingValves.All(v => ShortestPaths[(currentValve.Name, v.Name)] + 1 > remainingTime))
        {
            return elephantChooses
                ? pressure
                : GetBestPressureWithElephant(
                    26,
                    new Valve { Name = "AA", Pressure = 0 },
                    pressure,
                    remainingValves,
                    true);
        }

        return remainingValves.Where(v => remainingTime - ShortestPaths[(currentValve.Name, v.Name)] >= 1)
            .Select(
                remainingValve => GetBestPressureWithElephant(
                    remainingTime - ShortestPaths[(currentValve.Name, remainingValve.Name)] - 1,
                    remainingValve,
                    pressure + remainingValve.Pressure * (remainingTime - ShortestPaths[(currentValve.Name, remainingValve.Name)] - 1),
                    remainingValves.Where(v => v != remainingValve).ToList(),
                    elephantChooses))
            .Prepend(pressure)
            .Max();
    }

    private static void PopulatePaths(List<Valve> input)
    {
        foreach (var valve in input)
        {
            PopulatePathsHelper(valve, 0, valve.Name);
        }
    }

    private static void PopulatePathsHelper(Valve valve, int distance, string startingPoint)
    {
        if (ShortestPaths.TryGetValue((startingPoint, valve.Name), out var length) && length <= distance)
        {
            return;
        }
        
        ShortestPaths[(startingPoint, valve.Name)] = distance;
        foreach (var nextValve in valve.LeadsTo)
        {
            PopulatePathsHelper(nextValve, distance + 1, startingPoint);
        }
    }
}

public class Valve
{
    public string Name { get; init; }
    
    public int Pressure { get; init; }

    public List<Valve> LeadsTo { get; } = new();
}