namespace Csharp.Solutions;

public static class Day19
{
    public static int FirstPuzzle(List<Blueprint> blueprints) => blueprints.Sum(b => b.Id * OptimizeForGeodes(b, 24));

    public static int SecondPuzzle(List<Blueprint> blueprints) =>
        blueprints.Take(3).Select(b => OptimizeForGeodes(b, 32)).Aggregate((a, b) => a * b);

    private static int OptimizeForGeodes(Blueprint blueprint, int time) => BestAmountOfGeodesAfter((0, 0, 0, 0), time, (1, 0, 0, 0), blueprint, 0, new List<Resource>());

    private static int BestAmountOfGeodesAfter((int ore, int clay, int obsidian, int geodes) resourceAmounts, int minutesLeft, (int ore, int clay, int obsidian, int geodes) robots, Blueprint blueprint, int noneBuilt, List<Resource> didntBuild)
    {
        if (minutesLeft == 0)
        {
            return resourceAmounts.geodes;
        }

        var possibleNewRobots = PossibleRobotsToBuild(resourceAmounts, blueprint, robots, minutesLeft);
        resourceAmounts.ore += robots.ore;
        resourceAmounts.clay += robots.clay;
        resourceAmounts.obsidian += robots.obsidian;
        resourceAmounts.geodes += robots.geodes;

        var paths = new List<int>(possibleNewRobots.Count + 1);
        foreach (var robot in possibleNewRobots.Except(didntBuild))
        {
            var newState = BuildRobot(resourceAmounts, robots, robot, blueprint);
            paths.Add(BestAmountOfGeodesAfter(newState.resources, minutesLeft - 1, newState.robs, blueprint, 0, new List<Resource>()));
        }
        
        paths.Add(BestAmountOfGeodesAfter(resourceAmounts, minutesLeft - 1, robots, blueprint, noneBuilt + 1, didntBuild.Concat(possibleNewRobots).Distinct().ToList()));
        
        return paths.Max();
    }

    private static ((int, int, int, int) resources, (int, int, int, int) robs) BuildRobot(
        (int ore, int clay, int obsidian, int geodes) resourceAmounts,
        (int ore, int clay, int obsidian, int geodes) robots,
        Resource robot,
        Blueprint blueprint) =>
        robot switch
        {
            Resource.Ore => (resourceAmounts with { ore = resourceAmounts.ore - blueprint.OreRobotCost },
                robots with { ore = robots.ore + 1 }),
            Resource.Clay => (resourceAmounts with { ore = resourceAmounts.ore - blueprint.ClayRobotCost },
                robots with { clay = robots.clay + 1 }),
            Resource.Obsidian => (
                resourceAmounts with
                {
                    ore = resourceAmounts.ore - blueprint.ObsidianRobotOreCost,
                    clay = resourceAmounts.clay - blueprint.ObsidianRobotClayCost
                },
                robots with { obsidian = robots.obsidian + 1 }),
            Resource.Geode => (
                resourceAmounts with
                {
                    ore = resourceAmounts.ore - blueprint.GeodeRobotOreCost,
                    obsidian = resourceAmounts.obsidian - blueprint.GeodeRobotObsidianCost
                },
                robots with { geodes = robots.geodes + 1 })
        };
    
    private static List<Resource> PossibleRobotsToBuild((int ore, int clay, int obsidian, int geodes) resourceAmounts, Blueprint blueprint, (int ore, int clay, int obsidian, int geodes) robots, int timeLeft)
    {
        var result = new List<Resource>();
        
        var maxOreNeed = new[]
        {
            blueprint.OreRobotCost, blueprint.ClayRobotCost, blueprint.ObsidianRobotOreCost, blueprint.GeodeRobotOreCost
        }.Max();
        if (resourceAmounts.ore >= blueprint.OreRobotCost && robots.ore < maxOreNeed && resourceAmounts.ore + robots.ore * timeLeft < timeLeft * maxOreNeed)
        {
            result.Add(Resource.Ore);
        }

        if (resourceAmounts.ore >= blueprint.ClayRobotCost && robots.clay < blueprint.ObsidianRobotClayCost && resourceAmounts.clay + robots.clay * timeLeft < blueprint.ObsidianRobotClayCost * timeLeft)
        {
            result.Add(Resource.Clay);
        }

        if (resourceAmounts.ore >= blueprint.ObsidianRobotOreCost &&
            resourceAmounts.clay >= blueprint.ObsidianRobotClayCost &&
            robots.obsidian < blueprint.GeodeRobotObsidianCost &&
            resourceAmounts.obsidian + robots.obsidian * timeLeft < blueprint.GeodeRobotObsidianCost * timeLeft)
        {
            result.Add(Resource.Obsidian);
        }
        
        if (resourceAmounts.ore >= blueprint.GeodeRobotOreCost &&
            resourceAmounts.obsidian >= blueprint.GeodeRobotObsidianCost)
        {
            result.Add(Resource.Geode);
        }

        return result;
    }

    private static void PrintDict((int ore, int clay, int obsidian, int geodes) dict, string heading) =>
        Console.WriteLine($"{heading} - Ore: {dict.ore}, Clay: {dict.clay}, Obsidian: {dict.obsidian}, Geode: {dict.geodes}");
}

public enum Resource
{
    Ore,
    Clay,
    Obsidian,
    Geode
}

public record struct Blueprint
{
    public int Id { get; init; }
    public int OreRobotCost { get; init; }
    public int ClayRobotCost { get; init; }
    public int ObsidianRobotOreCost { get; init; }
    public int ObsidianRobotClayCost { get; init; }
    public int GeodeRobotOreCost { get; init; }
    public int GeodeRobotObsidianCost { get; init; }
}