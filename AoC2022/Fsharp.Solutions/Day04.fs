namespace Fsharp.Solutions

module Day04 =
    let contains a b =
        fst(a) >= fst(b) && snd(a) <= snd(b)
        
    let isAnyCovered a b =
        contains a b || contains b a
        
    let firstPuzzle (input: ((int * int) * (int * int)) list) =
        List.map (fun r -> isAnyCovered (fst(r)) (snd(r))) input
        |> List.filter id |> List.length
        
    let overlaps a b =
        fst(a) >= fst(b) && fst(a) <= snd(b) || snd(a) >= fst(b) && snd(a) <= snd(b)
        
    let isAnyOverlap a b =
        overlaps a b || overlaps b a
        
    let secondPuzzle (input: ((int * int) * (int * int)) list) =
        List.map (fun r -> isAnyOverlap (fst(r)) (snd(r))) input
        |> List.filter id |> List.length