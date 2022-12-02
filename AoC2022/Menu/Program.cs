using Inputs;

Console.WriteLine("Please choose a day:");
var input = Console.ReadLine();

if (!int.TryParse(input, out var day))
{
    Console.WriteLine("You have to choose a day by it's number");
    return;
}

if (day is < 1 or > 2)
{
    Console.WriteLine("The day must be within the range 1-2");
    return;
}

Console.WriteLine("First (1) or second (2) puzzle?");
var firstPuzzle = Console.ReadLine() == "1";

Console.WriteLine("Test input (t) or real input (r)?");
var useTestInput = Console.ReadLine() == "t";

var result = day switch
{
    1 => firstPuzzle
        ? Fsharp.Solutions.Day01.firstPuzzle(Input01.ParseInput(useTestInput ? Input01.TestInput : Input01.RealInput))
        : Fsharp.Solutions.Day01.secondPuzzle(Input01.ParseInput(useTestInput ? Input01.TestInput : Input01.RealInput)),
    2 => firstPuzzle
        ? Fsharp.Solutions.Day02.firstPuzzle(Input02.ParseInput(useTestInput ? Input02.TestInput : Input02.RealInput))
        : Fsharp.Solutions.Day02.secondPuzzle(Input02.ParseInputTwo(useTestInput ? Input02.TestInput : Input02.RealInput)),
    _ => -1
};

Console.WriteLine($"Result for day {day}, puzzle {(firstPuzzle ? '1' : '2')}, {(useTestInput ? "test" : "real")} input is \n{result}");