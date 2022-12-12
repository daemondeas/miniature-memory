using System.Numerics;
using Fsharp.Solutions;
using Microsoft.FSharp.Collections;
using Microsoft.FSharp.Core;

namespace Inputs;

public class Input11 : AbstractInput<Day11.Monkey[], (Day11.Monkey[] input, BigInteger divisor)>
{
    protected override Day11.Monkey[] ParseInput(string input) => input.Split("\n\n").Select(ParseMonkey).ToArray();

    private static Day11.Monkey ParseMonkey(string input)
    {
        var rows = input.Split('\n');
        var items = ListModule.OfSeq(rows[1].Split(": ")[1].Split(", ").Select(BigInteger.Parse));
        var operation = ParseOperation(rows[2]);
        var monkeyTest = ParseMonkeyTest(rows[3], rows[4], rows[5]);

        return new Day11.Monkey(items: items, operation: operation, inspections: 0, test: monkeyTest);
    }

    private static FSharpFunc<BigInteger, BigInteger> ParseOperation(string row)
    {
        var operationParts = row.Split("old ")[1].Split(' ');
        Func<BigInteger, BigInteger> fun;
        if (operationParts[1] == "old")
        {
            fun = operationParts[0] switch
            {
                "*" => i => i * i,
                "/" => i => i / i,
                "+" => i => i + i,
                "-" => i => i - i
            };
        }
        else
        {
            var number = BigInteger.Parse(operationParts[1]);
            fun = operationParts[0] switch
            {
                "*" => i => i * number,
                "/" => i => i / number,
                "+" => i => i + number,
                "-" => i => i - number
            };
        }

        return FuncConvert.FromFunc(fun);
    }

    private static Day11.MonkeyTest ParseMonkeyTest(string testRow, string trueDest, string falseDest)
    {
        var number = BigInteger.Parse(testRow.Split("divisible by ")[1]);
        var test = FuncConvert.FromFunc(new Func<BigInteger, bool>(i => i % number == 0));
        var trueNumber = int.Parse(trueDest.Split("monkey ")[1]);
        var falseNumber = int.Parse(falseDest.Split("monkey ")[1]);

        return new Day11.MonkeyTest(test: test, successDestination: trueNumber, failDestination: falseNumber);
    }

    protected override (Day11.Monkey[], BigInteger) ParseInputTwo(string input) => (ParseInput(input), ParseDivisor(input));

    private static BigInteger ParseDivisor(string input) =>
        input.Split("\n\n").Select(m => m.Split('\n')[3])
            .Aggregate(
                BigInteger.One,
                (current, line) =>
                    current * BigInteger.Parse(line.Split("divisible by ")[1]));

    protected override long SolveFirstPuzzle(Day11.Monkey[] input) => Day11.firstPuzzle(input);

    protected override long SolveSecondPuzzle((Day11.Monkey[] input, BigInteger divisor) input) => Day11.secondPuzzle(input.input, input.divisor);

    public override string TestInput => @"Monkey 0:
  Starting items: 79, 98
  Operation: new = old * 19
  Test: divisible by 23
    If true: throw to monkey 2
    If false: throw to monkey 3

Monkey 1:
  Starting items: 54, 65, 75, 74
  Operation: new = old + 6
  Test: divisible by 19
    If true: throw to monkey 2
    If false: throw to monkey 0

Monkey 2:
  Starting items: 79, 60, 97
  Operation: new = old * old
  Test: divisible by 13
    If true: throw to monkey 1
    If false: throw to monkey 3

Monkey 3:
  Starting items: 74
  Operation: new = old + 3
  Test: divisible by 17
    If true: throw to monkey 0
    If false: throw to monkey 1";

    public override string RealInput => @"Monkey 0:
  Starting items: 71, 56, 50, 73
  Operation: new = old * 11
  Test: divisible by 13
    If true: throw to monkey 1
    If false: throw to monkey 7

Monkey 1:
  Starting items: 70, 89, 82
  Operation: new = old + 1
  Test: divisible by 7
    If true: throw to monkey 3
    If false: throw to monkey 6

Monkey 2:
  Starting items: 52, 95
  Operation: new = old * old
  Test: divisible by 3
    If true: throw to monkey 5
    If false: throw to monkey 4

Monkey 3:
  Starting items: 94, 64, 69, 87, 70
  Operation: new = old + 2
  Test: divisible by 19
    If true: throw to monkey 2
    If false: throw to monkey 6

Monkey 4:
  Starting items: 98, 72, 98, 53, 97, 51
  Operation: new = old + 6
  Test: divisible by 5
    If true: throw to monkey 0
    If false: throw to monkey 5

Monkey 5:
  Starting items: 79
  Operation: new = old + 7
  Test: divisible by 2
    If true: throw to monkey 7
    If false: throw to monkey 0

Monkey 6:
  Starting items: 77, 55, 63, 93, 66, 90, 88, 71
  Operation: new = old * 7
  Test: divisible by 11
    If true: throw to monkey 2
    If false: throw to monkey 4

Monkey 7:
  Starting items: 54, 97, 87, 70, 59, 82, 59
  Operation: new = old + 8
  Test: divisible by 17
    If true: throw to monkey 1
    If false: throw to monkey 3";
}