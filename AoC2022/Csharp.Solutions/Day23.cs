namespace Csharp.Solutions;

public static class Day23
{
    public static int FirstPuzzle(List<Elf> elves)
    {
        var positions = MoveElves(elves, 10).Select(e => e.Position).ToList();
        var xs = positions.Select(p => p.x).ToList();
        var ys = positions.Select(p => p.y).ToList();
        return (xs.Max() - xs.Min() + 1) * (ys.Max() - ys.Min() + 1) - elves.Count;
    }

    public static int SecondPuzzle(List<Elf> elves) => MoveElvesUntilStationary(elves);

    private static List<Elf> MoveElves(List<Elf> elves, int rounds)
    {
        var movementOrders = new[]
        {
            new[] { Direction.North, Direction.South, Direction.West, Direction.East },
            new[] { Direction.South, Direction.West, Direction.East, Direction.North },
            new[] { Direction.West, Direction.East, Direction.North, Direction.South },
            new[] { Direction.East, Direction.North, Direction.South, Direction.West }
        };

        var resultingElves = new List<Elf>(elves.Select(e => new Elf(e)));
        for (var i = 0; i < rounds; i++)
        {
            var beforeMoving = new List<Elf>(resultingElves.Select(e => new Elf(e)));
            resultingElves.Clear();
            var currentPositions = beforeMoving.Select(e => e.Position).ToHashSet();
            foreach (var elf in beforeMoving)
            {
                elf.WhatToVisit(movementOrders[i % 4], currentPositions);
            }
            
            resultingElves.AddRange(beforeMoving.Where(e => !e.WantsToVisit.HasValue).Select(e => new Elf(e)));
            var groupedByMovementWish =
                beforeMoving.Where(e => e.WantsToVisit.HasValue).GroupBy(e => e.WantsToVisit.Value);
            resultingElves.AddRange(
                groupedByMovementWish.Where(g => g.Count() > 1).SelectMany(g => g.Select(e => new Elf(e))));
            resultingElves.AddRange(
                groupedByMovementWish.Where(g => g.Count() == 1).Select(g => new Elf(g.Single().WantsToVisit.Value)));
        }

        return resultingElves;
    }
    
    private static int MoveElvesUntilStationary(List<Elf> elves)
    {
        var movementOrders = new[]
        {
            new[] { Direction.North, Direction.South, Direction.West, Direction.East },
            new[] { Direction.South, Direction.West, Direction.East, Direction.North },
            new[] { Direction.West, Direction.East, Direction.North, Direction.South },
            new[] { Direction.East, Direction.North, Direction.South, Direction.West }
        };

        var resultingElves = new List<Elf>(elves.Select(e => new Elf(e)));
        var i = 0;
        var finished = false;
        while (!finished)
        {
            i++;
            var beforeMoving = new List<Elf>(resultingElves.Select(e => new Elf(e)));
            var currentPositions = beforeMoving.Select(e => e.Position).ToHashSet();
            resultingElves.Clear();
            foreach (var elf in beforeMoving)
            {
                elf.WhatToVisit(movementOrders[i % 4], currentPositions);
            }

            resultingElves.AddRange(beforeMoving.Where(e => !e.WantsToVisit.HasValue).Select(e => new Elf(e)));
            var groupedByMovementWish =
                beforeMoving.Where(e => e.WantsToVisit.HasValue).GroupBy(e => e.WantsToVisit.Value);
            resultingElves.AddRange(
                groupedByMovementWish.Where(g => g.Count() > 1).SelectMany(g => g.Select(e => new Elf(e))));
            var movers = groupedByMovementWish.Where(g => g.Count() == 1)
                .Select(g => new Elf(g.Single().WantsToVisit.Value));
            resultingElves.AddRange(movers);
            
            finished = !movers.Any();
        }

        return i + 1;
    }
}

public class Elf
{
    public (int x, int y) Position { get; }
    public (int, int)? WantsToVisit { get; private set; }

    public Elf((int, int) position)
    {
        Position = position;
    }

    public Elf(Elf elf) : this(elf.Position)
    {
    }

    public void WhatToVisit(IEnumerable<Direction> directionOrder, HashSet<(int, int)> occupiedPositions)
    {
        if (!DirectionHelper.GetAdjacent(Position).Any(occupiedPositions.Contains))
        {
            WantsToVisit = null;
            return;
        }

        foreach (var direction in directionOrder)
        {
            if (DirectionHelper.GetAdjacent(Position, direction).Any(occupiedPositions.Contains)) continue;
            WishToMove(direction);
            return;
        }

        WantsToVisit = null;
    }

    private void WishToMove(Direction direction) =>
        WantsToVisit = direction switch
        {
            Direction.North => Position with { y = Position.y - 1 },
            Direction.South => Position with { y = Position.y + 1 },
            Direction.West  => Position with { x = Position.x - 1 },
            Direction.East  => Position with { x = Position.x + 1 }
        };
}

public static class DirectionHelper
{
    public static IEnumerable<(int, int)> GetAdjacent((int x, int y) position) =>
        new[]
        {
            (position.x - 1, position.y - 1),
            (position.x, position.y - 1),
            (position.x + 1, position.y - 1),
            (position.x - 1, position.y),
            (position.x + 1, position.y),
            (position.x - 1, position.y + 1),
            (position.x, position.y + 1),
            (position.x + 1, position.y + 1)
        };

    public static IEnumerable<(int, int)> GetAdjacent((int x, int y) position, Direction direction) =>
        direction switch
        {
            Direction.North =>
                new[]
                {
                    (position.x - 1, position.y - 1),
                    (position.x, position.y - 1),
                    (position.x + 1, position.y - 1)
                },
            Direction.East =>
                new[]
                {
                    (position.x + 1, position.y - 1),
                    (position.x + 1, position.y),
                    (position.x + 1, position.y + 1)
                },
            Direction.South =>
                new[]
                {
                    (position.x - 1, position.y + 1),
                    (position.x, position.y + 1),
                    (position.x + 1, position.y + 1)
                },
            Direction.West =>
                new[]
                {
                    (position.x - 1, position.y - 1),
                    (position.x - 1, position.y),
                    (position.x - 1, position.y + 1)
                }
        };
}

public enum Direction
{
    North,
    South,
    West,
    East
}