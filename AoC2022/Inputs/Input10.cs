using Fsharp.Solutions;
using Microsoft.FSharp.Collections;

namespace Inputs;

public class Input10 : AbstractInput<FSharpList<Day10.Instruction>, FSharpList<Day10.Instruction>>
{
    protected override FSharpList<Day10.Instruction> ParseInput(string input) =>
        ListModule.OfSeq(input.Split('\n').Select(ParseInstruction));
    
    private static Day10.Instruction ParseInstruction(string instruction)
    {
        if (instruction == "noop")
        {
            return new Day10.Instruction(iType: Day10.InstructionType.NOOP, value: 0);
        }

        var parts = instruction.Split(' ');
        return new Day10.Instruction(iType: Day10.InstructionType.ADDX, value: int.Parse(parts[1]));
    }

    protected override FSharpList<Day10.Instruction> ParseInputTwo(string input) => ParseInput(input);

    protected override long SolveFirstPuzzle(FSharpList<Day10.Instruction> input) => Day10.firstPuzzle(input);

    protected override long SolveSecondPuzzle(FSharpList<Day10.Instruction> input)
    {
        foreach (var row in Day10.secondPuzzle(input))
        {
            Console.WriteLine(row);
        }
        
        return 0;
    }

    public override string TestInput => @"addx 15
addx -11
addx 6
addx -3
addx 5
addx -1
addx -8
addx 13
addx 4
noop
addx -1
addx 5
addx -1
addx 5
addx -1
addx 5
addx -1
addx 5
addx -1
addx -35
addx 1
addx 24
addx -19
addx 1
addx 16
addx -11
noop
noop
addx 21
addx -15
noop
noop
addx -3
addx 9
addx 1
addx -3
addx 8
addx 1
addx 5
noop
noop
noop
noop
noop
addx -36
noop
addx 1
addx 7
noop
noop
noop
addx 2
addx 6
noop
noop
noop
noop
noop
addx 1
noop
noop
addx 7
addx 1
noop
addx -13
addx 13
addx 7
noop
addx 1
addx -33
noop
noop
noop
addx 2
noop
noop
noop
addx 8
noop
addx -1
addx 2
addx 1
noop
addx 17
addx -9
addx 1
addx 1
addx -3
addx 11
noop
noop
addx 1
noop
addx 1
noop
noop
addx -13
addx -19
addx 1
addx 3
addx 26
addx -30
addx 12
addx -1
addx 3
addx 1
noop
noop
noop
addx -9
addx 18
addx 1
addx 2
noop
noop
addx 9
noop
noop
noop
addx -1
addx 2
addx -37
addx 1
addx 3
noop
addx 15
addx -21
addx 22
addx -6
addx 1
noop
addx 2
addx 1
noop
addx -10
noop
noop
addx 20
addx 1
addx 2
addx 2
addx -6
addx -11
noop
noop
noop";

    public override string RealInput => @"addx 1
noop
addx 4
noop
noop
addx 7
noop
noop
noop
addx 3
noop
noop
addx 5
addx -1
addx 1
addx 5
addx 3
noop
addx 3
noop
addx -1
noop
addx 3
addx 5
addx -38
addx 7
addx 10
addx -14
addx 5
addx 30
addx -25
noop
addx 2
addx 3
addx -2
addx 2
addx 5
addx 2
addx 2
addx -21
addx 22
addx 5
addx 2
addx 3
noop
addx -39
addx 1
noop
noop
addx 3
addx 5
addx 4
addx -5
addx 4
addx 4
noop
addx -9
addx 12
addx 5
addx 2
addx -1
addx 6
addx -2
noop
addx 3
addx 3
addx 2
addx -37
addx 39
addx -33
addx -1
addx 1
addx 8
noop
noop
noop
addx 2
addx 20
addx -19
addx 4
noop
noop
noop
addx 3
addx 2
addx 5
noop
addx 1
addx 4
addx -21
addx 22
addx -38
noop
noop
addx 7
addx 32
addx -27
noop
addx 3
addx -2
addx 2
addx 5
addx 2
addx 2
addx 3
addx -2
addx 2
noop
addx 3
addx 5
addx 2
addx 3
noop
addx -39
addx 2
noop
addx 4
addx 8
addx -8
addx 6
addx -1
noop
addx 5
noop
noop
noop
addx 3
addx 5
addx 2
addx -11
addx 12
addx 2
noop
addx 3
addx 2
addx 5
addx -6
noop";
}