using Csharp.Solutions;

namespace Inputs;

public class Input23 : AbstractInput<List<Elf>, List<Elf>>
{
    protected override List<Elf> ParseInput(string input)
    {
        var result = new List<Elf>();
        var rows = input.Split('\n');
        for (var y = 0; y < rows.Length; y++)
        {
            var tiles = rows[y].ToCharArray();
            for (var x = 0; x < tiles.Length; x++)
            {
                if (tiles[x] == '#')
                {
                    result.Add(new Elf((x, y)));
                }
            }
        }

        return result;
    }

    protected override List<Elf> ParseInputTwo(string input) => ParseInput(input);

    protected override long SolveFirstPuzzle(List<Elf> input) => Day23.FirstPuzzle(input);

    protected override long SolveSecondPuzzle(List<Elf> input) => Day23.SecondPuzzle(input);

    public override string TestInput => "....#..\n..###.#\n#...#.#\n.#...##\n#.###..\n##.#.##\n.#..#..";
    public override string RealInput => @"...##.##..###.#.#....#.#######...###..#.##.#..##...#..####...#..#.##...#.#
..#.#..##.###.#..#...##....##....#..#.####...#####.#.#..#...##..#.#.#.####
#.##.##.###....#..#.#..##...##..#..#.#####.###..#.#.....####.#...#.###.##.
#.####..#.####..#.#..#####.##....##..##..##....#......#...##..#..##....###
###.#.###..####...###.###.##...#..##.#.#...##.###..##.##.##.....####..##..
#...####.#..#.#.##.#.##.#.#.####..####...#..#..##..#.#..##...#....##...##.
..#.#.##..#.##..#.#####.##..#.....###..###.#...###.#.####.###.#.#..#..##.#
##...###.###...##.###.#..#...#....#.##.#....##..#....##...#.#.#.##.....#.#
#..##.#.##..#.#..##.#.###.###....#..##...#..########..#.#..##.....##..####
##.#...#.....###..##..###.#.##..###.##.#.#..##.####.#.#.#.#..##..#...##..#
###....#.....#..##......#####...#.#.#.##.##.###.#.####.....#.#.#####....#.
.##..##..##.#..#....#.#.##.###.###....######.....###.##..######..####.#...
#..#.....##...#...#........##..##.#..##.###.##...#.####...###.#.#.##...#..
...#..#..##..#.#..##..#.###..#.#..#..#..#.######..##.#####..#...#...#.##.#
##.#.....#..#...#####.#.###..#.......#..###.##.##.##.##.###.####.#....#.#.
.##..#..##..#....#..####.##.#..########...##..##.#..#..#.###..###.........
###.#..#####.####.#....####.#####.##...#####.#.#.###.#.#.####........#..##
.##..#...###....####.##.####.#...##.....##.#..####...####..#.#...###..##..
...#.##.###.......##.#..#.#..###..#...##...#..#.###......#..##.#..##.#####
#..##..#.##..#..#..#.#.###.###.####.###.#.#.....#.....#...##...##....#...#
...##.##.##.####.##.....#.##....#.#.....#.#..##.####.###.####.##.......###
#.######....########..#...##.#.##....#...####..#.#..#....#....#...#####..#
...#.###..####..#.#####...#.##...#.###..#.#.####..######...####...#.###.#.
..###.####.#..#.#...##.##...#...#.#.#.#.#.#####.#...##..#....###.#.###..##
.#.#.#..###.....#...###......#.####..##..#.#..####.#.####..##..#.#######.#
.###.####.##..#.##..#...#...#.##.#..##....#.##.....##.#...#.###.##.##....#
..#.####..#.#.##.###..######..#####.###.#...#...###..#.#########.##..#.#.#
...###.#.###..#.#...##.#....##...#.#....#..#..#.##.#.###...#..#..#..#..#.#
.##.....###..#####.#.#######..###.###..##..##..#.#.#.#.###..#..#.#.####..#
...##...#..#...##..###..#..#..####...##.#.......#.##..##.#....###.##.##.#.
.....##.#...#..#.##...##.##.##.##..#...#.#.##..#.....##.###..###.####..###
..###..##..#..#.##.##..#....###.#..#..............#..#.#......#.##.##..###
##....##..##..####.#.##...#.......#..#.##.#..####.###.......##..#.##.##..#
##############.#.######..#.#....###..##.#....#.#.#.#.......#.##..#..#....#
#.####...#.####.#.#...##.#.#...##...##......###..###...##.#.##..#.#.#..###
#......#.#..#.#.....##..#.....##########.##.#....#.##.#.##...#.#.......#..
###.###....####.##.#####.####.#....#.......#....##...###..###.#...##..#.#.
..##.##...###.#..#....##.##..#....#...#.#.####..##...#.##.#####.###..###.#
.#.#..#####..#.#....#..#.#...#......#....#.#..#.####.#.#...#...#....####..
##.#.##.###.##.#....###..#.#..#.###.##.#....#.#.##..#..##.###..#..##.###..
.####..##.#.#.#####.#######.##....#.##..#####..#####.#....#.##.###........
#####....#....#######.#.##.#.##.....####.###..##.#.#....#.#.######.#.#....
...#.#...#.#..#....#.......#.##.#.#.##...#..#.#####..##.#..#..#.#....##.#.
..#.####.##.#.....###.###.#...#####...#..###.#..#...##.##..#.#..#.##..#.##
.#######..#.#.###.#.....#...#######..#...#....##..#.#...#.....##.##.#####.
#.##..#####.....##.###.###...###.##.#..#..#....#.#.####..##..#.###..##...#
#.##.#..##.#.....#...##..##..##.#.#...####..#...####.#...##....##..##.####
#.###.#..##.############...#...#####....###..#..#.##.##..#.##...#.##......
..#..####.#...#.####.#.#.#.#..#..#.##.#.##..##...###...#..##..#.###..#....
##.####.####...#......####.###...#...###....#.###.#..#.#....####..#....###
..#.#..#..#.....#.....#.###.#.##..#.####...##.#..#.##..#######.#...###.#.#
..#.#.###.##.##..###..##.##.#.....#..####.#######....##...#.....#..#....##
...#..###.##.####.#..##.#####.###.#####.#..#.#..#..##.##.######.#.#.#.#.#.
#.###...#####..#.....#..#.#...##...#####.#..##.######..#..###.#....##..##.
#...#...#......#....#.#....##...###.#....#.....###.#.#...##...##.#.#..##..
#...#..###.#.#.#..#..###.#.##.##..#..##..##.##..#...#.#..####.##.##.###.##
#..##.#...#..#.##..#..##...###.##.##.##.#.####.#.......##...##.#..####..##
##.###.#.#..######.##.##.##..##....#.##.#....####.....##.##.#..##.#.#####.
###.#.##...###.#####..#.##.##.#..#.#...#.....####.....#..###....#.....#.#.
#.#####.#..#.#.#...#....#.#.##...##.##.##..#...#.####.#.#..#....#.#.#.#...
#..###.####.###.##..##..........##..##.#...#.##.#....##.##.#....#########.
.##.#.#..####...#.........#...##.#...##.###.#.###.####.##....##.#.#....#..
.....####..#..##..##..#..##..###.#.#.#.#..#.#.#..##.#..#.###..#..#.#..#.#.
#...#.#.#......#..#.#...##.##.##....#..#.#.#.#.#.###..#.#...#.#.#.##..#...
..##.#.##..#...#.#####.#####.....#.....#.##...#..##.###.#.#############.##
.##..#.#.#####..#.#.#.#...####.#######.###...#########..####...#..##.....#
#....#..#...###..#.#.####..#....#.#.####.....###..###..####...##......#.#.
..........#...#.#....#..###..#..#.##..##.#..#...#.#.#....#.##...####.#....
##..#.#.#.###..#####.#..#..#....#.#.#..#....##.#.####.#.##..##.#######..##
###..###..#..##..#.#..#..####......##.#######.#..####.#.#.##.......#.#...#
..#.#.###.#.##...#.#...###..#######..##.###.###..##.#.....##.#..#.#.##....
#.###...#####.##...###..#..#.#..#.##...##..##..#.#....####.#.##.##.###.#.#
#.#..###.#..#..#.#.##...#.######......###.##.#.##......######.##..#######.
###.####.#...#..#####..##....##.#.#....#.##..#.##..##..##.....###.##.#..##";
}