using Inputs;

Console.WriteLine("Please choose a day:");
var input = Console.ReadLine();

if (!int.TryParse(input, out var day))
{
    Console.WriteLine("You have to choose a day by it's number");
    return;
}

if (day is < 1 or > 3)
{
    Console.WriteLine("The day must be within the range 1-2");
    return;
}

Console.WriteLine("First (1) or second (2) puzzle?");
var firstPuzzle = Console.ReadLine() == "1";

Console.WriteLine("Test input (t) or real input (r)?");
var useTestInput = Console.ReadLine() == "t";

var solver = new Solver(
    new Dictionary<int, IInput>
    {
        { 1, new Input01() },
        { 2, new Input02() },
        { 3, new Input03() },
    });

var result = solver.Solve(day, firstPuzzle, useTestInput);

Console.WriteLine($"Result for day {day}, puzzle {(firstPuzzle ? '1' : '2')}, {(useTestInput ? "test" : "real")} input is \n{result}");