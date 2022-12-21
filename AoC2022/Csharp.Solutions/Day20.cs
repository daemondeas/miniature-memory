namespace Csharp.Solutions;

public static class Day20
{
    public static long FirstPuzzle(long[] input)
    {
        var array = new CircularIndexedLongArray(input);
        array.Mix();
        var index = array.IndexOf(0);
        return array.GetIndex(index + 1000) + array.GetIndex(index + 2000) + array.GetIndex(index + 3000);
    }
    
    public static long SecondPuzzle(long[] input)
    {
        for (var i = 0; i < input.Length; i++)
        {
            input[i] *= 811589153;
        }
        
        var array = new CircularIndexedLongArray(input);
        array.Mix();
        array.Mix();
        array.Mix();
        array.Mix();
        array.Mix();
        array.Mix();
        array.Mix();
        array.Mix();
        array.Mix();
        array.Mix();
        var index = array.IndexOf(0);
        return array.GetIndex(index + 1000) + array.GetIndex(index + 2000) + array.GetIndex(index + 3000);
    }
}

public class CircularIndexedLongArray
{
    private readonly (int index, long value)[] _innerArray;
    public int Length { get; }
    
    public CircularIndexedLongArray(IReadOnlyList<long> array)
    {
        Length = array.Count;
        _innerArray = new (int index, long value)[Length];
        for (var i = 0; i < Length; i++)
        {
            _innerArray[i] = (i, array[i]);
        }
    }

    public long GetIndex(int index)
    {
        while (index < 0)
        {
            index += Length;
        }
        
        return _innerArray[index % Length].value;
    }
    
    public void Mix()
    {
        for (var i = 0; i < Length; i++)
        {
            for (var j = 0; j < Length; j++)
            {
                if (_innerArray[j].index == i)
                {
                    MoveIndex(j);
                    break;
                }
            }
        }
    }

    public int IndexOf(long value)
    {
        for (var i = 0; i < Length; i++)
        {
            if (_innerArray[i].value == value)
            {
                return i;
            }
        }

        throw new ArgumentException(null, nameof(value));
    }

    private void MoveIndex(int index)
    {
        var element = GetFullIndex(index);
        var newIndex = (int)((index + element.value) % (Length - 1));
        if (index > newIndex)
        {
            for (var i = index; i > newIndex; i--)
            {
                SetIndex(i, GetFullIndex(i - 1));
            }
        }
        else
        {
            for (var i = index; i < newIndex; i++)
            {
                SetIndex(i, GetFullIndex(i + 1));
            }
        }
        
        SetIndex(newIndex, element);
    }
    
    private (int index, long value) GetFullIndex(int index)
    {
        while (index < 0)
        {
            index += Length;
        }
        
        return _innerArray[index % Length];
    }

    private void SetIndex(long index, (int, long) value)
    {
        index %= Length;
        if (index < 0)
        {
            index += Length;
        }

        _innerArray[index] = value;
    }
}