using Csharp.Solutions;

namespace Inputs;

public class Input05 : AbstractInput<(Dictionary<char, Stack<char>>, IEnumerable<MovementInstruction>), (Dictionary<char, Stack<char>>, IEnumerable<MovementInstruction>)>
{
    protected override (Dictionary<char, Stack<char>>, IEnumerable<MovementInstruction>) ParseInput(string input)
    {
        var parts = input.Split("\n\n");
        return (ParseCrates(parts[0]), ParseInstructions(parts[1]));
    }

    private static Dictionary<char, Stack<char>> ParseCrates(string input)
    {
        var lines = input.Split('\n');
        var result = new Dictionary<char, Stack<char>>();
        var indexLine = lines.Last();
        for (var i = 0; i < indexLine.Length; i++)
        {
            if (indexLine[i] == ' ')
            {
                continue;
            }
            
            result.Add(indexLine[i], new Stack<char>());
            var j = lines.Length - 2;

            while (j >= 0 && lines[j][i] != ' ')
            {
                result[indexLine[i]].Push(lines[j--][i]);
            }
        }

        return result;
    }

    private static IEnumerable<MovementInstruction> ParseInstructions(string input) =>
        input.Split('\n').Select(ParseInstruction);

    private static MovementInstruction ParseInstruction(string input)
    {
        var parts = input.Split(' ');
        return new MovementInstruction(int.Parse(parts[1]), parts[3].Single(), parts[5].Single());
    }

    protected override (Dictionary<char, Stack<char>>, IEnumerable<MovementInstruction>) ParseInputTwo(string input) => ParseInput(input);

    protected override int SolveFirstPuzzle((Dictionary<char, Stack<char>>, IEnumerable<MovementInstruction>) input)
    {
        Console.WriteLine(Day05.FirstPuzzle(input.Item1, input.Item2));
        return 0;
    }

    protected override int SolveSecondPuzzle((Dictionary<char, Stack<char>>, IEnumerable<MovementInstruction>) input)
    {
        Console.WriteLine(Day05.SecondPuzzle(input.Item1, input.Item2));
        return 0;
    }

    public override string TestInput =>
        "     [D]    \n[N] [C]    \n[Z] [M] [P]\n 1   2   3 \n\nmove 1 from 2 to 1\nmove 3 from 1 to 3\nmove 2 from 2 to 1\nmove 1 from 1 to 2";

