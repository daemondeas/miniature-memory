using Csharp.Solutions;

namespace Inputs;

public class Input16 : AbstractInput<List<Valve>, List<Valve>>
{
    protected override List<Valve> ParseInput(string input)
    {
        var valves = input.Split('\n').Select(ParseValve).ToList();
        foreach (var valve in valves)
        {
            foreach (var destination in valve.Item2)
            {
                valve.Item1.LeadsTo.Add(valves.First(v => v.Item1.Name == destination).Item1);
            }
        }

        return valves.Select(v => v.Item1).ToList();
    }

    private static (Valve, List<string>) ParseValve(string row)
    {
        var parts = row.Split(' ');
        List<string> destinations;
        if (row.IndexOf("valves ", StringComparison.InvariantCulture) == -1)
        {
            destinations = new List<string> { row.Split("valve ")[1] };
        }
        else
        {
            destinations = row.Split("valves ")[1].Split(", ").ToList();
        }
        
        return (new Valve
        {
            Name = parts[1],
            Pressure = int.Parse(parts[4].Split('=')[1][..^1])
        }, destinations);
    }

    protected override List<Valve> ParseInputTwo(string input) => ParseInput(input);

    protected override long SolveFirstPuzzle(List<Valve> input) => Day16.FirstPuzzle(input);

    protected override long SolveSecondPuzzle(List<Valve> input) => Day16.SecondPuzzle(input);

    public override string TestInput =>
        "Valve AA has flow rate=0; tunnels lead to valves DD, II, BB\nValve BB has flow rate=13; tunnels lead to valves CC, AA\nValve CC has flow rate=2; tunnels lead to valves DD, BB\nValve DD has flow rate=20; tunnels lead to valves CC, AA, EE\nValve EE has flow rate=3; tunnels lead to valves FF, DD\nValve FF has flow rate=0; tunnels lead to valves EE, GG\nValve GG has flow rate=0; tunnels lead to valves FF, HH\nValve HH has flow rate=22; tunnel leads to valve GG\nValve II has flow rate=0; tunnels lead to valves AA, JJ\nValve JJ has flow rate=21; tunnel leads to valve II";
    public override string RealInput => @"Valve SY has flow rate=0; tunnels lead to valves GW, LW
Valve TS has flow rate=0; tunnels lead to valves CC, OP
Valve LU has flow rate=0; tunnels lead to valves PS, XJ
Valve ND has flow rate=0; tunnels lead to valves EN, TL
Valve PD has flow rate=0; tunnels lead to valves TL, LI
Valve VF has flow rate=0; tunnels lead to valves LW, RX
Valve LD has flow rate=0; tunnels lead to valves AD, LP
Valve DG has flow rate=0; tunnels lead to valves DR, SS
Valve IG has flow rate=8; tunnels lead to valves AN, YA, GA
Valve LK has flow rate=0; tunnels lead to valves HQ, LW
Valve TD has flow rate=14; tunnels lead to valves BG, CQ
Valve CQ has flow rate=0; tunnels lead to valves TD, HD
Valve AZ has flow rate=0; tunnels lead to valves AD, XW
Valve ZU has flow rate=0; tunnels lead to valves TL, AN
Valve HD has flow rate=0; tunnels lead to valves BP, CQ
Valve FX has flow rate=0; tunnels lead to valves LW, XM
Valve CU has flow rate=18; tunnels lead to valves BX, VA, RX, DF
Valve SS has flow rate=17; tunnels lead to valves DG, ZD, ZG
Valve BP has flow rate=19; tunnels lead to valves HD, ZD
Valve DZ has flow rate=0; tunnels lead to valves XS, CC
Valve PS has flow rate=0; tunnels lead to valves GH, LU
Valve TA has flow rate=0; tunnels lead to valves LI, AA
Valve BG has flow rate=0; tunnels lead to valves TD, ZG
Valve WP has flow rate=0; tunnels lead to valves OB, AA
Valve XS has flow rate=9; tunnels lead to valves EN, DZ
Valve AA has flow rate=0; tunnels lead to valves WG, GA, VO, WP, TA
Valve LW has flow rate=25; tunnels lead to valves LK, FX, SY, VF
Valve AD has flow rate=23; tunnels lead to valves DF, GW, AZ, LD, FM
Valve EN has flow rate=0; tunnels lead to valves ND, XS
Valve ZG has flow rate=0; tunnels lead to valves SS, BG
Valve LI has flow rate=11; tunnels lead to valves YA, XM, TA, PD
Valve VO has flow rate=0; tunnels lead to valves AA, OD
Valve AN has flow rate=0; tunnels lead to valves IG, ZU
Valve GH has flow rate=15; tunnels lead to valves VA, PS
Valve OP has flow rate=4; tunnels lead to valves AJ, TS, FM, BX, NM
Valve BX has flow rate=0; tunnels lead to valves OP, CU
Valve RX has flow rate=0; tunnels lead to valves CU, VF
Valve FM has flow rate=0; tunnels lead to valves OP, AD
Valve OB has flow rate=0; tunnels lead to valves WP, XW
Valve CC has flow rate=3; tunnels lead to valves QS, LP, DZ, OD, TS
Valve LP has flow rate=0; tunnels lead to valves LD, CC
Valve NM has flow rate=0; tunnels lead to valves WH, OP
Valve HQ has flow rate=0; tunnels lead to valves XW, LK
Valve GW has flow rate=0; tunnels lead to valves SY, AD
Valve QS has flow rate=0; tunnels lead to valves CC, XW
Valve DF has flow rate=0; tunnels lead to valves AD, CU
Valve XM has flow rate=0; tunnels lead to valves LI, FX
Valve VA has flow rate=0; tunnels lead to valves CU, GH
Valve GA has flow rate=0; tunnels lead to valves IG, AA
Valve YA has flow rate=0; tunnels lead to valves LI, IG
Valve XW has flow rate=20; tunnels lead to valves OB, HQ, QS, WH, AZ
Valve XJ has flow rate=24; tunnel leads to valve LU
Valve AJ has flow rate=0; tunnels lead to valves WG, OP
Valve WH has flow rate=0; tunnels lead to valves XW, NM
Valve TL has flow rate=13; tunnels lead to valves PD, DR, ZU, ND
Valve OD has flow rate=0; tunnels lead to valves CC, VO
Valve ZD has flow rate=0; tunnels lead to valves SS, BP
Valve DR has flow rate=0; tunnels lead to valves DG, TL
Valve WG has flow rate=0; tunnels lead to valves AJ, AA";
}