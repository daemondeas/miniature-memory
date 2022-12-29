namespace Csharp.Solutions;

public static class Day25
{
    public static long FirstPuzzle(string[] input)
    {
        var result = input.Sum(ParseSnafuNumber);
        
        Console.WriteLine(ConvertToSnafu(result));
        return result;
    }

    private static long ParseSnafuNumber(string number)
    {
        var backwards = number.Reverse();
        var multiplier = 1L;
        var result = 0L;
        foreach (var part in backwards)
        {
            result += ParseSnafuDigit(part) * multiplier;
            multiplier *= 5;
        }

        return result;
    }

    private static long ParseSnafuDigit(char digit) =>
        digit switch
        {
            '=' => -2,
            '-' => -1,
            '0' => 0,
            '1' => 1,
            '2' => 2
        };

    private static string ConvertToSnafu(long number)
    {
        var result = string.Empty;
        var digits = new[] { '=', '-', '0', '1', '2' };
        while (number != 0)
        {
            result = digits[(number + 2) % 5] + result;
            number += 2;
            number /= 5;
        }
        
        return result;
    }
}