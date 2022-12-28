namespace Csharp.Solutions;

public static class Day21
{
    private static readonly Dictionary<string, long> ResultDictionary = new();
    private static readonly Dictionary<string, YellingMonkey> MonkeyDictionary = new();

    public static long FirstPuzzle(List<YellingMonkey> input)
    {
        Populate(input);
        return Evaluate("root");
    }

    public static long SecondPuzzle(List<YellingMonkey> input)
    {
        Populate(input);
        return EvaluateRootAsEquality();
    }

    private static long EvaluateRootAsEquality()
    {
        var root = MonkeyDictionary["root"];
        var leftMonkey = MonkeyDictionary[root.LeftOperand];
        var rightMonkey = MonkeyDictionary[root.RightOperand];
        if (ContainsHuman(leftMonkey))
        {
            return GetValue(leftMonkey, Evaluate(rightMonkey.Name));
        }

        return GetValue(rightMonkey, Evaluate(leftMonkey.Name));
    }
    
    private static long Evaluate(string nameOfMonkey)
    {
        if (ResultDictionary.TryGetValue(nameOfMonkey, out var value))
        {
            return value;
        }

        var monkey = MonkeyDictionary[nameOfMonkey];
        return monkey.Operation switch
        {
            MonkeyOperation.Add      => Evaluate(monkey.LeftOperand) + Evaluate(monkey.RightOperand),
            MonkeyOperation.Subtract => Evaluate(monkey.LeftOperand) - Evaluate(monkey.RightOperand),
            MonkeyOperation.Multiply => Evaluate(monkey.LeftOperand) * Evaluate(monkey.RightOperand),
            MonkeyOperation.Divide   => Evaluate(monkey.LeftOperand) / Evaluate(monkey.RightOperand),
            MonkeyOperation.Shout    => monkey.YellingNumber.Value
        };
    }

    private static long GetValue(YellingMonkey monkey, long shouldBe)
    {
        if (monkey.Name == "humn")
        {
            return shouldBe;
        }

        var leftMonkey = MonkeyDictionary[monkey.LeftOperand];
        var rightMonkey = MonkeyDictionary[monkey.RightOperand];

        if (ContainsHuman(leftMonkey))
        {
            return monkey.Operation switch
            {
                MonkeyOperation.Add      => GetValue(leftMonkey, shouldBe - Evaluate(rightMonkey.Name)),
                MonkeyOperation.Subtract => GetValue(leftMonkey, shouldBe + Evaluate(rightMonkey.Name)),
                MonkeyOperation.Multiply => GetValue(leftMonkey, shouldBe / Evaluate(rightMonkey.Name)),
                MonkeyOperation.Divide   => GetValue(leftMonkey, shouldBe * Evaluate(rightMonkey.Name))
            };
        }

        return monkey.Operation switch
        {
            MonkeyOperation.Add      => GetValue(rightMonkey, shouldBe - Evaluate(leftMonkey.Name)),
            MonkeyOperation.Subtract => GetValue(rightMonkey, Evaluate(leftMonkey.Name) - shouldBe),
            MonkeyOperation.Multiply => GetValue(rightMonkey, shouldBe / Evaluate(leftMonkey.Name)),
            MonkeyOperation.Divide   => GetValue(rightMonkey, Evaluate(leftMonkey.Name) / shouldBe)
        };
    }

    private static bool ContainsHuman(YellingMonkey monkey) =>
        monkey.Name == "humn"
        || (monkey.Operation == MonkeyOperation.Shout
            ? false
            : ContainsHuman(MonkeyDictionary[monkey.LeftOperand])
              || ContainsHuman(MonkeyDictionary[monkey.RightOperand]));

    private static void Populate(List<YellingMonkey> monkeys)
    {
        foreach (var monkey in monkeys)
        {
            if (monkey.Operation == MonkeyOperation.Shout)
            {
                ResultDictionary[monkey.Name] = monkey.YellingNumber.Value;
            }

            MonkeyDictionary[monkey.Name] = monkey;
        }
    }
}

public class YellingMonkey
{
    public string Name { get; init; }
    public MonkeyOperation Operation { get; init; }
    public string? LeftOperand { get; set; }
    public string? RightOperand { get; set; }
    public long? YellingNumber { get; set; }
}

public enum MonkeyOperation
{
    Add,
    Subtract,
    Multiply,
    Divide,
    Shout
}