    public override string RealInput => @"        [J]         [B]     [T]    
        [M] [L]     [Q] [L] [R]    
        [G] [Q]     [W] [S] [B] [L]
[D]     [D] [T]     [M] [G] [V] [P]
[T]     [N] [N] [N] [D] [J] [G] [N]
[W] [H] [H] [S] [C] [N] [R] [W] [D]
[N] [P] [P] [W] [H] [H] [B] [N] [G]
[L] [C] [W] [C] [P] [T] [M] [Z] [W]
 1   2   3   4   5   6   7   8   9 

move 6 from 6 to 5
move 2 from 5 to 9
move 8 from 9 to 1
move 3 from 5 to 4
move 9 from 1 to 8
move 2 from 1 to 5
move 1 from 1 to 8
move 14 from 8 to 2
move 1 from 1 to 2
move 2 from 6 to 8
move 2 from 5 to 7
move 6 from 8 to 6
move 4 from 4 to 2
move 2 from 4 to 9
move 5 from 7 to 4
move 2 from 7 to 5
move 6 from 2 to 4
move 2 from 4 to 7
move 4 from 5 to 8
move 1 from 5 to 2
move 3 from 3 to 5
move 3 from 8 to 3
move 4 from 3 to 7
move 2 from 9 to 8
move 1 from 3 to 7
move 1 from 6 to 8
move 5 from 7 to 1
move 3 from 7 to 2
move 1 from 6 to 3
move 2 from 5 to 9
move 5 from 4 to 2
move 3 from 5 to 9
move 5 from 9 to 6
move 2 from 1 to 3
move 4 from 4 to 1
move 2 from 8 to 1
move 18 from 2 to 5
move 3 from 4 to 1
move 1 from 1 to 2
move 1 from 6 to 8
move 1 from 7 to 1
move 10 from 1 to 5
move 1 from 1 to 5
move 3 from 8 to 1
move 2 from 1 to 5
move 3 from 6 to 5
move 8 from 2 to 9
move 2 from 9 to 7
move 3 from 3 to 8
move 1 from 4 to 8
move 3 from 5 to 3
move 15 from 5 to 8
move 4 from 6 to 1
move 2 from 7 to 4
move 9 from 5 to 7
move 1 from 6 to 8
move 5 from 3 to 5
move 5 from 7 to 5
move 3 from 1 to 5
move 2 from 4 to 8
move 3 from 1 to 6
move 20 from 5 to 4
move 1 from 7 to 6
move 21 from 8 to 2
move 1 from 3 to 7
move 2 from 4 to 2
move 1 from 7 to 1
move 18 from 2 to 8
move 3 from 9 to 2
move 1 from 6 to 4
move 1 from 1 to 9
move 8 from 8 to 6
move 4 from 8 to 2
move 1 from 2 to 6
move 7 from 8 to 5
move 2 from 5 to 3
move 1 from 9 to 5
move 5 from 2 to 4
move 1 from 3 to 7
move 2 from 5 to 7
move 4 from 4 to 9
move 2 from 5 to 9
move 6 from 2 to 8
move 3 from 7 to 3
move 2 from 5 to 4
move 4 from 8 to 2
move 2 from 7 to 4
move 7 from 6 to 4
move 1 from 8 to 4
move 3 from 6 to 7
move 2 from 7 to 2
move 7 from 9 to 7
move 1 from 9 to 2
move 3 from 3 to 6
move 3 from 7 to 4
move 2 from 7 to 9
move 6 from 4 to 1
move 3 from 7 to 9
move 1 from 8 to 5
move 1 from 3 to 6
move 3 from 9 to 4
move 2 from 6 to 4
move 3 from 9 to 1
move 4 from 2 to 8
move 1 from 8 to 5
move 9 from 1 to 2
move 1 from 6 to 5
move 1 from 7 to 2
move 1 from 8 to 1
move 2 from 8 to 9
move 1 from 9 to 8
move 1 from 5 to 7
move 1 from 7 to 6
move 1 from 9 to 8
move 1 from 6 to 3
move 26 from 4 to 3
move 1 from 5 to 8
move 3 from 6 to 3
move 7 from 4 to 3
move 1 from 1 to 3
move 1 from 4 to 8
move 13 from 3 to 1
move 1 from 3 to 4
move 12 from 2 to 5
move 20 from 3 to 2
move 1 from 4 to 1
move 4 from 5 to 7
move 1 from 7 to 8
move 9 from 5 to 2
move 5 from 1 to 5
move 21 from 2 to 8
move 5 from 8 to 4
move 4 from 5 to 2
move 6 from 1 to 7
move 1 from 5 to 4
move 4 from 3 to 1
move 6 from 1 to 3
move 1 from 1 to 9
move 6 from 8 to 7
move 4 from 8 to 2
move 4 from 2 to 7
move 5 from 4 to 1
move 8 from 8 to 4
move 1 from 9 to 6
move 18 from 7 to 6
move 15 from 6 to 5
move 2 from 6 to 8
move 2 from 6 to 3
move 8 from 3 to 7
move 15 from 5 to 7
move 3 from 4 to 9
move 12 from 2 to 3
move 3 from 9 to 4
move 6 from 7 to 9
move 9 from 4 to 5
move 10 from 3 to 5
move 9 from 5 to 2
move 14 from 7 to 8
move 14 from 8 to 5
move 4 from 2 to 4
move 1 from 4 to 6
move 2 from 8 to 4
move 3 from 8 to 9
move 18 from 5 to 1
move 1 from 5 to 9
move 1 from 7 to 4
move 5 from 5 to 9
move 3 from 2 to 4
move 13 from 9 to 2
move 13 from 2 to 6
move 1 from 7 to 3
move 3 from 3 to 1
move 9 from 6 to 5
move 1 from 7 to 8
move 20 from 1 to 8
move 2 from 2 to 8
move 5 from 6 to 9
move 15 from 8 to 7
move 3 from 5 to 3
move 5 from 1 to 3
move 2 from 3 to 4
move 3 from 9 to 5
move 4 from 5 to 2
move 4 from 5 to 7
move 3 from 4 to 9
move 10 from 7 to 8
move 2 from 9 to 4
move 1 from 5 to 6
move 8 from 7 to 9
move 1 from 6 to 7
move 6 from 3 to 4
move 12 from 9 to 8
move 1 from 1 to 5
move 2 from 7 to 8
move 1 from 7 to 5
move 1 from 9 to 5
move 2 from 2 to 9
move 11 from 8 to 1
move 7 from 1 to 5
move 3 from 1 to 6
move 5 from 8 to 9
move 8 from 4 to 3
move 4 from 4 to 6
move 5 from 9 to 3
move 4 from 4 to 5
move 2 from 6 to 7
move 1 from 9 to 5
move 2 from 7 to 4
move 12 from 5 to 2
move 8 from 8 to 9
move 8 from 8 to 6
move 9 from 6 to 2
move 4 from 9 to 2
move 1 from 5 to 1
move 5 from 2 to 1
move 2 from 5 to 4
move 5 from 2 to 5
move 5 from 5 to 6
move 3 from 4 to 7
move 11 from 2 to 7
move 2 from 2 to 1
move 4 from 3 to 7
move 2 from 2 to 4
move 6 from 1 to 4
move 1 from 2 to 8
move 2 from 9 to 5
move 4 from 4 to 3
move 5 from 4 to 1
move 2 from 2 to 1
move 1 from 8 to 5
move 14 from 7 to 6
move 3 from 9 to 2
move 15 from 6 to 8
move 4 from 1 to 3
move 2 from 2 to 3
move 1 from 1 to 7
move 2 from 3 to 5
move 4 from 5 to 4
move 1 from 3 to 5
move 5 from 1 to 6
move 12 from 6 to 7
move 7 from 8 to 4
move 12 from 7 to 9
move 4 from 7 to 9
move 1 from 2 to 8
move 12 from 9 to 4
move 23 from 4 to 3
move 1 from 6 to 5
move 3 from 9 to 3
move 1 from 7 to 9
move 1 from 9 to 1
move 1 from 9 to 7
move 42 from 3 to 1
move 3 from 5 to 4
move 5 from 1 to 3
move 3 from 4 to 7
move 1 from 1 to 9
move 4 from 3 to 8
move 1 from 3 to 7
move 1 from 9 to 1
move 2 from 7 to 8
move 8 from 1 to 6
move 2 from 7 to 5
move 9 from 1 to 2
move 5 from 2 to 3
move 3 from 2 to 4
move 20 from 1 to 2
move 1 from 1 to 5
move 1 from 6 to 7
move 3 from 4 to 7
move 2 from 3 to 6
move 3 from 6 to 1
move 1 from 6 to 4
move 2 from 1 to 6
move 3 from 5 to 9
move 1 from 4 to 3
move 2 from 7 to 4
move 6 from 8 to 4
move 1 from 1 to 9
move 1 from 2 to 9
move 2 from 8 to 7
move 3 from 6 to 2
move 5 from 7 to 5
move 4 from 2 to 5
move 4 from 4 to 6
move 3 from 9 to 6
move 4 from 3 to 1
move 1 from 9 to 2
move 7 from 8 to 9
move 4 from 2 to 4
move 2 from 1 to 7
move 3 from 4 to 5
move 4 from 2 to 4
move 1 from 7 to 4
move 4 from 2 to 9
move 7 from 4 to 3
move 1 from 7 to 3
move 6 from 2 to 3
move 2 from 1 to 5
move 10 from 3 to 6
move 2 from 6 to 1
move 2 from 2 to 7
move 2 from 3 to 1
move 1 from 7 to 8
move 11 from 5 to 3
move 2 from 3 to 1
move 4 from 6 to 1
move 1 from 4 to 6
move 8 from 3 to 4
move 2 from 5 to 6
move 3 from 3 to 5
move 1 from 8 to 4
move 1 from 4 to 9
move 2 from 6 to 1
move 1 from 5 to 1
move 9 from 4 to 3
move 5 from 6 to 9
move 5 from 6 to 7
move 13 from 9 to 3
move 5 from 1 to 8
move 4 from 8 to 4
move 10 from 3 to 2
move 3 from 6 to 1
move 2 from 7 to 9
move 1 from 8 to 3
move 1 from 7 to 3
move 1 from 9 to 5
move 1 from 6 to 3
move 7 from 2 to 4
move 3 from 5 to 2
move 8 from 3 to 5
move 7 from 4 to 3
move 5 from 9 to 7
move 1 from 7 to 1
move 9 from 1 to 8
move 9 from 5 to 8
move 2 from 7 to 8
move 3 from 8 to 1
move 10 from 3 to 6
move 1 from 1 to 6
move 5 from 1 to 7
move 3 from 2 to 8
move 7 from 8 to 6
move 7 from 8 to 6
move 1 from 3 to 5
move 5 from 7 to 9
move 4 from 8 to 4
move 3 from 2 to 8
move 1 from 7 to 8
move 3 from 3 to 9
move 3 from 7 to 4
move 1 from 7 to 2
move 9 from 9 to 1
move 5 from 1 to 9
move 4 from 8 to 6
move 1 from 2 to 7
move 1 from 5 to 3
move 1 from 3 to 7
move 1 from 1 to 9
move 1 from 1 to 7
move 5 from 9 to 6
move 2 from 7 to 6
move 10 from 4 to 8
move 1 from 4 to 2
move 1 from 4 to 1
move 1 from 9 to 2
move 3 from 1 to 2
move 1 from 7 to 3
move 1 from 2 to 1
move 16 from 6 to 3
move 9 from 6 to 1
move 6 from 6 to 1
move 5 from 6 to 1
move 3 from 8 to 1
move 11 from 3 to 4
move 1 from 6 to 2
move 3 from 8 to 2
move 4 from 1 to 6
move 5 from 3 to 2
move 1 from 2 to 5
move 1 from 8 to 5
move 5 from 8 to 3
move 4 from 6 to 9
move 2 from 9 to 6
move 3 from 3 to 9
move 1 from 5 to 7
move 5 from 1 to 6
move 3 from 6 to 4
move 2 from 2 to 9
move 8 from 4 to 2
move 9 from 1 to 7
move 3 from 3 to 5
move 3 from 5 to 7
move 12 from 7 to 1
move 5 from 4 to 6
move 1 from 4 to 5
move 7 from 1 to 8
move 5 from 9 to 3
move 1 from 7 to 4
move 10 from 1 to 8
move 1 from 4 to 8
move 4 from 6 to 8
move 1 from 6 to 9
move 2 from 5 to 1
move 4 from 3 to 4
move 1 from 1 to 8
move 4 from 4 to 7
move 2 from 1 to 8
move 4 from 6 to 1
move 3 from 9 to 5
move 1 from 6 to 5
move 1 from 3 to 7
move 24 from 8 to 6
move 3 from 6 to 5
move 4 from 6 to 7
move 1 from 1 to 7
move 7 from 7 to 6
move 7 from 5 to 3
move 13 from 6 to 8
move 3 from 1 to 2
move 7 from 6 to 3
move 12 from 2 to 4
move 4 from 6 to 9
move 6 from 3 to 1
move 1 from 2 to 4
move 2 from 8 to 7
move 2 from 2 to 9
move 6 from 3 to 4
move 12 from 8 to 2
move 18 from 2 to 5
move 10 from 4 to 3
move 4 from 7 to 3
move 5 from 4 to 7
move 3 from 5 to 2
move 4 from 7 to 9
move 1 from 5 to 4
move 3 from 2 to 1
move 4 from 3 to 6
move 7 from 5 to 6
move 2 from 5 to 7
move 5 from 1 to 7
move 9 from 7 to 6
move 8 from 9 to 8
move 1 from 1 to 3
move 1 from 3 to 1
move 10 from 3 to 9
move 8 from 8 to 4
move 1 from 3 to 8
move 1 from 1 to 3
move 6 from 9 to 1
move 5 from 5 to 3
move 5 from 3 to 6
move 1 from 8 to 9
move 19 from 6 to 2
move 13 from 4 to 1
move 4 from 1 to 5
move 6 from 2 to 1
move 2 from 9 to 4
move 1 from 3 to 1
move 9 from 2 to 3
move 4 from 5 to 1
move 5 from 9 to 6
move 4 from 3 to 4
move 3 from 2 to 7
move 2 from 4 to 8
move 6 from 1 to 9
move 1 from 8 to 6
move 4 from 1 to 5
move 3 from 4 to 5
move 1 from 7 to 2
move 11 from 1 to 6
move 1 from 2 to 7
move 5 from 3 to 7
move 1 from 3 to 4
move 1 from 4 to 8
move 3 from 5 to 6
move 8 from 1 to 7
move 1 from 8 to 9
move 1 from 6 to 9
move 1 from 8 to 5
move 11 from 6 to 5
move 12 from 5 to 2
move 1 from 5 to 2
move 8 from 7 to 3
move 1 from 5 to 6
move 2 from 5 to 6
move 3 from 7 to 1
move 6 from 2 to 6
move 1 from 3 to 1
move 1 from 4 to 1
move 4 from 6 to 2
move 5 from 1 to 5
move 10 from 2 to 3
move 2 from 9 to 4
move 4 from 5 to 8
move 2 from 2 to 7
move 12 from 6 to 7
move 1 from 8 to 2
move 10 from 3 to 4
move 2 from 3 to 5
move 1 from 3 to 1";
}