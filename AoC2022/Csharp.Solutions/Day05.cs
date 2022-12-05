using System.Text;

namespace Csharp.Solutions;

public readonly record struct MovementInstruction(int Amount, char From, char To);

public static class Day05
{
    public static string FirstPuzzle(
        Dictionary<char, Stack<char>> crates,
        IEnumerable<MovementInstruction> instructions)
    {
        foreach (var instruction in instructions)
        {
            for (var i = 0; i < instruction.Amount; i++)
            {
                crates[instruction.To].Push(crates[instruction.From].Pop());
            }
        }

        var result = new StringBuilder(crates.Count);
        foreach (var crate in crates.Values)
        {
            result.Append(crate.Peek());
        }

        return result.ToString();
    }
    
    public static string SecondPuzzle(
        Dictionary<char, Stack<char>> crates,
        IEnumerable<MovementInstruction> instructions)
    {
        foreach (var instruction in instructions)
        {
            var middleStack = new Stack<char>();
            for (var i = 0; i < instruction.Amount; i++)
            {
                middleStack.Push(crates[instruction.From].Pop());
            }

            while (middleStack.Count > 0)
            {
                crates[instruction.To].Push(middleStack.Pop());
            }
        }

        var result = new StringBuilder(crates.Count);
        foreach (var crate in crates.Values)
        {
            result.Append(crate.Peek());
        }

        return result.ToString();
    }
}