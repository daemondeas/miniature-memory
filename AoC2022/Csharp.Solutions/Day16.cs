namespace Csharp.Solutions;

public static class Day16
{
    private static readonly Dictionary<(string, string), int> ShortestPaths = new();

    private static readonly List<Valve> ClosedValves = new();

    public static int FirstPuzzle(List<Valve> input)
    {
        ClosedValves.AddRange(input.Where(v => v.Pressure > 0));
        PopulatePaths(input);
        // var result = 0;
        // var valvesOfInterest = input.Where(v => v.Pressure > 0);
        // foreach (var valveOfInterest in valvesOfInterest)
        // {
        //     var remainingTime = 30;
        //     var currentValve = input.Single(v => v.Name == "AA");
        //     while (ClosedValves.Any() && remainingTime > 0)
        //     {
        //         var (nextValve, totalPressure) = ChooseNextValve(currentValve, remainingTime);
        //         if (ShortestPaths[(currentValve.Name, nextValve.Name)] + 1 <= remainingTime)
        //         {
        //             result += totalPressure;
        //             remainingTime -= ShortestPaths[(currentValve.Name, nextValve.Name)] + 1;
        //             currentValve = nextValve;
        //         }
        //     
        //         ClosedValves.Remove(nextValve);
        //     }
        // }

        return GetBestPressure(30, input.First(v => v.Name == "AA"), 0, ClosedValves);
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

    private static (Valve, int) ChooseNextValve(Valve current, int timeLeft) =>
        ClosedValves.Select(v => (v, (timeLeft - ShortestPaths[(current.Name, v.Name)] - 1) * v.Pressure))
            .OrderByDescending(v => v.Item2).FirstOrDefault();
}

public class Valve
{
    public string Name { get; init; }
    
    public int Pressure { get; init; }

    public List<Valve> LeadsTo { get; } = new();
}