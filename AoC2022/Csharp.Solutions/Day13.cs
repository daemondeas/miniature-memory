using System.Text;
using Microsoft.FSharp.Core;

namespace Csharp.Solutions;

public static class Day13
{
    public static long SecondPuzzle(Fsharp.Solutions.Day13.Listish[] packets)
    {
        Array.Sort(packets, new PacketComparer());
        var stringRepresentation = packets.Select(AsString).ToArray();
        var indices = FindIndices(stringRepresentation, "[[2]]", "[[6]]");
        return indices.i1 * indices.i2;
    }

    private static (int i1, int i2) FindIndices(string[] array, string a, string b)
    {
        var index1 = -1;
        var index2 = -1;
        for (var i = 0; i < array.Length && (index1 < 0 || index2 < 0); i++)
        {
            if (array[i] == a)
            {
                index1 = i;
            }
            else if (array[i] == b)
            {
                index2 = i;
            }
        }

        return (index1 + 1, index2 + 1);
    }

    private static string AsString(this Fsharp.Solutions.Day13.Listish unit)
    {
        var builder = new StringBuilder("[");
        foreach (var element in unit.elements)
        {
            if (element.IsChoice1Of2)
            {
                builder.Append(((FSharpChoice<int, Fsharp.Solutions.Day13.Listish>.Choice1Of2)element).Item);
            }
            else
            {
                builder.Append(((FSharpChoice<int, Fsharp.Solutions.Day13.Listish>.Choice2Of2)element).Item.AsString());
            }
            
            builder.Append(',');
        }

        if (unit.elements.Any())
        {
            builder[^1] = ']';
        }
        else
        {
            builder.Append(']');
        }
        
        return builder.ToString();
    }
}

public class PacketComparer : IComparer<Fsharp.Solutions.Day13.Listish>
{
    public int Compare(Fsharp.Solutions.Day13.Listish? x, Fsharp.Solutions.Day13.Listish? y)
    {
        var result = Fsharp.Solutions.Day13.isInOrder(x.elements, y.elements);
        if (result.Equals(FSharpOption<bool>.None))
        {
            return 0;
        }

        if (result.Value)
        {
            return -1;
        }

        return 1;
    }
}