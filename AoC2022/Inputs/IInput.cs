namespace Inputs;

public interface IInput
{
    int FirstPuzzle(string input);

    int SecondPuzzle(string input);
    
    string TestInput { get; }
    
    string RealInput { get; }
}