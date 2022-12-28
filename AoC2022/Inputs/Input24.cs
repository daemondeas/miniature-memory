using Csharp.Solutions;

namespace Inputs;

public class Input24 : AbstractInput<(IEnumerable<Blizzard>, IReadOnlySet<(int, int)>, (int, int)), (IEnumerable<Blizzard>, IReadOnlySet<(int, int)>, (int, int))>
{
    protected override (IEnumerable<Blizzard>, IReadOnlySet<(int, int)>, (int, int)) ParseInput(string input)
    {
        var rows = input.Split('\n');
        var blizzards = new List<Blizzard>();
        var walls = new HashSet<(int, int)>();
        var maxX = rows[0].Length - 2;
        var maxY = rows.Length - 2;
        for (var y = 0; y < rows.Length; y++)
        {
            var row = rows[y].ToCharArray();
            for (int x = 0; x < row.Length; x++)
            {
                if (row[x] == '.')
                {
                    continue;
                }

                if (row[x] == '#')
                {
                    walls.Add((x, y));
                    continue;
                }

                var direction = ParseDirection(row[x]);
                var max = direction == Direction.West || direction == Direction.East
                    ? maxX
                    : maxY;
                blizzards.Add(new Blizzard((x, y), direction, 1, max));
            }
        }

        return (blizzards, walls, (maxX, rows.Length - 1));
    }

    private static Direction ParseDirection(char direction) =>
        direction switch
        {
            '^' => Direction.North,
            '>' => Direction.East,
            'v' => Direction.South,
            '<' => Direction.West
        };

    protected override (IEnumerable<Blizzard>, IReadOnlySet<(int, int)>, (int, int)) ParseInputTwo(string input) =>
        ParseInput(input);

    protected override long SolveFirstPuzzle((IEnumerable<Blizzard>, IReadOnlySet<(int, int)>, (int, int)) input) =>
        Day24.FirstPuzzle(input);

    protected override long SolveSecondPuzzle((IEnumerable<Blizzard>, IReadOnlySet<(int, int)>, (int, int)) input) =>
        Day24.SecondPuzzle(input);

