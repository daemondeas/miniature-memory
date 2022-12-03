namespace Inputs;

public abstract class AbstractInput<TU, TV> : IInput
{
    public int FirstPuzzle(string input) => SolveFirstPuzzle(ParseInput(input));

    public int SecondPuzzle(string input) => SolveSecondPuzzle(ParseInputTwo(input));

    protected abstract TU ParseInput(string input);

    protected abstract TV ParseInputTwo(string input);

    protected abstract int SolveFirstPuzzle(TU input);

    protected abstract int SolveSecondPuzzle(TV input);

    public abstract string TestInput { get; }
    public abstract string RealInput { get; }
}