    public override string TestInput => "#.######\n#>>.<^<#\n#.<..<<#\n#>v.><>#\n#<^v^^>#\n######.#";
    public override string RealInput => @"#.####################################################################################################
#>>^<^v>v<>^>>v<<.<<<^<vv^<^>v><^<v>><^>.<><^^>v<^>v>^^>v><v.^<.^>>>^>v^^^<vv^<><vvv>v>^vv<<>v^^<v><>#
#>^>^><^v<>.v><><<<>><v><>vv^.><v^<^v^>v<<<v..<v.v^v.v<<v<>>^^<v^v.<^.^v><^v^.^^<v>..v<^>^<<^>.v.>v.>#
#.>^>v<v^><v<v^><>^v>><>v<>^><>v^<^v<>v<>^<v<><^^v.v.>^^.v<<^v<>>.v>>v<^^.v<v^<^>>..>><<v.^>.<<<^v.^>#
#<>^^^..v^^v^<<><<<^<>.>.vvv.v>^^.><.^>>>v^^>^^vv>v<^v>>^^<v>v^<<^>..^v^<>>>>>.>>><v^^^vv<>v^v<>^^.<>#
#>v^vvv><>>v^v.v><v<<^.>.>>^<.^<^<>><^<><^>v.>v<>v><<>>><^v<.>v<^v<v^..<vv><><.^^^^<^.<v<<^^>>v^>^<><#
#>^>^>^<<>^><<v^^^^^^^v^<<>>v^^>v>v<.><><^v^<v>>>>^<..>v<.^v><^<^v>v^^vvv^v^<v>>.^<<<.^^vv>vvv>^..<v>#
#><vv.vv^v.>^>>v<<<v^>>^^>><v<<.^>>v>v^.<v<vv<<<.vv^<>^.<v>vv<.>^^v><><^<v<^><.^vv^^<><<.vv^<v<<v.v^>#
#<^<v>.>^v^<v^^><vv.>>.<^>.^..><v^<<v^<^<<.<v<^v^v<^<.<<v<^v>^<<^v<>^<^v>vv^>vv>^<v.>^><v..>><v^<^^^>#
#>^><v><<<>><^v^>v<><>v^><.v>>.>^v<.^><vvv<.>v<v>^>>v><^^^v^.v>^.^^>^v>>v^v<^v>.^.<^>^^v<.<<v^..<<.v>#
#>vv>^.<<>><^>.<^><v<^<>>v><v>^.>v^<><.v.>^>>^><<v>^v..<^^^<vv^..^^<<<.v<<v<vv^v^<^><><v>.<<><^<v<^<<#
#>^>v<^v^.<<v<<^^>^<>vvv<v^>^<v<.^v^>><>vv<>v<.^^>^<>>><>v<<>.>^<<>vvv^v.v<><<>^.v.>^><^^>>^>>><>v.^<#
#>^<.^.<>v<.<^v>v<^^<>^>vvv..v.^><<^<.><...<.^>^>^vv<>v^^><<v>v^^<^^><<^>>vvv^<^^v>>vv<v<><v^<^v<^v^>#
#.>>^.v<>v>><<..vv>^.>>^>^^.v>v.<^><>v<v^^><<v<.>>^<v<<><>.^<<.>>^<v<>v^>><v>^>..v<^<^.vv<>^^>>>v^>v<#
#<>^^^<v><^v^vvv>^v^v><<>>^<vv^><<v.>>v>.v>^>>v<^vv.<><<>><<v<>>^.<<>v.v^^vv<.>^<^v.<^<vvv^v.vv><vv.>#
#>vv.vv^><v>v>.<v^><v<vv^v<.v<<>^<^>^><v^>v>^<v><>v>>v>>>>v^.<v<vv>^v<^.^.^>>><>^<^<^.<v<.><v.^<^.><.#
#>^vvv<>^v^^>^<^v<^>v<<^<vv.vvv<<<v<v^..^.<<v<<<>>^>v^v^>^><v.^<v<<v>^^v<v.>>>v^<v^<v>^<^^.>v<^<>^.v<#
#<>^<<<<v<>^v>v^<vv<^v>v^^<>.^>^^<.>>v>v<<^<<<>v<v^v><>>.<<.^<>><<<v>v^>v^^<v^^.><vv>^^<v.v>^<v<.vv><#
#<>vvv<^^.v^^.v.<^<^>.^^v>^v>>.vv.>.v<<^<^^>vvv^<<<vv^<vv<>>v^^<^.<^.>v.^>v><^^>>^>^<>.>.^<<<.v.<>^>>#
#><>v<>>>><.^v>v><v>^<<><>^vv.^.<.>>vvvv><>^^><>v>v.^^<^<^v>v<^<.<^<>^^^^vvv>.>>><^vv<^^^>.^<<v^>>><>#
#<<><^>>^>^^>><.v^^^.v^^^>.^v<vvv.^^<^^>^<>>><<>>><<.v<^>><<v<v^>vv^<>vv.<<>v<v^><<vv^^^>vvvv<<v<><<<#
#<^<><>><<>^^>v<<^v<v>^^^>^^vvvv<^^vv<>><.<^^.vv^>>.<>^.v<><^^vv>^^>>^<^^v>.v<>>^v.v^>.^v<.<v.<>>>>v<#
#<^<<^^^vv><^<>^^>^v<v^v^.<v.>v>><<^^>v^>v>><v<<><>^^.vv^><.^>vv^v<<>^><^>^>^v^<><>^.^<.^.^>>^<.>^><<#
#>^.<<v>^.<<<^>v>>>^<.^>>>.^^<<^.v^<<>v^<<<^<><>^vvvv^>vv^^>.>>^>><>^^^^>v<^^>^^<^vv.v.^<<^<<^>v><.>>#
#>vv^.^vv>^<>^v.^^<v>v>><^^^v<<>>^>>^^>^<^<<<^.v><^>v<<v^<^^^>vv><<v<.<v><^vv>^>^v^<^v>^<^vv<<^v<.^^>#
#<v>vv<^>v^^<<^^<<<v>v><<<.v^>^<><vvv^>.^.v>v<^^vvvv>>^v^^v^^^<>>>>v>>^>v^.<>.>.^vvv^<^<.<^>^<.v<><.>#
#.<.<^v<v><^>.^.v<>>^<>.<<^.vv^>v^v<..>^.^^vv^><.<>^<><^vv>^<.<>v..<^^<v^^v<<<v<><<>>vv<>^^<>^vvvvv><#
#<.^v^>vvv<.v<.vv><^vv^>>.^>><><<v>v<^<<vv.<>v<vv^v>^>^^<v>^>^<<>^<<^>.v^^^><>.^^v>^...^<^>>>.>v^^vv<#
#.<^v>^>^^<^v.v>.^<><<<>>vv>.^v<>.<<>>v^v<v>>^.^<^^<.v^<><vvv>><^.>.vv<.vv^>^><<.<.>><^>vv^^>>v^>^>.<#
#.>^<>><v<.>vv<>>^<v.>^.<v^v.v<><>^<v^<v.<<^v<>v<<v>v^>^>v.>>v^.><^^.>v<v.v^.>^^<>^<.^<v^^><<>^vvv>><#
#.^.^vv^..^>.v.<<v>^^^>^<^v<^>>v<>v>.<><>>.<v^<^>.<v.^^v^<v><>^>v.<^<<><>v.<<<^>.<<.^>^>^<^.>^v>>>.>>#
#.^v>><>v<<.<^>^^<<>^>v>vv<>>^>>^.v^>.>^<v^v>>v^^><.v.<v>.>vv<<^v<v<v^v>v<v<.^<.^<<v>^^v.^v.<^>.v>^>>#
#>^v>^<<>>vv>^<.<v^v<^>>^v>^v^<<><<^^^^v>^^^^^^^vv<^..<vvv><^.<..^>^^<^>v^>>>>vv>v<<<<>.>.>^.^<^.v<v>#
#.^v.^>><.><<v^>^^.>><^v<..>..><.><><<v>>>v<>^.>v<vv^^<<.^.^^v<v..^<v^>.<vvv>^>v>^<^><>>>>v<>^>>vv^.>#
#<v<vv^vvv><v^<^>>.v^v^>><.>><>>v^^<v>>>>.v^.^.<vv.>>>^v>^<>v^.^>v<vv.>>vvv^v^<vv<>v<^<^.<<^..v^.^v<<#
#<.<v>^>v<^<.<><>.v<<^v<^<>^.vv<v<<v>v<^<^v>v<v^^vv<><..v<<<<.^^v<>>^<v>.^>>v<^>v>^><vvv<^>>.^v>^<.><#
####################################################################################################.#";
}